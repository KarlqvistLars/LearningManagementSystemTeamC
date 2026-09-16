import { useNavigate } from "react-router";
import { useAuth } from "../../auth/AuthContext";
import { Button } from "../../../shared/components/Button";
import { ListItemField } from "../../../shared/components/ListItemField";
import type { ResourceDto } from "../types/interfaces";

interface ResourceSummaryCardProps {
  resource: ResourceDto;
}

export function ResourceSummaryCard({ resource }: ResourceSummaryCardProps) {
  const navigate = useNavigate();
  const { isTeacher } = useAuth();

  return (
    <div className="flex items-center rounded-xl border border-border bg-menu px-4 py-3">
      <ListItemField
        label="Name"
        value={resource.resourceName}
        className="flex-2"
        link={`/resources/${resource.id}`}
      />

      <ListItemField label="Type" value={resource.type} className="flex-2" />

      <ListItemField
        label="Created date"
        value={new Date(resource.createdAt).toLocaleDateString()}
        className="flex-2"
      />

      <div className="flex items-center gap-3">
        {isTeacher && (
          <>
            <Button
              variant="list"
              color="edit"
              onClick={() => navigate(`/resources/${resource.id}/edit`)}
            >
              Edit
            </Button>

            <Button
              variant="list"
              color="delete"
              onClick={() =>
                alert(
                  `Deleted resource ${resource.resourceName}\nWell NOT REALLY but just to show the functionality`,
                )
              }
            >
              Delete
            </Button>
          </>
        )}
      </div>
    </div>
  );
}
