#!/usr/bin/env bash

docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=th1sIsStr()ng' -e 'MSSQL_PID=Express' -p 1433:1433 mcr.microsoft.com/mssql/server:latest
