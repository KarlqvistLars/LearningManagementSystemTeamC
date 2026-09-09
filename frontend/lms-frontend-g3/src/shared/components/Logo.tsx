import { Link } from "react-router";

export function Logo() {
  return (
    <Link to="/" className="flex items-center gap-2">
      <img src="/src/assets/logo.svg" alt="LMS" className="h-15 w-15" />

      <span className="text-xl font-bold text-white">
        Learning Portal TeamC
      </span>
    </Link>
  );
}
