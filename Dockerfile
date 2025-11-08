# =============================================================================
# BUILD STAGE
# This stage compiles the .NET application and generates database scripts
# =============================================================================
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Build argument passed from GitHub Actions to specify the project name
ARG APP_NAME
# Optional: Version tag for tracking builds
ARG VERSION_TAG

WORKDIR /app

# Copy all source code into the container
COPY . .

# Install Entity Framework Core CLI tools for database migrations
# Version 7.0.11 matches the .NET 7.0 SDK
RUN dotnet tool install --global dotnet-ef --version 7.0.11

# Add dotnet tools to PATH so we can use 'dotnet ef' command
ENV PATH="$PATH:/root/.dotnet/tools"

# =============================================================================
# GENERATE DATABASE MIGRATION SCRIPT
# Creates a SQL script from Entity Framework migrations
# This script can be used to update the database schema
# =============================================================================
RUN dotnet ef --project ./$APP_NAME.DAL --startup-project ./$APP_NAME \
    dbcontext script -o ./script.sql

# Return to app root directory
WORKDIR /app

# Restore NuGet packages for all projects in the solution
RUN dotnet restore

# Environment variable to indicate we're building inside Docker
# Can be used in code to adjust behavior for containerized environments
ENV DOCKER_BUILD=true

# =============================================================================
# BUILD AND PUBLISH APPLICATION
# Compiles the .NET application in Release mode for production
# -c Release: Use Release configuration (optimized, no debug symbols)
# -r linux-x64: Target Linux 64-bit runtime
# -o out: Output compiled files to 'out' directory
# =============================================================================
RUN dotnet publish -c Release -r linux-x64 -o out

# =============================================================================
# RUNTIME STAGE
# Creates a smaller final image with only the runtime and compiled application
# This reduces the final image size significantly
# =============================================================================
FROM mcr.microsoft.com/dotnet/aspnet:7.0

# Pass build arguments to runtime stage
ARG APP_NAME
ARG VERSION_TAG

# Store APP_NAME as environment variable for use at runtime
ENV ENV_APP_NAME=$APP_NAME

# Optional: Store version information
ENV VERSION=$VERSION_TAG

WORKDIR /app

# =============================================================================
# COPY ARTIFACTS FROM BUILD STAGE
# =============================================================================
# Copy compiled application from the build stage
COPY --from=build /app/out/ ./

# Copy database migration script to a dedicated folder
COPY --from=build /app/script.sql /app/script/

# Copy the configured appsettings.json (modified by GitHub Actions)
# This file contains environment-specific settings like connection strings
COPY $APP_NAME/appsettings.json /app/appsettings.json

# =============================================================================
# INSTALL SYSTEM DEPENDENCIES
# =============================================================================
# Update package lists
RUN apt-get update

# Install required libraries:
# - wget: For downloading files
# - libgdiplus: GDI+ compatible API for non-Windows platforms (needed for System.Drawing)
RUN apt-get install wget libgdiplus -y

# =============================================================================
# INSTALL WKHTMLTOPDF LIBRARIES
# Required for PDF generation using DinkToPdf
# Downloads platform-specific binaries for HTML to PDF conversion
# =============================================================================
RUN wget -P /app https://github.com/rdvojmoc/DinkToPdf/raw/master/v0.12.4/64%20bit/libwkhtmltox.dll

RUN wget -P /app https://github.com/rdvojmoc/DinkToPdf/raw/master/v0.12.4/64%20bit/libwkhtmltox.dylib

RUN wget -P /app https://github.com/rdvojmoc/DinkToPdf/raw/master/v0.12.4/64%20bit/libwkhtmltox.so

# =============================================================================
# COPY HTML TEMPLATES
# Templates used for generating PDF documents or email content
# =============================================================================
ADD ./$APP_NAME/HtmlTemplates/ /app/HtmlTemplates/

# =============================================================================
# CONFIGURE ASP.NET CORE
# =============================================================================
# Set the URLs that the application will listen on
# http://+:5108 means listen on all network interfaces on port 5108
ENV ASPNETCORE_URLS=http://+:5108

# Expose port 5108 to the host machine
# Note: This is documentation only; actual port mapping happens at runtime
EXPOSE 5108

# =============================================================================
# STARTUP COMMAND
# Runs the application DLL using the dotnet runtime
# Uses the environment variable set earlier to construct the DLL name
# =============================================================================
CMD dotnet "./$ENV_APP_NAME.dll"