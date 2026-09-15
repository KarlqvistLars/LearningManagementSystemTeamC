import { useNavigate, useParams } from "react-router";
import { ModuleForm, type ModuleFormData } from "../components/ModuleForm";
import type { EditModule, Module } from "../types";
import { useEffect, useState } from "react";
import { editModule, fetchModuleById } from "../api/ModulesApi";
import { FormTitle } from "../../../shared/components/FormTitle";
import { ResponseMessage } from "../../../shared/components/ResponseMessage";

export function ModuleEditPage() {
    const { moduleId } = useParams();
    const navigate = useNavigate();
    const [module, setModule] = useState<Module | null>(null);
    const [message, setMessage] = useState("");
    const [messageType, setMessageType] = useState<"message" 
        | "error" | "success">("message");

    useEffect(() => {
        async function loadModule() {
            if (!moduleId) return;

            try {
                const fetchModule = await fetchModuleById(moduleId);
                setModule(fetchModule);
            } catch (error) {
                console.error("Failed to load module", error);
            }
        }

        loadModule();
    }, [moduleId]);
    
    async function handleEdit(data: ModuleFormData) {
        if (!module) return;

        try {
            const edit: EditModule = {
                id: module.id,
                name: data.name,
                description: data.description,
                startDate: data.startDate,
                endDate: data.endDate,
                courseId: module.courseId,
            };
            await editModule(edit);

            setMessage("Module edited successfully.");
            setMessageType("success");

        } catch (error) {
            console.error("Failed to edit module.", error);
            setMessage("Failed to edit module.");
            setMessageType("error");
        }
    }
    if (!module) {
        return <div>Loading...</div>
    }

    const values: ModuleFormData = {
        name: module.moduleName,
        description: module.description,
        startDate: new Date(module.startDate),
        endDate: new Date(module.endDate),
    };

    return (
        <section className="flex h-full flex-col gap-6 p-6">
            <div className="flex flex-1 flex-col gap-8 rounded-lg border border-border bg-menu px-10 py-10">
                <FormTitle title="Edit Module" />
                {module && (
                    <ModuleForm
                        values={values}
                        onSubmit={handleEdit}
                        submitLabel="Save"/>
                )}
            </div>

            {message &&
                <ResponseMessage
                    message={message}
                    type={messageType}
                    onClose={() => setMessage("")}/>
                }
        </section>
    )
};