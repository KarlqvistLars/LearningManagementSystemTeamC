import { useParams } from "react-router";
import { useModules } from "../hooks/useModules";
import { ModuleList } from "../components/ModuleList";

export function CourseModulesPage() {
    const { courseId } = useParams();
    const { modules, loading, error } = useModules(courseId);

    return (
        <section className="min-h-screen bg-slate-100 px-6 py-20">
            <div className="mx-auto max-w-5xl">
                <h1 className="mb-6 text-4xl font-bold">Course Modules</h1>

                {loading && <p className="text-slate-500">Loading...</p>}
                {error && <p className="text-red-500">{error}</p>}

                {!loading && !error && <ModuleList modules={modules} />}
            </div>
        </section>
    );
}