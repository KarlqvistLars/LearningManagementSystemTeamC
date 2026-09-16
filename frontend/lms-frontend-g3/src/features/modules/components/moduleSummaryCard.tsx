import type { Module } from "../types";
import { Link } from "react-router";
import { useAuth } from "../../auth/AuthContext";


interface ModuleSummaryCardProps {
  module: Module;
  onEdit?: (module: Module) => void;
}

export function ModuleSummaryCard({ module, onEdit }: ModuleSummaryCardProps) {
  const { isTeacher } = useAuth();
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
          <div className="flex gap-2">
            {isTeacher && (
              <button
                className="px-4 py-2 bg-blue-500 text-white rounded whitespace-nowrap hover:bg-blue-700 hover:cursor-pointer"
                onClick={() => onEdit?.(module)}
              >
                Edit
              </button>
            )}
            <Link
              to={`/modules/${module.id}/activities`}
              className="inline-flex items-center justify-center px-4 py-2 bg-slate-600 text-white rounded whitespace-nowrap hover:bg-slate-800"
            >
              Activities
            </Link>
          </div>
        </>
      )}
    </div>
  );
}
