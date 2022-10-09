# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source

EXPOSE 80

ENV ASPNETCORE_ENVIRONMENT $ENVIRONMENT

# copy csproj and restore as distinct layers
COPY *.sln .
COPY Checkout.Api/*.csproj ./Checkout.Api/
COPY Checkout.Application/*.csproj ./Checkout.Application/
COPY Checkout.Domain/*.csproj ./Checkout.Domain/
COPY Checkout.Persistence/*.csproj ./Checkout.Persistence/
COPY Checkout.Tests/*.csproj ./Checkout.Tests/
COPY Checkout.IntegrationTests/*.csproj ./Checkout.IntegrationTests/
RUN dotnet restore ./Checkout.Api/

# copy everything else and build app
COPY Checkout.Api/. ./Checkout.Api/
COPY Checkout.Application/. ./Checkout.Application/
COPY Checkout.Domain/. ./Checkout.Domain/
COPY Checkout.Persistence/. ./Checkout.Persistence/
COPY Checkout.Tests/. ./Checkout.Tests/
COPY Checkout.IntegrationTests/. ./Checkout.IntegrationTests/
WORKDIR /source/Checkout.Api
RUN dotnet publish -c release -o /app

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "Checkout.Api.dll"]
