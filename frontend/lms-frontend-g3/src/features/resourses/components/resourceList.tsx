import { DisplayText } from "../../../shared/components/DisplayText";
import type { ResourceDto } from "../types/interfaces";
import { ResourceSummaryCard } from "./resourceSummaryCard";

interface ResourceListProps {
  resources: ResourceDto[];
}

export function ResourceList({ resources }: ResourceListProps) {
  if (resources.length === 0) {
    return <DisplayText text="No resources found." />;
  }

  return (
    <div className="flex flex-col gap-2">
      {resources.map((resource) => (
        <ResourceSummaryCard key={resource.id} resource={resource} />
      ))}
    </div>
  );
}
