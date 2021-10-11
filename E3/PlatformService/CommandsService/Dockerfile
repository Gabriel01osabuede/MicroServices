FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /src

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet build -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /src
COPY --from=build-env /src/out .
ENTRYPOINT ["dotnet", "CommandsService.dll" ]