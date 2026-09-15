import type { Module } from "../types";
import { ModuleSummaryCard } from "./ModuleSummaryCard";
import { fetchModules } from "../api/ModulesApi";
import { useEffect, useState } from "react"
import { useAuth } from "../../auth/AuthContext";



interface ModuleListProps {
    courseId: string;
    searchTerm: string;
}

export function ModuleList({ courseId, searchTerm}: ModuleListProps){
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
    }, [courseId,  isTeacher]);

    const filteredModules = modules.filter((module) =>
        module.moduleName.toLowerCase().includes(searchTerm.toLowerCase()) ||
        module.description.toLowerCase().includes(searchTerm.toLowerCase()))
        .sort((a, b) =>
            new Date(a.startDate).getTime() -
             new Date(b.startDate).getTime()
        );

    if (error) {
        return <p>{error}</p>
    }

    return (
        <div className="flex flex-col gap-2">
            {filteredModules.map((module) => (
                <ModuleSummaryCard
                    key={module.id}
                    module={module}/>
            ))}
        </div>
    );
}