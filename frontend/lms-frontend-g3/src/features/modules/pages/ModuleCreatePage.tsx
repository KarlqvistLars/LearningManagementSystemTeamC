import { useNavigate, useParams } from "react-router";
import type { CreateModule } from "../types";
import { createModule } from "../api/ModulesApi";
import { DisplayText } from "../../../shared/components/DisplayText";
import { ModuleForm, type ModuleFormData } from "../components/ModuleForm";


export function ModuleCreatePage() {
    const { courseId } = useParams<{ courseId: string}>();
    const navigate = useNavigate();

    if (!courseId) {
        return <div>Course not found</div>
    }

    const handleCreate = async (data: ModuleFormData) => {
        const create: CreateModule = {
                name: data.name,
                description: data.description,
                startDate: data.startDate,
                endDate: data.endDate,
                courseId,
            };

            await createModule(create);
            
        navigate(`/courses/${courseId}/modules`);
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
        </section>
    );
}