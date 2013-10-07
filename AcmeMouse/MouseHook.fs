﻿module MouseHandler

open PInvoke.Methods
open PInvoke.Types


type LowLevelMouseHook(handler) as this =
    let proc = LowLevelMouseProc(fun nCode wParam lParam ->
        if nCode < 0 then
            // CallNextHook seems to raise error code 6 (handle invalid) This doesn't seem consistent though, so maybe not my code?
            CallNextHookEx(0n, nCode, wParam, &lParam) //|> RaiseWin32Err
        else
            let callNext = handler nCode wParam lParam
            if callNext then
                CallNextHookEx(0n, nCode, wParam, &lParam) // |> RaiseWin32Err
            else
                -1n // non-zero stops the event from propagating
        )
    let hookId = SetWindowsHookEx(14, proc, GetModuleHandle(null), 0u) |> RaiseWin32Err
    member this.HookId = hookId
    member this.Handler = proc // need to stop the proc from being garbage collected (the let binding is being put into the constructor)
    interface System.IDisposable with
        member this.Dispose() =            
            UnhookWindowsHookEx(this.HookId) 
            |> RaiseWin32Err
            |> ignore







type MouseObservable() =
    let mouseEvent = new Event<WM>()
    let hook =  new LowLevelMouseHook(fun nCode wParam lParam ->
            mouseEvent.Trigger(wParam) |> ignore
            true
        )
    member this.MouseEvent = mouseEvent.Publish
    member this.Hook = hook

