#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CreditRiskAssessment.API/CreditRiskAssessment.API.csproj", "CreditRiskAssessment.API/"]
COPY ["CreditRiskAssessment/CreditRiskAssessment.csproj", "CreditRiskAssessment/"]
COPY ["CreditRiskAssessment.ML/CreditRiskAssessment.ML.csproj", "CreditRiskAssessment.ML/"]
COPY ["CreditRiskAssessment.Test/CreditRiskAssessment.Test.csproj", "CreditRiskAssessment.Test/"]
RUN dotnet restore "CreditRiskAssessment.Test/CreditRiskAssessment.Test.csproj"
RUN dotnet restore "CreditRiskAssessment.ML/CreditRiskAssessment.ML.csproj"
RUN dotnet restore "CreditRiskAssessment/CreditRiskAssessment.csproj"
RUN dotnet restore "CreditRiskAssessment.API/CreditRiskAssessment.API.csproj"
COPY . .
WORKDIR "/src/CreditRiskAssessment.API"
RUN dotnet build "CreditRiskAssessment.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CreditRiskAssessment.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

FROM final AS deploy
WORKDIR /app
# COPY ["./CreditRiskAssessment.API/CRAS.zip", "/app/CRAS.zip"]
# COPY ["./CreditRiskAssessment.API/CRASML.zip", "/app/CRASML.zip"]
# COPY ["./CreditRiskAssessment.API/Helpers/cleaned_again.csv", "/app/Helpers/cleaned_again.csv"]
# COPY ["./CreditRiskAssessment.API/Helpers/ReadFile.csv", "/app/Helpers/ReadFile.csv"]

ENTRYPOINT ["dotnet", "CreditRiskAssessment.API.dll"]