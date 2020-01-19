# NetCoreHealthChecksDemo
Demo for healthchecks on net core

> healthcheck ui is served on default path `/healthchecks-ui`
>
>> 2 healthcheck endpoints :
>> - Liveness (light checks) : accessible on `/live`
>> - Readiness (full checks) : accessible on `/ready`
>
>> 3 healthcheck checkers :
>> - Ping checker
>> - Random checker
>> - Azure sql checker
>
>> 2 healthcheck publishers :
>> - appinsights publisher (xabaril nuget)
>> - appinsights availability publisher (homemade)

**`Tools`** : vs19, net core 3.1, nlog.web 4.9