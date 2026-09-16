import { Button } from "../../../shared/components/Button";
import { DisplayText } from "../../../shared/components/DisplayText";
import { ListItemField } from "../../../shared/components/ListItemField";
import type { ActivityDto } from "../types";

const ACTIVITY_TYPES: Record<number, string> = {
  0: "ELearningSession",
  1: "Lecture",
  2: "ExerciseSession",
  3: "Assignment",
};

interface ActivityListProps {
  activities: ActivityDto[];
  onEditActivity?: (activity: ActivityDto) => void;
}

export function ActivityList({
  activities,
  onEditActivity,
}: ActivityListProps) {
  if (activities.length === 0) {
    return <DisplayText text="No activities in this module yet." />;
  }

  return (
    <div className="flex flex-col gap-2">
      {activities.map((activity) => (
        <div
          key={activity.id}
          className="flex items-center rounded-xl border border-border bg-menu px-4 py-3"
        >
          <ListItemField
            label="Name"
            value={activity.activityName}
            className="flex-2"
          />

          <ListItemField
            label="Type"
            value={ACTIVITY_TYPES[activity.type] ?? String(activity.type)}
            className="flex-2"
          />

          <ListItemField
            label="Start date"
            value={new Date(activity.startDate).toLocaleString()}
            className="flex-2"
          />

          <ListItemField
            label="End date"
            value={new Date(activity.endDate).toLocaleString()}
            className="flex-2"
          />

          {onEditActivity && (
            <div className="flex items-center gap-3">
              <Button
                variant="list"
                color="edit"
                onClick={() => onEditActivity(activity)}
              >
                Edit
              </Button>
            </div>
          )}
        </div>
      ))}
    </div>
  );
}