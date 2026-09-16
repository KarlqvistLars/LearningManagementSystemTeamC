import { useNavigate } from "react-router";
import type { AssignmentSubmissionDto } from "../types";
import { ListItemField } from "../../../shared/components/ListItemField";
import { Button } from "../../../shared/components/Button";
import { Tag } from "../../../shared/components/Tag";

interface SubmissionListItemProps {
  submission: AssignmentSubmissionDto;
}

export function SubmissionListItem({ submission }: SubmissionListItemProps) {
  const navigate = useNavigate();

  const isSubmitted = submission.submissionId !== null;

  const isLate =
    isSubmitted &&
    submission.submittedAt !== null &&
    new Date(submission.submittedAt) > new Date(submission.endDate);

  const handleDetails = () => {
    if (!submission.submissionId) {
      return;
    }

    navigate(
      `/activities/${submission.activityId}/submission/${submission.submissionId}/edit`,
    );
  };

  return (
    <div className="flex items-center rounded-xl border border-border bg-menu px-4 py-3">
      <ListItemField
        label="Student"
        value={`${submission.studentFirstName} ${submission.studentLastName}`}
        className="flex-2"
      />

      <ListItemField
        label="Assignment"
        value={submission.activityName}
        className="flex-2"
      />

      <ListItemField
        label="Submitted"
        value={
          submission.submittedAt
            ? new Date(submission.submittedAt).toLocaleDateString()
            : "-"
        }
        className="flex-1"
      />

      <div className="flex flex-1 items-center">
        {!isSubmitted && (
          <Tag title="Status" label="Not submitted" variant="Not-submitted" />
        )}

        {isSubmitted && !isLate && (
          <Tag title="Status" label="On time" variant="On-time" />
        )}

        {isSubmitted && isLate && (
          <Tag title="Status" label="Late" variant="Late" />
        )}
      </div>

      <div className="flex items-center gap-3">
        <Button
          variant="list"
          color="edit"
          disabled={!submission.submissionId}
          onClick={handleDetails}
        >
          DETAILS
        </Button>
      </div>
    </div>
  );
}
