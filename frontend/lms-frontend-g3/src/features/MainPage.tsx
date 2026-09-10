import { useAuth } from "./auth/AuthContext";
import { SideMenu } from "../shared/components/SideMenu";
import {
  studentMenuItems,
  teacherMenuItems,
} from "../shared/components/types/menu";

export function MainPage() {
  const { isTeacher } = useAuth();

  const menuItems = isTeacher ? teacherMenuItems : studentMenuItems;

  return (
    <section className="flex">
      <SideMenu menuItems={menuItems} />
    </section>
  );
}
