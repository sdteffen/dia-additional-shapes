SHEET = AUTOSAR

shapedir = $(datadir)/dia/shapes

AUTOSARdir = $(shapedir)/$(SHEET)

# ls -1 *.png *.shape | sed 's/^/\t/;s/$/ \\/'

SHAPES = \
	AS_Cal_H.png \
	AS_Cal_H.shape \
	AS_Cal_V.png \
	AS_Cal_V.shape \
	AS_Component.png \
	AS_Component.shape \
	AS_Composition.png \
	AS_Composition.shape \
	AS_CS_provides.png \
	AS_CS_provides.shape \
	AS_CS_requires_H.png \
	AS_CS_requires_H.shape \
	AS_CS_requires_V.png \
	AS_CS_requires_V.shape \
	AS_IRV.png \
	AS_IRV.shape \
	AS_Runnable.png \
	AS_Runnable.shape \
	AS_SA_SWC.png \
	AS_SA_SWC.shape \
	AS_Service_CS_provides.png \
	AS_Service_CS_provides.shape \
	AS_Service_CS_requires_H.png \
	AS_Service_CS_requires_H.shape \
	AS_Service_CS_requires_V.png \
	AS_Service_CS_requires_V.shape \
	AS_Service_SR_H.png \
	AS_Service_SR_H.shape \
	AS_Service_SR_V.png \
	AS_Service_SR_V.shape \
	AS_SR_H.png \
	AS_SR_H.shape \
	AS_SR_V.png \
	AS_SR_V.shape \
	AS_SWC.png \
	AS_SWC.shape
 
EXTRA_DIST = $(SHAPES)

AUTOSAR_DATA = $(SHAPES)

$(SHEET)-zip: $(SHAPES) ../../sheets/$(SHEET).sheet
	-(cd ../.. && \
	mkdir -p website/$(SHEET)/images && \
	zip website/$(SHEET)/$(SHEET).zip COPYING README.$(SHEET).md \
	sheets/$(SHEET).sheet \
	shapes/$(SHEET)/*.png shapes/$(SHEET)/*.shape && \
	cp shapes/$(SHEET)/*.png website/$(SHEET)/images )

all: $(SHEET)-zip

test:
	mkdir -p ~/.dia/shapes/$(SHEET)
	cp * ~/.dia/shapes/$(SHEET)
	cp ../../sheets/$(SHEET).sheet ~/.dia/sheets

clean-test:
	-rm ~/.dia/sheets/$(SHEET).sheet
	-rm -Rf ~/.dia/shapes/$(SHEET)

clean-local:
	-rm ../../website/$(SHEET)/$(SHEET).zip
