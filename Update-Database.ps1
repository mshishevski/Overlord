Param(
  $Target,
  $Context = 'OverlordContext',
  $Project = './Overlord.DataAccess/',
  $StartupProject = './Overlord.Api/',

  [ValidateSet('Debug', 'Release')]
  $Configuration = 'Debug',

  [ValidateSet('Localhost', 'Prod')]
  $Environment = 'Localhost',  

  [switch]$Verbose
)

$migrateArgs = @()
If ($null -ne $Target) {
  $migrateArgs += $Target
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

Write-Host -f Blue 'Running command:'
Write-Host -f Cyan "  & dotnet ef database update $migrateArgs`n"
& dotnet ef database update $migrateArgs

Pop-Location

$Env:ASPNETCORE_ENVIRONMENT = $originalEnvironment
