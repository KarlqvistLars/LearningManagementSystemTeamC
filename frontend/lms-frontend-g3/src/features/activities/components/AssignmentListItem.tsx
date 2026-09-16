import { useEffect, useState } from "react";
import type { ActivityDetailsDto } from "../types";
import { ListItemField } from "../../../shared/components/ListItemField";
import { Button } from "../../../shared/components/Button";
import { Tag } from "../../../shared/components/Tag";
import { useNavigate } from "react-router";
import { useAuth } from "../../auth/AuthContext";
import { getMySubmission } from "../../resourses/api/resources";
import type { StudentSubmissionDto } from "../../resourses/types/interfaces";

interface AssignmentListItemProps {
  assignment: ActivityDetailsDto;
}

export function AssignmentListItem({ assignment }: AssignmentListItemProps) {
  const navigate = useNavigate();
  const { isTeacher } = useAuth();

  const [submission, setSubmission] = useState<StudentSubmissionDto | null>(
    null,
  );

  const [isLoadingSubmission, setIsLoadingSubmission] = useState(false);

  useEffect(() => {
    if (isTeacher) {
      return;
    }

    const loadSubmission = async () => {
      setIsLoadingSubmission(true);

      try {
        const result = await getMySubmission(assignment.id);
        setSubmission(result);
      } catch (error) {
        console.error(error);
      } finally {
        setIsLoadingSubmission(false);
      }
    };

    void loadSubmission();
  }, [assignment.id, isTeacher]);

  const isSubmitted = submission !== null;

  const isLate =
    isSubmitted &&
    submission.createdAt !== null &&
    new Date(submission.createdAt) > new Date(assignment.endDate);

  const handleDetails = () => {
    if (isTeacher) {
      navigate(`/activities/${assignment.id}/submissions`);
      return;
    }

    if (submission) {
      navigate(`/activities/${assignment.id}/submission/${submission.id}/edit`);
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
        label="Submitted"
        value={`${assignment.submittedCount ?? 0} of ${assignment.totalStudents ?? 0}`}
        className="flex-1"
      />

      <ListItemField
        label="End date"
        value={new Date(assignment.endDate).toLocaleDateString()}
        className="flex-1"
      />

      {!isTeacher && (
        <div className="flex flex-1 items-center">
          {isLoadingSubmission && (
            <span className="text-sm text-primary-display-text">
              Loading...
            </span>
          )}

          {!isLoadingSubmission && !submission && (
            <Tag title="Status" label="Not submitted" variant="Not-submitted" />
          )}

          {!isLoadingSubmission && submission && !isLate && (
            <Tag title="Status" label="On time" variant="On-time" />
          )}

          {!isLoadingSubmission && submission && isLate && (
            <Tag title="Status" label="Late" variant="Late" />
          )}
        </div>
      )}

      <div className="flex items-center gap-3">
        {isTeacher ? (
          <Button variant="list" color="edit" onClick={handleDetails}>
            DETAILS
          </Button>
        ) : (
          <Button
            variant="list"
            color={submission ? "edit" : "create"}
            disabled={isLoadingSubmission}
            onClick={handleDetails}
          >
            {submission ? "EDIT" : "SUBMIT"}
          </Button>
        )}
      </div>
    </div>
  );
}
