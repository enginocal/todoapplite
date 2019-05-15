# FROM microsoft/dotnet:2.2-runtime
# WORKDIR /dotnetapp
# COPY ./bin/Docker .
# ENV ASPNETCORE_URLS http://*:5000
# ENV ASPNETCORE_ENVIRONMENT docker
# ENTRYPOINT dotnet TodoAppLite.dll

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /app
COPY *.csproj ./
RUN dotnet restore TodoAppLite.csproj
COPY . ./
RUN dotnet publish TodoAppLite.csproj -c Release -o out
FROM microsoft/dotnet:2.2-aspnetcore-runtime AS runtime
WORKDIR /app
COPY --from=build /app/out .
ENV ASPNETCORE_URLS http://*:5000
EXPOSE 5000
ENTRYPOINT ["dotnet", "TodoAppLite.dll", "--urls", "http://*:5000;http://*:5001"]