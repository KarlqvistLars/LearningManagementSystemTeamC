import { useParams } from "react-router";
import { useState } from "react"
import { ModuleList } from "../components/modulesList";
import { ModuleForm } from "../components/moduleForm";
import type { Module } from "../types";
import type { User } from "../../users/types";
import ROLES from "../../auth/roleConstants";

const user: User | null = JSON.parse(localStorage.getItem("user") || "null");
const role = user?.roleName;
const isTeacher = role === ROLES.TEACHER;


export function ModulePage() {
    const { courseId } = useParams<{courseId: string}>();
    const [selectedModule, setSelectedModule] = useState<Module | null>(null);
    const [showModuleForm, setShowModuleForm] = useState(false);
    const [reloadList, setReloadList] = useState(0);

    if (!courseId) {
        return <div>Course not found</div>;
    }
    return(
        <section className="min-h-screen bg-slate-100 px-6 py-20">
              <div className="mx-auto max-w-5xl">
                <h1 className="mb-6 text-4xl font-bold text-slate-600">Moduler</h1>
                <ModuleList 
                    courseId={courseId} 
                    reloadList={reloadList}
                    onEditModule={(module) => {
                        setSelectedModule(module);
                        setShowModuleForm(true);
                    }}/>
              </div>
              <div>
                {isTeacher && (
                  <button 
                    onClick={() => {
                      setSelectedModule(null);
                      setShowModuleForm(true);}}
                    className="mt-16 px-4 py-2 bg-blue-500 text-white rounded hover:bg-blue-700 hover:cursor-pointer">
                    Create new module
                  </button>
                )}
                {showModuleForm && (
                <ModuleForm 
                    courseId={courseId}
                    module={selectedModule ?? undefined}
                    onModuleSaved={() => {
                        setReloadList((prev) => prev + 1);
                        setShowModuleForm(false);
                        setSelectedModule(null);
                    }}/>
                )}
              </div>
            </section>
    );
}