import { Outlet } from 'react-router';
import { Header } from '../shared/components/Header';
import { Footer } from '../shared/components/Footer';

export function MainLayout() {
    return (
        <>
            <Header />
            <main>
                <Outlet />
            </main>
            <Footer />
        </>
    );
}