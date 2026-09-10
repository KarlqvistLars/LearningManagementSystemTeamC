export type TagVariant =
  | "Student"
  | "Teacher"
  | "On-time"
  | "Late"
  | "Not-submitted";

interface TagProps {
  label: string;
  variant: TagVariant;
}

const tagStyles: Record<TagVariant, string> = {
  Student: "bg-tag-student/20 text-tag-student",
  Teacher: "bg-tag-teacher/20 text-tag-teacher",
  "On-time": "bg-tag-ontime/20 text-tag-ontime",
  Late: "bg-tag-late/20 text-tag-late",
  "Not-submitted": "bg-tag-unsubmitted/20 text-tag-unsubmitted",
};

export function Tag({ label, variant }: TagProps) {
  return (
    <span
      className={`w-fit rounded-md px-3 py-1 text-sm font-medium ${tagStyles[variant]}`}
    >
      {label}
    </span>
  );
}
