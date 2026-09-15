import { useNavigate } from "react-router";
import { useState, useEffect } from "react";
import type { CourseDto } from "../types";
import { useAuth } from "../../auth/AuthContext";
import { createCourse } from "../api/courses";
import { FormTitle } from "../../../shared/components/FormTitle";
import { createPortal } from "react-dom";
import { ResponseMessage } from "../../../shared/components/ResponseMessage";
import { CourseForm, type CourseFormData } from "../components/courseForm";

export function CourseCreatePage() {
  const { isTeacher } = useAuth();
  const navigate = useNavigate();
  const [message, setMessage] = useState("");
  const [messageType, setMessageType] = useState<
    "message" | "error" | "success"
  >("message");

  useEffect(() => {
    if (!isTeacher) {
      navigate(`/courses`);
    }
  }, [isTeacher, navigate]);

  const handleSubmit = async (data: CourseFormData) => {
    try {
      const create: CourseDto = {
        courseName: data.name,
        description: data.description,
        startDate: data.startDate,
        endDate: data.endDate,
      };

      const createdCourse = await createCourse(create);
      setMessage("Course created successfully.");
      setMessageType("success");

      setTimeout(() => {
        navigate(`/courses/${createdCourse.id}`);
      }, 3000);
    } catch (error) {
      console.error(error);
      setMessage("Couldn't create course.");
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
          <FormTitle title="Create Course" />
          <CourseForm
            values={{
              name: "",
              description: "",
              startDate: "",
              endDate: "",
            }}
            onSubmit={handleSubmit}
            submitLabel="Create"
          />
        </div>
      </section>
    </>
  );
}
