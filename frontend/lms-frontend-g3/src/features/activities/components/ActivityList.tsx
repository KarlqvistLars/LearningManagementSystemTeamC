import type { ActivityDto } from "../types";

const ACTIVITY_TYPES = {
    0: "ELearningSession",
    1: "Lecture",
    2: "ExerciseSession",
    3: "Assignment",
} as const;

export function ActivityList({ activities }: { activities: ActivityDto[] }) {
    if (activities.length === 0) {
        return (
            <div className="rounded-lg border border-dashed border-slate-300 p-8 text-center text-slate-500">
                No activities in this module yet.
            </div>
        );
    }

    return (
        <ul className="space-y-4">
            {activities.map((activity) => (
                <li key={activity.id} className="rounded-lg border border-slate-200 bg-white p-4 shadow-sm">
                    <div className="flex items-center justify-between">
                        <h3 className="text-lg font-semibold">{activity.activityName}</h3>
                        <span className="rounded-full bg-slate-100 px-3 py-1 text-sm text-slate-600">
                            {ACTIVITY_TYPES[activity.type as keyof typeof ACTIVITY_TYPES] ?? activity.type}
                        </span>
                    </div>
                    <p className="mt-2 text-sm text-slate-600">{activity.description}</p>
                    <p className="mt-2 text-xs text-slate-400">
                        {new Date(activity.startDate).toLocaleString()} – {new Date(activity.endDate).toLocaleString()}
                    </p>
                </li>
            ))}
        </ul>
    );
}
