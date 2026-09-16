import { useEffect, useState } from "react";
import type { ActivityDto, CreateActivity, EditActivity } from "../types";
import { createActivity, editActivity } from "../api";
import { FormInput } from "../../../shared/components/FormInput";
import { Button } from "../../../shared/components/Button";
import { FormLabel } from "../../../shared/components/FormLabel";

const ACTIVITY_TYPES = [
  "ELearningSession",
  "Lecture",
  "ExerciseSession",
  "Assignment",
] as const;

interface ActivityFormProps {
  moduleId: string;
  activity?: ActivityDto;
  onCancel: () => void;
  onSave: () => void;
}

export function ActivityForm({
  moduleId,
  activity,
  onCancel,
  onSave,
}: ActivityFormProps) {
  const [activityName, setActivityName] = useState("");
  const [type, setType] = useState<string>(ACTIVITY_TYPES[1]);
  const [description, setDescription] = useState("");
  const [startDate, setStartDate] = useState("");
  const [endDate, setEndDate] = useState("");
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [generalError, setGeneralError] = useState("");
  const [fieldErrors, setFieldErrors] = useState<Record<string, string[]>>({});

  const isEditing = Boolean(activity);

  useEffect(() => {
    if (activity) {
      setActivityName(activity.activityName);
      setType(ACTIVITY_TYPES[activity.type] ?? String(activity.type));
      setDescription(activity.description);
      setStartDate(toLocalDateTime(activity.startDate));
      setEndDate(toLocalDateTime(activity.endDate));
    } else {
      setActivityName("");
      setType(ACTIVITY_TYPES[1]);
      setDescription("");
      setStartDate("");
      setEndDate("");
    }
  }, [activity]);

  function toLocalDateTime(iso: string): string {
    const date = new Date(iso);
    const local = new Date(date.getTime() - date.getTimezoneOffset() * 60000);
    return local.toISOString().slice(0, 16);
  }

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    setGeneralError("");
    setFieldErrors({});
    setIsSubmitting(true);

    try {
      const payload = {
        activityName,
        description,
        startDate: new Date(startDate).toISOString(),
        endDate: new Date(endDate).toISOString(),
        type,
        moduleId,
      };

      if (isEditing) {
        const body: EditActivity = { id: activity!.id, ...payload };
        await editActivity(body);
      } else {
        const body: CreateActivity = payload;
        await createActivity(body);
      }

      onSave();
    } catch (error) {
      const e = error as Error & { details?: Record<string, string[]> };
      if (e.details && Object.keys(e.details).length > 0) {
        setFieldErrors(e.details);
      } else {
        setGeneralError(e.message || "Something went wrong.");
      }
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <form onSubmit={handleSubmit} className="flex flex-col">
      <div className="grid grid-cols-2 gap-x-10 gap-y-5">
        <div>
          <FormLabel htmlFor="activityName" className="text-white">
            Name
          </FormLabel>

          <FormInput
            id="activityName"
            type="text"
            value={activityName}
            required
            onChange={(event) => setActivityName(event.target.value)}
            placeholder="Enter activity name"
          />

          {fieldErrors.ActivityName && (
            <p className="mt-1 text-sm text-red-500">
              {fieldErrors.ActivityName[0]}
            </p>
          )}
        </div>

        <div>
          <FormLabel htmlFor="type" className="text-white">
            Type
          </FormLabel>

          <select
            id="type"
            value={type}
            onChange={(event) => setType(event.target.value)}
            className="w-full rounded-md bg-form-input px-4 py-3 text-primary-display-text"
          >
            {ACTIVITY_TYPES.map((option) => (
              <option key={option} value={option}>
                {option}
              </option>
            ))}
          </select>
        </div>

        <div>
          <FormLabel htmlFor="startDate" className="text-white">
            Start date &amp; time
          </FormLabel>

          <FormInput
            id="startDate"
            type="datetime-local"
            value={startDate}
            onChange={(event) => setStartDate(event.target.value)}
            required
          />

          {fieldErrors.StartDate && (
            <p className="mt-1 text-sm text-red-500">
              {fieldErrors.StartDate[0]}
            </p>
          )}
        </div>

        <div>
          <FormLabel htmlFor="endDate" className="text-white">
            End date &amp; time
          </FormLabel>

          <FormInput
            id="endDate"
            type="datetime-local"
            value={endDate}
            onChange={(event) => setEndDate(event.target.value)}
            required
          />

          {fieldErrors.EndDate && (
            <p className="mt-1 text-sm text-red-500">
              {fieldErrors.EndDate[0]}
            </p>
          )}
        </div>

        <div className="col-span-2">
          <FormLabel htmlFor="description" className="text-white">
            Description
          </FormLabel>

          <textarea
            id="description"
            value={description}
            onChange={(event) => setDescription(event.target.value)}
            required
            rows={6}
            placeholder="Enter activity description"
            className="min-h-32 w-full resize-y rounded-md bg-form-input px-4 py-3 text-primary-display-text"
          />

          {fieldErrors.Description && (
            <p className="mt-1 text-sm text-red-500">
              {fieldErrors.Description[0]}
            </p>
          )}
        </div>
      </div>

      <div className="my-6 flex items-center gap-4">
        <Button
          type="submit"
          disabled={isSubmitting}
          color={isEditing ? "edit" : "create"}
        >
          {isSubmitting
            ? "Saving..."
            : isEditing
              ? "Update activity"
              : "Create activity"}
        </Button>

        <Button variant="list" color="cancel" onClick={onCancel}>
          Cancel
        </Button>
      </div>

      {generalError && (
        <p className="rounded-lg bg-red-100 p-3 text-red-700">{generalError}</p>
      )}
    </form>
  );
}