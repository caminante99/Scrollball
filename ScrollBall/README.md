AcmeMouse
=========

> Acme inspired mouse chording

What
----
AcmeMouse provides global mouse bindings for cut, copy, and paste operations.
Specifically:

    L->M = Cut
    L->M->R = Copy
    L->R = Paste
    L->R->M = Cancel paste

If you wanted to copy some text highlight it, and hold down the left mouse button.
Without releasing the left mouse button, press the middle button, *immediately*
followed by the right mouse button.

Why
---
After briefly perusing http://acme.cat-v.org/mouse, I immediately wanted mouse
shortcuts across my whole Windows system. So far it has proven surprisingly pleasant.

How
---
AcmeMouse installs a low level mouse hook that watches your mouse buttons. When
you press the mouse buttons in the right sequence, it sends a key combo that
corresponds to the action you have triggered. Copy will send ctrl+c, cut ctrl+x.