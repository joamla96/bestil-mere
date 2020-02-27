#!/bin/bash

for i in `ls data/restaurant`
do
	curl --header "Content-Type: application/json" \
	  --request POST \
	  --data @data/restaurant/$i \
	  https://gateway.bestilmere.xyz/restaurants
	echo $i
done
