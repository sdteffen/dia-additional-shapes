SHEET = chemistry_lab

shapedir = $(datadir)/dia/shapes

chemistry_labdir = $(shapedir)/chemistry_lab

SHAPES = \
	30-degree-bend.png \
	30-degree-bend.shape \
	beaker.png \
	beaker.shape \
	buechner-funnel.png \
	buechner-funnel.shape \
	burette.png \
	burette.shape \
	clamp-extra-large.png \
	clamp-extra-large.shape \
	clamp-front.png \
	clamp-front.shape \
	clamp-large.png \
	clamp-large.shape \
	clamp.png \
	clamp.shape \
	column-reflux.png \
	column-reflux-red-blue.png \
	column-reflux-red-blue.shape \
	column-reflux-red.png \
	column-reflux-red.shape \
	column-reflux.shape \
	condenser-black.png \
	condenser-black.shape \
	condenser-blue.png \
	condenser-blue.shape \
	condenser-green.png \
	condenser-green.shape \
	condenser-liebig-vertical.png \
	condenser-liebig-vertical.shape \
	condenser-orange.png \
	condenser-orange.shape \
	condenser-pale-blue.png \
	condenser-pale-blue.shape \
	condenser.png \
	condenser-red.png \
	condenser-red.shape \
	condenser.shape \
	erlenmeyer.png \
	erlenmeyer.shape \
	funnel.png \
	funnel.shape \
	gooseneck.png \
	gooseneck.shape \
	lab-stand-front.png \
	lab-stand-front.shape \
	lab-stand.png \
	lab-stand.shape \
	ring-front.png \
	ring-front.shape \
	ring.png \
	ring.shape \
	sep-funnel.png \
	sep-funnel.shape \
	sep-funnel-three-phases.png \
	sep-funnel-three-phases.shape \
	sep-funnel-two-phases.png \
	sep-funnel-two-phases.shape \
	stirbar.png \
	stirbar.shape \
	stirplate-front.png \
	stirplate-front.shape \
	stirplate.png \
	stirplate.shape

EXTRA_DIST = $(SHAPES)

chemistry_lab_DATA = $(SHAPES)

chemistry_lab-zip: $(SHAPES) ../../sheets/chemistry_lab.sheet
	-(cd ../.. && \
	mkdir -p website/chemistry_lab/images && \
	zip website/chemistry_lab/chemistry_lab.zip COPYING sheets/chemistry_lab.sheet \
	shapes/chemistry_lab/*.png shapes/chemistry_lab/*.shape && \
	cp shapes/chemistry_lab/*.png website/chemistry_lab/images )

all: chemistry_lab-zip

clean-local:
	-rm ../../website/$(SHEET)/$(SHEET).zip
