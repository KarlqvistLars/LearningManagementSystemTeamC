import { useNavigate, useParams } from "react-router";
import { useState, useEffect } from "react";
import type { ResourceDto } from "../types/interfaces";
import { useAuth } from "../../auth/AuthContext";
import { getAllResources } from "../api/index";
import { DisplayText } from "../../../shared/components/DisplayText";
import { Button } from "../../../shared/components/Button";

export function ResourceDetailsPage() {
  const { isTeacher } = useAuth();
  const { resourceId } = useParams<{ resourceId: string }>();
  const navigate = useNavigate();
  const [resource, setResource] = useState<ResourceDto | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    async function loadResource() {
      try {
        if (!resourceId) {
          throw new Error("Resource ID is required");
        }
        const resourceFetched = await getAllResources(resourceId);
        setResource(resourceFetched);
      } catch (error) {
        console.error(error);
      } finally {
        setLoading(false);
      }
    }

    if (loading) {
      loadResource();
    }
  }, [loading, resourceId]);

  return (
    <section className="flex h-full flex-col gap-6 p-6">
      <div className="flex flex-1 flex-col gap-8 rounded-lg border border-border bg-menu px-10 py-10">
        <h1 className="uppercase">
          <DisplayText text="Resource Details" />
        </h1>
        {loading && <DisplayText text="Loading resource details..." />}
        {resource && (
          <>
            <div className="grid grid-cols-2 gap-x-10 gap-y-5">
              <div>
                <span className="text-white mb-1 block text-base font-medium block">
                  Name
                </span>
                <span className="text-lg w-full rounded-md bg-form-input px-4 py-3 text-primary-display-text block">
                  {resource.resourceName}
                </span>
              </div>

              <div className="row-span-2">
                <span className="text-white mb-1 block text-base font-medium block">
                  Mentors
                </span>
                <span className="text-lg w-full rounded-md bg-form-input px-4 py-3 text-primary-display-text block">
                  John Doe
                </span>
              </div>

              <div>
                <span className="text-white mb-1 block text-base font-medium block">
                  Start date
                </span>
                <span className="text-lg w-full rounded-md bg-form-input px-4 py-3 text-primary-display-text block">
                  {new Date(resource.createdDate).toLocaleDateString()}
                </span>
              </div>

              <div>
                <span className="text-white mb-1 block text-base font-medium block">
                  End date
                </span>
                <span className="text-lg w-full rounded-md bg-form-input px-4 py-3 text-primary-display-text block">
                  {resource.createdBy.toString()}
                </span>
              </div>

              <div className="row-span-2">
                <span className="text-white mb-1 block text-base font-medium block">
                  Students
                </span>
                <span className="text-lg w-full rounded-md bg-form-input px-4 py-3 text-primary-display-text block">
                  John Doe
                </span>
              </div>

              <div className="row-span-4">
                <span className="text-white mb-1 block text-base font-medium block">
                  Description
                </span>
                <span className="text-lg w-full rounded-md bg-form-input px-4 py-3 text-primary-display-text block">
                  {resource.content}
                </span>
              </div>
            </div>
          </>
        )}
      </div>
      <div className="flex items-center">
        {isTeacher && (
          <Button
            variant="list"
            color="edit"
            onClick={() => navigate(`/resources/${resourceId}/edit`)}
          >
            Edit
          </Button>
        )}
      </div>
    </section>
  );
}
