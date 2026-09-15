import { apiRequest } from "../../../api/request";
import type { ChatRoom } from "../types/chatRoom";

export async function getOrCreateChatRoom(
  targetUserId: string,
): Promise<ChatRoom> {
  return apiRequest<ChatRoom>(`/chatrooms/${targetUserId}`, {
    method: "POST",
  });
}

export async function getMyChatRooms(): Promise<ChatRoom[]> {
  return apiRequest<ChatRoom[]>("/chatrooms");
}
