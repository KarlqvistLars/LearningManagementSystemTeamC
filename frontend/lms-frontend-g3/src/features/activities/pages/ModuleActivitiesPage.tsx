import { useState } from "react";
import { useParams } from "react-router";
import { useActivities } from "../hooks/useActivities";
import { ActivityList } from "../components/ActivityList";
import { ActivityForm } from "../components/ActivityForm";
import { useAuth } from "../../auth/AuthContext";
import type { ActivityDto } from "../types";

export function ModuleActivitiesPage() {
    const { moduleId } = useParams();
    const { activities, loading, error, refetch } = useActivities(moduleId);
    const { isTeacher } = useAuth();

    const [showForm, setShowForm] = useState(false);
    const [editingActivity, setEditingActivity] = useState<ActivityDto | null>(null);

    return (
        <section className="min-h-screen bg-slate-100 px-6 py-20">
            <div className="mx-auto max-w-5xl">
                <h1 className="mb-6 text-4xl font-bold">Module Activities</h1>

                {loading && <p className="text-slate-500">Loading...</p>}
                {error && <p className="text-red-500">{error}</p>}

                {!loading && !error && (
                    <ActivityList
                        activities={activities}
                        onEditActivity={
                            isTeacher
                                ? (activity) => {
                                      setEditingActivity(activity);
                                      setShowForm(true);
                                  }
                                : undefined
                        }
                    />
                )}

                {isTeacher && !showForm && (
                    <button
                        onClick={() => {
                            setEditingActivity(null);
                            setShowForm(true);
                        }}
                        className="mt-6 px-4 py-2 bg-blue-500 text-white rounded hover:bg-blue-700 hover:cursor-pointer"
                    >
                        Create activity
                    </button>
                )}

                {isTeacher && showForm && (
                    <button
                        onClick={() => {
                            setEditingActivity(null);
                            setShowForm(false);
                        }}
                        className="mt-6 px-4 py-2 bg-red-500 text-white rounded hover:bg-red-700 hover:cursor-pointer"
                    >
                        Cancel
                    </button>
                )}

                {showForm && (
                    <ActivityForm
                        moduleId={moduleId ?? ""}
                        activity={editingActivity ?? undefined}
                        onSave={() => {
                            setShowForm(false);
                            setEditingActivity(null);
                            refetch();
                        }}
                    />
                )}
            </div>
        </section>
    );
}