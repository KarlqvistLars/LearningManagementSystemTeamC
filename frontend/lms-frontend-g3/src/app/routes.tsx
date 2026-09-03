import {
    Route,
    Routes,
} from 'react-router';

import { MainLayout } from '../layouts/MainLayout';
import { MainPage } from '../features/MainPage';
import { CoursePage } from '../features/courses/CoursePage';
import { NotFoundPage } from '../features/not-found/NotFoundPage';
import { LoginPage } from '../features/login/LoginPage';
import { ModuleActivitiesPage } from '../features/activities/pages/ModuleActivitiesPage';

export function AppRoutes() {
    return (
        <Routes>
            <Route path="/" element={<MainLayout />}>
                <Route index element={<MainPage />} />
                <Route path="/courses" element={<CoursePage />} />
                <Route path="/modules/:moduleId/activities" element={<ModuleActivitiesPage />} />
                <Route path="/login" element={<LoginPage />} />    
            </Route>
            <Route path="*" element={<NotFoundPage />} />
        </Routes>
    );
}