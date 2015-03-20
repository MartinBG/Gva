:: -S server instance, -S . local Sql server instance
:: -i input file
:: -v variable
:: Windows Authentication is the default authentication mode for sqlcmd. To use SQL Server Authentication, you must specify a user name and password by using the -U and -P options.
:: -o output file
@sqlcmd -S . -v dbName="Ems1" -i "CreateAll.sql"
@echo off
pause
