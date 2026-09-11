import type { Course } from "../types";
import { CourseSummaryCard } from "./courseSummaryCard";
import { useState, Suspense, useEffect } from "react";
import { fetchCourses, fetchCoursesByStudent } from "../api/courses";
import type { User } from "../../users/types/types";
import ROLES from "../../auth/roleConstants";

const user: User | null = JSON.parse(localStorage.getItem("user") || "null");
const role = user?.roleName;
const isTeacher = role === ROLES.TEACHER;

export function CourseList() {
  const [courses, setCourses] = useState<Course[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    async function loadCourses() {
      try {
        const coursesFetched = isTeacher
          ? await fetchCourses()
          : await fetchCoursesByStudent(user?.id || "");
        setCourses(coursesFetched);
      } catch (error) {
        console.error(error);
      } finally {
        setLoading(false);
      }
    }

    if (loading) {
      loadCourses();
    }
  }, [loading]);

  async function handleEditCourse(id: string | null) {
    if (id !== null) {
      try {
        //Put api call

        //if (course was edited successfully) {
        if (id !== null) {
          alert("Course edited successfully.");
          //Handle successful edit
        }
      } catch (error) {
        console.error(error);
        //Handle edit error
      }
    }
  }

  return (
    <div className="grid gap-4 grid-cols-1">
      <Suspense fallback={<p>Laddar kurser...</p>}>
        {courses.length > 0 ? (
          courses.map((course) => (
            <CourseSummaryCard
              key={course.id}
              course={course}
              onEdit={handleEditCourse}
            />
          ))
        ) : (
          <p className="text-gray-600">Det finns inga kurser att visa.</p>
        )}
      </Suspense>
    </div>
  );
}
