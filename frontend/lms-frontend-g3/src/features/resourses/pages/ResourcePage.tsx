import { useAuth } from "../../auth/AuthContext";
import { useEffect } from "react";
import { useNavigate } from "react-router";
// import { fetchResourcesById } from "../api/resources";
import { DisplayText } from "../../../shared/components/DisplayText";
import { SearchInput } from "../../../shared/components/SearchInput";
import { Suspense, useState } from "react";
import { ResourceList } from "../components/resourceList";
import { Button } from "../../../shared/components/Button";
import type { ResourceDto, ResourceWithCreatorDto } from "../types/interfaces";
import { getAllResources } from "../api";

export function ResourcePage() {
    const { user, isTeacher } = useAuth();
    const navigate = useNavigate();
    const [searchTerm, setSearchTerm] = useState("");
    const search = searchTerm.toLowerCase();
    const [resources, setResources] = useState<ResourceDto[]>([]);
    const [loading, setLoading] = useState(true);

    const filteredResources = resources.filter((resource) => {
        return (
            resource.resourceName.toLowerCase().includes(search) ||
            resource.content.toLowerCase().includes(search)
        );
    });

    useEffect(() => {
        async function loadResources() {
            try {
                // const resourcesFetched = isTeacher
                //     ? await getAllResources()
                //     : await fetchResourcesById(user?.id || "");

                const resourcesFetched = await getAllResources();
                setResources(resourcesFetched);
            } catch (error) {
                console.error(error);
            } finally {
                setLoading(false);
            }
        }

        if (loading) {
            loadResources();
        }
    }, [isTeacher, loading, user?.id]);

    return (
        <section className="flex flex-col gap-6 p-6 h-full">

            <h1 className="uppercase">
                <DisplayText text="Resources" />
            </h1>
            <SearchInput
                value={searchTerm}
                onChange={setSearchTerm}
                placeholder="Search resources..."
            />
            <Suspense fallback={<DisplayText text="Loading resources..." />}>
                <ResourceList resources={filteredResources} />
            </Suspense>
            <div className="self-center mt-auto">
                {isTeacher && (
                    <Button
                        variant="list"
                        onClick={() => navigate("/resources/create")}
                    >
                        Create new resource
                    </Button>
                )}
            </div>
            {/* </div> */}
        </section>
    );
}