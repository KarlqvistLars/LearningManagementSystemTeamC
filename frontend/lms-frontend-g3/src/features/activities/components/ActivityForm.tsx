import { useEffect, useState } from "react";
import type { ActivityDto, CreateActivity, EditActivity } from "../types";
import { createActivity, editActivity } from "../api";
import { FormInput } from "../../../shared/components/FormInput";
import { Button } from "../../../shared/components/Button";
import { FormLabel } from "../../../shared/components/FormLabel";

const ACTIVITY_TYPES = ["ELearningSession", "Lecture", "ExerciseSession", "Assignment"] as const;

interface ActivityFormProps {
    moduleId: string;
    activity?: ActivityDto;
    onSave: () => void;
}

export function ActivityForm({ moduleId, activity, onSave }: ActivityFormProps) {
    const [activityName, setActivityName] = useState("");
    const [type, setType] = useState<string>(ACTIVITY_TYPES[1]);
    const [description, setDescription] = useState("");
    const [startDate, setStartDate] = useState("");
    const [endDate, setEndDate] = useState("");
    const [isSubmitting, setIsSubmitting] = useState(false);
    const [success, setSuccess] = useState("");
    const [generalError, setGeneralError] = useState("");
    const [fieldErrors, setFieldErrors] = useState<Record<string, string[]>>({});

    const isEditing = Boolean(activity);

    useEffect(() => {
        if (activity) {
            setActivityName(activity.activityName);
            setType(String(activity.type));
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

    // datetime-local needs local time "YYYY-MM-DDTHH:mm" from the ISO string the API returns
    function toLocalDateTime(iso: string): string {
        const date = new Date(iso);
        const local = new Date(date.getTime() - date.getTimezoneOffset() * 60000);
        return local.toISOString().slice(0, 16);
    }

    const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();

        setSuccess("");
        setGeneralError("");
        setFieldErrors({});
        setIsSubmitting(true);

        try {
            if (isEditing) {
                const body: EditActivity = {
                    id: activity!.id,
                    activityName,
                    description,
                    startDate: new Date(startDate).toISOString(),
                    endDate: new Date(endDate).toISOString(),
                    type,
                    moduleId,
                };
                await editActivity(moduleId, body.id, body);
                setSuccess("Activity updated successfully!");
            } else {
                const body: CreateActivity = {
                    activityName,
                    description,
                    startDate: new Date(startDate).toISOString(),
                    endDate: new Date(endDate).toISOString(),
                    type,
                    moduleId,
                };
                await createActivity(moduleId, body);
                setSuccess("Activity created successfully!");
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
        <form onSubmit={handleSubmit} className="mt-6 space-y-5">
            {/* Name */}
            <div>
                <FormLabel htmlFor="activityName">Name</FormLabel>
                <FormInput
                    id="activityName"
                    type="text"
                    value={activityName}
                    required
                    onChange={(event) => setActivityName(event.target.value)}
                    placeholder="Enter activity name"
                />
                {fieldErrors.activityName && (
                    <p className="mt-1 text-sm text-red-500">{fieldErrors.activityName[0]}</p>
                )}
            </div>

            {/* Type */}
            <div>
                <FormLabel htmlFor="type">Type</FormLabel>
                <select
                    id="type"
                    value={type}
                    onChange={(event) => setType(event.target.value)}
                    className="w-full rounded-lg border border-gray-300 bg-white px-4 py-2.5 text-gray-900 outline-none transition focus:border-blue-500 focus:ring-2 focus:ring-blue-200"
                >
                    {ACTIVITY_TYPES.map((option) => (
                        <option key={option} value={option}>
                            {option}
                        </option>
                    ))}
                </select>
            </div>

            {/* Description */}
            <div>
                <FormLabel htmlFor="description">Description</FormLabel>
                <textarea
                    id="description"
                    value={description}
                    onChange={(event) => setDescription(event.target.value)}
                    required
                    rows={4}
                    placeholder="Enter activity description"
                    className="w-full resize-none rounded-lg border border-gray-300 bg-white px-4 py-2.5 text-gray-900 outline-none transition placeholder:text-gray-400 focus:border-blue-500 focus:ring-2 focus:ring-blue-200"
                ></textarea>
                {fieldErrors.description && (
                    <p className="mt-1 text-sm text-red-500">{fieldErrors.description[0]}</p>
                )}
            </div>

            {/* StartDate */}
            <div>
                <FormLabel htmlFor="startDate">Start date &amp; time</FormLabel>
                <FormInput
                    id="startDate"
                    type="datetime-local"
                    value={startDate}
                    onChange={(event) => setStartDate(event.target.value)}
                    required
                />
                {fieldErrors.startDate && (
                    <p className="mt-1 text-sm text-red-500">{fieldErrors.startDate[0]}</p>
                )}
            </div>

            {/* EndDate */}
            <div>
                <FormLabel htmlFor="endDate">End date &amp; time</FormLabel>
                <FormInput
                    id="endDate"
                    type="datetime-local"
                    value={endDate}
                    onChange={(event) => setEndDate(event.target.value)}
                    required
                />
                {fieldErrors.endDate && (
                    <p className="mt-1 text-sm text-red-500">{fieldErrors.endDate[0]}</p>
                )}
            </div>

            {/* Submit */}
            <Button type="submit" disabled={isSubmitting}>
                {isSubmitting ? "Submitting..." : isEditing ? "Update activity" : "Create activity"}
            </Button>

            {success && <p className="rounded-lg bg-green-100 p-3 text-green-700">{success}</p>}
            {generalError && <p className="rounded-lg bg-red-100 p-3 text-red-700">{generalError}</p>}
        </form>
    );
}