import type { Module } from "../types";
import { Link, useNavigate } from "react-router";
import { useAuth } from "../../auth/AuthContext";
import { Button } from "../../../shared/components/Button";
import { ListItemField } from "../../../shared/components/ListItemField";


interface ModuleSummaryCardProps {
  module: Module;
}

export function ModuleSummaryCard({ module }: ModuleSummaryCardProps) {
  const navigate = useNavigate();
  const { isTeacher } = useAuth();

  return (
    <div className="flex items-center rounded-xl border border-border bg-menu px-4 py-3">
      {module && (
        <>
          <ListItemField
            label="Name"
            value={module.moduleName}
            className="flex-2"
            link={`/modules/${module.id}`}
            />

          <ListItemField
            label="Start date"
            value={new Date(module.startDate).toDateString()}
            className="flex-2"/>

          <ListItemField
            label="End date"
            value={new Date(module.endDate).toDateString()}
            className="flex-2"/>
          
          <div className="flex items-center gap-3">
          {isTeacher && (
            <Link to={`/modules/${module.id}/edit`}>
              <Button
                variant="list"
                color="edit">
                  Edit
              </Button>
            </Link>
          )}
          {isTeacher && (
            <Button
              variant="list"
              color="resource"
              onClick={() => navigate(``)}>
                Resource
            </Button>
          )}
          {isTeacher && (
            <Button
              variant="list"
              color="resource"
              onClick={() => navigate(`/modules/${module.id}/activities`)}>
                Activities
            </Button>
          )}
          </div>
        </>
      )}
    </div>
  );
}
