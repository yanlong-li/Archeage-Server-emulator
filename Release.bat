md TEMP
cd temp
md AAServer\ArcheAge\bin\Debug\log
md AAServer\ArcheAge\bin\Debug\sql
md AAServer\ArcheAgeLauncher\bin\Debug\
md AAServer\ArcheAgeLogin\bin\Debug\log
md AAServer\ArcheAgeLogin\bin\Debug\sql
cd ..

copy ArcheAge\bin\Debug\*.* temp\AAServer\ArcheAge\bin\Debug\
copy ArcheAge\bin\Debug\log\*.* temp\AAServer\ArcheAge\bin\Debug\log\
copy ArcheAge\bin\Debug\sql\*.* temp\AAServer\ArcheAge\bin\Debug\sql\
copy ArcheAgeLauncher\bin\Debug\*.* temp\AAServer\ArcheAgeLauncher\bin\Debug\
copy ArcheAgeLogin\bin\Debug\*.* temp\AAServer\ArcheAgeLogin\bin\Debug\
copy ArcheAgeLogin\bin\Debug\sql\*.* temp\AAServer\ArcheAgeLogin\bin\Debug\sql\

copy readme.md temp\AAServer\
copy "start AAServer.bat" temp\AAServer\
copy "Release.bat" temp\AAServer\

cd temp
"C:\Program Files\WinRAR\rar.exe" a -df -m5 -r -y ..\AAServer.1.2.xx.rar *.*
cd ..
rd TEMP