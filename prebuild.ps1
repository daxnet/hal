#
# Substitute the version number for each AssemblyInfo.cs and project.json file
#
param (
	[string]$assemblyVersion = "1.0.0.0",
	[string]$packageVersion = "1.0.0",
	[string]$sourceBranch = "refs/heads/main"
)

$suffix="-dev";
if (($sourceBranch -ne "refs/heads/main") -and ($sourceBranch -ne "refs/heads/dev")) {
	$splitted = $sourceBranch -split "/"
	if ($splitted.Length -ge 2) {
		$stage = $splitted[$splitted.Length - 2]
		if ($stage -eq "release") {
			$suffix=""
		} elseif ($stage -eq "feature") {
			$suffix="-"+$splitted[$splitted.Length - 1]
		}
		else {
			$suffix="-"+$stage
		}
	}
}

$files = Get-ChildItem . -include project.json,AssemblyInfo.cs,*.csproj -Recurse
foreach ($file in $files)
{
	(Get-Content $file.FullName) |
	ForEach-Object { $_ -replace "0.999.0.0", "$($assemblyVersion)" } |
	ForEach-Object { $_ -replace "0.999.0-dev", "$($packageVersion)$($suffix)" } |
	Set-Content $file.FullName
}
