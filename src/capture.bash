#!/bin/bash
import $1
convert -fuzz 15% -trim \
-filter cubic -define filter:b=1.3 -define filter:c=1.3 -resize 190x190 \
-background white -gravity center -extent 200x200 \
-resize 20x20 \
-gamma 0.5 \
$1 $1
#-fuzz 15% -transparent white \
#viewnior $1
