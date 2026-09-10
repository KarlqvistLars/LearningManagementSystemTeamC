interface DisplayTextProps {
  text: string;
}

export function DisplayText({ text }: DisplayTextProps) {
  return <span className="text-lg text-primary-display-text">{text}</span>;
}
