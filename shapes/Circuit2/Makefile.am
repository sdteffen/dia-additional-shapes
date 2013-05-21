SHEET = Circuit2

shapedir = $(datadir)/dia/shapes

Circuit2dir = $(shapedir)/Circuit2

SHAPES = \
	dcapacitor.png \
	dcapacitor.shape \
	ddiode.png \
	ddiode.shape \
	dinductor_de.png \
	dinductor_de.shape \
	dinductor.png \
	dinductor.shape \
	dled_de.png \
	dled_de.shape \
	dpcapacitor.png \
	dpcapacitor.shape \
	dresistor_de.png \
	dresistor_de.shape \
	dresistor.png \
	dresistor.shape \
	dzener.png \
	dzener.shape \
	hammeter.png \
	hammeter.shape \
	hpcapacitor.png \
	hpcapacitor.shape \
	hvoltmeter.png \
	hvoltmeter.shape \
	Makefile.am \
	newhled.png \
	newhled.shape \
	newvled.png \
	newvled.shape \
	opto.png \
	opto.shape \
	ramp.png \
	ramp.shape \
	relay.png \
	relay.shape \
	sine.png \
	sine.shape \
	square.png \
	square.shape \
	triac.png \
	triac.shape \
	triangle.png \
	triangle.shape \
	vammeter.png \
	vammeter.shape \
	vpcapacitor.png \
	vpcapacitor.shape \
	vvoltmeter.png \
	vvoltmeter.shape
	
EXTRA_DIST = $(SHAPES)

Circuit2_DATA = $(SHAPES)

Circuit2-zip: $(SHAPES) ../../sheets/Circuit2.sheet
	-(cd ../.. && \
	mkdir -p website/Circuit2/images && \
	zip website/Circuit2/Circuit2.zip COPYING sheets/Circuit2.sheet \
	shapes/Circuit2/*.png shapes/Circuit2/*.shape && \
	cp shapes/Circuit2/*.png website/Circuit2/images )

all: Circuit2-zip

clean-local:
	-rm ../../website/$(SHEET)/$(SHEET).zip
