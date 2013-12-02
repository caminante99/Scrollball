Scrollball
=========

> Scrolling for the trackball (and trackpoint)

What
----
Scrollball mimics the mousewheel when moving the mouse while holding down the right mouse
button.

Why
---
I recently found an old Logitech Marble Mouse. The two button kind. I wanted to use it as my
primary pointer but there was no way I was going to give up scrolling through large documents.

Supposedly there was an option in the drivers to scroll with the scrollball when holding
both mouse buttons. That looked like what I wanted. However I could not get them to work with the newer version of windows.

So I decided to create the functionality myself.

How
---
Scrollball installs a low level mouse hook that watches your mouse movement and buttons. When
the right mouse button is pressed, it cancels the input and waits to see if you move the mouse
or release the button. If you move the mouse the movement is canceled and mousewheel events
are sent instead. If you release the button without moving a full right mouse button press is
sent (down and up).