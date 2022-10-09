sleep 3s
echo 'Starting setup script'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P mssql_12345 -d master -i initdb.sql
echo 'Finished setup script'
exit