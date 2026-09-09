import { useNavigate } from "react-router";

interface AuthTabsProps {
  active: "login" | "register";
}

export function AuthTabs({ active }: AuthTabsProps) {
  const navigate = useNavigate();

  return (
    <div className="flex w-full rounded-lg bg-menu p-2 border border-border">
      <button
        type="button"
        onClick={() => navigate("/login")}
        className={`hover:brightness-75 flex-1 basis-0 cursor-pointer rounded-md px-6 py-3 font-medium ${
          active === "login"
            ? "bg-primary text-black"
            : "text-white hover:bg-white/10"
        }`}
      >
        Login
      </button>

      <button
        type="button"
        onClick={() => navigate("/register")}
        className={`hover:brightness-75 flex-1 basis-0 cursor-pointer rounded-md px-6 py-3 font-medium ${
          active === "register"
            ? "bg-primary text-black"
            : "text-white hover:bg-white/10"
        }`}
      >
        Register
      </button>
    </div>
  );
}
