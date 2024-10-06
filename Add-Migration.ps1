[cmdletbinding()]
Param(
  [Parameter(Mandatory = $true)]
  [string]$Name,

  [switch]$ShowJson,
  $Context = 'OverlordContext',
  $Project = './Overlord.DataAccess/',
  $StartupProject = './Overlord.Api/',

  [ValidateSet('Debug', 'Release')]
  $Configuration = 'Debug',

  [ValidateSet('Localhost', 'Prod')]
  $Environment = 'Localhost'
)

$Verbose = $VerbosePreference -eq 'Continue'

$migrateArgs = @()
$migrateArgs += "$Name"
If ($ShowJson) {
  $migrateArgs += '--json'
}
$migrateArgs += '-c'
$migrateArgs += "$Context"
$migrateArgs += '-p'
$migrateArgs += "$Project"
$migrateArgs += '-s'
$migrateArgs += "$StartupProject"
$migrateArgs += '--configuration'
$migrateArgs += "$Configuration"
$migrateArgs += '--prefix-output'

If ($Verbose) {
  $migrateArgs += '-v'
}

$originalEnvironment = $Env:ASPNETCORE_ENVIRONMENT
$Env:ASPNETCORE_ENVIRONMENT = $Environment

Push-Location $PSScriptRoot

& dotnet tool restore | Out-Null
if (!$?) {
  return
}

Write-Host -f Blue 'Running command: '
Write-Host -f Cyan "  & dotnet ef migrations add $migrateArgs`n"
& dotnet ef migrations add $migrateArgs

Pop-Location

$Env:ASPNETCORE_ENVIRONMENT = $originalEnvironment
