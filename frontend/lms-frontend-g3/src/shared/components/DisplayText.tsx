interface DisplayTextProps {
  text: string;
}

export function DisplayText({ text }: DisplayTextProps) {
  return <span className="text-base text-primary-display-text">{text}</span>;
}
