import { NavLink } from "react-router";
import { useAuth } from "../../features/auth/AuthContext";
import type { MenuItem } from "../types/menu";

interface SideMenuProps {
  menuItems: MenuItem[];
}

export function SideMenu({ menuItems }: SideMenuProps) {
  const { logout } = useAuth();

  return (
    <nav className="flex w-48 flex-col border border-border bg-menu">
      <div className="flex flex-col gap-1 px-4 py-4">
        {menuItems.map((item) => (
          <NavLink
            key={item.path}
            to={item.path}
            end={item.path === "/"}
            className={({ isActive }) =>
              `relative rounded-xl border-l-4 px-6 py-2 text-sm font-medium transition ${
                isActive
                  ? "border-[#F0A04B] bg-[#2F2725] text-[#F0A04B]"
                  : "border-transparent text-primary-display-text hover:bg-[#2F2725]/50"
              }`
            }
          >
            {item.label}
          </NavLink>
        ))}
      </div>

      <div className="mt-auto border-t border-border px-4 py-4">
        <button
          type="button"
          onClick={logout}
          className="w-full cursor-pointer rounded-xl px-6 py-2 text-left text-sm font-medium text-display-text transition hover:bg-[#2F2725]/50  text-button-delete"
        >
          Logout
        </button>
      </div>
    </nav>
  );
}
