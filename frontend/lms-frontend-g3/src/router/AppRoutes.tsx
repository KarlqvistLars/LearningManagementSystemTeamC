import {
    Route,
    Routes,
} from 'react-router';

import { MainLayout } from '../layouts/MainLayout';
import { MainPage } from '../pages/MainPage';
import { CoursePage } from '../pages/CoursePage';
import { NotFoundPage } from '../pages/NotFoundPage';
import { LoginPage } from '../pages/LoginPage';

export function AppRoutes() {
    return (
        <Routes>
            <Route path="/" element={<MainLayout />}>
                <Route index element={<MainPage />} />
                <Route path="/courses" element={<CoursePage />} />
                <Route path="/login" element={<LoginPage />} />
            </Route>
            <Route path="*" element={<NotFoundPage />} />
        </Routes>
    );
}