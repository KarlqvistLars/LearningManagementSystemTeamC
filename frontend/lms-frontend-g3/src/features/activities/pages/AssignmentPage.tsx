import { useEffect, useState } from "react";
import { DisplayText } from "../../../shared/components/DisplayText";
import { SearchInput } from "../../../shared/components/SearchInput";
import type { ActivityDetailsDto } from "../types";
import { AssignmentList } from "../components/AssignmentList";
import { getAssignments } from "../api/index";

export function AssignmentPage() {
  const [assignments, setAssignments] = useState<ActivityDetailsDto[]>([]);

  const [searchTerm, setSearchTerm] = useState("");

  const [loading, setLoading] = useState(true);

  const [error, setError] = useState("");

  useEffect(() => {
    const loadAssignments = async () => {
      try {
        const result = await getAssignments();

        setAssignments(result);
      } catch (error) {
        console.error(error);
        setError("Could not load assignments.");
      } finally {
        setLoading(false);
      }
    };

    void loadAssignments();
  }, []);

  const search = searchTerm.toLowerCase().trim();

  const filteredAssignments = assignments.filter((assignment) =>
    assignment.activityName.toLowerCase().includes(search),
  );

  return (
    <section className="flex h-full flex-col gap-6 p-6">
      <DisplayText text="Assignments" />

      <SearchInput
        value={searchTerm}
        onChange={setSearchTerm}
        placeholder="Search assignments..."
      />

      {loading && <DisplayText text="Loading assignments..." />}

      {error && (
        <p className="rounded-lg bg-red-100 p-3 text-red-700">{error}</p>
      )}

      {!loading && !error && (
        <AssignmentList assignments={filteredAssignments} />
      )}
    </section>
  );
}
