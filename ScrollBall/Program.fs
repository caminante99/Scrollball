module Main
open ApplicationContext
#if DEBUG
open HIDManip.Mouse.Hook
#endif

[<EntryPoint; System.STAThread>]
let main argv =                                        
    printfn "started"
#if DEBUG
    let mouseEvent = new Event<string> ()
    use eventedHook = new LowLevelMouseHook (fun nCode wParam lParam ->
        mouseEvent.Trigger (sprintf "%A %A (x:%A y:%A d:%A f:%A t:%A)" nCode wParam lParam.pt.x lParam.pt.y lParam.mouseData lParam.flags lParam.time) |> ignore
        true
    )
    mouseEvent.Publish.Subscribe (fun (str: string) -> System.Console.WriteLine str) |> ignore
 #endif


    use context = new ScrollBallContext ()
    System.Windows.Forms.Application.Run (context)

    0 // return an integer exit code
