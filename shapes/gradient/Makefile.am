SHEET = gradient

shapedir = $(datadir)/dia/shapes

gradientdir = $(shapedir)/gradient

SHAPES = \
	dark_light_horizontal_gradient_box.png \
	dark_light_horizontal_gradient_box.shape \
	dark_light_horizontal_gradient_box.svg \
	dark_light_horizontal_gradient.png \
	dark_light_horizontal_gradient.shape \
	dark_light_horizontal_gradient.svg \
	dark_light_vertical_gradient_box.png \
	dark_light_vertical_gradient_box.shape \
	dark_light_vertical_gradient_box.svg \
	dark_light_vertical_gradient.png \
	dark_light_vertical_gradient.shape \
	dark_light_vertical_gradient.svg \
	light_dark_horizontal_gradient_box.png \
	light_dark_horizontal_gradient_box.shape \
	light_dark_horizontal_gradient_box.svg \
	light_dark_horizontal_gradient.png \
	light_dark_horizontal_gradient.shape \
	light_dark_horizontal_gradient.svg \
	light_dark_vertical_gradient_box.png \
	light_dark_vertical_gradient_box.shape \
	light_dark_vertical_gradient_box.svg \
	light_dark_vertical_gradient.png \
	light_dark_vertical_gradient.shape \
	light_dark_vertical_gradient.svg

EXTRA_DIST = $(SHAPES)

gradient_DATA = $(SHAPES)

gradient-zip: $(SHAPES) ../../sheets/gradient.sheet
	-(cd ../.. && \
	mkdir -p website/gradient/images && \
	zip website/gradient/gradient.zip COPYING sheets/gradient.sheet \
	shapes/gradient/*.png shapes/gradient/*.shape \
	shapes/gradient/*.svg && \
	cp shapes/gradient/*.png website/gradient/images )

all: gradient-zip

clean-local:
	-rm ../../website/$(SHEET)/$(SHEET).zip
