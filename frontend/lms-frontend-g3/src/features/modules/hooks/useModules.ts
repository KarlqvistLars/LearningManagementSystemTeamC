import { useEffect, useState } from "react";
import { fetchModules } from "../api";
import type { Module } from "../types";

export function useModules(courseId: string | undefined) {
    const [modules, setModules] = useState<Module[]>([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        if (!courseId) return;

        setLoading(true);
        setError(null);

        fetchModules(courseId)
            .then(setModules)
            .catch((err: Error) => setError(err.message))
            .finally(() => setLoading(false));
    }, [courseId]);

    return { modules, loading, error };
}