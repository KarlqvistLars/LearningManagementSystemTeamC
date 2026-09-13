import {
  HubConnectionBuilder,
  LogLevel,
  HubConnectionState,
} from "@microsoft/signalr";

export const chatConnection = new HubConnectionBuilder()
  .withUrl("https://localhost:7001/hubs/chat")
  .withAutomaticReconnect()
  .configureLogging(LogLevel.Information)
  .build();

let startPromise: Promise<void> | null = null;

export async function startChatConnection(): Promise<void> {
  if (chatConnection.state === HubConnectionState.Connected) return;

  if (startPromise) return startPromise;

  startPromise = chatConnection.start().finally(() => {
    startPromise = null;
  });

  return startPromise;
}
