import { DisplayText } from "../../../shared/components/DisplayText";
import type { Course } from "../types";
import { CourseSummaryCard } from "./courseSummaryCard";

interface CourseListProps {
  courses: Course[];
}

export function CourseList({ courses }: CourseListProps) {
  return (
    <div className="flex flex-col gap-2">
      {courses.length > 0 ? (
        courses.map((course) => (
          <CourseSummaryCard key={course.id} course={course} />
        ))
      ) : (
        <DisplayText text="No courses found." />
      )}
    </div>
  );
}
