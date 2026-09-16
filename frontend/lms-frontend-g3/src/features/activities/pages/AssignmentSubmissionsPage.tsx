import { useEffect, useMemo, useState } from "react";
import { useParams } from "react-router";
import { DisplayText } from "../../../shared/components/DisplayText";
import { SearchInput } from "../../../shared/components/SearchInput";
import { SubmissionList } from "../components/SubmissionList";
import { getAssignmentSubmissions } from "../api/index";
import type { AssignmentSubmissionDto } from "../types";

export function AssignmentSubmissionsPage() {
  const { activityId } = useParams<{
    activityId: string;
  }>();

  const [submissions, setSubmissions] = useState<AssignmentSubmissionDto[]>([]);

  const [searchTerm, setSearchTerm] = useState("");

  const [loading, setLoading] = useState(true);

  const [error, setError] = useState("");

  useEffect(() => {
    if (!activityId) {
      setError("Activity ID is required.");
      setLoading(false);
      return;
    }

    const loadSubmissions = async () => {
      setLoading(true);
      setError("");

      try {
        const result = await getAssignmentSubmissions(activityId);

        setSubmissions(result);
      } catch (error) {
        console.error(error);
        setError("Could not load assignment submissions.");
      } finally {
        setLoading(false);
      }
    };

    void loadSubmissions();
  }, [activityId]);

  const filteredSubmissions = useMemo(() => {
    const search = searchTerm.toLowerCase().trim();

    if (!search) {
      return submissions;
    }

    return submissions.filter((submission) => {
      const studentName =
        `${submission.studentFirstName} ${submission.studentLastName}`.toLowerCase();

      return (
        studentName.includes(search) ||
        submission.activityName.toLowerCase().includes(search)
      );
    });
  }, [submissions, searchTerm]);

  return (
    <section className="flex h-full flex-col gap-6 p-6">
      <DisplayText text="Assignment Submissions" />

      <SearchInput
        value={searchTerm}
        onChange={setSearchTerm}
        placeholder="Search students or assignments..."
      />

      {loading && <DisplayText text="Loading submissions..." />}

      {error && (
        <p className="rounded-lg bg-red-100 p-3 text-red-700">{error}</p>
      )}

      {!loading && !error && (
        <SubmissionList submissions={filteredSubmissions} />
      )}
    </section>
  );
}
