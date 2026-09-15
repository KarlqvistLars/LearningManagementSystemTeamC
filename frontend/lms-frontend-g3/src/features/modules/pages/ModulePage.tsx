import { useNavigate, useParams } from "react-router";
import { useEffect, useState } from "react"
import { ModuleList } from "../components/ModuleList";
import type { Course } from "../../courses/types";
import type { Module } from "../types";
import { useAuth } from "../../auth/AuthContext"; 
import { DisplayText } from "../../../shared/components/DisplayText";
import { fetchCourseById } from "../../courses/api/courses";
import { Button } from "../../../shared/components/Button";
import { SearchInput } from "../../../shared/components/SearchInput";


export function ModulePage() {
  const { courseId } = useParams<{courseId: string}>();
  const [ course, setCourse] = useState<Course | null>(null);
  const [searchTerm, setSearchTerm] = useState("");
  const search = searchTerm.toLowerCase();
  const [reloadList, setReloadList] = useState(0);
  const { isTeacher } = useAuth();
  const navigate = useNavigate();



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
                <h1 className="uppercase">
                  <DisplayText text={`${course?.courseName ?? "Course"} - Modules `}/>
                </h1>
                <SearchInput
                    value={searchTerm}
                    onChange={setSearchTerm}
                    placeholder="Search modules..."/>
                <ModuleList 
                    courseId={courseId} 
                    reloadList={reloadList}
                    searchTerm={searchTerm}
                    />
              <div className="self-center mt-auto">
                {isTeacher && (
                  <Button 
                    variant="list"
                    color="create"
                    onClick={() => navigate("create")}>
                    Create new module
                  </Button>
                )}
                </div>
            </section>
    );
}