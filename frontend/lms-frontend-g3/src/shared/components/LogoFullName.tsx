interface LogoFullNameProps {
  fullName: string;
}

export function LogoFullName({ fullName }: LogoFullNameProps) {
  return (
    <span className="text-lg font-medium text-primary-title-text">
      {fullName}
    </span>
  );
}
