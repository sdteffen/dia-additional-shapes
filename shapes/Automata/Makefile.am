SHEET = Automata

shapedir = $(datadir)/dia/shapes

Automatadir = $(shapedir)/Automata

SHAPES = \
	finalstate.shape \
	fstate.png \
	istate.png \
	sfstate.png \
	sstate.png \
	startfinalstate.shape \
	startstate.shape \
	state.shape

EXTRA_DIST = $(SHAPES)

Automata_DATA = $(SHAPES)

Automata-zip: $(SHAPES) ../../sheets/Automata.sheet
	-(cd ../.. && \
	mkdir -p website/Automata/images && \
	zip website/Automata/Automata.zip COPYING sheets/Automata.sheet \
	shapes/Automata/*.png shapes/Automata/*.shape && \
	cp shapes/Automata/*.png website/Automata/images )

all: Automata-zip

clean-local:
	-rm ../../website/$(SHEET)/$(SHEET).zip
