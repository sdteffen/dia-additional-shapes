SHEET = Electrical

shapedir = $(datadir)/dia/shapes

Electricaldir = $(shapedir)/Electrical

# ls -1 *.png *.shape | sed 's/^/\t/;s/$/ \\/'

SHAPES = \
	ac1_h.png \
	ac1_h.shape \
	ac1_v.png \
	ac1_v.shape \
	ac3_h.png \
	ac3_h.shape \
	ac3_v.png \
	ac3_v.shape \
	ac_h.png \
	ac_h.shape \
	ac_v.png \
	ac_v.shape \
	bel_l.png \
	bel_l.shape \
	bel_r.png \
	bel_r.shape \
	bel_v.png \
	bel_v.shape \
	buzzer_l.png \
	buzzer_l.shape \
	buzzer_r.png \
	buzzer_r.shape \
	buzzer_v.png \
	buzzer_v.shape \
	carrier_1_h.png \
	carrier_1_h.shape \
	carrier_1_v.png \
	carrier_1_v.shape \
	carrier_1wire_h.png \
	carrier_1wire_h.shape \
	carrier_1wire_v.png \
	carrier_1wire_v.shape \
	carrier_2_h.png \
	carrier_2_h.shape \
	carrier_2_v.png \
	carrier_2_v.shape \
	carrier_2wire_h.png \
	carrier_2wire_h.shape \
	carrier_2wire_v.png \
	carrier_2wire_v.shape \
	carrier_3_h.png \
	carrier_3_h.shape \
	carrier_3_v.png \
	carrier_3_v.shape \
	carrier_3wire_h.png \
	carrier_3wire_h.shape \
	carrier_3wire_v.png \
	carrier_3wire_v.shape \
	carrier_inair.png \
	carrier_inair.shape \
	carrier_inground_h.png \
	carrier_inground_h.shape \
	carrier_inground_v.png \
	carrier_inground_v.shape \
	carrier_intube_h.png \
	carrier_intube_h.shape \
	carrier_intube_v.png \
	carrier_intube_v.shape \
	carrier_inwall_h.png \
	carrier_inwall_h.shape \
	carrier_inwall_v.png \
	carrier_inwall_v.shape \
	carrier_n_h.png \
	carrier_n_h.shape \
	carrier_n_v.png \
	carrier_n_v.shape \
	carrier_nwire_h.png \
	carrier_nwire_h.shape \
	carrier_nwire_v.png \
	carrier_nwire_v.shape \
	carrier_onwall_h.png \
	carrier_onwall_h.shape \
	carrier_onwall_v.png \
	carrier_onwall_v.shape \
	dc_h.png \
	dc_h.shape \
	dc_v.png \
	dc_v.shape \
	devicefurnace.png \
	devicefurnace.shape \
	device.png \
	device.shape \
	dispatch_box.png \
	dispatch_box.shape \
	fuseauto_h.png \
	fuseauto_h.shape \
	fuseauto_v.png \
	fuseauto_v.shape \
	fusemelt_h.png \
	fusemelt_h.shape \
	fusemelt_v.png \
	fusemelt_v.shape \
	grounding_d.png \
	grounding_d.shape \
	groundingstrip_v.png \
	groundingstrip_v.shape \
	light.png \
	light.shape \
	s1p.png \
	s1p.shape \
	s2p.png \
	s2p.shape \
	s3p.png \
	s3p.shape \
	s4p.png \
	s4p.shape \
	sd1p.png \
	sd1p.shape \
	sd2p.png \
	sd2p.shape \
	sd3p.png \
	sd3p.shape \
	sd4p.png \
	sd4p.shape \
	s_light.png \
	s_light.shape \
	socketchild_l.png \
	socketchild_l.shape \
	socketchild_r.png \
	socketchild_r.shape \
	socketchild_v.png \
	socketchild_v.shape \
	socketgroundchild_l.png \
	socketgroundchild_l.shape \
	socketgroundchild_r.png \
	socketgroundchild_r.shape \
	socketgroundchild_v.png \
	socketgroundchild_v.shape \
	socketground_l.png \
	socketground_l.shape \
	socketground_r.png \
	socketground_r.shape \
	socketground_v.png \
	socketground_v.shape \
	socket_l.png \
	socket_l.shape \
	socket_r.png \
	socket_r.shape \
	socket_v.png \
	socket_v.shape \
	sx1p.png \
	sx1p.shape \
	sx2p.png \
	sx2p.shape \
	sx4p.png \
	sx4p.shape \
	sy1p.png \
	sy1p.shape \
	sy2p.png \
	sy2p.shape \
	sy3p.png \
	sy3p.shape \
	sy4p.png \
	sy4p.shape
 
EXTRA_DIST = $(SHAPES)

Electrical_DATA = $(SHAPES)

Electrical-zip: $(SHAPES) ../../sheets/Electrical.sheet
	-(cd ../.. && \
	mkdir -p website/Electrical/images && \
	zip website/Electrical/Electrical.zip COPYING README.Electrical.md \
	sheets/Electrical.sheet \
	shapes/Electrical/*.png shapes/Electrical/*.shape && \
	cp shapes/Electrical/*.png website/Electrical/images )

all: Electrical-zip

clean-local:
	-rm ../../website/$(SHEET)/$(SHEET).zip
