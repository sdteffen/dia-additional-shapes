
shapedir = $(datadir)/dia/shapes

electric2dir = $(shapedir)/edpc

SHAPES = \
	and.png \
	and.shape \
	function.png \
	function.shape \
	or.png \
	or.shape \
	organization_unit.png \
	organization_unit.shape \
	xor.png \
	xor.shape
	
EXTRA_DIST = $(SHAPES)

edpc_DATA = $(SHAPES)

edpc-zip: $(SHAPES) ../../sheets/edpc.sheet
	-(cd ../.. && \
	mkdir -p website/edpc/images && \
	zip website/edpc/edpc.zip COPYING sheets/edpc.sheet \
	shapes/edpc/*.png shapes/edpc/*.shape && \
	cp shapes/edpc/*.png website/edpc/images )

all: edpc-zip
