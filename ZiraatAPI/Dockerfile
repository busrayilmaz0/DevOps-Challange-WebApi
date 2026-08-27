FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ZiraatAPI.csproj", "./"]
RUN dotnet restore "ZiraatAPI.csproj"
COPY . .
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
# Render'ın zorunlu kıldığı 10000 portunu doğrudan sabitliyoruz
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000
ENTRYPOINT ["dotnet", "ZiraatAPI.dll"]