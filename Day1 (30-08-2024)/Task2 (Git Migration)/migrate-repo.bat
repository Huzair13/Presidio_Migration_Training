@echo off

REM 
set GITHUB_REPO_URL=https://github.com/RameshMF/todo-application-jsp-servlet-jdbc-mysql
set AZURE_REPO_URL=https://MigrationTask@dev.azure.com/MigrationTask/MigrationTaskApp/_git/MigrationTaskApp
set REPO_NAME=todo-application-jsp-servlet-jdbc-mysql

REM 
git clone %GITHUB_REPO_URL%

REM 
cd %REPO_NAME%

REM 
git remote add azure %AZURE_REPO_URL%

REM 
git push azure --all

REM 
git push azure --tags

pause
