#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["ChatWebApp/ChatWebApp.csproj", "ChatWebApp/"]
COPY ["Chat.App/Chat.App.csproj", "Chat.App/"]
COPY ["Chat.Models/Chat.Models.csproj", "Chat.Models/"]
COPY ["Chat.Services/Chat.Services.csproj", "Chat.Services/"]
COPY ["Chat.QueueConsumer/Chat.QueueConsumer.csproj", "Chat.QueueConsumer/"]
COPY ["Chat.CommandInterpreter/Chat.CommandInterpreter.csproj", "Chat.CommandInterpreter/"]
COPY ["Chat.WebService/Chat.WebService.csproj", "Chat.WebService/"]
COPY ["Chat.QueueProducer/Chat.QueueProducer.csproj", "Chat.QueueProducer/"]
RUN dotnet restore "ChatWebApp/ChatWebApp.csproj"
COPY . .
WORKDIR "/src/ChatWebApp"
RUN dotnet build "ChatWebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ChatWebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChatWebApp.dll"]