import { Link } from "react-router";
import { Logo } from "../../shared/components/Logo";
import { useAuth } from "../../features/auth/AuthContext";
import { MenuFullName } from "../../shared/components/MenuFullName";

export default function Header() {
  const { user } = useAuth();

  const initial = user?.firstName?.charAt(0).toUpperCase();

  return (
    <header className="h-16 bg-background">
      <div className="mx-auto flex h-full max-w-7xl items-center justify-between px-6">
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
