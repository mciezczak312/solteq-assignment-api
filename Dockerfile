FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["EmployeesManagement.API/EmployeesManagement.API.csproj", "EmployeesManagement.API/"]
COPY ["EmployeesManagement.Infrastructure/EmployeesManagement.Infrastructure.csproj", "EmployeesManagement.Infrastructure/"]
COPY ["EmployeesManagement.Core/EmployeesManagement.Core.csproj", "EmployeesManagement.Core/"]

ENV ASPNETCORE_ENVIRONMENT="Production"

RUN dotnet restore "EmployeesManagement.API/EmployeesManagement.API.csproj"
COPY . .
WORKDIR "/src/EmployeesManagement.API"
RUN dotnet build "EmployeesManagement.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "EmployeesManagement.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "EmployeesManagement.API.dll"]