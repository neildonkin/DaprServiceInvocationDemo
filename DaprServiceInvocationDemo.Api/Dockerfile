FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["DaprServiceInvocationDemo.Api/DaprServiceInvocationDemo.Api.csproj", "DaprServiceInvocationDemo.Api/"]
RUN dotnet restore "DaprServiceInvocationDemo.Api/DaprServiceInvocationDemo.Api.csproj"
COPY . .
WORKDIR "/src/DaprServiceInvocationDemo.Api"
RUN dotnet build "DaprServiceInvocationDemo.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DaprServiceInvocationDemo.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DaprServiceInvocationDemo.Api.dll"]
