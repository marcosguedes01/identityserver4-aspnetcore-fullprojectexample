# identityserver4-aspnetcore-fullprojectexample
Exemplo completo utilizando o IdentityServer4 com ASP.NET Core 3.0

## Criando um certificado para o Windows:
openssl req -newkey rsa:2048 -nodes -keyout identityserver4fullexample.key -x509 -days 365 -out identityserver4fullexample.cer

## Criando uma chave a partir do certificado:
openssl pkcs12 -export -in identityserver4fullexample.cer -inkey identityserver4fullexample.key -out identityserver4fullexample.pfx

## Comando para instalação do QuickStart (via PowerShell)
iex ((New-Object System.Net.WebClient).DownloadString('https://raw.githubusercontent.com/IdentityServer/IdentityServer4.Quickstart.UI/release/get.ps1'))
