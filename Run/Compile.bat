
Echo "#### Compile the Solution First!!! ####"

if not exist build mkdir build

if exist build\main.exe del /q /f build\main.exe 

cd build

..\..\bin\mosacl.exe -a=x86ii -f=PE --pe-file-alignment=4096 --map=hello.map -b=mb0.7ii -sa -ssa -o ..\build\main.exe ..\..\bin\%1

rem -mped=..\..\bin\output 

cd ..

