import type { User } from "../types/types";
import { ListItemField } from "../../../shared/components/ListItemField";
import { Tag } from "../../../shared/components/Tag";
import type { TagVariant } from "../../../shared/components/Tag";
import { Button } from "../../../shared/components/Button";
import { useNavigate } from "react-router";
import { getOrCreateChatRoom } from "../../chat/api/chatRoomApi";
import { useAuth } from "../../auth/AuthContext";
import type { ApiError } from "../../../api/types";
import { ApiRequestError } from "../../../api/error";

interface UserListItemProps {
  user: User;
  onToggleStatus: (userId: string) => void;
  onError: (error: ApiError) => void;
}

export function UserListItem({
  user: listedUser,
  onToggleStatus,
  onError,
}: UserListItemProps) {
  const navigate = useNavigate();
  const { isTeacher } = useAuth();

  const handleEdit = () => {
    navigate(`/users/${listedUser.id}/edit`);
  };

  const handleChat = async () => {
    try {
      const chatRoom = await getOrCreateChatRoom(listedUser.id);
      navigate(`/chat/${chatRoom.id}`);
    } catch (error) {
      if (error instanceof ApiRequestError) {
        onError(error);
      }
    }
  };

  return (
    <div className="flex items-center rounded-xl border border-border bg-menu px-4 py-3">
      <ListItemField
        label="Name"
        value={`${listedUser.firstName} ${listedUser.lastName}`}
        className="flex-2"
      />

      <Tag
        title="Role"
        label={listedUser.roleName}
        variant={listedUser.roleName as TagVariant}
        className="flex-1"
      />

      <ListItemField
        label="Email"
        value={listedUser.email}
        className="min-w-0 flex-2"
      />

      {isTeacher && (
        <Tag
          title="Status"
          label={listedUser.isActive ? "Active" : "Inactive"}
          variant={listedUser.isActive ? "Active" : "Inactive"}
          className="flex-1"
        />
      )}

      <div className="flex items-center gap-3">
        {isTeacher && (
          <Button
            children={listedUser.isActive ? "Deactivate" : "Activate"}
            variant="list"
            color={listedUser.isActive ? "delete" : "resource"}
            onClick={() => onToggleStatus(listedUser.id)}
          />
        )}

        {isTeacher && (
          <Button
            children="Edit"
            variant="list"
            color="edit"
            onClick={handleEdit}
          />
        )}

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
