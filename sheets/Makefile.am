
# To create a .sheet.in file from a .sheet file, run
# sed 's!<description!<_description!;s!</description!</_description!;s!<name!<_name!;s!</name!</_name!;' 

sheet_in_files = \
  electric2.sheet.in \
  Racks.sheet.in

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

