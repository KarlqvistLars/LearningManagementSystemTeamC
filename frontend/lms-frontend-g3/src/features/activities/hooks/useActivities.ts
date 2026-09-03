import { useEffect, useState } from "react";
import { getActivitiesByModule } from "../api";
import type { ActivityDto } from "../types";

export function useActivities(moduleId: string | undefined) {
    const [activities, setActivities] = useState<ActivityDto[]>([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        if (!moduleId) return;

        setLoading(true);
        setError(null);

        getActivitiesByModule(moduleId)
            .then(setActivities)
            .catch((err: Error) => setError(err.message))
            .finally(() => setLoading(false));
    }, [moduleId]);

    return { activities, loading, error };
}
