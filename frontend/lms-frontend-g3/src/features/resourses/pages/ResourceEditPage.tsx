import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router";
import {
  createResource,
  updateResource,
  getResourceTypes,
  getResourceById,
} from "../api";
import type {
  CreateResource,
  EditResource,
  ResourceTypeOption,
} from "../types/interfaces";
import { FormInput } from "../../../shared/components/FormInput";
import { Button } from "../../../shared/components/Button";
import { FormLabel } from "../../../shared/components/FormLabel";
import { FormTitle } from "../../../shared/components/FormTitle";
import { DisplayText } from "../../../shared/components/DisplayText";

export function ResourceEditPage() {
  const navigate = useNavigate();

  const { resourceId, activityId } = useParams<{
    resourceId?: string;
    activityId?: string;
  }>();

  const [name, setName] = useState("");
  const [content, setContent] = useState("");
  const [url, setUrl] = useState("");
  const [type, setType] = useState("");

  const [resourceTypes, setResourceTypes] = useState<ResourceTypeOption[]>([]);

  const [error, setError] = useState("");
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [isLoading, setIsLoading] = useState(false);

  const isEditing = Boolean(resourceId);
  const isSubmission = Boolean(activityId);

  useEffect(() => {
    const loadData = async () => {
      setIsLoading(true);
      setError("");

      try {
        if (isSubmission && !resourceId) {
          setType("Submission");
          return;
        }

        const types = await getResourceTypes();
        setResourceTypes(types);

        if (!resourceId) {
          return;
        }

        const resource = await getResourceById(resourceId);

        setName(resource.resourceName);
        setContent(resource.content);
        setUrl(resource.url ?? "");
        setType(resource.type);
      } catch (error) {
        console.error(error);
        setError("Could not load resource data.");
      } finally {
        setIsLoading(false);
      }
    };

    void loadData();
  }, [resourceId, activityId]);

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    if (isSubmission && !activityId) {
      setError("Activity ID is required.");
      return;
    }

    if (!isSubmission && !type) {
      setError("Please select a resource type.");
      return;
    }

    setError("");
    setIsSubmitting(true);

    try {
      if (isEditing && resourceId) {
        const edit: EditResource = {
          resourceName: name,
          content,
          url: url || null,
          type,
        };

        await updateResource(resourceId, edit);

        if (isSubmission) {
          navigate("/activities/assignments");
          return;
        }

        navigate(`/resources/${resourceId}`);
        return;
      }

      const create: CreateResource = {
        resourceName: name,
        content,
        url: url || null,
        type: isSubmission ? "Submission" : type,
        activityId: isSubmission ? activityId! : null,
      };

      await createResource(create);

      if (isSubmission) {
        navigate("/activities/assignments");
        return;
      }

      navigate("/resources");
    } catch (error) {
      console.error(error);

      setError(
        isEditing ? "Couldn't update resource." : "Couldn't create resource.",
      );
    } finally {
      setIsSubmitting(false);
    }
  };

  if (isLoading) {
    return (
      <section className="p-6">
        <DisplayText text="Loading resource..." />
      </section>
    );
  }

  return (
    <section className="flex min-h-full w-full flex-col gap-8 p-6">
      <div className="w-full rounded-lg border border-border bg-menu px-10 py-5">
        <FormTitle
          title={
            isEditing && isSubmission
              ? "Edit Submission"
              : isSubmission
                ? "Submit Assignment"
                : isEditing
                  ? "Edit Resource"
                  : "Create Resource"
          }
        />

        <form onSubmit={handleSubmit} className="flex flex-col">
          <div className="grid grid-cols-2 gap-x-10 gap-y-5">
            <div>
              <FormLabel htmlFor="name" className="text-white">
                Resource Name
              </FormLabel>

              <FormInput
                id="name"
                type="text"
                value={name}
                required
                onChange={(event) => setName(event.target.value)}
                placeholder="Enter Resource Name"
              />
            </div>

            {!isSubmission && (
              <div>
                <FormLabel htmlFor="resourceType" className="text-white">
                  Resource type
                </FormLabel>

                <select
                  id="resourceType"
                  value={type}
                  onChange={(event) => setType(event.target.value)}
                  required
                  className="w-full rounded-md bg-form-input px-4 py-3 text-primary-display-text"
                >
                  <option value="" disabled>
                    Select resource type
                  </option>

                  {resourceTypes.map((option) => (
                    <option key={option.value} value={option.name}>
                      {option.name}
                    </option>
                  ))}
                </select>
              </div>
            )}

            <div className={isSubmission ? "col-span-2" : ""}>
              <FormLabel htmlFor="content" className="text-white">
                {isSubmission ? "Submission" : "Description"}
              </FormLabel>

              <textarea
                id="content"
                value={content}
                onChange={(event) => setContent(event.target.value)}
                required
                rows={8}
                placeholder={
                  isSubmission
                    ? "Enter your submission"
                    : "Enter resource description"
                }
                className="min-h-48 w-full resize-y rounded-md bg-form-input px-4 py-3 text-primary-display-text"
              />
            </div>

            <div>
              <FormLabel htmlFor="url" className="text-white">
                URL
              </FormLabel>

              <FormInput
                id="url"
                type="url"
                value={url}
                onChange={(event) => setUrl(event.target.value)}
                placeholder="Enter resource URL"
              />
            </div>
          </div>

          <div className="my-6">
            <Button
              type="submit"
              disabled={isSubmitting}
              color={isEditing ? "edit" : "create"}
            >
              {isSubmitting
                ? "Saving..."
                : isEditing
                  ? "Edit submission"
                  : isSubmission
                    ? "Submit assignment"
                    : "Create resource"}
            </Button>
          </div>

          {error && (
            <p className="rounded-lg bg-red-100 p-3 text-red-700">{error}</p>
          )}
        </form>
      </div>
    </section>
  );
}
