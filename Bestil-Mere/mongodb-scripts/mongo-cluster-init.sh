#!/bin/bash
if [ ! -f ./.initialized ]; then
	touch .initialized
	set -m
	sleep 10
	mongos --port 27017 --configdb configserver/config01:27017,config02:27017,config03:27017 --bind_ip_all &
	mongo --host config01 --port 27017 < /scripts/init-configserver.js
	mongo --host shard01a --port 27018 < /scripts/init-shard01.js
	mongo --host shard02a --port 27019 < /scripts/init-shard02.js
	mongo --host shard03a --port 27020 < /scripts/init-shard03.js
	sleep 35
	mongo < /scripts/init-router.js
	mongo < /scripts/init-shard-zones.js
	mongo < /scripts/init-orders-zone-sharding.js
	fg %1
fi

if [ -f ./.initialized ]; then
	echo "Config has been initialized once. Skipping configuration.."
	mongos --port 27017 --configdb configserver/config01:27017,config02:27017,config03:27017 --bind_ip_all
fi

