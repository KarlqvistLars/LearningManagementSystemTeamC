import { useEffect, useState } from "react";
import type { Resource, CreateResource, EditResource } from "../types/interfaces";
import { createResource, editResource, getResourceTypes } from "../api/resources";
import { FormInput } from "../../../shared/components/FormInput";
import { Button } from "../../../shared/components/Button";
import { FormLabel } from "../../../shared/components/FormLabel";
import type { ResourceTypeOption } from "../../modules/types";

interface ResourceFormProps {
  courseId: string;
  resource?: Resource;
  onResourceSaved: () => void;
}

export function ResourceForm({
  resource,
  onResourceSaved,
}: ResourceFormProps) {
  const [name, setName] = useState("");
  const [content, setContent] = useState("");
  const [createdDate, setCreatedDate] = useState("");
  const [url, setUrl] = useState("");
  // const [type] = useState(0);
  const [type, setType] = useState<number | "">("");
  const [resourceTypes, setResourceTypes] = useState<
    ResourceTypeOption[]
  >([]);

  const [message, setMessage] = useState("");
  const [error, setError] = useState("");
  const [isSubmitting, setIsSubmitting] = useState(false);

  useEffect(() => {
    async function loadResourceTypes() {
      try {
        const options = await getResourceTypes();
        setResourceTypes(options);
      } catch (error) {
        setError(
          error instanceof Error
            ? error.message
            : "Failed to load resource types"
        );
      }
    }

    void loadResourceTypes();
  }, []);

  const handleSubmit = async (event: React.SubmitEvent<HTMLFormElement>) => {
    event.preventDefault();

    setMessage("");
    setError("");
    setIsSubmitting(true);

    try {
      if (resource) {
        const edit: EditResource = {
          id: resource.id,
          resourceName: name,
          content: content,
          url: url,
          createdAt: new Date(createdDate),
          type: type,
        };

        await editResource(edit);

        setMessage("Resource updated successfully!");
      } else {
        const create: CreateResource = {
          resourceName: name,
          content: content,
          url: url,
          createdAt: new Date(Date.now()),
          type: 2,
        };

        await createResource(create);

        setMessage("Resource created successfully!");
      }

      // Clears the form
      setName("");
      setContent("");
      setUrl("");
      setCreatedDate("");
      onResourceSaved();
    } catch (error) {
      console.error(error);
      setError(resource ? "Couldn't update resource." : "Couldn't create resource.");
    } finally {
      setIsSubmitting(false);
    }
  };

  function formateDateForInput(date: string | Date) {
    return new Date(date).toISOString().split("T")[0];
  }

  useEffect(() => {
    if (resource) {
      setName(resource.resourceName);
      setContent(resource.content);
      setCreatedDate(formateDateForInput(resource.createdAt));
      setUrl(resource.url);

    } else {
      setName("");
      setContent("");
      setCreatedDate("");
      setUrl("");
    }
  }, [resource]);

  return (
    <div>
      <form onSubmit={handleSubmit} className="mt-6 space-y-5">
        {/* Name */}
        <div>
          <FormLabel htmlFor="name">Name</FormLabel>

          <FormInput
            className="w-full resize-none rounded-lg border border-gray-300
                        bg-form-input px-4 py-2.5 text-gray-400 outline-none transition placeholder:text-gray-400
                        focus:border-blue-500 focus:ring-2 focus:ring-blue-200"
            id="name"
            type="text"
            value={name}
            required
            onChange={(event) => setName(event.target.value)}
            placeholder="Enter Resource Name"
          />
        </div>

        {/* Description */}
        <div>
          <FormLabel htmlFor="description">Description</FormLabel>

          <textarea
            className="w-full resize-none rounded-lg border border-gray-300
                        bg-form-input px-4 py-2.5 text-gray-400 outline-none transition placeholder:text-gray-400
                        focus:border-blue-500 focus:ring-2 focus:ring-blue-200"
            id="description"
            value={content}
            onChange={(event) => setContent(event.target.value)}
            required
            rows={5}
            placeholder="Enter resource description"
          ></textarea>
        </div>

        {/* Url */}
        <div>
          <FormLabel htmlFor="url">Url</FormLabel>

          <FormInput
            className="w-full resize-none rounded-lg border border-gray-300
                        bg-form-input px-4 py-2.5 text-gray-400 outline-none transition placeholder:text-gray-400
                        focus:border-blue-500 focus:ring-2 focus:ring-blue-200"
            id="url"
            type="text"
            value={url}
            onChange={(event) => setUrl(event.target.value)}
            required
            placeholder="Enter resource URL"
          />
        </div>

        {/* Type */}
        <div>
          <FormLabel htmlFor="resourceType">
            Resource type
          </FormLabel>

          <select
            id="resourceType"
            value={type}
            onChange={(event) => {
              const selectedType = Number(event.target.value);
              setType(selectedType);
            }}
            required
            className="
      w-full rounded-lg border border-border
      bg-form-input px-4 py-2.5
      text-white outline-none transition
      focus:border-primary focus:ring-2
      focus:ring-primary/20
    "
          >
            <option value="" disabled>
              Select resource type
            </option>

            {resourceTypes.map((option) => (
              <option key={option.value} value={option.value}>
                {option.name}
              </option>
            ))}
          </select>
        </div>

        {/* Submit */}
        <Button type="submit" disabled={isSubmitting}>
          {isSubmitting ? "Submitting..." : "Submit resource"}
        </Button>

        {/* Success */}
        {message && (
          <p className="rounded-lg bg-green-100 p-3 text-green-700">
            {message}
          </p>
        )}

        {/* Error */}
        {error && (
          <p className="rounded-lg bg-red-100 p-3 text-red-700">{error}</p>
        )}
      </form>
    </div>
  );
}
