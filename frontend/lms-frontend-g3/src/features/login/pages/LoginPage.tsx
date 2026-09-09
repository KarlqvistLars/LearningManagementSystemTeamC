import { useState } from "react";
import { Navigate, useNavigate } from "react-router";

import { useAuth } from "../../auth/AuthContext";
import { AuthTabs } from "../../auth/components/AuthTabs";

export function LoginPage() {
  const { login, isAuthenticated } = useAuth();
  const navigate = useNavigate();

  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState<string | null>(null);
  const [isLoading, setIsLoading] = useState(false);

  if (isAuthenticated) {
    return <Navigate to="/" replace />;
  }

  async function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault();

    setError(null);
    setIsLoading(true);

    try {
      const errorMessage = await login(email, password);

      if (errorMessage) {
        setError(errorMessage);
        return;
      }

      navigate("/", { replace: true });
    } catch {
      setError("Unable to connect to the server.");
    } finally {
      setIsLoading(false);
    }
  }

  return (
    <section className="flex-1 px-6 py-20">
      <div className="mx-auto w-full max-w-md">
        <AuthTabs active="login" />

        <div className="mt-8">
          <h1 className="mb-2 text-center text-4xl font-bold text-black">
            Login
          </h1>

          <p className="text-center text-lg text-black">
            Här placeras inloggningsformuläret.
          </p>

          <form
            onSubmit={handleSubmit}
            className="mt-6 flex w-full flex-col gap-4"
          >
            <div>
              <label
                htmlFor="email"
                className="mb-1 block text-sm font-medium text-black"
              >
                Email
              </label>

              <input
                type="email"
                id="email"
                name="email"
                value={email}
                onChange={(event) => setEmail(event.target.value)}
                required
                className="w-full rounded-md bg-white px-4 py-2 text-black"
              />
            </div>

            <div>
              <label
                htmlFor="password"
                className="mb-1 block text-sm font-medium text-black"
              >
                Password
              </label>

              <input
                type="password"
                id="password"
                name="password"
                value={password}
                onChange={(event) => setPassword(event.target.value)}
                required
                className="w-full rounded-md bg-white px-4 py-2 text-black"
              />
            </div>

            {error && <p className="text-sm text-red-600">{error}</p>}

            <button
              type="submit"
              disabled={isLoading}
              className="cursor-pointer rounded-md bg-primary px-4 py-2 font-medium text-black hover:brightness-95 disabled:cursor-not-allowed disabled:opacity-50"
            >
              {isLoading ? "Logging in..." : "Login"}
            </button>
          </form>
        </div>
      </div>
    </section>
  );
}
