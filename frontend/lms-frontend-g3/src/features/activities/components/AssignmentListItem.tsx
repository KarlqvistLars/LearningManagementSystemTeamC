import type { ActivityDetailsDto } from "../types";
import { ListItemField } from "../../../shared/components/ListItemField";
import { Button } from "../../../shared/components/Button";
import { useNavigate } from "react-router";
import { useAuth } from "../../auth/AuthContext";

interface AssignmentListItemProps {
  assignment: ActivityDetailsDto;
}

export function AssignmentListItem({ assignment }: AssignmentListItemProps) {
  const navigate = useNavigate();
  const { isTeacher } = useAuth();

  const handleDetails = () => {
    if (isTeacher) {
      navigate(`/activities/${assignment.id}/details`);
      return;
    }

    navigate(`/activities/${assignment.id}/submission`);
  };

  return (
    <div className="flex items-center rounded-xl border border-border bg-menu px-4 py-3">
      <ListItemField
        label="Assignment"
        value={assignment.activityName}
        className="flex-2"
      />

      <ListItemField
        label="Course"
        value={assignment.courseName}
        className="flex-2"
      />

      <ListItemField
        label="Module"
        value={assignment.moduleName}
        className="flex-2"
      />

      <ListItemField
        label="End date"
        value={new Date(assignment.endDate).toLocaleDateString()}
        className="flex-1"
      />

      <div className="flex items-center gap-3">
        <Button
          children="DETAILS"
          variant="list"
          color="edit"
          onClick={handleDetails}
        />
      </div>
    </div>
  );
}
