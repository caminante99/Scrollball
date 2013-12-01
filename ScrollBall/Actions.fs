module Actions

open WinApi.Methods
open WinApi.Types

open System.Runtime.InteropServices

let SendKeys (keys: List<VK>) pos =
    if keys.Length = 0 then
        ()
    else        
        // I have been having issues using SendInput to send keyup, so I have switched to the slightly worse keybd_event for now
    
        // Send the keys
        for key in keys do        
            keybd_event (key, MapVirtualKey(key, 0u), KEYEVENTF.NONE, 0n)
            
        // and their corresponding key up (don't want to get into ctrl lock...)
        for key in keys do            
            keybd_event (key, MapVirtualKey(key, 0u), KEYEVENTF.KEYUP, 0n)
        
        ()

let SendMouse flags dx dy data =
    mouse_event (flags, dx, dy, data, 0n)