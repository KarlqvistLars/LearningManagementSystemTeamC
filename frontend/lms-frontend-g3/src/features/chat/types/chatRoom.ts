import type { ChatRoomMember } from "./chatRoomMember";

export interface ChatRoom {
  id: string;
  name: string | null;
  createdAt: string;
  members: ChatRoomMember[];
}
