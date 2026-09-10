import ROLES from "../../features/auth/roleConstants";

interface RoleTagProps {
  roleName: string;
}

export function RoleTag({ roleName }: RoleTagProps) {
  const isTeacher = roleName === ROLES.TEACHER;

  return (
    <div className="flex flex-col gap-3">
      <p className="text-xs text-primary-title-text">Role</p>

      <span
        className={`w-fit rounded-md px-3 py-1 text-sm font-medium ${
          isTeacher
            ? "bg-tag-teacher/20 text-tag-teacher"
            : "bg-tag-student/20 text-tag-student"
        }`}
      >
        {roleName}
      </span>
    </div>
  );
}
