import type { Course } from "../types";
import { Link } from "react-router";
import type { User } from "../../users/types/types";
import ROLES from "../../auth/roleConstants";

const user: User | null = JSON.parse(localStorage.getItem("user") || "null");
const role = user?.roleName;
const isTeacher = role === ROLES.TEACHER;

interface CourseSummaryCardProps {
  course: Course;
  onEdit?: (id: string) => void;
}

export function CourseSummaryCard({ course, onEdit }: CourseSummaryCardProps) {
  return (
    <div className="w-full p-4 bg-gray-200 flex gap-4 align-items-start justify-between">
      {course && (
        <>
          <div className="w-5/6 text-left text-gray-600 flex gap-4">
            <div className="w-2/4">
              <p className="text-sm uppercase">Name</p>
              <Link to={`/courses/${course.id}/modules`}>
                <p className="text-lg">{course.courseName}</p>
              </Link>
            </div>
            <div className="w-1/4">
              <p className="text-sm uppercase">Start Date</p>
              <p className="text-lg">
                {new Date(course.startDate).toDateString()}
              </p>
            </div>
            <div className="w-1/4">
              <p className="text-sm uppercase">End Date</p>
              <p className="text-lg">
                {new Date(course.endDate).toDateString()}
              </p>
            </div>
          </div>
          {isTeacher && (
            <button
              className="w-1/6 max-w-25 h-fit px-4 py-2 bg-blue-500 text-white rounded hover:bg-blue-700 hover:cursor-pointer"
              onClick={() => onEdit?.(course.id)}
            >
              Edit
            </button>
          )}
        </>
      )}
    </div>
  );
}
