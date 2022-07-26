@echo off
for /f "delims=" %%a in ('wmic OS Get localdatetime ^| find "."') do set DateTime=%%a
set mydatetime=%DateTime:~0,4%%DateTime:~4,2%%DateTime:~6,2%_%DateTime:~8,2%%DateTime:~10,2%%DateTime:~12,2%
echo %mydatetime%

dotnet ef migrations add TabanMed_%mydatetime% --context ApplicationDbContext --startup-project ../TabanMed.Admin/TabanMed.Admin.csproj
