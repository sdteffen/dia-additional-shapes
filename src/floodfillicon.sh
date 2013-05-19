#!/bin/bash
# Floodfill all white areas from the outside with transparency

convert $1 -bordercolor white -border 1 +repage  png:- | convert - -alpha on channel RGBA -fuzz 8% -fill none -draw 'color 0,0 floodfill' png:- | convert - -crop '22x22+1+1!' $1
