FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
LABEL maintainer="jeff-dias"
ENV ASPNETCORE_ENVIRONMENT=Docker
ENV ASPNETCORE_URLS="http://*:8080"
RUN useradd nonroot -m
USER nonroot

WORKDIR /home/nonroot/app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
RUN useradd nonroot -m
USER nonroot
WORKDIR /home/nonroot/src

COPY --chown=nonroot ["FinancialCalculator/FinancialCalculator.csproj", "FinancialCalculator/"]
RUN dotnet restore "FinancialCalculator/FinancialCalculator.csproj"
COPY --chown=nonroot . .
#WORKDIR /home/nonroot/src
RUN dotnet build "FinancialCalculator/FinancialCalculator.csproj" -c Release -o /home/nonroot/app/build

FROM build AS publish
RUN dotnet publish "FinancialCalculator/FinancialCalculator.csproj" -c Release -o /home/nonroot/app/publish

FROM base AS final
WORKDIR /home/nonroot/app
COPY --chown=nonroot --from=publish /home/nonroot/app/publish .
ENTRYPOINT ["dotnet", "FinancialCalculator.dll"]