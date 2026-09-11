import { Route, Routes } from "react-router";

import { MainLayout } from "../layouts/MainLayout";
import { MainPage } from "../features/MainPage";
import { CoursePage } from "../features/courses/pages/CoursesPage";
import { NotFoundPage } from "../features/not-found/NotFoundPage";
import { LoginPage } from "../features/auth/pages/LoginPage";
import { RegisterPage } from "../features/auth/pages/RegisterPage";
import { ModuleActivitiesPage } from "../features/activities/pages/ModuleActivitiesPage";
import { ProtectedRoute } from "../routes/ProtectedRoute";
import { ForgotPasswordPage } from "../features/auth/pages/ForgotPasswordPage";
import { ResetPasswordPage } from "../features/auth/pages/ResetPasswordPage";
import { UserPage } from "../features/users/pages/UserPage";
import { EditUserPage } from "../features/users/pages/EditUserPage";
import { CreateUserPage } from "../features/users/pages/CreateUserPage";

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
          <Route
            path="/modules/:moduleId/activities"
            element={<ModuleActivitiesPage />}
          />

          <Route path="users">
            <Route index element={<UserPage />} />
            <Route path="create" element={<CreateUserPage />} />
            <Route path=":userId/edit" element={<EditUserPage />} />
          </Route>
        </Route>
      </Route>

      <Route path="*" element={<NotFoundPage />} />
    </Routes>
  );
}
