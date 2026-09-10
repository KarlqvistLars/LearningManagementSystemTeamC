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
    label: "Students",
    path: "/students",
  },
  {
    label: "Courses",
    path: "/courses",
  },
  {
    label: "Students",
    path: "/students",
  },
  {
    label: "Assignments",
    path: "/assignments",
  },
];
