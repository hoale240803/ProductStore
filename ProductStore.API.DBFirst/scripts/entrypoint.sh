#!/bin/bash
set -x

###################################################################
# Remember to save this file in LF (Unix) line format (no \r)
###################################################################
wait_time=90s 
echo "Starting the SQL Server in the background"
/opt/mssql/bin/sqlservr &
BACKGROUND_WORKER_PID=$!

echo "Waiting for the SQL Server to start"
until /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P MSSQL_SA_PASSWORD -Q "select 1"

#until /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $MSSQL_SA_PASSWORD -d master -i store.sql
do
  echo "Waiting for the SQL Server to start"
done



# wait for SQL Server to come up

echo "Initializing db"
#sleep $wait_time
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $MSSQL_SA_PASSWORD -d master -i store.sql 

#/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $MSSQL_SA_PASSWORD -d master -i store.sql &
#INIT_DB=$!

# Do not exit the docker image
wait $BACKGROUND_WORKER_PID
#wait $INIT_DB