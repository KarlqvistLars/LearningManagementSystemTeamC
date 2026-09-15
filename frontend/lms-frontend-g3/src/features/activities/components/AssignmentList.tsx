import type { ActivityDetailsDto } from "../types";
import { AssignmentListItem } from "./AssignmentListItem";
import { DisplayText } from "../../../shared/components/DisplayText";

interface AssignmentListProps {
  assignments: ActivityDetailsDto[];
}

export function AssignmentList({ assignments }: AssignmentListProps) {
  if (assignments.length === 0) {
    return <DisplayText text="No assignments found." />;
  }

  return (
    <div className="flex flex-col gap-2">
      {assignments.map((assignment) => (
        <AssignmentListItem key={assignment.id} assignment={assignment} />
      ))}
    </div>
  );
}
