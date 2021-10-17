# APIUsageTracker
RESTful API that tracks the number of times your api is called and calculates a monthly charge based on the number of calls. The calls will be charged in bands for.

# Technology Used
* C# .NetCore
* XUnit

# Sample Requests and Responses
[PostmanCollection](https://www.getpostman.com/collections/5cf256b7f070efc5dbba)

# Possible Improvements
* Include CI/CD
* Docker

# Notes
1. Every party accessing this servie must be issued a unique token to track their requests seeing as one vendor could have multiple sources.
2. Issued token is to be added to the config variable.
3. Monthly charge has a limit of three months i.e you can't retrieve charges past 3 months.
