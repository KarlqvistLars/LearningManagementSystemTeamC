export function LoginPage() {
    return (
        <section className="min-h-screen bg-slate-100 px-6 py-20">
            <div className="mx-auto max-w-5xl">
                <h1 className="mb-6 text-4xl font-bold">
                    Login
                </h1>
                <p className="text-lg">
                    Här placeras inloggningsformuläret.
                </p>
                <form className="flex flex-col mt-6 *:gap-4 ">
                    <div className="mb-4">
                        <label htmlFor="username" className="block text-sm font-medium text-gray-700">
                            Username</label>
                        <input
                            type="text"
                            id="username"
                            name="username"
                            className="bg-white"
                        />
                        <label htmlFor="password" className="block text-sm font-medium text-gray-700">
                            Password</label>
                        <input
                            type="password"
                            id="password"
                            name="password"
                            className="bg-white"
                        />
                    </div>
                </form>
            </div>
        </section>
    );
}