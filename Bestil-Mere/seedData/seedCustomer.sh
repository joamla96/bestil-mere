#!/bin/bash

for i in `ls data/customer`
do
	curl --header "Content-Type: application/json" \
	  --request POST \
	  --data @data/customer/$i \
	  https://gateway.bestilmere.xyz/customers
	echo $i
done
