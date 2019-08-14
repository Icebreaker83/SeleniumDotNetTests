param(
    [ValidateSet('firefox', 'chrome', 'internetexplorer')]
    $Browser = 'firefox',

    [ValidateSet('quiet','minimal','normal','detailed')]
    $Verbosity = 'normal', 

    [switch] $RemovePreviousResults
 )

Enter-Build {
    $script:PackagesPath = "NewTests/packages.config" 
    $script:PackagesDir  = "packages"
    $script:SettingsFile = "$Browser.runsettings"

    Write-Host "Executing as:" $(whoami.exe)
}

task . Run

# Synopsis: Install Chocolatey packages 
task DepsChoco {
    if (!(Get-Command choco -ea 0)) {
        if (Test-Path C:\ProgramData\chocolatey\bin\choco.exe) {
            Set-Alias choco C:\ProgramData\chocolatey\bin\choco.exe
        } else { 
            Invoke-WebRequest https://chocolatey.org/install.ps1 | Invoke-Expression
        }
    }

    #choco install -y git.install            # Only on git repository and with some TFS options such as clean.
    choco install -y nuget.commandline
    choco install -y netfx-4.6.2-devpack
    choco install -y dotnetcore-sdk

    #choco install -y firefox
    #choco install -y chrome

    Import-Module C:\ProgramData\chocolatey\helpers\chocolateyProfile.psm1
    Update-SessionEnvironment
}

# Synopsis: Restore project nuget packages
task DepsNuget {
    exec { nuget restore $PackagesPath -PackagesDirectory $PackagesDir }
}

# Synopsis: Run the tests, by default in Firefox or specify using -Browser parameter
task Run DepsNuget, {
    Write-Host "Running in $Browser using vstest.console.exe"
    
    if ($RemovePreviousResults) { rm TestResults -ea 0 -Recurse }

    cd NewTests

    $params = @(
        '--settings',           $SettingsFile
        '--results-directory',  '..\TestResults'
        '--logger',             'trx'
        '--verbosity',          $Verbosity[0]
        '--diag',               ('..\TestResults\diag_{0}.log' -f (Get-Date -Format s).Replace(':','.') )
    )
    exec { dotnet test ./NewTests.csproj $params }
}

# Synopsis: Create single documentation file from README.md TOC
task MakeSingleDoc {
    rm Docs.md -ea 0
    
    (Get-Content README.md -Raw) -match "## Dokumentacija(.|\n)+?\n(?=##)"
    $doc_files = $Matches[0] -split "`n" | % { if ($_ -match "(?<=\()docs/.+(?=\))") { $Matches[0] } }

    ls (@("README.md") + $doc_files ) | % { Get-Content -Encoding UTF8 $_; "`n---`n" } | Out-File -Encoding utf8 -Append 'docs\Docs.md'
    ls docs\Docs.md
}