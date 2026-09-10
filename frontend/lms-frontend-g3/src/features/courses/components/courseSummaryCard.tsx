import type { Course } from "../types";
import { Link } from "react-router";
import type { User } from "../../users/types";
import ROLES from "../../auth/roleConstants";

const user: User | null = JSON.parse(localStorage.getItem("user") || "null");
const role = user?.roleName;
const isTeacher = role === ROLES.TEACHER;

interface CourseSummaryCardProps {
  course: Course;
  onEdit?: (id: string) => void;
}

const options = {
  weekday: "long",
  year: "numeric",
  month: "long",
  day: "numeric",
};

export function CourseSummaryCard({ course, onEdit }: CourseSummaryCardProps) {
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
