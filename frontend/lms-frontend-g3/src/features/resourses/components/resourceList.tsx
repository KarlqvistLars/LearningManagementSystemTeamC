import { DisplayText } from "../../../shared/components/DisplayText";
import type { ResourceDto } from "../types";
import { ResourceSummaryCard } from "./resourceSummaryCard";

interface ResourceListProps {
    resources: ResourceDto[];
}

export function ResourceList({ resources }: ResourceListProps) {

    return (
        <ul className="space-y-4">
            <div className="flex flex-col gap-2">
                <div className="flex flex-col gap-2">
                    {resources.length > 0 ? (
                        resources.map((resource) => (
                            <ResourceSummaryCard key={resource.id} resource={resource} />
                        ))
                    ) : (
                        <DisplayText text="No resources found." />
                    )}
                </div>
            </div>
        </ul>
    );
}
