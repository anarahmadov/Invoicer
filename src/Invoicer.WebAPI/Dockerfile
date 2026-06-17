# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

WORKDIR /app

EXPOSE 8080


# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src


# Copy project files only
COPY ["Invoicer.WebAPI/Invoicer.WebAPI.csproj", "Invoicer.WebAPI/"]
COPY ["Invoicer.Application/Invoicer.Application.csproj", "Invoicer.Application/"]
COPY ["Invoicer.Infrastructure/Invoicer.Infrastructure.csproj", "Invoicer.Infrastructure/"]
COPY ["Invoicer.Domain/Invoicer.Domain.csproj", "Invoicer.Domain/"]


# Restore dependencies
RUN dotnet restore "Invoicer.WebAPI/Invoicer.WebAPI.csproj"


# Copy everything else
COPY . .


WORKDIR "/src/Invoicer.WebAPI"


# Build
RUN dotnet build "Invoicer.WebAPI.csproj" \
    -c Release \
    -o /app/build



# Publish
FROM build AS publish

RUN dotnet publish "Invoicer.WebAPI.csproj" \
    -c Release \
    -o /app/publish



# Final image
FROM base AS final

WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Invoicer.WebAPI.dll"]