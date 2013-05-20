SHEET = central_data_processing

shapedir = $(datadir)/dia/shapes

central_data_processingdir = $(shapedir)/central_data_processing

SHAPES = \
	CPD_rack_15.png \
	CPD_rack_15.shape \
	CPD_rack_32.png \
	CPD_rack_32.shape \
	CPD_rack_36.png \
	CPD_rack_36.shape \
	CPD_rack_42.png \
	CPD_rack_42.shape \
	CPD_rack_44.png \
	CPD_rack_44.shape \
	CPD_rack_5.png \
	CPD_rack_5.shape \
	CPD_rack_7.png \
	CPD_rack_7.shape \
	Litter.png \
	Litter.shape \
	Modem.png \
	Modem.shape \
	Organizador_Rack_1U.png \
	Organizador_Rack_1U.shape \
	Organizador_Rack_2U.png \
	Organizador_Rack_2U.shape \
	Organizer_Rack_1U.png \
	Organizer_Rack_1U.shape \
	Organizer_Rack_2U.png \
	Organizer_Rack_2U.shape \
	Path_Painel_24P.png \
	Path_Painel_24P.shape \
	Router.png \
	Router.shape \
	Server_Rack_1U.png \
	Server_Rack_1U.shape \
	Server_Rack_2U.png \
	Server_Rack_2U.shape \
	Server_Rack_2U_Slow.png \
	Server_Rack_2U_Slow.shape \
	Server_Rack_2U_Very_Slow.png \
	Server_Rack_2U_Very_Slow.shape \
	Server_Rack_3U.png \
	Server_Rack_3U.shape \
	Server_Rack_3U_Slow_Density.png \
	Server_Rack_3U_Slow_Density.shape \
	Server_Rack_4U.png \
	Server_Rack_4U_Right_Capacity.png \
	Server_Rack_4U_Right_Capacity.shape \
	Server_Rack_4U.shape \
	Server_Rack_4U_Slow_Capacity.png \
	Server_Rack_4U_Slow_Capacity.shape \
	Server_Rack_4U_Very_Right_Capacity.png \
	Server_Rack_4U_Very_Right_Capacity.shape \
	Server_Rack_5U.png \
	Server_Rack_5U_Right_Density.png \
	Server_Rack_5U_Right_Density.shape \
	Server_Rack_5U.shape \
	Storage_Rack_3U.png \
	Storage_Rack_3U_SAN.png \
	Storage_Rack_3U_SAN.shape \
	Storage_Rack_3U.shape \
	Switch_Rack_16P.png \
	Switch_Rack_16P.shape \
	Switch_Rack_24P.png \
	Switch_Rack_24P.shape \
	Switch_Rack_48P.png \
	Switch_Rack_48P.shape \
	Switch_Rack_50P.png \
	Switch_Rack_50P.shape \
	Telephone_exchange.png \
	Telephone_exchange.shape \
	Voice_Painel_20P.png \
	Voice_Painel_20P.shape \
	Voice_Painel_50P.png \
	Voice_Painel_50P.shape

EXTRA_DIST = $(SHAPES)

central_data_processing_DATA = $(SHAPES)

central_data_processing-zip: $(SHAPES) ../../sheets/central_data_processing.sheet
	-(cd ../.. && \
	mkdir -p website/central_data_processing/images && \
	zip website/central_data_processing/central_data_processing.zip COPYING \
	sheets/central_data_processing.sheet \
	shapes/central_data_processing/*.png shapes/central_data_processing/*.shape && \
	cp shapes/central_data_processing/*.png website/central_data_processing/images )

all: central_data_processing-zip

clean-local:
	-rm ../../website/$(SHEET)/$(SHEET).zip

