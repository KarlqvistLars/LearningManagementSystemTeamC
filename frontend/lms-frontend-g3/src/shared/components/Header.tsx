import { NavLink } from "react-router";

export function Header() {
  const linkClasses = ({ isActive }: { isActive: boolean }) =>
    [
      "rounded px-3 py-2 transition",
      isActive
        ? "bg-slate-800 text-white"
        : "text-slate-700 hover:bg-slate-200",
    ].join(" ");

  return (
    <header className="sticky top-0 z-50 bg-white shadow">
      <nav
        className="mx-auto flex max-w-7xl items-center justify-between px-6 py-4"
        aria-label="Huvudmeny"
      >
        <ul className="flex gap-3">
          <li>
            <NavLink to="/" end className={linkClasses}>
              Hem
            </NavLink>
          </li>

          <li>
            <NavLink to="/courses" className={linkClasses}>
              Kurser
            </NavLink>
          </li>
          {/* <li>
            <NavLink to="/login" className={linkClasses}>
              Login
            </NavLink>
          </li> */}
        </ul>
      </nav>
    </header>
  );
}
