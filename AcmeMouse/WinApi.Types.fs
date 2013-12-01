// turn off FS0009 "Uses of this construct may result in the generation of unverifiable .NET IL code"
// this is the result of the StructLayout attribute. I have no choice, what with dipping into
// unmanaged code anyway.
#nowarn "9"

namespace WinApi.Types
open System.Runtime.InteropServices


type WORD = int
type DWORD = uint32
type HWND = nativeint
type HANDLE = nativeint
type LONG = int
type UINT = int
type ULONG_PTR = nativeint
type HINSTANCE = nativeint
type HHOOK = nativeint


type WM =
    | LBUTTONDOWN = 0x0201u
    | LBUTTONUP = 0x0202u
    | RBUTTONDOWN = 0x0204u
    | RBUTTONUP = 0x0205u
    | MBUTTONDOWN = 0x207u
    | MBUTTONUP = 0x208u
    | MOUSEWHEEL = 0x20Au
    | MOUSEHWHEEL = 0x20Eu
    | MOUSEMOVE = 0x0200u

type KEYEVENTF =
    | NONE = 0x0000
    | EXTENDEDKEY = 0x0001
    | KEYUP = 0x0002
    | SCANCODE = 0x0008
    | UNICODE = 0x0004

type VK =
(*
 * Virtual Keys, Standard Set
 *)
| LBUTTON = 0x01us
| RBUTTON = 0x02us
| CANCEL = 0x03us
| MBUTTON = 0x04us    (* NOT contiguous with L & RBUTTON *)

| XBUTTON1 = 0x05us    (* NOT contiguous with L & RBUTTON *)
| XBUTTON2 = 0x06us    (* NOT contiguous with L & RBUTTON *)

(*
 * 0x07 : unassigned
 *)

| BACK = 0x08us
| TAB = 0x09us

(*
 * 0x0A - 0x0B : reserved
 *)

| CLEAR = 0x0Cus
| RETURN = 0x0Dus

| SHIFT = 0x10us
| CONTROL = 0x11us
| MENU = 0x12us
| PAUSE = 0x13us
| CAPITAL = 0x14us

| KANA = 0x15us
| HANGEUL = 0x15us  (* old name - should be here for compatibility *)
| HANGUL = 0x15us
| JUNJA = 0x17us
| FINAL = 0x18us
| HANJA = 0x19us
| KANJI = 0x19us

| ESCAPE = 0x1Bus

| CONVERT = 0x1Cus
| NONCONVERT = 0x1Dus
| ACCEPT = 0x1Eus
| MODECHANGE = 0x1Fus

| SPACE = 0x20us
| PRIOR = 0x21us
| NEXT = 0x22us
| END = 0x23us
| HOME = 0x24us
| LEFT = 0x25us
| UP = 0x26us
| RIGHT = 0x27us
| DOWN = 0x28us
| SELECT = 0x29us
| PRINT = 0x2Aus
| EXECUTE = 0x2Bus
| SNAPSHOT = 0x2Cus
| INSERT = 0x2Dus
| DELETE = 0x2Eus
| HELP = 0x2Fus

(*
 * 0 - VK_9 are the same as ASCII '0' - '9' (0x30 - 0x39)
 * 0x40 : unassigned
 * A - VK_Z are the same as ASCII 'A' - 'Z' (0x41 - 0x5A)
 *)
| ZERO = 0x30us
| ONE = 0x31us
| TWO = 0x32us
| THREE = 0x33us
| FOUR = 0x34us
| FIVE = 0x35us
| SIX = 0x36us
| SEVEN = 0x37us
| EIGHT = 0x38us
| NINE = 0x39us

| A = 0x41us
| B = 0x42us
| C = 0x43us
| D = 0x44us
| E = 0x45us
| F = 0x46us
| G = 0x47us
| H = 0x48us
| I = 0x49us
| J = 0x4Aus
| K = 0x4Bus
| L = 0x4Cus
| M = 0x4Dus
| N = 0x4Eus
| O = 0x4Fus
| P = 0x50us
| Q = 0x51us
| R = 0x52us
| S = 0x53us
| T = 0x54us
| U = 0x55us
| V = 0x56us
| W = 0x57us
| X = 0x58us
| Y = 0x59us
| Z = 0x5Aus

| LWIN = 0x5Bus
| RWIN = 0x5Cus
| APPS = 0x5Dus

(*
 * 0x5E : reserved
 *)

| SLEEP = 0x5Fus

| NUMPAD0 = 0x60us
| NUMPAD1 = 0x61us
| NUMPAD2 = 0x62us
| NUMPAD3 = 0x63us
| NUMPAD4 = 0x64us
| NUMPAD5 = 0x65us
| NUMPAD6 = 0x66us
| NUMPAD7 = 0x67us
| NUMPAD8 = 0x68us
| NUMPAD9 = 0x69us
| MULTIPLY = 0x6Aus
| ADD = 0x6Bus
| SEPARATOR = 0x6Cus
| SUBTRACT = 0x6Dus
| DECIMAL = 0x6Eus
| DIVIDE = 0x6Fus
| F1 = 0x70us
| F2 = 0x71us
| F3 = 0x72us
| F4 = 0x73us
| F5 = 0x74us
| F6 = 0x75us
| F7 = 0x76us
| F8 = 0x77us
| F9 = 0x78us
| F10 = 0x79us
| F11 = 0x7Aus
| F12 = 0x7Bus
| F13 = 0x7Cus
| F14 = 0x7Dus
| F15 = 0x7Eus
| F16 = 0x7Fus
| F17 = 0x80us
| F18 = 0x81us
| F19 = 0x82us
| F20 = 0x83us
| F21 = 0x84us
| F22 = 0x85us
| F23 = 0x86us
| F24 = 0x87us

(*
 * 0x88 - 0x8F : unassigned
 *)

| NUMLOCK = 0x90us
| SCROLL = 0x91us

(*
 * NEC PC-9800 kbd definitions
 *)
| OEM_NEC_EQUAL = 0x92us   // '=' key on numpad

(*
 * Fujitsu/OASYS kbd definitions
 *)
| OEM_FJ_JISHO = 0x92us   // 'Dictionary' key
| OEM_FJ_MASSHOU = 0x93us   // 'Unregister word' key
| OEM_FJ_TOUROKU = 0x94us   // 'Register word' key
| OEM_FJ_LOYA = 0x95us   // 'Left OYAYUBI' key
| OEM_FJ_ROYA = 0x96us   // 'Right OYAYUBI' key

(*
 * 0x97 - 0x9F : unassigned
 *)

(*
 * L* & VK_R* - left and right Alt, Ctrl and Shift virtual keys.
 * Used only as parameters to GetAsyncKeyState() and GetKeyState().
 * No other API or message will distinguish left and right keys in this way.
 *)
| LSHIFT = 0xA0us
| RSHIFT = 0xA1us
| LCONTROL = 0xA2us
| RCONTROL = 0xA3us
| LMENU = 0xA4us
| RMENU = 0xA5us

| BROWSER_BACK = 0xA6us
| BROWSER_FORWARD = 0xA7us
| BROWSER_REFRESH = 0xA8us
| BROWSER_STOP = 0xA9us
| BROWSER_SEARCH = 0xAAus
| BROWSER_FAVORITES = 0xABus
| BROWSER_HOME = 0xACus

| VOLUME_MUTE = 0xADus
| VOLUME_DOWN = 0xAEus
| VOLUME_UP = 0xAFus
| MEDIA_NEXT_TRACK = 0xB0us
| MEDIA_PREV_TRACK = 0xB1us
| MEDIA_STOP = 0xB2us
| MEDIA_PLAY_PAUSE = 0xB3us
| LAUNCH_MAIL = 0xB4us
| LAUNCH_MEDIA_SELECT = 0xB5us
| LAUNCH_APP1 = 0xB6us
| LAUNCH_APP2 = 0xB7us


(*
 * 0xB8 - 0xB9 : reserved
 *)

| OEM_1 = 0xBAus   // ';:' for US
| OEM_PLUS = 0xBBus   // '+' any country
| OEM_COMMA = 0xBCus   // ',' any country
| OEM_MINUS = 0xBDus   // '-' any country
| OEM_PERIOD = 0xBEus   // '.' any country
| OEM_2 = 0xBFus   // '/?' for US
| OEM_3 = 0xC0us   // '`~' for US

(*
 * 0xC1 - 0xD7 : reserved
 *)

(*
 * 0xD8 - 0xDA : unassigned
 *)

| OEM_4 = 0xDBus  //  '[{' for US
| OEM_5 = 0xDCus  //  '\|' for US
| OEM_6 = 0xDDus  //  ']}' for US
| OEM_7 = 0xDEus  //  ''"' for US
| OEM_8 = 0xDFus

(*
 * 0xE0 : reserved
 *)

(*
 * Various extended or enhanced keyboards
 *)
| OEM_AX = 0xE1us  //  'AX' key on Japanese AX kbd
| OEM_102 = 0xE2us  //  "<>" or "\|" on RT 102-key kbd.
| ICO_HELP = 0xE3us  //  Help key on ICO
| ICO_00 = 0xE4us  //  00 key on ICO

| PROCESSKEY = 0xE5us

| ICO_CLEAR = 0xE6us


| PACKET = 0xE7us

(*
 * 0xE8 : unassigned
 *)

(*
 * Nokia/Ericsson definitions
 *)
| OEM_RESET = 0xE9us
| OEM_JUMP = 0xEAus
| OEM_PA1 = 0xEBus
| OEM_PA2 = 0xECus
| OEM_PA3 = 0xEDus
| OEM_WSCTRL = 0xEEus
| OEM_CUSEL = 0xEFus
| OEM_ATTN = 0xF0us
| OEM_FINISH = 0xF1us
| OEM_COPY = 0xF2us
| OEM_AUTO = 0xF3us
| OEM_ENLW = 0xF4us
| OEM_BACKTAB = 0xF5us

| ATTN = 0xF6us
| CRSEL = 0xF7us
| EXSEL = 0xF8us
| EREOF = 0xF9us
| PLAY = 0xFAus
| ZOOM = 0xFBus
| NONAME = 0xFCus
| PA1 = 0xFDus
| OEM_CLEAR = 0xFEus

(*
 * 0xFF : reserved
 *)


 type MOUSEEVENT =
    | ABSOLUTE = 0x8000
    | MOVE = 0x0001
    | LEFTDOWN = 0x0002
    | LEFTUP = 0x0002
    | MIDDLEDOWN = 0x0020
    | MIDDLEUP = 0x0040
    | RIGHTDOWN = 0x0008
    | RIGHTUP = 0x0010
    | XDOWN = 0x0080
    | XUP = 0x0100
    | WHEEL = 0x0800
    | HWHEEL = 0x01000



[<Struct; StructLayout(LayoutKind.Sequential)>]
type POINT =
    val x : int32
    val y : int32


    
[<Struct; StructLayout(LayoutKind.Sequential)>]
type RECT =
    val mutable left: LONG
    val mutable top: LONG
    val mutable right: LONG
    val mutable bottom: LONG

[<Struct; StructLayout(LayoutKind.Sequential)>]
type MSLLHOOKSTRUCT =
    val pt          : POINT
    val mouseData   : int32
    val flags       : int32
    val time        : int32
    val dwExtraInfo : nativeint


type WPARAM = WM
type LPARAM = MSLLHOOKSTRUCT
type LRESULT = HHOOK

[<Struct; StructLayout(LayoutKind.Sequential)>]
type MSG =
    val hwnd          : nativeint
    val message   : uint32
    val wParam    : int32
    val lParam   : uint32
    val time        : uint32
    val pt : POINT

[<Struct; StructLayout(LayoutKind.Sequential)>]
type GUITHREADINFO =
    val mutable cbSize: DWORD
    val mutable flags: DWORD
    val mutable hwndActive: HWND
    val mutable hwndFocus: HWND
    val mutable hwndCapture: HWND
    val mutable hwndMenuOwner: HWND
    val mutable hwndMoveSize: HWND
    val mutable hwndCaret: HWND
    val mutable rcCaret: RECT


[<Struct; StructLayout(LayoutKind.Sequential)>]
type MOUSEINPUT =
    val dx: LONG
    val dy: LONG
    val mouseData: DWORD
    val dwflags: DWORD
    val time: DWORD
    val dwExtraInfo: ULONG_PTR

[<Struct; StructLayout(LayoutKind.Sequential)>]
type KEYBDINPUT =
    val wVk: VK
    val wScan: WORD
    val dwflags: KEYEVENTF
    val time: DWORD
    val dwExtraInfo: ULONG_PTR
    new(_wVk, _wScan, _dwflags, _time, _dwExtraInfo) =
        { wVk = _wVk
          wScan = _wScan
          dwflags = _dwflags
          time = _time
          dwExtraInfo = _dwExtraInfo }


[<Struct; StructLayout(LayoutKind.Sequential)>]
type HARDWAREINPUT =
    val uMSG: DWORD
    val wParamL: WORD
    val wParamH: WORD

[<Struct; StructLayout(LayoutKind.Explicit)>]
type INPUTUNION =
    [<FieldOffset(0)>]
    val mutable mi: MOUSEINPUT
    [<FieldOffset(0)>]
    val mutable ki: KEYBDINPUT
    [<FieldOffset(0)>]
    val mutable hi: HARDWAREINPUT

[<Struct; StructLayout(LayoutKind.Sequential)>]
type INPUT = 
    val ``type``: int
    val input: INPUTUNION
    new(_type, _input) = {
        ``type`` = _type
        input = _input
    }
