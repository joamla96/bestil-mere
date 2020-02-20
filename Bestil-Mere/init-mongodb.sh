#!/bin/bash

docker exec bestilmere_config01_1 sh -c "mongo --port 27017 < /scripts/init-configserver.js"
docker exec bestilmere_shard01a_1 sh -c "mongo --port 27018 < /scripts/init-shard01.js"
docker exec bestilmere_shard02a_1 sh -c "mongo --port 27019 < /scripts/init-shard02.js"
docker exec bestilmere_shard03a_1 sh -c "mongo --port 27020 < /scripts/init-shard03.js"
sleep 35
docker exec bestilmere_router_1 sh -c "mongo < /scripts/init-router.js"
