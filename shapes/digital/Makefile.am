SHEET = digital

shapedir = $(datadir)/dia/shapes

digitaldir = $(shapedir)/digital

SHAPES = \
	add_h.png \
	add_h.shape \
	add_v.png \
	add_v.shape \
	and_h.png \
	and_h.shape \
	and_v.png \
	and_v.shape \
	buff_h.png \
	buff_h.shape \
	buff_v.png \
	buff_v.shape \
	conn.png \
	conn.shape \
	inv_h.png \
	inv_h.shape \
	inv_v.png \
	inv_v.shape \
	mux_h.png \
	mux_h.shape \
	mux_v.png \
	mux_v.shape \
	nand_h.png \
	nand_h.shape \
	nand_v.png \
	nand_v.shape \
	nor_h.png \
	nor_h.shape \
	nor_v.png \
	nor_v.shape \
	or_h.png \
	or_h.shape \
	or_v.png \
	or_v.shape \
	reg_h.png \
	reg_h.shape \
	reg_v.png \
	reg_v.shape \
	xnor_h.png \
	xnor_h.shape \
	xnor_v.png \
	xnor_v.shape \
	xor_h.png \
	xor_h.shape \
	xor_v.png \
	xor_v.shape

EXTRA_DIST = $(SHAPES)

digital_DATA = $(SHAPES)

digital-zip: $(SHAPES) ../../sheets/digital.sheet
	-(cd ../.. && \
	mkdir -p website/digital/images && \
	zip website/digital/digital.zip COPYING sheets/digital.sheet \
	shapes/digital/*.png shapes/digital/*.shape && \
	cp shapes/digital/*.png website/digital/images )

all: digital-zip

clean-local:
	-rm ../../website/$(SHEET)/$(SHEET).zip
