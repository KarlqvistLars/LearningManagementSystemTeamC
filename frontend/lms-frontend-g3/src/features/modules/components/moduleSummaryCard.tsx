import type { Module } from "../types";
import { Link } from "react-router";
import { useAuth } from "../../auth/AuthContext";
import { Button } from "../../../shared/components/Button";


interface ModuleSummaryCardProps {
  module: Module;
}

export function ModuleSummaryCard({ module }: ModuleSummaryCardProps) {
  const { isTeacher } = useAuth();
  return (
    <div className="w-full p-7 bg-menu flex gap-4 align-items-start justify-between border border-border rounded-xl">
      {module && (
        <>
          <div className="w-5/6 text-left text-gray-600 flex gap-4">
            <div className="w-2/4">
              <p className="text-xs uppercase text-primary-title-text mb-4">
                Name
              </p>
              <Link to={`/courses/${module.id}`}>
                <p className="text-lg">{module.moduleName}</p>
              </Link>
            </div>
            <div className="w-1/4">
              <p className="text-xs uppercase text-primary-title-text mb-4">
                Start Date
              </p>
              <p className="text-lg">
                {new Date(module.startDate).toDateString()}
              </p>
            </div>
            <div className="w-1/4">
              <p className="text-xs uppercase text-primary-title-text mb-4">
                End Date
              </p>
              <p className="text-lg">
                {new Date(module.endDate).toDateString()}
              </p>
            </div>
          </div>
          {isTeacher && (
            <Link to={`/modules/${module.id}/edit`}>
              <Button
                variant="list"
                color="edit">
                  Edit
              </Button>
            </Link>
          )}
        </>
      )}
    </div>
  );
}
