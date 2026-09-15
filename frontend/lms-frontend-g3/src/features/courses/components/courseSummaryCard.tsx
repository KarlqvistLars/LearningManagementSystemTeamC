import type { Course } from "../types";
import { useNavigate } from "react-router";
import { useAuth } from "../../auth/AuthContext";
import { Button } from "../../../shared/components/Button";
import { ListItemField } from "../../../shared/components/ListItemField";

interface CourseSummaryCardProps {
  course: Course;
}

export function CourseSummaryCard({ course }: CourseSummaryCardProps) {
  const navigate = useNavigate();
  const { isTeacher } = useAuth();

  return (
    <div className="flex items-center rounded-xl border border-border bg-menu px-4 py-3">
      {course && (
        <>
          <ListItemField
            label="Name"
            value={course.courseName}
            className="flex-2"
            link={`/courses/${course.id}`}
          />

          <ListItemField
            label="Start date"
            value={new Date(course.startDate).toLocaleDateString()}
            className="flex-2"
          />

          <ListItemField
            label="End date"
            value={new Date(course.endDate).toLocaleDateString()}
            className="flex-2"
          />

          <div className="flex items-center gap-3">
            {isTeacher && (
              <Button
                variant="list"
                color="edit"
                onClick={() => navigate(`/courses/${course.id}/edit`)}
              >
                Edit
              </Button>
            )}
              <Button
                variant="list"
                color="resource"
                onClick={() => navigate(`/courses/${course.id}/modules`)}
              >
                Modules
              </Button>
          </div>
        </>
      )}
    </div>
  );
}
