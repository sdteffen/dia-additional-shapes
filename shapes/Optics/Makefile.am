SHEET = Optics

shapedir = $(datadir)/dia/shapes

Opticsdir = $(shapedir)/Optics

SHAPES = \
	atten.png \
	atten.shape \
	beam.png \
	beam.shape \
	circulator.png \
	circulator.shape \
	coupler.png \
	coupler.shape \
	coupler_vert.png \
	coupler_vert.shape \
	detector.png \
	detector.shape \
	detector_vert.png \
	detector_vert.shape \
	dfb_laser.png \
	dfb_laser.shape \
	dfb_laser_vert.png \
	dfb_laser_vert.shape \
	edfa.png \
	edfa.shape \
	edfa_vert.png \
	edfa_vert.shape \
	fibre.png \
	fibre.shape \
	fibre_vert.png \
	fibre_vert.shape \
	isolator.png \
	isolator.shape \
	isolator_vert.png \
	isolator_vert.shape \
	lpg.png \
	lpg.shape \
	lpg_vert.png \
	lpg_vert.shape \
	mod.png \
	mod.shape \
	mod_vert.png \
	mod_vert.shape \
	phasemod.png \
	phasemod.shape \
	phasemod_vert.png \
	phasemod_vert.shape \
	polarisation_control.png \
	polarisation_control.shape \
	polarisation_control_vert.png \
	polarisation_control_vert.shape \
	scope.png \
	scope.shape \
	sine.png \
	sine.shape \
	spectrum.png \
	spectrum.shape \
	square.png \
	square.shape \
	tuneable.png \
	tuneable.shape \
	wave.png \
	wave.shape

EXTRA_DIST = $(SHAPES)

Optics_DATA = $(SHAPES)

Optics-zip: $(SHAPES) ../../sheets/Optics.sheet
	-(cd ../.. && \
	mkdir -p website/Optics/images && \
	zip website/Optics/Optics.zip COPYING sheets/Optics.sheet \
	shapes/Optics/*.png shapes/Optics/*.shape && \
	cp shapes/Optics/*.png website/Optics/images )

all: Optics-zip

clean-local:
	-rm ../../website/$(SHEET)/$(SHEET).zip
