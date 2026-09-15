import { useEffect, useState } from "react";
import { useNavigate } from "react-router";
import { SearchInput } from "../../../shared/components/SearchInput";
import { DisplayText } from "../../../shared/components/DisplayText";
import { Button } from "../../../shared/components/Button";
import { ErrorList } from "../../../shared/components/ErrorList";
import { AssignmentList } from "../components/AssignmentList";
import { getAssignments } from "../api";
import type { ActivityDetailsDto } from "../types";
import type { ApiError } from "../../../api/types";
import { ApiRequestError } from "../../../api/error";
import { useAuth } from "../../auth/AuthContext";

export function AssignmentPage() {
  const [searchTerm, setSearchTerm] = useState("");
  const [assignments, setAssignments] = useState<ActivityDetailsDto[]>([]);
  const [error, setError] = useState<ApiError | undefined>();

  const navigate = useNavigate();
  const { isTeacher } = useAuth();

  const fetchAssignments = async () => {
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

  useEffect(() => {
    fetchAssignments();
  }, []);

  const search = searchTerm.toLowerCase().trim();

  const filteredAssignments = assignments.filter((assignment) => {
    return (
      assignment.activityName.toLowerCase().includes(search) ||
      assignment.moduleName.toLowerCase().includes(search) ||
      assignment.courseName.toLowerCase().includes(search)
    );
  });

  return (
    <section className="flex flex-col gap-6 p-6 min-h-full">
      <DisplayText text="ASSIGNMENTS" />

      <SearchInput
        value={searchTerm}
        onChange={setSearchTerm}
        placeholder="Search assignments..."
      />

      <ErrorList error={error} variant="list" />

      <AssignmentList assignments={filteredAssignments} />

      {isTeacher && (
        <div className="self-center mt-auto">
          <Button
            type="button"
            children="Create new activity"
            variant="list"
            color="create"
            onClick={() => navigate("/activities/create")}
          />
        </div>
      )}
    </section>
  );
}
