SHEET = edpc
shapedir = $(datadir)/dia/shapes

edpcdir = $(shapedir)/edpc

SHAPES = \
	and.png \
	and.shape \
	function.png \
	function.shape \
	or.png \
	or.shape \
	organisation_unit.png \
	organisation_unit.shape \
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

clean-local:
	-rm ../../website/$(SHEET)/$(SHEET).zip
