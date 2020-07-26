FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

WORKDIR /home/app
COPY bin/Debug/netcoreapp3.1/publish .

ENTRYPOINT ["dotnet", "WebPersonal.BackEnd.API.dll"]