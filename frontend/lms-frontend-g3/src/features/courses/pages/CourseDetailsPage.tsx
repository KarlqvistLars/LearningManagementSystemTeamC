import { useParams } from "react-router";
import { useState, useEffect } from "react";
import type { Course } from "../types";
import { useAuth } from "../../auth/AuthContext";
import { fetchCourseById } from "../api/courses";
import { DisplayText } from "../../../shared/components/DisplayText";
import { ListItemField } from "../../../shared/components/ListItemField";

export function CourseDetailsPage() {
  const { isTeacher } = useAuth();
  const { courseId } = useParams<{ courseId: string }>();
  const [course, setCourse] = useState<Course | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    async function loadCourse() {
      try {
        if (!courseId) {
          throw new Error("Course ID is required");
        }
        const courseFetched = await fetchCourseById(courseId);
        setCourse(courseFetched);
      } catch (error) {
        console.error(error);
      } finally {
        setLoading(false);
      }
    }

    if (loading) {
      loadCourse();
    }
  }, [loading, courseId]);

  return (
    <section className="flex h-full flex-col gap-6 p-6">
      <div className="flex flex-1 flex-col gap-8 rounded-lg border border-border bg-menu px-10 py-10">
        <h1 className="uppercase">
          <DisplayText text="Course Details" />
        </h1>
        {loading && <DisplayText text="Loading course details..." />}
        {course && (
          // <div className="flex items-center rounded-xl border border-border bg-menu px-4 py-3">
          //   <ListItemField
          //     label="Name"
          //     value={course.courseName}
          //     className="flex-2"
          //   />
          // </div>
          <>
            <div className="grid grid-cols-2 gap-x-10 gap-y-5">
              <ListItemField
                label="Name"
                value={course.courseName}
                className="w-full rounded-md bg-form-input px-4 py-3 text-primary-display-text"
              />

              <ListItemField
                label="Description"
                value={course.description}
                className="w-full rounded-md bg-form-input px-4 py-3 text-primary-display-text"
              />

              <ListItemField
                label="Start Date"
                value={new Date(course.startDate).toLocaleDateString()}
                className="w-full rounded-md bg-form-input px-4 py-3 text-primary-display-text"
              />

              <ListItemField
                label="End Date"
                value={new Date(course.endDate).toLocaleDateString()}
                className="w-full rounded-md bg-form-input px-4 py-3 text-primary-display-text"
              />
            </div>
          </>
        )}
      </div>
    </section>
  );
}
