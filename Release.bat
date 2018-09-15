md TEMP
cd temp
md AAServer\ArcheAge\bin\Debug\log
md AAServer\ArcheAge\bin\Debug\sql
md AAServer\ArcheAgeLauncher\bin\Debug\
md AAServer\ArcheAgeLogin\bin\Debug\log
md AAServer\ArcheAgeLogin\bin\Debug\sql
md AAServer\ArcheAgeStream\bin\Debug\log
md AAServer\ArcheAgeStream\bin\Debug\sql
md AAServer\packages\dll\
cd ..

xcopy ArcheAge\bin temp\AAServer\ArcheAge\bin /i /e
xcopy ArcheAgeLauncher\bin temp\AAServer\ArcheAgeLauncher\bin /i /e
xcopy ArcheAgeLogin\bin temp\AAServer\ArcheAgeLogin\bin /i /e
xcopy ArcheAgeStream\bin temp\AAServer\ArcheAgeStream\bin /i /e
xcopy packages temp\AAServer\packages /i /e

copy readme.md temp\AAServer\
copy "start AAServer.bat" temp\AAServer\
copy "Release.bat" temp\AAServer\

cd temp
"C:\Program Files\WinRAR\rar.exe" a -df -m5 -r -y ..\AAServer.1.2.xx.rar *.*
cd ..
rd TEMP