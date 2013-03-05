
shapedir = $(datadir)/dia/shapes

value_stream_mappingdir = $(shapedir)/value_stream_mapping

SHAPES = \
	air_transport.png \
	air_transport.shape \
	happy_face.png \
	happy_face.shape 

EXTRA_DIST = $(SHAPES)

value_stream_mapping_DATA = $(SHAPES)

value_stream_mapping-zip: $(SHAPES) ../../sheets/value_stream_mapping.sheet
	-(cd ../.. && \
	mkdir -p website/value_stream_mapping/images && \
	zip website/value_stream_mapping/value_stream_mapping.zip COPYING \
	sheets/value_stream_mapping.sheet \
	shapes/value_stream_mapping/*.png shapes/value_stream_mapping/*.shape \
	&& \
	cp shapes/value_stream_mapping/*.png \
	website/value_stream_mapping/images )

all: value_stream_mapping-zip

test:
	mkdir -p ~/.dia/shapes/value_stream_mapping
	cp * ~/.dia/shapes/value_stream_mapping 
