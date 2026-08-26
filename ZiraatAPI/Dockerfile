FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ZiraatAPI.csproj", "./"]
RUN dotnet restore "ZiraatAPI.csproj"
COPY . .
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 11130
ENV ASPNETCORE_URLS=http://+:11130
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ZiraatAPI.dll"]
