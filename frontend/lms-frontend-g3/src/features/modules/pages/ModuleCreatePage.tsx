import { useNavigate, useParams } from "react-router";
import type { CreateModule } from "../types";
import { createModule } from "../api/ModulesApi";
import { DisplayText } from "../../../shared/components/DisplayText";
import { ResponseMessage } from "../../../shared/components/ResponseMessage";
import { ModuleForm, type ModuleFormData } from "../components/moduleForm";
import { useState } from "react";

export function ModuleCreatePage() {
    const { courseId } = useParams<{ courseId: string}>();
    const [message, setMessage] = useState("");
    const [messageType, setMessageType] = useState<"message" 
        | "error" | "success">("message");
    const navigate = useNavigate();

    if (!courseId) {
        return <div>Course not found</div>
    }

    const handleCreate = async (data: ModuleFormData) => {
        try {
            const create: CreateModule = {
                name: data.name,
                description: data.description,
                startDate: data.startDate,
                endDate: data.endDate,
                courseId,
            };
    
            await createModule(create);
    
            setMessage("Module created successfully.");
            setMessageType("success");
            
            setTimeout(() => {
                navigate(`/courses/${courseId}/modules`);
            }, 3000);
            
        } catch (error) {
            console.error("Failed to create module", error);
            setMessage("Failed to create module.");
            setMessageType("error");
        }
    };

    return (
         <section className="flex h-full flex-col gap-6 p-6">
            <div className="flex flex-1 flex-col gap-8 rounded-lg border border-border bg-menu px-10 py-10">
                <DisplayText text="Create Module" />
                    <ModuleForm
                        onSubmit={handleCreate}
                        submitLabel="Create"
                    />
            </div>

            {message && 
                <ResponseMessage
                    message={message}
                    type={messageType}
                    onClose={() => setMessage("")}/>
            }
        </section>
    );
}