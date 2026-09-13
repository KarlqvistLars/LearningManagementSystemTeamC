import { Outlet } from "react-router";
import { Header } from "../shared/components/Header";
import { Footer } from "../shared/components/Footer";
import { SideMenu } from "../shared/components/SideMenu";
import { useAuth } from "../features/auth/AuthContext";
import { studentMenuItems, teacherMenuItems } from "../shared/types/menu";

export function MainLayout() {
  const { isAuthenticated, isTeacher } = useAuth();

  const menuItems = isTeacher ? teacherMenuItems : studentMenuItems;

  return (
    <div className="flex h-screen flex-col overflow-hidden">
      <div className="shrink-0">
        <Header />
      </div>

      <div className="flex min-h-0 flex-1 overflow-hidden">
        {isAuthenticated && (
          <div className="shrink-0">
            <SideMenu menuItems={menuItems} />
          </div>
        )}

        <main className="min-h-0 min-w-0 flex-1 overflow-hidden">
          <Outlet />
        </main>
      </div>

      <div className="shrink-0">
        <Footer />
      </div>
    </div>
  );
}
