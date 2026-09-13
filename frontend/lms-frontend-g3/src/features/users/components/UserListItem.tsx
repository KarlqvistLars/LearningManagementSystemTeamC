import type { User } from "../types/types";
import { ListItemField } from "../../../shared/components/ListItemField";
import { Tag } from "../../../shared/components/Tag";
import type { TagVariant } from "../../../shared/components/Tag";
import { Button } from "../../../shared/components/Button";
import { useNavigate } from "react-router";
import { getOrCreateChatRoom } from "../../chat/api/chatRoomApi";

interface UserListItemProps {
  user: User;
  onToggleStatus: (userId: string) => void;
}

export function UserListItem({ user, onToggleStatus }: UserListItemProps) {
  const navigate = useNavigate();

  const handleEdit = () => {
    navigate(`/users/${user.id}/edit`);
  };

  const handleChat = async () => {
    try {
      const chatRoom = await getOrCreateChatRoom(user.id);
      navigate(`/chat/${chatRoom.id}`);
    } catch (error) {
      console.error(error);
    }
  };

  return (
    <div className="flex items-center rounded-xl border border-border bg-menu px-4 py-3">
      <ListItemField
        label="Name"
        value={`${user.firstName} ${user.lastName}`}
        className="flex-2"
      />

      <Tag
        title="Role"
        label={user.roleName}
        variant={user.roleName as TagVariant}
        className="flex-1"
      />

      <ListItemField
        label="Email"
        value={user.email}
        className="min-w-0 flex-2"
      />

      <Tag
        title="Status"
        label={user.isActive ? "Active" : "Inactive"}
        variant={user.isActive ? "Active" : "Inactive"}
        className="flex-1"
      />

      <div className="flex items-center gap-3">
        <Button
          children="Edit"
          variant="list"
          color="edit"
          onClick={handleEdit}
        />

        <Button
          children={user.isActive ? "Deactivate" : "Activate"}
          variant="list"
          color={user.isActive ? "delete" : "resource"}
          onClick={() => onToggleStatus(user.id)}
        />

        <Button
          children="Chat"
          variant="list"
          color="create"
          onClick={handleChat}
        />
      </div>
    </div>
  );
}
