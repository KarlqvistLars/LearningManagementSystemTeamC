import { Route, Routes } from "react-router";

import { MainLayout } from "../layouts/MainLayout";
import { MainPage } from "../features/MainPage";
import { CoursePage } from "../features/courses/pages/CoursePage";
import { NotFoundPage } from "../features/not-found/NotFoundPage";
import { LoginPage } from "../features/login/LoginPage";
import { RegisterPage } from "../features/register/RegisterPage";
import { ProtectedRoute } from "../routes/ProtectedRoute";

export function AppRoutes() {
  return (
    <Routes>
      <Route path="/login" element={<LoginPage />} />
      <Route path="/register" element={<RegisterPage />} />

      <Route element={<ProtectedRoute />}>
        <Route path="/" element={<MainLayout />}>
          <Route index element={<MainPage />} />

          <Route path="courses" element={<CoursePage />} />
        </Route>
      </Route>

      <Route path="*" element={<NotFoundPage />} />
    </Routes>
  );
}
