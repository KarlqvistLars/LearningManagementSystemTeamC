import { Route, Routes } from "react-router";

import { MainLayout } from "../layouts/MainLayout";
import { MainPage } from "../features/MainPage";
import { CoursePage } from "../features/courses/pages/CoursesPage";
import { NotFoundPage } from "../features/not-found/NotFoundPage";
import { LoginPage } from "../features/auth/pages/LoginPage";
import { RegisterPage } from "../features/auth/pages/RegisterPage";
import { ModuleActivitiesPage } from "../features/activities/pages/ModuleActivitiesPage";
import { ProtectedRoute } from "../routes/ProtectedRoute";
import { ModulePage } from "../features/modules/pages/ModulePage";
import { ForgotPasswordPage } from "../features/auth/pages/ForgotPasswordPage";
import { ResetPasswordPage } from "../features/auth/pages/ResetPasswordPage";

export function AppRoutes() {
  return (
    <Routes>
      <Route element={<MainLayout />}>
        <Route path="/login" element={<LoginPage />} />
        <Route path="/register" element={<RegisterPage />} />
        <Route path="/forgot-password" element={<ForgotPasswordPage />} />
        <Route path="/reset-password" element={<ResetPasswordPage />} />
        <Route element={<ProtectedRoute />}>
          <Route path="/" element={<MainPage />} />
          <Route path="/courses" element={<CoursePage />} />
          <Route path="courses/:courseId/modules" element={<ModulePage />} />
          <Route
            path="/modules/:moduleId/activities"
            element={<ModuleActivitiesPage />}
          />
        </Route>{" "}
      </Route>

      <Route path="*" element={<NotFoundPage />} />
    </Routes>
  );
}
