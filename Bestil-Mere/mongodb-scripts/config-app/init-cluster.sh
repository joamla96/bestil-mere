#!/bin/bash

docker exec bestil-mere_config01_1 sh -c "mongo --port 27017 < /scripts/init-configserver.js"
docker exec bestil-mere_shard01a_1 sh -c "mongo --port 27018 < /scripts/init-shard01.js"
docker exec bestil-mere_shard02a_1 sh -c "mongo --port 27019 < /scripts/init-shard02.js"
docker exec bestil-mere_shard03a_1 sh -c "mongo --port 27020 < /scripts/init-shard03.js"
sleep 35
docker exec bestil-mere_router_1 sh -c "mongo < /scripts/init-router.js"
docker exec bestil-mere_router_1 sh -c "mongo < /scripts/init-shard-zones.js"
docker exec bestil-mere_router_1 sh -c "mongo < /scripts/init-orders-zone-sharding.js"
