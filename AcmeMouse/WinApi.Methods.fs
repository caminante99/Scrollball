module WinApi.Methods
open WinApi.Types

open System.Runtime.InteropServices

let RaiseWin32Err a =    
    let win32Err = System.Runtime.InteropServices.Marshal.GetLastWin32Error ()    
    if win32Err <> 0 then
        let err = System.ComponentModel.Win32Exception win32Err
        System.Console.WriteLine (sprintf "!%A: %A" win32Err err.Message)
        a
        //raise (System.ComponentModel.Win32Exception win32Err)
    else
        a

type LowLevelMouseProc =
    delegate of int * WM * byref<MSLLHOOKSTRUCT> -> LRESULT

[<DllImport("kernel32.dll", SetLastError = true)>]
extern uint32 GetCurrentThreadId()

[<DllImport("kernel32.dll", SetLastError = true)>]
extern nativeint GetModuleHandle(string lpModuleName)

[<DllImport("user32.dll", SetLastError = true)>]
extern bool UnhookWindowsHookEx(nativeint hhk)

// Sadly I can't marshal discriminated unions, otherwise I could have a HOOKPROC that could cover all the hook types
// As it is, I'm making distinct methods to marshal each type separately
[<DllImport("user32.dll", SetLastError = true, EntryPoint="SetWindowsHookEx")>]
extern nativeint SetWindowsHookEx_LowLevelMouseProc(int idhook, LowLevelMouseProc proc,  HINSTANCE hMod, DWORD threadId)

// helper method to set a low level mouse hook
let SetLowLevelMouseHook proc hMod threadId =
    SetWindowsHookEx_LowLevelMouseProc(14, proc, hMod, threadId)

[<DllImport("user32.dll", SetLastError = true)>]
extern bool GetMessage(MSG lpMsg, nativeint hWnd, uint32 wMsgFilterMin, uint32 wMsgFilterMax);

[<DllImport("user32.dll", SetLastError = true)>]
extern UINT SendInput(UINT nInputs, [<MarshalAs(UnmanagedType.LPArray); In>] INPUT[] pInputs, int cbSize);

[<DllImport("user32.dll", SetLastError = true)>]
extern bool GetGUIThreadInfo(DWORD idThread, [<In; Out>]GUITHREADINFO& lpgui)

[<DllImport("user32.dll", SetLastError = true)>]
extern ULONG_PTR GetMessageExtraInfo()

[<DllImport("kernel32.dll", SetLastError = true)>]
extern DWORD GetLastError()

[<DllImport("user32.dll", SetLastError = true)>]
extern uint32 MapVirtualKey(VK uCode, uint32 uMapType)

[<DllImport("user32.dll", SetLastError = true)>]
extern bool SetForegroundWindow(HWND hwnd)

[<DllImport("user32.dll", SetLastError = true)>]
extern LRESULT CallNextHookEx(HHOOK hhk, int nCode, WPARAM wParam, [<In>]LPARAM& lParam)

[<DllImport("user32.dll", SetLastError = true)>]
extern HWND GetFocus()

[<DllImport("user32.dll", SetLastError = true)>]
extern bool AttachThreadInput(DWORD idAttach, DWORD idAttachTo, bool fAttach)

[<DllImport("user32.dll", SetLastError = true)>]
extern DWORD GetWindowThreadProcessId(HWND hwnd, [<Out>] DWORD& lpdwProcessId)

[<DllImport("user32.dll", SetLastError = true)>]
extern bool SendMessage(HWND hwnd, UINT msg, VK wParam, int lParam)

[<DllImport("user32.dll", SetLastError = true)>]
extern bool PostMessage(HWND hwnd, UINT msg, VK wParam, int lParam)

[<DllImport("user32.dll", SetLastError = true)>]
extern HWND FindWindow(string lpClassName, string lpWindowName)

[<DllImport("user32.dll", SetLastError = true)>]
extern HWND WindowFromPoint(POINT point)

[<DllImport("user32.dll", SetLastError = true)>]
extern HWND WindowFromPhysicalPoint(POINT point)

[<DllImport("user32.dll", SetLastError = true)>]
extern HWND GetTopWindow(HWND hwnd)

[<DllImport("user32.dll", SetLastError = true)>]
extern HWND GetForegroundWindow()

[<DllImport("user32.dll", SetLastError = true)>]
extern UINT GetWindowModuleFileName(HWND hwnd, [<Out>]System.Text.StringBuilder lpszFileName, UINT cchFileNameMax)

[<DllImport("user32.dll", SetLastError = true)>]
extern int GetWindowText(HWND hWnd, [<Out>]System.Text.StringBuilder lpString, int nMaxCount)

 [<DllImport("user32.dll", SetLastError = true)>]
 extern int GetWindowTextLength(HWND hWnd)
 
 [<DllImport("user32.dll", SetLastError = true)>]
 extern bool LockSetForegroundWindow(UINT uLockCode)
 
 [<DllImport("user32.dll", SetLastError = true)>]
 extern HWND ChildWindowFromPoint(HWND hWndParent, POINT Point)
  
 [<DllImport("user32.dll", SetLastError = true)>]
 extern void keybd_event(VK bVk, uint32 bScan, KEYEVENTF dwFlags, nativeint dwExtraInfo)

 [<DllImport("user32.dll", SetLastError = true)>]
 extern void mouse_event(MOUSEEVENT dwFlags, DWORD dx, DWORD dy, DWORD dwData, nativeint dwExtraInfo)