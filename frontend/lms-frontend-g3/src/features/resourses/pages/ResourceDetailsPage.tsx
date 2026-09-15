import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router";
import { useAuth } from "../../auth/AuthContext";
import { getResourceById } from "../api";
import type { ResourceWithCreatorDto } from "../types/interfaces";
import { DisplayText } from "../../../shared/components/DisplayText";
import { Button } from "../../../shared/components/Button";

export function ResourceDetailsPage() {
  const { isTeacher } = useAuth();
  const { resourceId } = useParams<{ resourceId: string }>();
  const navigate = useNavigate();

  const [resource, setResource] = useState<ResourceWithCreatorDto | null>(null);

  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  useEffect(() => {
    if (!resourceId) {
      setError("Resource ID is required.");
      setLoading(false);
      return;
    }

    const loadResource = async () => {
      try {
        const resource = await getResourceById(resourceId);
        setResource(resource);
      } catch (error) {
        console.error(error);
        setError("Could not load resource.");
      } finally {
        setLoading(false);
      }
    };

    void loadResource();
  }, [resourceId]);

  return (
    <section className="flex h-full flex-col gap-6 p-6">
      <div className="flex flex-1 flex-col gap-8 rounded-lg border border-border bg-menu px-10 py-10">
        <DisplayText text="Resource Details" />

        {loading && <DisplayText text="Loading resource details..." />}

        {error && (
          <p className="rounded-lg bg-red-100 p-3 text-red-700">{error}</p>
        )}

        {resource && (
          <div className="grid grid-cols-2 gap-x-10 gap-y-5">
            <div>
              <span className="mb-1 block text-base font-medium text-white">
                Name
              </span>

              <span className="block w-full rounded-md bg-form-input px-4 py-3 text-lg text-primary-display-text">
                {resource.resourceName}
              </span>
            </div>

            <div>
              <span className="mb-1 block text-base font-medium text-white">
                Created
              </span>

              <span className="block w-full rounded-md bg-form-input px-4 py-3 text-lg text-primary-display-text">
                {new Date(resource.createdAt).toLocaleDateString()}
              </span>
            </div>

            <div>
              <span className="mb-1 block text-base font-medium text-white">
                Created by
              </span>

              <span className="block w-full rounded-md bg-form-input px-4 py-3 text-lg text-primary-display-text">
                {resource.createdByFirstName} {resource.createdByLastName}
              </span>
            </div>

            <div>
              <span className="mb-1 block text-base font-medium text-white">
                Type
              </span>

              <span className="block w-full rounded-md bg-form-input px-4 py-3 text-lg text-primary-display-text">
                {resource.type}
              </span>
            </div>

            <div className="col-span-2">
              <span className="mb-1 block text-base font-medium text-white">
                URL
              </span>

              <span className="block w-full rounded-md bg-form-input px-4 py-3 text-lg text-primary-display-text">
                {resource.url || "No URL"}
              </span>
            </div>

            <div className="col-span-2">
              <span className="mb-1 block text-base font-medium text-white">
                Description
              </span>

              <span className="block w-full whitespace-pre-wrap rounded-md bg-form-input px-4 py-3 text-lg text-primary-display-text">
                {resource.content}
              </span>
            </div>
          </div>
        )}
      </div>

      {isTeacher && resource && (
        <div className="flex items-center">
          <Button
            variant="list"
            color="edit"
            onClick={() => navigate(`/resources/${resourceId}/edit`)}
          >
            Edit
          </Button>
        </div>
      )}
    </section>
  );
}
