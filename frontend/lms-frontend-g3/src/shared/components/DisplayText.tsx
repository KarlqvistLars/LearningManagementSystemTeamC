interface DisplayTextProps {
  text: string;
}

export function DisplayText({ text }: DisplayTextProps) {
  return (
    <span className="text-sm text-center text-primary-display-text">
      {text}
    </span>
  );
}
