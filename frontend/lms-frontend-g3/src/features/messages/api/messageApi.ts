import { apiRequest } from "../../../api/request";
import type { Message } from "../types/message";

export async function getMessages(chatRoomId: string): Promise<Message[]> {
  return apiRequest<Message[]>(`/chatrooms/${chatRoomId}/messages`);
}

export async function sendMessage(
  chatRoomId: string,
  content: string,
): Promise<Message> {
  return apiRequest<Message>(`/chatrooms/${chatRoomId}/messages`, {
    method: "POST",
    body: JSON.stringify({
      content,
    }),
  });
}
