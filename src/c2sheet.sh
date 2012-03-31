#!/bin/bash
if [ $# -ne 2 ]
then
	echo "USAGE: c2sheet.sh objects_src_dir output_sheet_dir"
	exit
fi
mkdir -p $2/c2sheet
find $1 -name "*.xpm" -exec cp {} $2/c2sheet \;
for file in $2/c2sheet/*.xpm 
do 
	convert "$file" "$2/c2sheet/$(basename $file .xpm).png"
done
rm $2/c2sheet/*.xpm
find $1 -name "*.png" -exec cp {} $2/c2sheet \;
find $2/c2sheet -name "*.png" -exec file {} \; | grep -v "22 x 22" | sed 's/:.*//' | xargs rm
  
cat <<EOF  > $2/c2sheet.sheet
<?xml version="1.0" encoding="UTF-8"?>
<sheet xmlns="http://www.lysator.liu.se/~alla/dia/dia-sheet-ns">
  <name>c2sheet</name>
  <description>Dummy sheet created by c2sheet</description>
  <contents>
EOF
find $1 -name "*.c" -exec gcc -fpreprocessed -dD -E {} >> $2/c2sheet.tmp \;
indent $2/c2sheet.tmp;
sed -e '/^ *$/d;/^extern DiaObjectType /d;s/^static //;s/G_MODULE_EXPORT //;s/_icon/_xpm/;/^DiaObjectType .* {/,/_xpm,/!d;/^DiaObjectType/d;/^  [0-9],$/d;s/^ *"/<object type="/;s/",/">/;s/(char \*\*) */<icon>/;s/_xpm,/.png<\/icon><\/object>/;s/1,//;s/&custom_type_ops//;' $2/c2sheet.tmp >> $2/c2sheet.sheet
echo "</contents></sheet>" >> $2/c2sheet.sheet
rm $2/c2sheet.tmp
