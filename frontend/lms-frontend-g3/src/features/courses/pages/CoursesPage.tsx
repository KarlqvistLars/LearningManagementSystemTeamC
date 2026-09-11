import { CourseList } from "../components/courseList";
import type { User } from "../../users/types/types";
import ROLES from "../../auth/roleConstants";
import { DisplayText } from "../../../shared/components/DisplayText";

const user: User | null = JSON.parse(localStorage.getItem("user") || "null");
const role = user?.roleName;
const isTeacher = role === ROLES.TEACHER;

export function CoursesPage() {
  return (
    <section className="min-h-screen bg-background px-6 py-20">
      <div className="mx-auto max-w-5xl">
        <h1 className="mb-6 uppercase">
          <DisplayText text="Courses" />
        </h1>
        <CourseList />
      </div>
      <div className="flex justify-center">
        {isTeacher && (
          <button className="mt-16 px-6 py-5 text-trim bg-button-create text-button-create-text text-sm font-bold rounded hover:cursor-pointer">
            Create new course
          </button>
        )}
      </div>
    </section>
  );
}
