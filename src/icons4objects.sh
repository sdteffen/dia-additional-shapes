#!/bin/bash
rm $2
find $1 -name "*.c" -exec gcc -fpreprocessed -dD -E {} >> $2.tmp \;
indent $2.tmp;
sed -e "/^ *$/d;/^extern DiaObjectType /d;/DiaObjectType .* {/,/_xpm,$/!d;/^DiaObjectType/d;/^  [0-9],$/d;" $2.tmp > $2

