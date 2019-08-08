param(

    [Parameter(Mandatory=$true)]
    [string]
    $identityServiceBaseUrl,

    [Parameter(Mandatory=$true)]
    [string]
    $apiBaseUrl,

    [Parameter(Mandatory=$true)]
    [string]
    $tokenUser,

    [Parameter(Mandatory=$true)]
    [string]
    $tokenPassword,

    [Parameter(Mandatory=$true)]
    [string]
    $appName,

    [Parameter(Mandatory=$false)]
    [bool]
    $createRules
)

add-type @"
    using System.Net;
    using System.Security.Cryptography.X509Certificates;
    public class TrustAllCertsPolicy : ICertificatePolicy {
        public bool CheckValidationResult(
            ServicePoint srvPoint, X509Certificate certificate,
            WebRequest request, int certificateProblem) {
            return true;
        }
    }
"@
[System.Net.ServicePointManager]::CertificatePolicy = New-Object TrustAllCertsPolicy

 $token = .\Authentication.ps1 -identityServiceBaseUrl $identityServiceBaseUrl -tokenUser $tokenUser -tokenPassword $tokenPassword
 Write-Host $token
.\1_CreateApp.ps1 -token $token -apiBaseUrl $apiBaseUrl -appName $appName

$schemaResponse = .\2_CreateSchemas.ps1 -token $token -apiBaseUrl $apiBaseUrl -appName $appName
Write-Host $schemaResponse
.\3_CreateRoles.ps1 -token $token -apiBaseUrl $apiBaseUrl -appName $appName
.\4_CreateLanguages.ps1 -token $token -apiBaseUrl $apiBaseUrl -appName $appName 

if($createRules)
{
    .\5_CreateRules.ps1 -token $token -apiBaseUrl $apiBaseUrl -appName $appName -schemaIds $schemaResponse
}


.\6_CreateRefDataContent.ps1 -token $token -apiBaseUrl $apiBaseUrl -appName $appName
.\7_AddTestUsers.ps1 -token $token -apiBaseUrl $apiBaseUrl -appName $appName