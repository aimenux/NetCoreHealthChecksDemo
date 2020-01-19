# NetCoreHealthChecksDemo
Demo for healthchecks on net core

> healthcheck ui is served on default path `/healthchecks-ui`
>
>> 2 healthcheck endpoints :
>> - Liveness (light checks) : accessible on `/live`
>> - Readiness (full checks) : accessible on `/ready`
>
>> 2 healthcheck publishers :
>> - appinsights publisher (xabaril nuget)
>> - appinsights availability publisher (homemade)
>
> azure sql database was created manually using portal
>> login / user are created with tsql commands :
>> - CREATE LOGIN YourLogin WITH PASSWORD = 'YourPass';
>> - CREATE USER YourUser FOR LOGIN YourLogin;

**`Tools`** : vs19, net core 3.1, nlog.web 4.9