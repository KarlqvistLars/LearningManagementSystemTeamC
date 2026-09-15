import { useState } from "react";
import {
  getResourcesByActivity,
  getAllResources,
  createResource,
  updateResource,
} from "./api/index";
import type { ResourceDto, ResourceWithCreatorDto } from "./types";

export default function Example() {
  const [resources, setResources] = useState<ResourceWithCreatorDto[]>([]);
  const [error, setError] = useState<string | null>(null);
  const [loading, setLoading] = useState(false);

  async function handleGetResources() {
    setError(null);
    setLoading(true);

    try {
      const data = await getAllResources();
      setResources(data);
    } catch (error) {
      if (error instanceof Error) {
        setError(error.message);
      } else {
        setError("Something went wrong.");
      }
    } finally {
      setLoading(false);
    }
  }

  async function handleGetResourcesByActivity() {
    setError(null);
    setLoading(true);

    try {
      const data = await getResourcesByActivity("activity-id-here");
      setResources(data);
    } catch (error) {
      if (error instanceof Error) {
        setError(error.message);
      } else {
        setError("Something went wrong.");
      }
    } finally {
      setLoading(false);
    }
  }

  async function handleCreate() {
    setError(null);

    const resource = {
      resourceName: "My resource",
      content: "Some resource content",
      url: null,
      createdDate: new Date().toISOString(),
      type: 3,
    };

    try {
      const created: ResourceDto = await createResource(resource);

      console.log("Created resource:", created);
    } catch (error) {
      if (error instanceof Error) {
        setError(error.message);
      } else {
        setError("Something went wrong.");
      }
    }
  }

  async function handleUpdate(resourceId: string) {
    setError(null);

    const resource = {
      resourceName: "Updated resource",
      content: "Updated content",
      url: null,
      createdDate: new Date().toISOString(),
      type: 3,
    };

    try {
      const updated: ResourceDto = await updateResource(resourceId, resource);

      console.log("Updated resource:", updated);
    } catch (error) {
      if (error instanceof Error) {
        setError(error.message);
      } else {
        setError("Something went wrong.");
      }
    }
  }

  return (
    <div>
      <h1>Resources</h1>

      {error && <div className="text-red-500">{error}</div>}

      <button onClick={handleGetResources}>Get all resources</button>

      <button onClick={handleGetResourcesByActivity}>
        Get activity resources
      </button>

      <button onClick={handleCreate}>Create resource</button>

      <button onClick={() => handleUpdate("resource-id-here")}>
        Update resource
      </button>

      {loading && <p>Loading...</p>}

      {resources.map((resource) => (
        <div key={resource.id}>
          <h2>{resource.resourceName}</h2>
          <p>{resource.content}</p>
          <p>
            Created by: {resource.createdByFirstName}{" "}
            {resource.createdByLastName}
          </p>
        </div>
      ))}
    </div>
  );
}
