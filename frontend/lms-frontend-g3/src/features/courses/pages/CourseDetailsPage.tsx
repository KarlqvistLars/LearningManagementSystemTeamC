import { useParams } from "react-router";
import { useState, useEffect } from "react";
import type { Course } from "../types";
import { useAuth } from "../../auth/AuthContext";

export function CourseDetailsPage() {
  const { isTeacher } = useAuth();
  const { courseId } = useParams();
  // const [course, setCourse] = useState<Course | null>(null);
  // const [loading, setLoading] = useState(true);

  // useEffect(() => {
  //   async function loadCourse() {
  //     try {
  //       const courseFetched = await fetchCourseById(courseId);
  //       setCourse(courseFetched);
  //     } catch (error) {
  //       console.error(error);
  //     } finally {
  //       setLoading(false);
  //     }
  //   }

  //   if (loading) {
  //     loadCourse();
  //   }
  // }, [loading]);

  return (
    <div className="text-white">
      <h1>Course Details</h1>
      <p>Course ID: {courseId}</p>
    </div>
  );
}
