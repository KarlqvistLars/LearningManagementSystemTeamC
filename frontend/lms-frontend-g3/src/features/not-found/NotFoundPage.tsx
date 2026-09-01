import { Link } from 'react-router';

export function NotFoundPage() {
    return (
        <main className="flex min-h-screen items-center justify-center">
            <div className="text-center">
                <h1 className="text-6xl font-bold">404</h1>

                <p className="mt-4">
                    Sidan kunde inte hittas.
                </p>

                <Link
                    to="/"
                    className="mt-6 inline-block underline"
                >
                    Till startsidan
                </Link>
            </div>
        </main>
    );
}