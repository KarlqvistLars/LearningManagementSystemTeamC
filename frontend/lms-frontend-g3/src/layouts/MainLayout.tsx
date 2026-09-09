import { Outlet } from "react-router";
import { Header } from "../shared/components/Header";
import { Footer } from "../shared/components/Footer";

export function MainLayout() {
  return (
    <div className="flex min-h-screen flex-col">
      <Header />

      <main className="flex flex-1 flex-col">
        <Outlet />
      </main>

      <Footer />
    </div>
  );
}
