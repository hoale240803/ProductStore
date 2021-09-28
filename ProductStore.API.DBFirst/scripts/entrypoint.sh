#!/bin/bash
set -x

###################################################################
# Remember to save this file in LF (Unix) line format (no \r)
###################################################################

echo "Starting the SQL Server in the background"
/opt/mssql/bin/sqlservr &
BACKGROUND_WORKER_PID=$!

echo "Waiting for the SQL Server to start"
until /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $PASSWORD -Q "select 1"
do
  echo "Waiting for the SQL Server to start"
done

echo "Initializing db"
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $PASSWORD -d master -i store.sql

# Do not exit the docker image
wait $BACKGROUND_WORKER_PID