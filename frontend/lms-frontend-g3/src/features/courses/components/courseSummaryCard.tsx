import type { Course } from "../types";
import { Link, useNavigate } from "react-router";
import type { User } from "../../users/types/types";
import ROLES from "../../auth/roleConstants";
import { Button } from "../../../shared/components/Button";

const user: User | null = JSON.parse(localStorage.getItem("user") || "null");
const role = user?.roleName;
const isTeacher = role === ROLES.TEACHER;

interface CourseSummaryCardProps {
  course: Course;
  onEdit?: (id: string) => void;
}

export function CourseSummaryCard({ course, onEdit }: CourseSummaryCardProps) {
  const navigate = useNavigate();

  return (
    <div className="w-full p-7 bg-menu flex gap-4 align-items-start justify-between border border-border rounded-xl">
      {course && (
        <>
          <div className="w-5/6 text-left text-gray-600 flex gap-4">
            <div className="w-2/4">
              <p className="text-xs uppercase text-primary-title-text mb-4">
                Name
              </p>
              <Link to={`/courses/${course.id}`}>
                <p className="text-2xl">{course.courseName}</p>
              </Link>
            </div>
            <div className="w-1/4">
              <p className="text-xs uppercase text-primary-title-text mb-4">
                Start Date
              </p>
              <p className="text-2xl">
                {new Date(course.startDate).toLocaleDateString()}
              </p>
            </div>
            <div className="w-1/4">
              <p className="text-xs uppercase text-primary-title-text mb-4">
                End Date
              </p>
              <p className="text-2xl">
                {new Date(course.endDate).toLocaleDateString()}
              </p>
            </div>
          </div>
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
              Details
            </button>
          )}
        </>
      )}
    </div>
  );
}
