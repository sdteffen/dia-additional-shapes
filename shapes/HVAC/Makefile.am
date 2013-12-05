SHEET = HVAC

shapedir = $(datadir)/dia/shapes

HVACdir = $(shapedir)/$(SHEET)

# ls -1 *.png *.shape | sed 's/^/\t/;s/$/ \\/'

SHAPES = \
	1WayDiffuser.png \
	1WayDiffuser.shape \
	24x24Return.png \
	24x24Return.shape \
	2WayDiffuser.png \
	2WayDiffuser.shape \
	3WayDiffuser.png \
	3WayDiffuser.shape \
	4WayDiffuser.png \
	4WayDiffuser.shape \
	CeilingVent.png \
	CeilingVent.shape \
	CeilingVentVert.png \
	CeilingVentVert.shape \
	Duct.png \
	Duct.shape \
	DuctVert.png \
	DuctVert.shape \
	FloorVent.png \
	FloorVent.shape \
	FloorVentVert.png \
	FloorVentVert.shape \
	HumiditySensor.png \
	HumiditySensor.shape \
	InsulatedDuct.png \
	InsulatedDuct.shape \
	InsulatedDuctVert.png \
	InsulatedDuctVert.shape \
	Thermostat.png \
	Thermostat.shape \
	VolumeDamper.png \
	VolumeDamper.shape \
	VolumeDamperVert.png \
	VolumeDamperVert.shape \
	WallVent.png \
	WallVent.shape \
	WallVentVert.png \
	WallVentVert.shape
	
EXTRA_DIST = $(SHAPES)

HVAC_DATA = $(SHAPES)

HVAC-zip: $(SHAPES) ../../sheets/HVAC.sheet
	-(cd ../.. && \
	mkdir -p website/HVAC/images && \
	zip website/HVAC/HVAC.zip COPYING sheets/HVAC.sheet \
	shapes/HVAC/*.png shapes/HVAC/*.shape && \
	cp shapes/HVAC/*.png website/HVAC/images )

all: HVAC-zip

clean-local:
	-rm ../../website/$(SHEET)/$(SHEET).zip
