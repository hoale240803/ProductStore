#!/bin/bash
# Wait for database to startup 

/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P ${SA_PASSWORD} -i store.sql