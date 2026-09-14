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
            {isTeacher && (
              <Button
                variant="list"
                color="resource"
                onClick={() => navigate(`/courses/${course.id}/modules`)}
              >
                Modules
              </Button>
            )}
          </div>
<<<<<<< HEAD
          {isTeacher && (
            <button
              className="w-1/6 max-w-25 h-fit px-4 py-3 bg-button-edit text-button-edit-text rounded text-sm uppercase font-bold text-trim hover:cursor-pointer"
              onClick={() => onEdit?.(course.id)}
            >
              Edit
            </button>
          )}
          {isTeacher && (
            <button
              className="w-1/6 max-w-25 h-fit px-4 py-2 bg-blue-500 text-white rounded hover:bg-blue-700 hover:cursor-pointer"
              onClick={() => navigate(`/courses/${course.id}/modules`)}
            >
              Modules
            </button>
          )}
=======
>>>>>>> 8dd50a1163d8c2d210c8a19db05a6a75afb09c44
        </>
      )}
    </div>
  );
}
