module TrackballScroll

open MouseHandler
open WinApi.Types
open WinApi.Methods
open Actions

type TrackballScroll () =
    let mutable scrolling = false
    let mutable scrolled = false
    let mutable x = 0
    let mutable y = 0
    let mutable ignoreInput = false
    let hook = new LowLevelMouseHook(fun nCode wParam lParam ->        
        let dx = x - lParam.pt.x
        let dy = y - lParam.pt.y
        let scrollX = (log (float (abs dx))) // * (float ( dx / dx ))
        let scrollY = (log (float (abs dy)))  //* (float ( dy / dy ))
#if DEBUG
        printfn "dx %A, dy %A | sx %A, sy %A" dx dy scrollX scrollY
#endif
        if ignoreInput then
            x <- lParam.pt.x
            y <- lParam.pt.y
            true
        else if wParam = WM.RBUTTONDOWN then
            scrolling <- true
            false
        else if wParam = WM.RBUTTONUP && scrolled then
            scrolling <- false
            scrolled <- false
            false
        else if wParam = WM.RBUTTONUP then
            scrolling <- false
            ignoreInput <-true
            SendMouse MOUSEEVENT.RIGHTDOWN 0u 0u 0u |> ignore
            SendMouse MOUSEEVENT.RIGHTUP 0u 0u 0u |> ignore
            ignoreInput <-false
            false
        else if scrolling && wParam = WM.MOUSEMOVE then
            scrolled <- true

            if scrollX <> -infinity && scrollX > 0.50 then
                let steps = (uint32 (scrollX * 60.00)) * if dx < 0 then 1u else 0u - 1u
#if DEBUG                
                printfn "scrolling x by %A steps" steps
#endif
                SendMouse MOUSEEVENT.HWHEEL 0u 0u  steps
                |> ignore

            if scrollY <> -infinity && scrollY > 0.50 then
                let steps = (uint32 (scrollY * 60.00)) * if dy > 0 then 1u else 0u - 1u
#if DEBUG                
                printfn "scrolling y by %A steps" steps
#endif
                SendMouse MOUSEEVENT.WHEEL 0u 0u  steps
                |> ignore

            false
        else
            x <- lParam.pt.x
            y <- lParam.pt.y
            true
        )
    interface System.IDisposable with
        member this.Dispose () =
            (hook :> System.IDisposable).Dispose ()