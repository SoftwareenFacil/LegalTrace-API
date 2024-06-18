# Official .NET SDK base image for the build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

ARG APP_NAME 

WORKDIR /app

COPY . .

RUN dotnet tool install --global dotnet-ef --version 7.0.11
ENV PATH="$PATH:/root/.dotnet/tools"

# Generate migration script
#RUN dotnet ef --project ./LegalTrace.DAL --startup-project ./LegalTrace \
#dbcontext script -o ./script.sql

RUN dotnet ef --project ./$APP_NAME.DAL --startup-project ./$APP_NAME \
dbcontext script -o ./script.sql

WORKDIR /app
RUN dotnet restore

ENV DOCKER_BUILD=true

# Build the application
RUN dotnet publish -c Release -r linux-x64 -o out

# Build a runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0

ARG APP_NAME
ENV ENV_APP_NAME=$APP_NAME

WORKDIR /app

COPY --from=build /app/out/ ./
COPY --from=build /app/script.sql /app/script/

# Copy the substituted appsettings.json
COPY $APP_NAME/appsettings.json /app/appsettings.json

RUN apt-get update

RUN apt-get install wget libgdiplus -y

RUN wget -P /app https://github.com/rdvojmoc/DinkToPdf/raw/master/v0.12.4/64%20bit/libwkhtmltox.dll

RUN wget -P /app https://github.com/rdvojmoc/DinkToPdf/raw/master/v0.12.4/64%20bit/libwkhtmltox.dylib

RUN wget -P /app https://github.com/rdvojmoc/DinkToPdf/raw/master/v0.12.4/64%20bit/libwkhtmltox.so

ADD ./$APP_NAME/HtmlTemplates/ /app/HtmlTemplates/

ENV ASPNETCORE_URLS=http://+:5108;

# Expose port
EXPOSE 5108

CMD dotnet "./$ENV_APP_NAME.dll"
