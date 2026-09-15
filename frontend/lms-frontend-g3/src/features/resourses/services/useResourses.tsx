import { useEffect, useState } from "react";
import { getResourcesByActivity } from "../api/index";
import type { Resource } from "../types/interfaces";

export function useResources(moduleId: string | undefined) {
    const [activities, setActivities] = useState<Resource[]>([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        if (!moduleId) return;

        setLoading(true);
        setError(null);

        getResourcesByActivity(moduleId)
            .then(setActivities)
            .catch((err: Error) => setError(err.message))
            .finally(() => setLoading(false));
    }, [moduleId]);

    return { activities, loading, error };
}