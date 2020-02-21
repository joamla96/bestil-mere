sh.enableSharding("OrderDb")
sh.shardCollection("OrderDb.Orders", { Country: 1, CustomerId: 1})
sh.disableBalancing("OrderDb.Orders")
// Shard zones should be added when this script is running
// Add EU countries to zone
sh.addTagRange("OrderDb.Orders", {"Country": "DK", "CustomerId": MinKey}, {"Country": "DK", "CustomerId": MaxKey}, "EU")
sh.addTagRange("OrderDb.Orders", {"Country": "DE", "CustomerId": MinKey}, {"Country": "DE", "CustomerId": MaxKey}, "EU")
sh.addTagRange("OrderDb.Orders", {"Country": "SWE", "CustomerId": MinKey}, {"Country": "SWE", "CustomerId": MaxKey}, "EU")

// Add NA countries to zone
sh.addTagRange("OrderDb.Orders", {"Country": "US", "CustomerId": MinKey}, {"Country": "US", "CustomerId": MaxKey}, "NA")

// Add AS countries to zone
sh.addTagRange("OrderDb.Orders", {"Country": "CN", "CustomerId": MinKey}, {"Country": "CN", "CustomerId": MaxKey}, "AS")

// Enable balancing again
sh.startBalancer()
sh.enableBalancing("OrderDb.Orders")

