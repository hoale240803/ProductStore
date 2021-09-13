#!/bin/sh
set -e
  
host="$1"
shift

echo ".... Starting login ..."
# navigation
echo "current folder"

echo "msslq/bin folder"
pwd

#'/opt/mssql/bin/sqlservr & /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $${SA_PASSWORD} -q "CREATE DATABASE $${DATABASE}"; 
# /opt/mssql-tools/bin/sqlcmd -S localhost -i /db/store.sql  wait'

# login & seedding data

echo -e "Running under Sql Server xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"
  # sqlcmd -?
  # sqlcmd -S db.mssql -U rust -P  -d rust_test -q "Select * from rust.content_categories"
  # sqlcmd -S 192.168.1.108 -U test -P  -d shortpoetdb -q "Select * from vcc.admin_users"
  until /opt/mssql-tools/bin/sqlcmd -S ms-sqldb -U "${MSSQL_USER}" -P "${SA_PASSWORD}" -i "${DATA_SCRIPT}" -q ":exit"; do
  >&2 echo -e "$Mssql is $unavailable - sleeping"
  sleep 2

>&2 echo "MSSQL is up - executing command xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"
exec "$@"