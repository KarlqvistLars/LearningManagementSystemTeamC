interface GreetingTextProps {
  name?: string;
}

export function GreetingText({ name }: GreetingTextProps) {
  return (
    <p className="text-xl font-medium text-primary">Good morning, {name}</p>
  );
}
