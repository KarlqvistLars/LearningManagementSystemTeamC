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
import { FormLabel } from "../../../shared/components/FormLabel";
import { FormInput } from "../../../shared/components/FormInput";
import { Button } from "../../../shared/components/Button";
import { createPortal } from "react-dom";
import { ResponseMessage } from "../../../shared/components/ResponseMessage";
import type { UserSimplified } from "../../users/types/types";

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
  const [mentors, setMentors] = useState<string[]>([]);
  const [studentsSelected, setStudentsSelected] = useState<string[]>(students);
  const [mentorsSelected, setMentorsSelected] = useState<string[]>(mentors);

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

  function formateDateForInput(date: string | Date): string {
    return new Date(date).toISOString().split("T")[0];
  }

  async function enrollStudents(studentIds: string[]): Promise<void> {
    try {
      if (!courseId) {
        throw new Error("Course ID is required");
      }

      studentIds.forEach(async (studentId) => {
        await enrollUserInCourse(courseId, studentId);
      });

      setStudents((prevStudents) => [...prevStudents, ...studentIds]);
    } catch (error) {
      console.error(error);
    }
  }

  const handleSubmit = async (event: React.SubmitEvent<HTMLFormElement>) => {
    event.preventDefault();

    try {
      if (course) {
        const edit: Course = {
          id: course.id,
          courseName: course.courseName,
          description: course.description,
          startDate: course.startDate,
          endDate: course.endDate,
          createdAt: course.createdAt,
        };

        await editCourse(edit);
        enrollStudents(studentsSelected);
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
            <form onSubmit={handleSubmit} className="flex flex-1 flex-col">
              <div className="grid grid-cols-2 gap-x-10 gap-y-5">
                <div>
                  <FormLabel htmlFor="courseName" className="text-white">
                    Course name
                  </FormLabel>

                  <FormInput
                    id="courseName"
                    name="courseName"
                    value={course.courseName}
                    required
                    onChange={(event) =>
                      setCourse({ ...course, courseName: event.target.value })
                    }
                  />
                </div>
                <div className="row-span-2">
                  <FormLabel htmlFor="mentor" className="text-white">
                    Mentor
                  </FormLabel>

                  <select
                    id="mentor"
                    name="mentor"
                    multiple
                    value={mentorsSelected}
                    onChange={(event) =>
                      setMentorsSelected(
                        Array.from(
                          event.target.selectedOptions,
                          (option) => option.value,
                        ),
                      )
                    }
                    className="w-full rounded-md bg-form-input px-4 py-3 text-primary-display-text"
                  >
                    {mentorsAll.map((mentor) => (
                      <option key={mentor.id} value={mentor.id}>
                        {`${mentor.firstName} ${mentor.lastName}`}
                      </option>
                    ))}
                  </select>
                </div>

                <div>
                  <FormLabel htmlFor="startDate" className="text-white">
                    Start date
                  </FormLabel>

                  <FormInput
                    id="startDate"
                    name="startDate"
                    type="date"
                    value={formateDateForInput(course.startDate)}
                    required
                    onChange={(event) =>
                      setCourse({
                        ...course,
                        startDate: new Date(event.target.value),
                      })
                    }
                  />
                </div>

                <div>
                  <FormLabel htmlFor="endDate" className="text-white">
                    End date
                  </FormLabel>

                  <FormInput
                    id="endDate"
                    name="endDate"
                    type="date"
                    value={formateDateForInput(course.endDate)}
                    required
                    onChange={(event) =>
                      setCourse({
                        ...course,
                        endDate: new Date(event.target.value),
                      })
                    }
                  />
                </div>

                <div className="row-span-4">
                  <FormLabel htmlFor="students" className="text-white">
                    Students
                  </FormLabel>

                  <select
                    id="students"
                    name="students"
                    multiple
                    value={studentsSelected}
                    onChange={(event) =>
                      setStudentsSelected(
                        Array.from(
                          event.target.selectedOptions,
                          (option) => option.value,
                        ),
                      )
                    }
                    className="w-full rounded-md bg-form-input px-4 py-3 text-primary-display-text"
                  >
                    {studentsAll.map((student) => (
                      <option key={student.id} value={student.id}>
                        {`${student.firstName} ${student.lastName}`}
                      </option>
                    ))}
                  </select>

                  <div className="mt-6">
                    <span className="text-white mb-1 block text-base font-medium">
                      Number of students
                    </span>
                    <span className="text-3xl text-primary-display-text">
                      {students.length.toString()}
                    </span>
                  </div>
                </div>

                <div className="row-span-2">
                  <FormLabel htmlFor="description" className="text-white">
                    Description
                  </FormLabel>

                  <textarea
                    id="description"
                    value={course.description}
                    onChange={(event) =>
                      setCourse({ ...course, description: event.target.value })
                    }
                    required
                    rows={5}
                    placeholder="Enter module description"
                    className="w-full resize-none rounded-md bg-form-input px-4 py-3 text-primary-display-text"
                  ></textarea>
                </div>
              </div>

              <div className="mt-auto flex justify-center gap-4 pt-10">
                <Button type="submit" variant="list" color="edit">
                  Save
                </Button>
              </div>
            </form>
          )}
        </div>
      </section>
    </>
  );
}
