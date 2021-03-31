#
# Substitute the version number for each AssemblyInfo.cs and project.json file
#
param (
	[string]$assemblyVersion = "1.0.0.0",
	[string]$packageVersion = "1.0.0-dev"
)

$files = Get-ChildItem . -include project.json,AssemblyInfo.cs,*.csproj -Recurse
foreach ($file in $files)
{
	(Get-Content $file.FullName) |
	ForEach-Object { $_ -replace "0.999.0.0", "$($assemblyVersion)" } |
	ForEach-Object { $_ -replace "0.999.0-dev", "$($packageVersion)" } |
	Set-Content $file.FullName
}
