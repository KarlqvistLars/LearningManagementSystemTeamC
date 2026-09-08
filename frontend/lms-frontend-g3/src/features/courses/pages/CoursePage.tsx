import { CourseList } from "../components/courseList";
import type { User } from "../../users/types";
import ROLES from "../../auth/roleConstants";

const user: User | null = JSON.parse(localStorage.getItem("user") || "null");
const role = user?.roleName;
const isTeacher = role === ROLES.TEACHER;

export function CoursePage() {
  return (
    <section className="min-h-screen bg-slate-100 px-6 py-20">
      <div className="mx-auto max-w-5xl">
        <h1 className="mb-6 text-4xl font-bold text-slate-600">Kurser</h1>
        <CourseList />
      </div>
      <div>
        {isTeacher && (
          <button className="mt-16 px-4 py-2 bg-blue-500 text-white rounded hover:bg-blue-700 hover:cursor-pointer">
            Create new course
          </button>
        )}
      </div>
    </section>
  );
}
