SHEET = electric2

shapedir = $(datadir)/dia/shapes

electric2dir = $(shapedir)/electric2

SHAPES = \
	ckt-breaker.png \
	ckt-breaker.shape \
	generator.png \
	generator.shape \
	isolator.png \
	isolator.shape \
	transformer.png \
	transformer.shape
	
EXTRA_DIST = $(SHAPES)

electric2_DATA = $(SHAPES)

electric2-zip: $(SHAPES) ../../sheets/electric2.sheet
	-(cd ../.. && \
	mkdir -p website/electric2/images && \
	zip website/electric2/electric2.zip COPYING sheets/electric2.sheet \
	shapes/electric2/*.png shapes/electric2/*.shape && \
	cp shapes/electric2/*.png website/electric2/images )

all: electric2-zip

clean-local:
	-rm ../../website/$(SHEET)/$(SHEET).zip
