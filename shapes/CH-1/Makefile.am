SHEET = CH-1
shapedir = $(datadir)/dia/shapes

ch1dir = $(shapedir)/CH-1

SHAPES = \
	condition.png \
	condition.shape \
	delay.png \
	delay.shape \
	department.png \
	department.shape \
	device.png \
	device.shape \
	employee.png \
	employee.shape \
	event.png \
	event.shape \
	junction.png \
	junction.shape \
	note.png \
	note.shape \
	outpart.png \
	outpart.shape \
	storage.png \
	storage.shape \
	work.png \
	work.shape

EXTRA_DIST = $(SHAPES)

ch1_DATA = $(SHAPES)

ch1-zip: $(SHAPES) ../../sheets/CH-1.sheet
	-(cd ../.. && \
	mkdir -p website/CH-1/images && \
	zip website/CH-1/CH-1.zip COPYING sheets/CH-1.sheet \
	shapes/CH-1/*.png samples/CH-1.dia shapes/CH-1/*.shape && \
	cp shapes/CH-1/*.png website/CH-1/images )

all: ch1-zip

clean-local:
	-rm ../../website/$(SHEET)/$(SHEET).zip
