import type { Module } from "../types";
import { Link } from "react-router";

interface ModuleListProps {
  modules: Module[];
}

export function ModuleList({ modules }: ModuleListProps) {
  if (modules.length === 0) {
    return (
      <div className="rounded-lg border border-dashed border-slate-300 p-8 text-center text-slate-500">
        No modules in this course yet.
      </div>
    );
  }

  return (
    <ul className="space-y-4">
      {modules.map((module) => (
        <li
          key={module.id}
          className="rounded-lg border border-slate-200 bg-white p-4 shadow-sm"
        >
          <Link to={`/modules/${module.id}/activities`}>
            <div className="flex items-center justify-between">
              <h3 className="text-lg font-semibold">{module.moduleName}</h3>
            </div>
            <p className="mt-2 text-sm text-slate-600">{module.description}</p>
            <p className="mt-2 text-xs text-slate-400">
              {new Date(module.startDate).toLocaleString()} -- {new Date(module.endDate).toLocaleString()}
            </p>
          </Link>
        </li>
      ))}
    </ul>
  );
}