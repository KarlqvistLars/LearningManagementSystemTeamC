import type { Module } from "../types";
import { Link } from "react-router";
import type { User } from "../../users/types";
import ROLES from "../../auth/roleConstants";

const user: User | null = JSON.parse(localStorage.getItem("user") || "null");
const role = user?.roleName;
const isTeacher = role === ROLES.TEACHER;

interface ModuleSummaryCardProps {
  module: Module;
  onEdit?: (module: Module) => void;
}

export function ModuleSummaryCard({ module, onEdit }: ModuleSummaryCardProps) {
  return (
    <div className="w-full p-4 bg-gray-200 flex gap-4 align-items-start justify-between">
      {module && (
        <>
          <div className="w-5/6 text-left text-gray-600 flex gap-4">
            <div className="w-2/4">
              <p className="text-sm uppercase">Name</p>
              <Link to={`/courses/${module.id}`}>
                <p className="text-lg">{module.moduleName}</p>
              </Link>
            </div>
            <div className="w-1/4">
              <p className="text-sm uppercase">Start Date</p>
              <p className="text-lg">
                {new Date(module.startDate).toDateString()}
              </p>
            </div>
            <div className="w-1/4">
              <p className="text-sm uppercase">End Date</p>
              <p className="text-lg">
                {new Date(module.endDate).toDateString()}
              </p>
            </div>
          </div>
          {isTeacher && (
            <button
              className="w-1/6 max-w-25 h-fit px-4 py-2 bg-blue-500 text-white rounded hover:bg-blue-700 hover:cursor-pointer"
              onClick={() => onEdit?.(module)}
            >
              Edit
            </button>
          )}
        </>
      )}
    </div>
  );
}
