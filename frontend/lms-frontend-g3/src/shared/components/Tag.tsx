export type TagVariant =
  | "Student"
  | "Teacher"
  | "On-time"
  | "Late"
  | "Not-submitted";

interface TagProps {
  title: string;
  label: string;
  variant: TagVariant;
  className?: string;
}

const tagStyles: Record<TagVariant, string> = {
  Student: "bg-tag-student/20 text-tag-student",
  Teacher: "bg-tag-teacher/20 text-tag-teacher",
  "On-time": "bg-tag-ontime/20 text-tag-ontime",
  Late: "bg-tag-late/20 text-tag-late",
  "Not-submitted": "bg-tag-unsubmitted/20 text-tag-unsubmitted",
};

export function Tag({ title, label, variant, className = "" }: TagProps) {
  return (
    <div className={`flex flex-col gap-3 ${className}`}>
      <p className="text-xs text-primary-title-text">{title}</p>

      <span
        className={`w-fit rounded-xl px-3 py-1 text-sm font-medium ${tagStyles[variant]}`}
      >
        {label}
      </span>
    </div>
  );
}
