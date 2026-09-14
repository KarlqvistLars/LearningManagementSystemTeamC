import { useNavigate, useParams } from "react-router";
import { ModuleForm } from "../components/ModuleForm";
import type { Module } from "../types";
import { useEffect, useState } from "react";
import { fetchModulesById } from "../api/ModulesApi";
import { FormTitle } from "../../../shared/components/FormTitle";



export function ModuleEditPage() {
    const { moduleId } = useParams();
    const navigate = useNavigate();

    const [module, setModule] = useState<Module | null>(null);

    useEffect(() => {
        async function loadModule() {
            if (!moduleId) return;

            try {
                const fetchModule = await fetchModulesById(moduleId);
                setModule(fetchModule);
            } catch (error) {
                console.error("Failed to load module", error);
            }
        }

        loadModule();
    }, [moduleId]);
    
    function handleSaved() {
        if (module)
            navigate(`/courses/${module.courseId}/modules`);
    }
        return (
            <section className="flex h-full flex-col gap-6 p-6">
                <div className="flex flex-1 flex-col gap-8 rounded-lg border border-border bg-menu px-10 py-10">
                    <FormTitle title="Edit Module" />
                    {module && (
                        <ModuleForm
                            courseId={module.courseId}
                            module={module}
                            onModuleSaved={handleSaved}/>
                    )}
                </div>
            </section>
        )
};