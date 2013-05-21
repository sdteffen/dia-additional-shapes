SHEET = value_stream_mapping

shapedir = $(datadir)/dia/shapes

value_stream_mappingdir = $(shapedir)/value_stream_mapping

SHAPES = \
	air_transport.png \
	air_transport.shape \
	electronic_information_right.png \
	electronic_information_right.shape \
	glasses.png \
	glasses.shape \
	happy_face.png \
	happy_face.shape \
	Inventory.png \
	Inventory.shape \
	kanban_min_max.png \
	kanban_min_max.shape \
	kanban_post.png \
	kanban_post.shape \
	kanban_production.png \
	kanban_production.shape \
	kanban_withdrawal.png \
	kanban_withdrawal.shape \
	material_pull.png \
	material_pull.shape \
	Push.png \
	Push.shape \
	rail_transport.png \
	rail_transport.shape \
	storage.png \
	storage.shape \
	supplier.png \
	supplier.shape \
	working_cell_unit.png \
	working_cell_unit.shape

DIAGRAMS = \
	Push.dia \
	rail_transport.dia \
	storage.dia \
	working_cell_unit.dia   

EXTRA_DIST = $(SHAPES) $(DIAGRAMS)

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

clean-local:
	-rm ../../website/$(SHEET)/$(SHEET).zip
