import type { ApiResponse } from "../../../api/types";
import { apiFetch } from "../../../api/client";
import type { ChatRoom } from "../types/chatRoom";

export async function getOrCreateChatRoom(
  targetUserId: string,
): Promise<ChatRoom> {
  const result: ApiResponse<ChatRoom> = await apiFetch<ChatRoom>(
    `/chatrooms/${targetUserId}`,
    {
      method: "POST",
    },
  );

  if (!result.success) {
    const error = new Error(result.error.message);

    Object.assign(error, {
      code: result.error.code,
      details: result.error.details,
    });

    throw error;
  }

  return result.data;
}
