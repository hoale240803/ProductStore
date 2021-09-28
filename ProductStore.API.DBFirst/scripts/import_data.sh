#!/bin/bash
set -e

wait_time=90s 
password=Secret1234

# wait for SQL Server to come up
echo importing data will start in $wait_time...
sleep $wait_time

echo running store.sql ...
/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P $password -i store.sql

#echo running AttachDb...
#/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P $password -i store.sql

exec "$@"