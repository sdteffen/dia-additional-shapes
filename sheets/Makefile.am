
# To create a .sheet.in file from a .sheet file, run
# sed 's!<description!<_description!;s!</description!</_description!;s!<name!<_name!;s!</name!</_name!;' 

sheet_in_files = \
  Automata.sheet.in \
  AUTOSAR.sheet.in \
  building_site.sheet.in \
  central_data_processing.sheet.in \
  CH-1.sheet.in \
  chemistry_lab.sheet.in \
  Circuit2.sheet.in \
  CMOS.sheet.in \
  digital.sheet.in \
  edpc.sheet.in \
  electric2.sheet.in \
  Electrical.sheet.in \
  electronic.sheet.in \
  gradient.sheet.in \
  HVAC.sheet.in \
  LST.sheet.in \
  Optics.sheet.in \
  renewable_energy.sheet.in \
  Racks.sheet.in \
  scenegraph.sheet.in \
  value_stream_mapping.sheet.in

SHEETS = $(sheet_in_files:.sheet.in=.sheet)

sheetdir = $(datadir)/dia/sheets
sheet_DATA = $(SHEETS)

%.sheet: %.sheet.in $(INTLTOOL_MERGE) $(wildcard $(top_srcdir)/po/*.po) 
	$(INTLTOOL_MERGE) $(top_srcdir)/po $< $@ -x -u \
	 	-c $(top_srcdir)/po/.intltool-merge-cache

EXTRA_DIST = $(sheet_in_files) \
	checktrans checktrans.py

CLEANFILES = sheet-translation-report $(SHEETS) 

sheet-translation-report: $(SHEETS) checktrans checktrans.py
	-(cd $(srcdir) && ./checktrans $(SHEETS))

all-local: sheet-translation-report

