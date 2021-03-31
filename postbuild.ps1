$nupkgOutputDirectory = $PSScriptRoot + '\bld\nupkgs'
if (-not (Test-Path -LiteralPath $nupkgOutputDirectory)) {
	try {
        New-Item -Path $nupkgOutputDirectory -ItemType Directory -ErrorAction Stop | Out-Null
    }
    catch {
        Write-Error -Message "Unable to create directory '$nupkgOutputDirectory'. Error was: $_" -ErrorAction Stop
    }
    "Successfully created directory '$nupkgOutputDirectory'."
} else {
	$existingFiles = Get-ChildItem $nupkgOutputDirectory -Include *.nupkg -Recurse
	foreach ($file in $existingFiles)
	{
		Remove-Item $file.FullName
	}
}

$files = Get-ChildItem src/Hal -Include *.nupkg -Recurse
foreach ($file in $files)
{
	$fileName = $file.Name
	"Copying package '$fileName'."
	Copy-Item -Path $file.FullName -Destination $nupkgOutputDirectory
}
