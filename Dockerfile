FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build-env
WORKDIR /app
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o out


# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:10.0
LABEL "Maintainer"="Farzan Hajian"
LABEL "Version"="1.2"
EXPOSE 5000/tcp
WORKDIR /app
COPY --from=build-env /app/out .


ENTRYPOINT ["./AsanRegEx.Server", "--urls=http://0.0.0.0:5000"]
