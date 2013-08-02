RMDIR /s /q Chrome.System.Bootstrap\content

del *.nupkg

xcopy ..\..\src\content\*.* Chrome.System.Bootstrap\content\ /Y /S /I /EXCLUDE:excludelist.txt
nuget pack Chrome.System.Bootstrap/Chrome.System.Bootstrap.nuspec

