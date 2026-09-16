import { useEffect, useState } from "react";
import { getActivitiesByModule } from "../api";
import type { ActivityDto } from "../types";

export function useActivities(moduleId: string | undefined) {
    const [activities, setActivities] = useState<ActivityDto[]>([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);
    const [reloadKey, setReloadKey] = useState(0);

    useEffect(() => {
        if (!moduleId) return;

        setLoading(true);
        setError(null);

        getActivitiesByModule(moduleId)
            .then(setActivities)
            .catch((err: Error) => setError(err.message))
            .finally(() => setLoading(false));
    }, [moduleId, reloadKey]);

    return { activities, loading, error, refetch: () => setReloadKey((k) => k + 1) };
}