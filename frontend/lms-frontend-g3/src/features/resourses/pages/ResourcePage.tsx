import { useParams } from "react-router";
import { ResourceForm } from "./ResourceEditPage";
import { DisplayText } from "../../../shared/components/DisplayText";
import { SearchInput } from "../../../shared/components/SearchInput";
import { Suspense } from "react";
import { CourseList } from "../../courses/components/courseList";
import { Button } from "../../../shared/components/Button";

export function ResourcePage() {
    const { moduleId } = useParams();
    // const { activities, loading, error } = useResources(moduleId);

    return (
        <section className="flex flex-col gap-6 p-6 h-full">
            <h1 className="uppercase">
                <DisplayText text="Courses" />
            </h1>
            <SearchInput
                value={searchTerm}
                onChange={setSearchTerm}
                placeholder="Search courses..."
            />
            <Suspense fallback={<DisplayText text="Loading courses..." />}>
                <CourseList courses={filteredCourses} />
            </Suspense>
            <div className="self-center mt-auto">
                {isTeacher && (
                    <Button
                        variant="list"
                        onClick={() => alert("Create new course clicked")}
                    >
                        Create new course
                    </Button>
                )}
            </div>
        </section>
    );

}

// <section className="min-h-screen bg-background px-6 py-20">
//     <div className="mx-auto max-w-5xl">
//         <h1 className="mb-6 text-primary text-4xl font-bold">Resources</h1>
//         <ResourceForm courseId={moduleId!} onResourceSaved={() => { }} />
//     </div>
// </section>