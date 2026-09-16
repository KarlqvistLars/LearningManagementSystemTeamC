import { DisplayText } from "../../../shared/components/DisplayText";
import type { AssignmentSubmissionDto } from "../types";
import { SubmissionListItem } from "./SubmissionListItem";

interface SubmissionListProps {
  submissions: AssignmentSubmissionDto[];
}

export function SubmissionList({ submissions }: SubmissionListProps) {
  if (submissions.length === 0) {
    return <DisplayText text="No students found." />;
  }

  return (
    <div className="flex flex-col gap-2">
      {submissions.map((submission) => (
        <SubmissionListItem
          key={submission.studentId}
          submission={submission}
        />
      ))}
    </div>
  );
}
