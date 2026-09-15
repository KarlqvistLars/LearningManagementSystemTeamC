import { useNavigate, useParams } from "react-router";
import { useAuth } from "../../auth/AuthContext";
import { useEffect, useState } from "react";
import type { Module } from "../types"; 
import { fetchModuleById } from "../api/ModulesApi";
import { DisplayText } from "../../../shared/components/DisplayText";
import { Button } from "../../../shared/components/Button";

export function ModuleDetailsPage() {
    const { isTeacher } = useAuth();
    const { moduleId } = useParams<{ moduleId: string}>();
    const navigate = useNavigate();
    const [module, setModule] = useState<Module | null>(null);

    useEffect(() => {
        async function loadModule() {
            try {
                if (!moduleId) {
                    throw new Error("Module ID is required");
                }
                const fetchModule = await fetchModuleById(moduleId);
                setModule(fetchModule);
            } catch (error) {
                console.error(error);
            }
        }

        loadModule();
    }, [moduleId]);

    return (
        <section className="flex h-full flex-col gap-6 p-6">
              <div className="flex flex-1 flex-col gap-8 rounded-lg border border-border bg-menu px-10 py-10">
                <h1 className="uppercase">
                  <DisplayText text="Module Details" />
                </h1>
                {module && (
                  <>
                    <div className="grid grid-cols-1 gap-x-10 gap-y-5">
                      <div>
                        <span className="text-white mb-1 block text-base font-medium">
                          Name
                        </span>
                        <span className="text-lg w-full rounded-md bg-form-input px-4 py-3 text-primary-display-text block">
                          {module.moduleName}
                        </span>
                      </div>
        
                      <div>
                        <span className="text-white mb-1 block text-base font-medium">
                          Start date
                        </span>
                        <span className="text-lg w-full rounded-md bg-form-input px-4 py-3 text-primary-display-text block">
                          {new Date(module.startDate).toLocaleDateString()}
                        </span>
                      </div>
        
                      <div>
                        <span className="text-white mb-1 block text-base font-medium">
                          End date
                        </span>
                        <span className="text-lg w-full rounded-md bg-form-input px-4 py-3 text-primary-display-text block">
                          {new Date(module.endDate).toLocaleDateString()}
                        </span>
                      </div>
        
        
                      <div className="row-span-4">
                        <span className="text-white mb-1 block text-base font-medium">
                          Description
                        </span>
                        <span className="text-lg w-full rounded-md bg-form-input px-4 py-3 text-primary-display-text block">
                          {module.description}
                        </span>
                      </div>
                    </div>
                  </>
                )}
              </div>
              <div className="mt-auto flex justify-center gap-4 pt-10">
                {isTeacher && (
                  <Button
                    variant="list"
                    color="edit"
                    onClick={() => navigate(`/modules/${moduleId}/edit`)}>
                    Edit
                  </Button>
                )}
                <Button
                  variant="list"
                  color="cancel"
                  onClick={() => navigate(-1)}>
                  Back
                </Button>
              </div>
            </section>
    );
}