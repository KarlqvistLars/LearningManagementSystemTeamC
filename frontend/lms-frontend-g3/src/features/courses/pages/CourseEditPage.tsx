import { useNavigate, useParams } from "react-router";
import { useState, useEffect } from "react";
import type { Course } from "../types";
import { useAuth } from "../../auth/AuthContext";
import {
  fetchCourseById,
  editCourse,
  fetchEnrollmentsByCourse,
  enrollUserInCourse,
} from "../api/courses";
import { fetchActiveUsersByRole } from "../../users/api/userApi";
import { DisplayText } from "../../../shared/components/DisplayText";
import { FormTitle } from "../../../shared/components/FormTitle";
import { createPortal } from "react-dom";
import { ResponseMessage } from "../../../shared/components/ResponseMessage";
import type { UserSimplified } from "../../users/types/types";
import { CourseForm, type CourseFormData } from "../components/courseForm";

export function CourseEditPage() {
  const { isTeacher } = useAuth();
  const { courseId } = useParams<{ courseId: string }>();
  const navigate = useNavigate();
  const [course, setCourse] = useState<Course | null>(null);
  const [loading, setLoading] = useState(true);
  const [message, setMessage] = useState("");
  const [messageType, setMessageType] = useState<
    "message" | "error" | "success"
  >("message");
  const [studentsAll, setStudentsAll] = useState<UserSimplified[]>([]);
  const [mentorsAll, setMentorsAll] = useState<UserSimplified[]>([]);
  const [students, setStudents] = useState<string[]>([]);
  const [mentors] = useState<string[]>([]);

  useEffect(() => {
    if (!isTeacher) {
      navigate(`/courses/${courseId}`);
    }
  }, [isTeacher, navigate, courseId]);

  useEffect(() => {
    async function loadCourse(): Promise<void> {
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

    async function loadEnrolledStudents(): Promise<void> {
      try {
        if (!courseId) {
          throw new Error("Course ID is required");
        }
        const studentsEnrolled = await fetchEnrollmentsByCourse(courseId);
        setStudents(studentsEnrolled.map((student) => student.studentId));
      } catch (error) {
        console.error(error);
      }
    }

    async function loadAllTeachers(): Promise<void> {
      try {
        if (!courseId) {
          throw new Error("Course ID is required");
        }
        const teachersFetched = await fetchActiveUsersByRole("teacher");
        setMentorsAll(teachersFetched);
      } catch (error) {
        console.error(error);
      }
    }

    async function loadAllStudents(): Promise<void> {
      try {
        if (!courseId) {
          throw new Error("Course ID is required");
        }
        const studentsFetched = await fetchActiveUsersByRole("student");
        setStudentsAll(studentsFetched);
      } catch (error) {
        console.error(error);
      }
    }

    if (loading) {
      loadCourse();
      loadEnrolledStudents();
      loadAllTeachers();
      loadAllStudents();
    }
  }, [loading, courseId]);

  function enrollStudents(studentIds: string[]): void {
    try {
      if (!courseId) {
        throw new Error("Course ID is required");
      }

      studentIds.forEach(async (studentId) => {
        if (!students.includes(studentId)) {
          await enrollUserInCourse(courseId, studentId);
          setStudents((prevStudents) => [...prevStudents, studentId]);
        }
      });
    } catch (error) {
      console.error(error);
    }
  }

  const handleSubmit = async (data: CourseFormData) => {
    try {
      if (course) {
        const edit: Course = {
          id: course.id,
          courseName: data.name,
          description: data.description,
          startDate: data.startDate,
          endDate: data.endDate,
          createdAt: course.createdAt,
        };

        await editCourse(edit);
        if (data.students && data.students.length > 0) {
          enrollStudents(data.students);
        }
        setLoading(true);
        setMessage("Course updated successfully.");
        setMessageType("success");
      }
    } catch (error) {
      console.error(error);
      setMessage("Couldn't update course.");
      setMessageType("error");
    }
  };

  return (
    <>
      {message &&
        createPortal(
          <ResponseMessage
            message={message}
            type={messageType}
            duration={3000}
            onClose={() => setMessage("")}
          />,
          document.body,
        )}
      <section className="flex h-full flex-col gap-6 p-6">
        <div className="flex flex-1 flex-col gap-8 rounded-lg border border-border bg-menu px-10 py-10">
          <FormTitle title="Edit Course" />
          {loading && <DisplayText text="Loading course details..." />}
          {course && (
            <>
              <CourseForm
                values={{
                  name: course.courseName,
                  description: course.description,
                  startDate: course.startDate,
                  endDate: course.endDate,
                  mentors: mentors,
                  students: students,
                }}
                onSubmit={handleSubmit}
                submitLabel="Save"
                mentorsAll={mentorsAll}
                studentsAll={studentsAll}
              />
            </>
          )}
        </div>
      </section>
    </>
  );
}
