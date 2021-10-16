# APIUsageTracker
Application to track API requests and calculate monthly cost

Notes:
1. Every party accessing this servie must be issued a unique token to track their requests seeing as one vendor could have multiple sources.
2. Issued token is to be added to the config variable.
3. Monthly charge has a limit of three months i.e you can't retrieve charges past 3 months.
