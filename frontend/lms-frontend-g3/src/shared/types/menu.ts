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
    label: "Courses",
    path: "/courses",
  },
  {
    label: "Assignments",
    path: "/assignments",
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
];
