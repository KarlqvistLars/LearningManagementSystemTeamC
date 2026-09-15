import { CourseList } from "../components/courseList";
import { useAuth } from "../../auth/AuthContext";
import { Suspense } from "react";
import { SearchInput } from "../../../shared/components/SearchInput";
import { DisplayText } from "../../../shared/components/DisplayText";
import { Button } from "../../../shared/components/Button";
import { useState, useEffect } from "react";
import { fetchCourses, fetchCoursesByStudent } from "../api/courses";
import type { Course } from "../types";

export function CoursesPage() {
  const { user, isTeacher } = useAuth();
  const [searchTerm, setSearchTerm] = useState("");
  const search = searchTerm.toLowerCase();
  const [courses, setCourses] = useState<Course[]>([]);
  const [loading, setLoading] = useState(true);

  const filteredCourses = courses.filter((course) => {
    return (
      course.courseName.toLowerCase().includes(search) ||
      course.description.toLowerCase().includes(search)
    );
  });

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
  }, [isTeacher, loading, user?.id]);

  return (
    <section className="flex flex-col gap-6 p-6 h-full">
      <h1 className="uppercase">
        <DisplayText text="Courses" />
      </h1>
      <SearchInput
        value={searchTerm}
        onChange={setSearchTerm}
        placeholder="Search courses..."
      />
      <Suspense fallback={<DisplayText text="Loading courses..." />}>
        <CourseList courses={filteredCourses} />
      </Suspense>
      <div className="self-center mt-auto">
        {isTeacher && (
          <Button
            variant="list"
            onClick={() => alert("Create new course clicked")}
          >
            Create new course
          </Button>
        )}
      </div>
    </section>
  );
}
