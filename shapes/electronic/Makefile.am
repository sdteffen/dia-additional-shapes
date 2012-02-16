
shapedir = $(datadir)/dia/shapes

electronicdir = $(shapedir)/electronic

SHAPES = \
	ampermeter.png \
	ampermeter.shape \
	antenna.png \
	antenna.shape \
	bell.png \
	bell.shape \
	button.png \
	button.shape \
	capacitor_electrolytic.png \
	capacitor_electrolytic.shape \
	capacitor.png \
	capacitor.shape \
	contact_pair.png \
	contact_pair.shape \
	contact.png \
	contact.shape \
	crystal.png \
	crystal.shape \
	diac.png \
	diac.shape \
	di_gate.png \
	di_gate.shape \
	diode_inverse.png \
	diode_inverse.shape \
	diode_schottky.png \
	diode_schottky.shape \
	diode_tunnel.png \
	diode_tunnel.shape \
	diode_zenner.png \
	diode_zenner.shape \
	engine.png \
	engine.shape \
	ground.png \
	ground.shape \
	headphone.png \
	headphone.shape \
	inductor.png \
	inductor.shape \
	led_display.png \
	led_display.shape \
	microphone.png \
	microphone.shape \
	photo_emiting_part.png \
	photo_emiting_part.shape \
	photo_sensitive_part.png \
	photo_sensitive_part.shape \
	port_inout.png \
	port_inout.shape \
	port_in.png \
	port_in.shape \
	port_out.png \
	port_out.shape \
	potenciometer.png \
	potenciometer.shape \
	source_current.png \
	source_current.shape \
	source.png \
	source.shape \
	source_substitution.png \
	source_substitution.shape \
	source_voltage_alternating.png \
	source_voltage_alternating.shape \
	source_voltage_direct.png \
	source_voltage_direct.shape \
	source_voltage.png \
	source_voltage.shape \
	transistor_bipolar_npn_1.png \
	transistor_bipolar_npn_1.shape \
	transistor_bipolar_npn_2.png \
	transistor_bipolar_npn_2.shape \
	transistor_bipolar_pnp_1.png \
	transistor_bipolar_pnp_1.shape \
	transistor_bipolar_pnp_2.png \
	transistor_bipolar_pnp_2.shape \
	transistor_jfe_n.png \
	transistor_jfe_n.shape \
	transistor_jfe_p.png \
	transistor_jfe_p.shape \
	transistor_misfe_c_n.png \
	transistor_misfe_c_n.shape \
	transistor_misfe_c_p.png \
	transistor_misfe_c_p.shape \
	transistor_misfe_i_n.png \
	transistor_misfe_i_n.shape \
	transistor_misfe_i_p.png \
	transistor_misfe_i_p.shape \
	transistor_single_pn.png \
	transistor_single_pn.shape \
	triac.png \
	triac.shape \
	tyristor_diode.png \
	tyristor_diode.shape \
	tyristor_triode.png \
	tyristor_triode.shape \
	vacuum_diode.png \
	vacuum_diode.shape \
	vacuum_pentode.png \
	vacuum_pentode.shape \
	vacuum_triode.png \
	vacuum_triode.shape \
	variable_linear_part.png \
	variable_linear_part.shape \
	variable_nonlinear_part.png \
	variable_nonlinear_part.shape \
	varicap.png \
	varicap.shape \
	voltmeter.png \
	voltmeter.shape
	
EXTRA_DIST = $(SHAPES)

electronic_DATA = $(SHAPES)

electronic-zip: $(SHAPES) ../../sheets/electronic.sheet
	-(cd ../.. && \
	mkdir -p website/electronic/images && \
	zip website/electronic/electronic.zip COPYING sheets/electronic2.sheet \
	shapes/electronic/*.png shapes/electronic/*.shape && \
	cp shapes/electronic/*.png website/electronic/images )

all: electronic-zip
