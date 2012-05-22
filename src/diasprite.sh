#!/bin/bash

find /usr/share/dia/sheets -name "*.png" > pngpath.txt
find /usr/share/dia/shapes -name "*.png" >> pngpath.txt
sed "s/.*\///" pngpath.txt > pngfiles.txt
wc -l pngfiles.txt
sort pngfiles.txt | uniq > pngfilesuniq.txt
wc -l pngfilesuniq.txt
find /usr/share/dia/shapes/ -name "*.png" -exec md5sum {} \; > pngmd5.txt
sed "s/ .*//" pngmd5.txt | sort | uniq > pngmd5uniq.txt
wc -l pngmd5uniq.txt



