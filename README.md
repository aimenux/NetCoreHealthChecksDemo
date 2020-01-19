# NetCoreHealthChecksDemo
Demo for healthchecks on net core

> healthcheck ui is served on default path `/healthchecks-ui`
>
>> 2 endpoints :
>> - Liveness (light checks) : accessible on `/live`
>> - Readiness (full checks) : accessible on `/ready`
>
>> 3 checkers :
>> - Ping checker
>> - Random checker
>> - Azure sql checker
>
>> 2 publishers :
>> - appinsights publisher (xabaril nuget)
>> - appinsights availability publisher (homemade)

**`Tools`** : vs19, net core 3.1, nlog.web 4.9