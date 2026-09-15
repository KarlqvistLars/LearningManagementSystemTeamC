export interface MenuItem {
  label: string;
  path: string;
}

export const studentMenuItems: MenuItem[] = [
  {
    label: "Dashboard",
    path: "/",
  },
  {
    label: "Users",
    path: "/users",
  },
  {
    label: "Courses",
    path: "/courses",
  },
  {
    label: "Assignments",
    path: "/assignments",
  },
  {
    label: "Chat",
    path: "/chat",
  },
];

export const teacherMenuItems: MenuItem[] = [
  {
    label: "Dashboard",
    path: "/",
  },
  {
    label: "Users",
    path: "/users",
  },
  {
    label: "Courses",
    path: "/courses",
  },
  {
    label: "Assignments",
    path: "/assignments",
  },
  {
    label: "Resources",
    path: "/resources",
  },
  {
    label: "Chat",
    path: "/chat",
  },
];
