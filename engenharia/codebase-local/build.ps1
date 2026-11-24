param([ValidateSet('verified', 'data-core', 'reports', 'domain-core')][string]$Project = 'verified')
$ErrorActionPreference = 'Stop'
& python (Join-Path $PSScriptRoot 'tools\build_workspace.py') --project $Project
if ($LASTEXITCODE -ne 0) { throw 'Local reconstruction build failed; inspect project logs.' }
