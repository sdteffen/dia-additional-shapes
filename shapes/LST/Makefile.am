
shapedir = $(datadir)/dia/shapes

LSTdir = $(shapedir)/LST

SHAPES = \
	associator_subsystem.png \
	associator_subsystem.shape \
	boundary.png \
	cn_subsystem.png \
	cn_subsystem.shape \
	converter.png \
	convert_subsystem.png \
	convert_subsystem.shape \
	decider_subsystem.png \
	decider_subsystem.shape \
	decoder.png \
	decoder.shape \
	decode_subsystem.png \
	decode_subsystem.shape \
	distributor_subsystem.png \
	distributor_subsystem.shape \
	encoder.png \
	encoder.shape \
	encode_subsystem.png \
	encode_subsystem.shape \
	extruder.png \
	extruder.shape \
	ingestor.png \
	ingestor_subsystem.png \
	ingestor_subsystem.shape \
	internal_subsystem.png \
	internal_subsystem.shape \
	it_subsystem.png \
	it_subsystem.shape \
	memory_subsystem.png \
	memory_subsystem.shape \
	mestorage.png \
	mestorage.shape \
	motor.png \
	motor.shape \
	ot_subsystem.png \
	ot_subsystem.shape \
	producer.png \
	producer.shape \
	reproducer_subsystem.png \
	reproducer_subsystem.shape \
	supporter_subsystem.png \
	supporter_subsystem.shape \
	timer.png \
	timer.shape

EXTRA_DIST = $(SHAPES)

LST_DATA = $(SHAPES)

LST-zip: $(SHAPES) ../../sheets/LST.sheet
	-(cd ../.. && \
	mkdir -p website/LST/images && \
	zip website/LST/LST.zip COPYING sheets/LST2.sheet \
	shapes/LST/*.png shapes/LST/*.shape && \
	cp shapes/LST/*.png website/LST/images )

all: LST-zip
