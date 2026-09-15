import { useEffect, useState } from "react";
import type { ResourceDto, CreateResource } from "../types";
import { useAuth } from "../../auth/AuthContext";
import { useNavigate } from "react-router";
import { createResource, updateResource, getResourceTypes, getResourceById } from "../api/index";
import { FormInput } from "../../../shared/components/FormInput";
import { Button } from "../../../shared/components/Button";
import { FormLabel } from "../../../shared/components/FormLabel";
import type { ResourceTypeOption } from "../types";
import { FormTitle } from "../../../shared/components/FormTitle";
import { useParams } from "react-router";

interface ResourceFormProps {
  courseId?: string | undefined;
  resource?: ResourceDto;
  onResourceSaved: () => void;
}

export function ResourceForm({
  resource,
  onResourceSaved,
}: ResourceFormProps) {
  const resourceProp = resource;
  const { user } = useAuth();
  const [name, setName] = useState("");
  const [content, setContent] = useState("");
  const [createdDate, setCreatedDate] = useState("");
  const [url, setUrl] = useState("");
  const [type, setType] = useState<number>(0);
  const [resourceTypes, setResourceTypes] = useState<
    ResourceTypeOption[]
  >([]);
  const { resourceId } = useParams<{ resourceId: string }>();
  const isEditing = Boolean(resourceId);
  const [message, setMessage] = useState("");
  const [error, setError] = useState("");
  const [isSubmitting, setIsSubmitting] = useState(false);

  const reroute = useNavigate();

  const currentResourceId = window.location.pathname.split("/") ?? "";
  const currentResourceIdValue = currentResourceId[currentResourceId.length - 2];

  useEffect(() => {
    async function loadData() {
      try {
        const types = await getResourceTypes();
        setResourceTypes(types);

        if (resourceId) {
          const fetchedResource = await getResourceById(resourceId);

          setName(fetchedResource.resourceName);
          setContent(fetchedResource.content);
          setUrl(fetchedResource.url ?? "");
          setCreatedDate(fetchedResource.createdDate);
          setType(fetchedResource.type);
        }
      } catch (error) {
        console.error(error);
        setError("Could not load resource data.");
      }
    }
    void loadData();
  }, [resourceId]);

  const handleSubmit = async (event: React.SubmitEvent<HTMLFormElement>) => {
    event.preventDefault();

    setMessage("");
    setError("");
    setIsSubmitting(true);

    try {

      if (isEditing) {

        const edit: ResourceDto = {
          id: currentResourceIdValue ?? "",
          resourceName: name,
          content: content,
          url: url,
          createdDate: resourceProp?.createdDate ?? "",
          type: type,
          createdBy: resourceProp?.createdBy ?? "",
        };

        await updateResource(edit.id, edit);

        setMessage("Resource updated successfully!");

      } else {
        if (!user) {
          setError("You must be logged in to create a resource.");
          return;
        }
        const create: CreateResource = {
          resourceName: name,
          content: content,
          url: url,
          createdDate: new Date(Date.now()).toISOString(),
          type: type,
          createdBy: user.id,
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
      reroute("/resources");
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
      setCreatedDate(formateDateForInput(resource.createdDate));
      setUrl(resource.url ?? "");
      setType(resource.type);

    } else {
      setName("");
      setContent("");
      setCreatedDate("");
      setUrl("");
      setType(0);
    }
  }, [resource]);

  return (
    <div>
      <section className="flex flex-col gap-8 p-6">
        <div className="gap-8 rounded-lg border border-border bg-menu px-10 py-5">
          <FormTitle title={isEditing ? "Edit Resource" : "Create Resource"} />
          <form onSubmit={handleSubmit} className="flex flex-1 flex-col">
            <div className="grid grid-cols-2 grid-rows-[auto_1fr] gap-x-10 gap-y-5">
              {/* Överst till vänster */}
              <div className="col-start-1 row-start-1">
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

              {/* Under Resource Name */}
              <div className="col-start-1 row-start-2">
                <FormLabel htmlFor="description" className="text-white">
                  Description
                </FormLabel>

                <textarea
                  id="description"
                  value={content}
                  onChange={(event) => setContent(event.target.value)}
                  required
                  rows={8}
                  placeholder="Enter resource description"
                  className="min-h-48 w-full resize-y rounded-md bg-form-input px-4 py-3 text-primary-display-text"
                />
              </div>

              {/* Överst till höger */}
              <div className="col-start-2 row-start-1">
                <FormLabel htmlFor="resourceType" className="text-white">
                  Resource type
                </FormLabel>

                <select
                  id="resourceType"
                  value={type}
                  onChange={(event) => setType(Number(event.target.value))}
                  required
                  className="w-full rounded-md bg-form-input px-4 py-3 text-primary-display-text"
                >
                  <option value={0} disabled>
                    Select resource type
                  </option>

                  {resourceTypes.map((option) => (
                    <option key={option.value} value={option.value}>
                      {option.name}
                    </option>
                  ))}
                </select>
              </div>

              {/* Under Resource type */}
              <div className="col-start-2 row-start-2">
                <FormLabel htmlFor="url" className="text-white">
                  URL
                </FormLabel>

                <FormInput
                  id="url"
                  type="url"
                  value={url}
                  onChange={(event) => setUrl(event.target.value)}
                  required
                  placeholder="Enter resource URL"
                />
              </div>
            </div>


            {/* Submit */}
            <div className="my-6">
              <Button type="submit" disabled={isSubmitting}>
                {isSubmitting ? "Submitting..." : isEditing ? "Update resource" : "Submit resource"}
              </Button>
            </div>

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
      </section >
    </div >
  );
}
