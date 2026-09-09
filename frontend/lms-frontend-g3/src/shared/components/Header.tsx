import { Link } from "react-router";
import { Logo } from "./Logo";
import { useAuth } from "../../features/auth/AuthContext";
import { MenuFullName } from "./MenuFullName";

export function Header() {
  const { user } = useAuth();

  const initial = user?.firstName?.charAt(0).toUpperCase();

  return (
    <header className="px-6 py-1 bg-menu">
      <div className="mx-auto flex h-full items-center justify-between">
        <Logo />

        {user && (
          <Link
            to="/profile"
            className="flex items-center gap-3 text-primary-title-text"
          >
            <div className="flex h-8 w-8 items-center justify-center rounded-full bg-primary font-semibold text-background">
              {initial}
            </div>

            <MenuFullName firstName={user.firstName} lastName={user.lastName} />
          </Link>
        )}
      </div>
    </header>
  );
}
