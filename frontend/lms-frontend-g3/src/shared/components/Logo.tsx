import { Link } from "react-router";
import { LogoFullName } from "./LogoFullName";

export function Logo() {
  return (
    <Link to="/" className="flex items-center gap-2">
      <img src="/src/assets/logo.svg" alt="LMS" className="h-15 w-15" />

      <LogoFullName fullName="Learning Management System" />
    </Link>
  );
}
