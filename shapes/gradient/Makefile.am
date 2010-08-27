
shapedir = $(datadir)/dia/shapes

gradientdir = $(shapedir)/gradient

SHAPES = \
	white_gray_horizontal.png \
	white_gray_horizontal.shape \
	white_gray_horizontal.svg

EXTRA_DIST = $(SHAPES)

gradient_DATA = $(SHAPES)

gradient-zip: $(SHAPES) ../../sheets/gradient.sheet
	-(cd ../.. && \
	mkdir -p website/gradient/images && \
	zip website/gradient/gradient.zip COPYING sheets/gradient.sheet \
	shapes/gradient/*.png shapes/gradient/*.shape && \
	cp shapes/gradient/*.png website/gradient/images )

all: gradient-zip
