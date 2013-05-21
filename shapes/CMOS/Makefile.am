SHEET = CMOS

shapedir = $(datadir)/dia/shapes

CMOSdir = $(shapedir)/CMOS

SHAPES = \
	gnd_h.png \
	gnd_h.shape \
	gnd_v.png \
	gnd_v.shape \
	nmos_h.png \
	nmos_h.shape \
	nmos_v.png \
	nmos_v.shape \
	pmos_h.png \
	pmos_h.shape \
	pmos_v.png \
	pmos_v.shape \
	vdd_h.png \
	vdd_h.shape \
	vdd_v.png \
	vdd_v.shape


EXTRA_DIST = $(SHAPES)

CMOS_DATA = $(SHAPES)

CMOS-zip: $(SHAPES) ../../sheets/CMOS.sheet
	-(cd ../.. && \
	mkdir -p website/CMOS/images && \
	zip website/CMOS/CMOS.zip COPYING sheets/CMOS.sheet \
	shapes/CMOS/*.png shapes/CMOS/*.shape && \
	cp shapes/CMOS/*.png website/CMOS/images )

all: CMOS-zip

clean-local:
	-rm ../../website/$(SHEET)/$(SHEET).zip
