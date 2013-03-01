
shapedir = $(datadir)/dia/shapes

Electricaldir = $(shapedir)/Electrical

SHAPES = \
	device.png \
	device.shape \
	devicefurnace.png \
	devicefurnace.shape \
	fuseauto.png \
	fuseauto.shape \
	fusemelt.png \
	fusemelt.shape \
	light.png \
	light.shape \
	socket.png \
	socket.shape \
	socketgrond.shape \
	socketground.png \
	sx1p.png \
	sx1p.shape \
	sx2p.png \
	sx2p.shape \
	sx4p.png \
	sx4p.shape
 
EXTRA_DIST = $(SHAPES)

Electrical_DATA = $(SHAPES)

Electrical-zip: $(SHAPES) ../../sheets/Electrical.sheet
	-(cd ../.. && \
	mkdir -p website/Electrical/images && \
	zip website/Electrical/Electrical.zip COPYING sheets/Electrical.sheet \
	shapes/Electrical/*.png shapes/Electrical/*.shape && \
	cp shapes/Electrical/*.png website/Electrical/images )

all: Electrical-zip
