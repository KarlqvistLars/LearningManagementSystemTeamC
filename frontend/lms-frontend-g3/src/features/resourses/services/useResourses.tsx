import { useEffect, useState } from "react";
import { getResourcesByActivity } from "../api";
import type { ResourceWithCreatorDto } from "../types/interfaces";

export function useResources(activityId: string | undefined) {
  const [resources, setResources] = useState<ResourceWithCreatorDto[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    if (!activityId) {
      setResources([]);
      return;
    }

    const loadResources = async () => {
      setLoading(true);
      setError(null);

      try {
        const resources = await getResourcesByActivity(activityId);
        setResources(resources);
      } catch (error) {
        setError(
          error instanceof Error ? error.message : "Could not load resources.",
        );
      } finally {
        setLoading(false);
      }
    };

    void loadResources();
  }, [activityId]);

  return {
    resources,
    loading,
    error,
  };
}
