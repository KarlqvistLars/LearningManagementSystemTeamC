import type { Course } from "../types";
import { CourseSummaryCard } from "./courseSummaryCard";
import { useState, Suspense, useEffect } from "react";
import { fetchCourses } from "../api/courses";

export function CourseList() {
  const [courses, setCourses] = useState<Course[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    async function loadCourses() {
      try {
        const coursesFetched = await fetchCourses();
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

  async function handleDeleteCourse(id: string | null) {
    if (id !== null) {
      try {
        //Delete api call

        //if (course was deleted successfully) {
        if (id !== null) {
          alert("Course deleted successfully.");
          //Handle successful deletion
        }
      } catch (error) {
        console.error(error);
        //Handle deletion error
      }
    }
  }

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
              onDelete={handleDeleteCourse}
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
