FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app
COPY src/. .
RUN dotnet restore
RUN dotnet publish -c Release -r linux-x64 --self-contained -o out


# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
LABEL "Maintainer"="Farzan Hajian"
LABEL "Version"="1.1"
EXPOSE 5000/tcp
WORKDIR /app
COPY --from=build-env /app/out .


ENTRYPOINT ["./AsanRegEx.Server", "--urls=http://0.0.0.0:5000"]
