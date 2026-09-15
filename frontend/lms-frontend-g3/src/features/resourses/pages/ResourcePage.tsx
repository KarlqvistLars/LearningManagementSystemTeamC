import { useParams } from "react-router";
import { ResourceForm } from "../components/resourceForm";

export function ResourcePage() {
    const { moduleId } = useParams();
    // const { activities, loading, error } = useResources(moduleId);

    return (
        <section className="min-h-screen bg-background px-6 py-20">
            <div className="mx-auto max-w-5xl">
                <h1 className="mb-6 text-primary text-4xl font-bold">Resources</h1>
                <ResourceForm courseId={moduleId!} onResourceSaved={() => { }} />
            </div>
        </section>
    );
}
