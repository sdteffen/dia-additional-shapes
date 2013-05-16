Dia symbols for electricity in BE (and EU ?)
============================================
Please be noted: These can't be used yet in Dia:
Place them on a sheet export to SVG, and use them in another program.
Currently it's impossible to rotate text or shapes, as a result, you cannot create a correct electrical drawing.

Why?
----
I needed it and didn't want to pay for Visio (or anything else).

Content
-------
I could explain, or just show and image of the symbols so far:

![Symbols so far](https://raw.github.com/qantourisc/Dia---Electrical/master/screenshot.png)

Installation
------------
###Linux
Place sheets/\* in ~/.dia/sheets.
Place shapes/electrical\_be in ~/.dia/shapes/.
###Windows
Place sheets/\* in %HOMEPATH%/.dia/sheets.
Place shapes/electrical\_be in %HOMEPATH%/.dia/shapes/.

FAQ
---
####Why is there a big gap around the symbols?
This ensures you can place them on a grid and don't constantly have to fight with Dia to keep the center of the symbol on the grid.
This also ensures if you resize a symbol, you can resize them consistently.

####In which countries can I use the symbols?
I would like to know myself, the are ok for BE.

####A symbol is missing!
Drop me the code/patch. Or provide me an example and pray I have time to add it :)

####A symbol is wrong / you misnamed a symbol / ...
Blame me (and make sure I receive your blame), then I can fix it.
