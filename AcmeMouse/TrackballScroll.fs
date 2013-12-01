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
            if abs dx > abs dy then
                if dx > 0 then
                    SendMouse MOUSEEVENT.HWHEEL 0u 0u (0u - 60u) |> ignore
                    false
                else
                    SendMouse MOUSEEVENT.HWHEEL 0u 0u 60u |> ignore
                    false
            else
                if dy > 0 then
                    SendMouse MOUSEEVENT.WHEEL 0u 0u 60u |> ignore
                    false
                else
                    SendMouse MOUSEEVENT.WHEEL 0u 0u (0u - 60u) |> ignore
                    false
        else
            x <- lParam.pt.x
            y <- lParam.pt.y
            true
        )
    interface System.IDisposable with
        member this.Dispose () =
            (hook :> System.IDisposable).Dispose ()