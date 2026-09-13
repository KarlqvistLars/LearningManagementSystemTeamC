import type { Module } from "../types";
import { ModuleSummaryCard } from "./moduleSummaryCard";
import { fetchModules } from "../api/modules";
import { useEffect, useState } from "react"
import { useAuth } from "../../auth/AuthContext";



interface ModuleListProps {
    courseId: string;
    reloadList: number;
    onEditModule: (module: Module) => void;
}

export function ModuleList({ courseId, reloadList, onEditModule }: ModuleListProps){
    const [modules, setModules] = useState<Module[]>([]);
    const [error, setError] = useState<string | null>(null);
    const  { isTeacher } = useAuth();
    
    useEffect(() => {
        async function loadModules() {
            try {
                const modulesFetched = isTeacher ? await fetchModules(courseId) : [];
                
                setModules(modulesFetched);
            } catch (error) {
                console.error("Failed to load modules.", error);
                setError("Failed to load modules.");
            }
        }

        if (courseId) {
            loadModules();
        }
    }, [courseId, reloadList]);

    if (error) {
        return <p>{error}</p>
    }

    return (
        <div>
            {modules.map((module) => (
                <ModuleSummaryCard
                    key={module.id}
                    module={module}
                    onEdit={() => onEditModule(module)}/>
            ))}
        </div>
    );
}