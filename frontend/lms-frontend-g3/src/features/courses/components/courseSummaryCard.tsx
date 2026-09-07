import type { Course } from "../types";
import { Link } from "react-router";

interface CourseSummaryCardProps {
  course: Course;
  onDelete?: (id: string) => void;
  onEdit?: (id: string) => void;
}

export function CourseSummaryCard({
  course,
  onDelete,
  onEdit,
}: CourseSummaryCardProps) {
  return (
    <div className="w-full p-4 bg-gray-200 flex gap-4 align-items-start">
      {course && (
        <>
          <div className="grow px-4 text-left text-gray-600">
            <Link to={`/courses/${course.id}`}>
              <h3 className="text-lg font-bold">{course.courseName}</h3>
            </Link>
            <p className="text-sm">
              {new Date(course.startDate).toDateString()} -{" "}
              {new Date(course.endDate).toDateString()}
            </p>
            <p className="text-md">{course.description}</p>
          </div>
          <button
            className="w-1/6 max-w-25 h-fit px-4 py-2 bg-blue-500 text-white rounded hover:bg-blue-700 hover:cursor-pointer"
            onClick={() => onEdit?.(course.id)}
          >
            Edit
          </button>
          <button
            className="w-1/6 max-w-25 h-fit px-4 py-2 bg-red-500 text-white rounded hover:bg-red-700 hover:cursor-pointer"
            onClick={() => onDelete?.(course.id)}
          >
            Delete
          </button>
        </>
      )}
    </div>
  );
}
