msbuild fa2cs.sln /p:Configuration=Release
mono ./fa2cs/bin/Release/fa2cs.exe `pwd`
nuget pack .nuspec
