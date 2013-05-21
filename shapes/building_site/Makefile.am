SHEET = building_site

shapedir = $(datadir)/dia/shapes

building_sitedir = $(shapedir)/building_site

SHAPES = \
	circle_diagonal_quarters_filled.png \
	circle_diagonal_quarters_filled.shape \
	concrete_mixer.png \
	concrete_mixer.shape \
	fence_horizontal.png \
	fence_horizontal.shape \
	fence_vertical.png \
	fence_vertical.shape \
	pre_silo.png \
	pre_silo.shape \
	proportioning_batcher.png \
	proportioning_batcher.shape \
	road_crossroads.png \
	road_crossroads.shape \
	road_horizontal.png \
	road_horizontal.shape \
	road_rotated_tee.png \
	road_rotated_tee.shape \
	road_tee.png \
	road_tee.shape \
	road_turn.png \
	road_turn.shape \
	road_vertical.png \
	road_vertical.shape \
	slope_horizontal.png \
	slope_horizontal.shape \
	slope_vertical.png \
	slope_vertical.shape \
	wall_horizontal.png \
	wall_horizontal.shape \
	wall_vertical.png \
	wall_vertical.shape

EXTRA_DIST = $(SHAPES)

building_site_DATA = $(SHAPES)

building_site-zip: $(SHAPES) ../../sheets/building_site.sheet
	-(cd ../.. && \
	mkdir -p website/building_site/images && \
	zip website/building_site/building_site.zip COPYING sheets/building_site.sheet \
	shapes/building_site/*.png shapes/building_site/*.shape && \
	cp shapes/building_site/*.png website/building_site/images )

all: building_site-zip

clean-local:
	-rm ../../website/$(SHEET)/$(SHEET).zip
