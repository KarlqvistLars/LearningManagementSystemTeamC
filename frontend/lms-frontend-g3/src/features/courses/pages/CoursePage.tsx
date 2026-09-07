import { CourseList } from "../components/courseList";

export function CoursePage() {
  return (
    <section className="min-h-screen bg-slate-100 px-6 py-20">
      <div className="mx-auto max-w-5xl">
        <h1 className="mb-6 text-4xl font-bold text-slate-600">Kurser</h1>
        <CourseList />
      </div>
    </section>
  );
}
