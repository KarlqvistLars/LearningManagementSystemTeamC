import type { ResourceWithCreatorDto } from "../types/interfaces";
import { useNavigate } from "react-router";
import { useAuth } from "../../auth/AuthContext";
import { Button } from "../../../shared/components/Button";
import { ListItemField } from "../../../shared/components/ListItemField";

interface ResourceSummaryCardProps {
    resource: ResourceWithCreatorDto;
}

export function ResourceSummaryCard({ resource }: ResourceSummaryCardProps) {
    const navigate = useNavigate();
    const { isTeacher } = useAuth();

    // resource created date
    // console.log(resource.resourceName.toString());
    // console.log(resource.createdDate.toString());

    return (
        <div className="flex items-center rounded-xl border border-border bg-menu px-4 py-3">
            {resource && (
                <>
                    <ListItemField
                        label="Name"
                        value={resource.resourceName}
                        className="flex-2"
                        link={`/resources/${resource.id}`}
                    />

                    <ListItemField
                        label="Created date"
                        value={new Date(resource.createdDate).toLocaleDateString()}
                        className="flex-2"
                    />

                    <div className="flex items-center gap-3">
                        {isTeacher && (
                            <Button
                                variant="list"
                                color="edit"
                                onClick={() => navigate(`/resources/${resource.id}/edit`)}
                            >
                                Edit
                            </Button>
                        )}
                        {isTeacher && (
                            <Button
                                variant="list"
                                color="delete"
                                onClick={() => alert(`Deleted resource ${resource.resourceName}\nWell NOT REALLY but just to show the functionality`)}
                            >
                                Delete
                            </Button>
                        )}
                    </div>
                </>
            )}
        </div>
    );
}
