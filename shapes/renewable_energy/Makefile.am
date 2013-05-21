SHEET = renewable_energy

shapedir = $(datadir)/dia/shapes

renewable_energydir = $(shapedir)/renewable_energy

SHAPES = \
	cup_anemometer.png \
	cup_anemometer.shape \
	data_logger.png \
	data_logger.shape \
	meas_tower.png \
	meas_tower.shape \
	solar_module.png \
	solar_module.shape \
	temp_humi_sensor.png \
	temp_humi_sensor.shape \
	wind_turbine.png \
	wind_turbine.shape \
	wind_vane.png \
	wind_vane.shape

EXTRA_DIST = $(SHAPES)

renewable_energy_DATA = $(SHAPES)

renewable_energy-zip: $(SHAPES) ../../sheets/renewable_energy.sheet
	-(cd ../.. && \
	mkdir -p website/renewable_energy/images && \
	zip website/renewable_energy/renewable_energy.zip COPYING sheets/renewable_energy.sheet \
	shapes/renewable_energy/*.png shapes/renewable_energy/*.shape && \
	cp shapes/renewable_energy/*.png website/renewable_energy/images )

all: renewable_energy-zip

clean-local:
	-rm ../../website/$(SHEET)/$(SHEET).zip
