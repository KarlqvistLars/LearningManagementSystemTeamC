import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr";

export const chatConnection = new HubConnectionBuilder()
  .withUrl("https://localhost:7001/hubs/chat")
  .withAutomaticReconnect()
  .configureLogging(LogLevel.Information)
  .build();
