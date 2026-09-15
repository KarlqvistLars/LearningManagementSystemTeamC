import { useEffect, useState } from "react";
import type { ApiError } from "../../../api/types";
import { ApiRequestError } from "../../../api/error";
import type { ActivityDetailsDto } from "../types";

export function AssignmentPage() {
  const [assignments, setAssignments] = useState<ActivityDetailsDto[] | []>();
  const [error, setError] = useState<ApiError | undefined>();

  useEffect(() => {
    const getAssignments = async () => {
      try {
        const assignments = await getAssignments();
        setAssignments(assignments);
      } catch (error) {
        if (error instanceof ApiRequestError) {
          setError(error);
          return;
        }
      }
    };

    getAssignments();
  }, []);

  return <section>aaaa</section>;
}
