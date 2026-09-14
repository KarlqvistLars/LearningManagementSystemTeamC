export type TextSize = "small" | "base" | "large";

interface DisplayTextProps {
  text: string;
  size?: TextSize;
}

export function DisplayText({ text, size = "base" }: DisplayTextProps) {
  const sizeClass = {
    small: "text-sm",
    base: "text-base",
    large: "text-lg",
  }[size];

  return (
    <span className={`${sizeClass} text-primary-display-text`}>{text}</span>
  );
}
