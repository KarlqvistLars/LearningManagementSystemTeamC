import { useState } from "react";
import { useNavigate, useParams } from "react-router";
import { useAuth } from "../../auth/AuthContext";
import { ActivityForm } from "../components/ActivityForm";
import { ActivityList } from "../components/ActivityList";
import { useActivities } from "../hooks/useActivities";
import { Button } from "../../../shared/components/Button";
import { DisplayText } from "../../../shared/components/DisplayText";
import { FormTitle } from "../../../shared/components/FormTitle";
import type { ActivityDto } from "../types";

export function ModuleActivitiesPage() {
  const { moduleId } = useParams();
  const navigate = useNavigate();
  const { activities, loading, error, refetch } = useActivities(moduleId);
  const { isTeacher } = useAuth();

  const [showForm, setShowForm] = useState(false);
  const [editingActivity, setEditingActivity] = useState<ActivityDto | null>(
    null,
  );

  return (
    <section className="flex h-full flex-col gap-6 p-6">
      <DisplayText text="Module Activities" />

      {loading && <DisplayText text="Loading activities..." />}

      {error && (
        <p className="rounded-lg bg-red-100 p-3 text-red-700">{error}</p>
      )}

      {!loading && !error && (
        <ActivityList
          activities={activities}
          onEditActivity={
            isTeacher
              ? (activity) => {
                  setEditingActivity(activity);
                  setShowForm(true);
                }
              : undefined
          }
        />
      )}

      {isTeacher && showForm && (
        <div className="w-full rounded-lg border border-border bg-menu px-10 py-10">
          <FormTitle
            title={editingActivity ? "Edit Activity" : "Create Activity"}
          />

          <ActivityForm
            moduleId={moduleId ?? ""}
            activity={editingActivity ?? undefined}
            onCancel={() => {
              setShowForm(false);
              setEditingActivity(null);
            }}
            onSave={() => {
              setShowForm(false);
              setEditingActivity(null);
              refetch();
            }}
          />
        </div>
      )}

      <div className="mt-auto flex justify-center gap-4 pt-10">
        {isTeacher && !showForm && (
          <Button
            variant="list"
            color="create"
            onClick={() => {
              setEditingActivity(null);
              setShowForm(true);
            }}
          >
            Create new activity
          </Button>
        )}

        <Button
          variant="list"
          color="cancel"
          onClick={() => navigate(`/modules/${moduleId}`)}
        >
          Back
        </Button>
      </div>
    </section>
  );
}