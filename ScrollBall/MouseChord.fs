module MouseChord

open MouseHandler
open Actions
open WinApi.Types
open WinApi.Methods
open System.Runtime.InteropServices

let ChordMap = Map.ofList [([WM.LBUTTONDOWN;WM.MBUTTONDOWN],[VK.CONTROL; VK.X]);
                           ([WM.LBUTTONDOWN;WM.MBUTTONDOWN;WM.RBUTTONDOWN],[VK.CONTROL;VK.C]);
                           ([WM.LBUTTONDOWN;WM.RBUTTONDOWN],[VK.CONTROL;VK.V]);
                           ([WM.LBUTTONDOWN;WM.RBUTTONDOWN; WM.MBUTTONDOWN],[]);
                           ]
type ChordedMouseHook () as this = 
    let mutable currentChord = List.Empty
    let mutable blockWheel = true
    let mutable cancellationSource = new System.Threading.CancellationTokenSource ()
    let hook = new LowLevelMouseHook(fun nCode wParam lParam ->        
        match wParam with
            | WM.MOUSEWHEEL | WM.MOUSEHWHEEL ->
                if this.BlockWheel && currentChord.Length > 0 then
                    false
                else
                    true

            | WM.LBUTTONDOWN | WM.MBUTTONDOWN | WM.RBUTTONDOWN ->                
                currentChord <-  (currentChord @ [wParam])
                System.Console.WriteLine currentChord
                if ChordMap.ContainsKey currentChord then
                    cancellationSource.Cancel () // cancel the previous chord (if any) to allow transition into more complex chords
                    cancellationSource <- new System.Threading.CancellationTokenSource ()
                    let chord = currentChord
                    let action = async {                                            
                        do! Async.Sleep 200
                        System.Console.WriteLine "executing"
                        SendKeys ChordMap.[chord] lParam.pt |> ignore
                        currentChord <- List.empty     
                    }

                    Async.Start(action,cancellationToken=cancellationSource.Token)
                    System.Console.WriteLine (sprintf "blocking %A" wParam)
                    false
                else if currentChord.Length > 1  then                    
                    // we are in a chord, so block the mouse event from reaching the application
                    // this was primarily to stop the scrolling behavior for the middle button,
                    // but probably is relevant for any button
                    false
                else                    
                    true

            | WM.LBUTTONUP | WM.MBUTTONUP | WM.RBUTTONUP ->
                if wParam = WM.RBUTTONUP && (currentChord.Length <> 1) then
                    // Swallow the right mouse button if we just performed a chord so the context menu
                    // doesn't show afterwards. This condition works because a normal click is a chord of one,
                    // so anything else should be in a chord
                    // We don't want to swallow left mouse button up though as the app probably won't release
                    // highlight mode. Who knows about the middle mouse button...
                    false
                else
                    // reset the chord
                    currentChord <- List.empty
                    true

            | _ -> true
    )
    member val Hook = hook
    member val BlockWheel = blockWheel with get, set
    interface System.IDisposable with
        member this.Dispose () =
            (hook :> System.IDisposable).Dispose ()