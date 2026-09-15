import { useEffect, useState } from "react";
import { useNavigate } from "react-router";
import { useAuth } from "../../auth/AuthContext";
import { DisplayText } from "../../../shared/components/DisplayText";
import { SearchInput } from "../../../shared/components/SearchInput";
import { Button } from "../../../shared/components/Button";
import { ResourceList } from "../components/resourceList";
import { getAllResources } from "../api";
import type { ResourceDto } from "../types/interfaces";

export function ResourcePage() {
  const { isTeacher } = useAuth();
  const navigate = useNavigate();

  const [searchTerm, setSearchTerm] = useState("");
  const [resources, setResources] = useState<ResourceDto[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const loadResources = async () => {
      try {
        const resources = await getAllResources();
        setResources(resources);
      } catch (error) {
        console.error(error);
      } finally {
        setLoading(false);
      }
    };

    void loadResources();
  }, []);

  const search = searchTerm.toLowerCase().trim();

  const filteredResources = resources.filter((resource) => {
    return (
      resource.resourceName.toLowerCase().includes(search) ||
      resource.content.toLowerCase().includes(search)
    );
  });

  return (
    <section className="flex h-full flex-col gap-6 p-6">
      <DisplayText text="Resources" />

      <SearchInput
        value={searchTerm}
        onChange={setSearchTerm}
        placeholder="Search resources..."
      />

      {loading ? (
        <DisplayText text="Loading resources..." />
      ) : (
        <ResourceList resources={filteredResources} />
      )}

      {isTeacher && (
        <div className="mt-auto self-center">
          <Button variant="list" onClick={() => navigate("/resources/create")}>
            Create new resource
          </Button>
        </div>
      )}
    </section>
  );
}
