SHEET = scenegraph

shapedir = $(datadir)/dia/shapes

scenegraphdir = $(shapedir)/scenegraph

SHAPES = \
	appearance.png \
	appearance.shape \
	callback.png \
	callback.shape \
	camera.png \
	camera.shape \
	component.png \
	component.shape \
	engine.png \
	engine.shape \
	field.png \
	field.shape \
	group.png \
	group.shape \
	light.png \
	light.shape \
	manipulator.png \
	manipulator.shape \
	metric.png \
	metric.shape \
	nodekit.png \
	nodekit.shape \
	property.png \
	property.shape \
	realtimeglobalfield.png \
	realtimeglobalfield.shape \
	separator.png \
	separator.shape \
	shape.png \
	shape.shape \
	soselection.png \
	soselection.shape \
	subgraph.png \
	subgraph.shape \
	switch.png \
	switch.shape \
	transform.png \
	transform.shape
	
EXTRA_DIST = $(SHAPES)

scenegraph_DATA = $(SHAPES)

scenegraph-zip: $(SHAPES) ../../sheets/scenegraph.sheet
	-(cd ../.. && \
	mkdir -p website/scenegraph/images && \
	zip website/scenegraph/scenegraph.zip COPYING sheets/scenegraph.sheet \
	shapes/scenegraph/*.png shapes/scenegraph/*.shape && \
	cp shapes/scenegraph/*.png website/scenegraph/images )

all: scenegraph-zip

clean-local:
	-rm ../../website/$(SHEET)/$(SHEET).zip
