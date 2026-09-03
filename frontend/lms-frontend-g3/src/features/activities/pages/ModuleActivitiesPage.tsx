import { useParams } from "react-router";
import { useActivities } from "../hooks/useActivities";
import { ActivityList } from "../components/ActivityList";

export function ModuleActivitiesPage() {
    const { moduleId } = useParams();
    const { activities, loading, error } = useActivities(moduleId);

    return (
        <section className="min-h-screen bg-slate-100 px-6 py-20">
            <div className="mx-auto max-w-5xl">
                <h1 className="mb-6 text-4xl font-bold">Module Activities</h1>

                {loading && <p className="text-slate-500">Loading...</p>}
                {error && <p className="text-red-500">{error}</p>}

                {!loading && !error && <ActivityList activities={activities} />}
            </div>
        </section>
    );
}
