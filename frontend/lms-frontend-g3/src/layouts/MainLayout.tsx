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
    <div className="flex min-h-screen flex-col">
      <Header />

      <div className="flex flex-1">
        {isAuthenticated && <SideMenu menuItems={menuItems} />}

        <main className="flex-1">
          <Outlet />
        </main>
      </div>

      <Footer />
    </div>
  );
}
