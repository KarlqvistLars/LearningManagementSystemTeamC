import { DisplayText } from "./DisplayText";

export function Footer() {
  return (
    <footer className="bg-menu flex flex-col justify-between px-6 py-4 border border-border items-center">
      <DisplayText text="© 2026 Learning Portal TeamC. All rights reserved." />
      <DisplayText text="Learning Management System" />
    </footer>
  );
}
