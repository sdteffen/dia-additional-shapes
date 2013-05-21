SHEET = Racks

shapedir = $(datadir)/dia/shapes

Racksdir = $(shapedir)/Racks

SHAPES = \
	equipment_10u.png \
	equipment_10u.shape \
	equipment_11u.png \
	equipment_11u.shape \
	equipment_12u.png \
	equipment_12u.shape \
	equipment_1u.png \
	equipment_1u.shape \
	equipment_2u.png \
	equipment_2u.shape \
	equipment_3u.png \
	equipment_3u.shape \
	equipment_4u.png \
	equipment_4u.shape \
	equipment_5u.png \
	equipment_5u.shape \
	equipment_6u.png \
	equipment_6u.shape \
	equipment_7u.png \
	equipment_7u.shape \
	equipment_8u.png \
	equipment_8u.shape \
	equipment_9u.png \
	equipment_9u.shape \
	power_strip_bs_7.shape \
	power_strip_bs.png \
	power_strip_schuko_8.shape \
	power_strip_schuko.png \
	power_strip_ute_8.shape \
	power_strip_ute.png \
	rack_12u.png \
	rack_12u.shape \
	rack_16u.png \
	rack_16u.shape \
	rack_20u.png \
	rack_20u.shape \
	rack_38u.png \
	rack_38u.shape \
	rack_42u.png \
	rack_42u.shape \
	rack_label_anchors_42u.png \
	rack_label_anchors_42u.shape

EXTRA_DIST = $(SHAPES)

Racks_DATA = $(SHAPES)

Racks-zip: $(SHAPES) ../../sheets/Racks.sheet
	-(cd ../.. && \
	mkdir -p website/Racks/images && \
	zip website/Racks/Racks.zip COPYING sheets/Racks.sheet \
	shapes/Racks/*.png shapes/Racks/*.shape && \
	cp shapes/Racks/*.png website/Racks/images )

all: Racks-zip

clean-local:
	-rm ../../website/$(SHEET)/$(SHEET).zip
