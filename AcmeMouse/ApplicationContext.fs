module ApplicationContext

open System.Windows.Forms
open System.Resources
open System.Reflection
open TrackballScroll


let menuItem text handler config =
    let item = new MenuItem(text, new System.EventHandler (handler))
    match config with
        | Some (config) -> 
            config item
            item
        | None ->
            item

type ScrollBallContext() =
    inherit ApplicationContext()
    let resources = new ResourceManager("Resources",Assembly.GetExecutingAssembly ())
    let components = new System.ComponentModel.Container ()
    let trayIcon = new NotifyIcon (components)
    let hook = new TrackballScroll ()

    let showHelp = fun sender e -> MessageBox.Show(([ "Hold down the right mouse button and move"
                                                      "the pointer to scroll"
                                                    ] |> String.concat System.Environment.NewLine),
                                                    "ScrollBall")
                                    |> ignore
        
    let onExit args =
        trayIcon.Visible <- false
        (hook :> System.IDisposable).Dispose ()
    do
        trayIcon.Icon <- resources.GetObject "AppIcon" :?> System.Drawing.Icon
        trayIcon.Text <- "ScrollBall"
        trayIcon.ContextMenu <- new ContextMenu([|
                                                menuItem "&Help" showHelp None;
                                                menuItem "E&xit" (fun s e -> Application.Exit ()) None;
                                                |])
        trayIcon.Visible <- true
        System.Console.WriteLine "context created"
        Application.ApplicationExit.Add onExit
    member this.TrayIcon = trayIcon
    member this.Hook = hook