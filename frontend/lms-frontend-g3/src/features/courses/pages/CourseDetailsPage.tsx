import { useNavigate, useParams } from "react-router";
import { useState, useEffect } from "react";
import type { Course } from "../types";
import { useAuth } from "../../auth/AuthContext";
import { fetchCourseById } from "../api/courses";
import { DisplayText } from "../../../shared/components/DisplayText";
import { Button } from "../../../shared/components/Button";

export function CourseDetailsPage() {
  const { isTeacher } = useAuth();
  const { courseId } = useParams<{ courseId: string }>();
  const navigate = useNavigate();
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
          <>
            <div className="grid grid-cols-2 gap-x-10 gap-y-5">
              <div>
                <span className="text-white mb-1 block text-base font-medium block">
                  Name
                </span>
                <span className="text-lg w-full rounded-md bg-form-input px-4 py-3 text-primary-display-text block">
                  {course.courseName}
                </span>
              </div>

              <div className="row-span-2">
                <span className="text-white mb-1 block text-base font-medium block">
                  Mentors
                </span>
                <span className="text-lg w-full rounded-md bg-form-input px-4 py-3 text-primary-display-text block">
                  John Doe
                </span>
              </div>

              <div>
                <span className="text-white mb-1 block text-base font-medium block">
                  Start date
                </span>
                <span className="text-lg w-full rounded-md bg-form-input px-4 py-3 text-primary-display-text block">
                  {new Date(course.startDate).toLocaleDateString()}
                </span>
              </div>

              <div>
                <span className="text-white mb-1 block text-base font-medium block">
                  End date
                </span>
                <span className="text-lg w-full rounded-md bg-form-input px-4 py-3 text-primary-display-text block">
                  {new Date(course.endDate).toLocaleDateString()}
                </span>
              </div>

              <div className="row-span-2">
                <span className="text-white mb-1 block text-base font-medium block">
                  Students
                </span>
                <span className="text-lg w-full rounded-md bg-form-input px-4 py-3 text-primary-display-text block">
                  John Doe
                </span>
              </div>

              <div className="row-span-4">
                <span className="text-white mb-1 block text-base font-medium block">
                  Description
                </span>
                <span className="text-lg w-full rounded-md bg-form-input px-4 py-3 text-primary-display-text block">
                  {course.description}
                </span>
              </div>
            </div>
          </>
        )}
      </div>
      <div className="flex items-center">
        {isTeacher && (
          <Button
            variant="list"
            color="edit"
            onClick={() => navigate(`/courses/${courseId}/edit`)}
          >
            Edit
          </Button>
        )}
      </div>
    </section>
  );
}
