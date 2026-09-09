import { Link } from "react-router";
import { Logo } from "../../shared/components/Logo";
import { useAuth } from "../../features/auth/AuthContext";

export default function Header() {
  const { user } = useAuth();

  const initial = user?.firstName?.charAt(0).toUpperCase();

  return (
    <header className="h-16 bg-[#141720]">
      <div className="mx-auto flex h-full max-w-7xl items-center justify-between px-6">
        <Logo />

        {user && (
          <Link to="/profile" className="flex items-center gap-3 text-white">
            <div className="flex h-8 w-8 items-center justify-center rounded-full bg-[#F0A04B] font-semibold text-[#141720]">
              {initial}
            </div>

            <span className="text-sm font-medium">
              {user.firstName} {user.lastName}
            </span>
          </Link>
        )}
      </div>
    </header>
  );
}
