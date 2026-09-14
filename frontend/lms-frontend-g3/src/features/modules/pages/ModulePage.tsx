import { useParams } from "react-router";
import { useEffect, useState } from "react"
import { ModuleList } from "../components/ModuleList";
import { ModuleForm } from "../components/ModuleForm";
import type { Module } from "../types";
import type { Course } from "../../courses/types";
import { useAuth } from "../../auth/AuthContext"; 
import { DisplayText } from "../../../shared/components/DisplayText";
import { fetchCourseById } from "../../courses/api/courses";
import { Button } from "../../../shared/components/Button";


export function ModulePage() {
  const { courseId } = useParams<{courseId: string}>();
  const [ course, setCourse] = useState<Course | null>(null);
  const [selectedModule, setSelectedModule] = useState<Module | null>(null);
  const [showModuleForm, setShowModuleForm] = useState(false);
  const [reloadList, setReloadList] = useState(0);
  const { isTeacher } = useAuth();

  useEffect(() => {
    if (!courseId) return;

    fetchCourseById(courseId)
        .then(setCourse)
        .catch(console.error);
  }, [courseId]);

    if (!courseId) {
        return <div>Course not found</div>;
    }
    return(
        <section className="flex h-full flex-col gap-6 p-6">
              <div className="flex flex-1 flex-col gap-8 rounded-lg border border-border bg-menu px-10 py-10">
                <h1 className="mb-6 uppercase">
                  <DisplayText text={course?.courseName ?? "Course"}/>
                </h1>
                <h2 className="mb-6 uppercase">
                  <DisplayText text="Moduler"/>
                </h2>
                <ModuleList 
                    courseId={courseId} 
                    reloadList={reloadList}
                    />
              </div>
              <div className="flex justify-center align-items-center mt-6">
                {isTeacher && (
                  <Button 
                    variant="list"
                    color="create"
                    onClick={() => {
                      setSelectedModule(null);
                      setShowModuleForm(true);}}>
                    Create new module
                  </Button>
                )}
                {isTeacher && showModuleForm && (
                  <Button 
                    variant="list"
                    color="cancel"
                    onClick={() => {
                      setSelectedModule(null);
                      setShowModuleForm(false);
                    }}>
                    Cancel
                  </Button>
                )}
                </div>
                <div className="mt-6">
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