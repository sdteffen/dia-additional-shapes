dia-shapes
==========

These are additional shapes for Dia.

More information is available from [dia-installer.de/shapes](http://dia-installer.de/shapes)

The build system allows to package the shapes for use with [Diashapes](http://dia-installer.de/diashapes).

This package was previously named dia-additional-shapes.

Like Dia, this package is licensed under the terms of the [GNU General Public License, version 2](COPYING).

Sheets
------

Detailed information for some of the included sheets:

* [Automata](README.Automata.md)
* [AUTOSAR](README.AUTOSAR.md)
* [Electrical](README.Electrical.md)
* [Racks](README.Racks.md)


Shape processing
----------------

To turn all white pixels in a PNG icon transparent, use ImageMagick:

	convert -alpha on -channel RGBA -fuzz 8% -fill none -opaque "#ffffff" \
		+repage foo.png foo.png 

src/floodfillicon.sh can be used to To floodfill all white areas from the outside.

