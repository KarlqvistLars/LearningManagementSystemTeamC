import { useState } from "react";
import { useNavigate } from "react-router";

import { apiFetch } from "../../api/client";

interface RegisterUserResponse {
  id: string;
  email: string;
  roleId: string;
  roleName: string;
}

export function RegisterPage() {
  const navigate = useNavigate();

  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [message, setMessage] = useState<string | null>(null);
  const [error, setError] = useState<string | null>(null);
  const [isLoading, setIsLoading] = useState(false);

  async function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault();

    setMessage(null);
    setError(null);
    setIsLoading(true);

    try {
      const response = await apiFetch<RegisterUserResponse>("/auth", {
        method: "POST",
        body: JSON.stringify({
          email,
          password,
        }),
      });

      if (!response.success) {
        setError(response.error.message);
        return;
      }

      setMessage(`Student account created for ${response.data.email}.`);

      setEmail("");
      setPassword("");
    } catch {
      setError("Unable to connect to the server.");
    } finally {
      setIsLoading(false);
    }
  }

  return (
    <section className="min-h-screen bg-slate-100 px-6 py-20">
      <div className="mx-auto max-w-5xl">
        <h1 className="mb-6 text-center text-4xl font-bold !text-black">
          Register
        </h1>

        <div className="mx-auto max-w-md">
          <form onSubmit={handleSubmit} className="flex flex-col gap-4">
            <div>
              <label
                htmlFor="email"
                className="block text-sm font-medium text-black"
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
                className="mt-1 w-full bg-white px-3 py-2 text-black"
              />
            </div>

            <div>
              <label
                htmlFor="password"
                className="block text-sm font-medium text-black"
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
                className="mt-1 w-full bg-white px-3 py-2 text-black"
              />
            </div>

            {error && <p className="text-sm text-red-600">{error}</p>}

            {message && <p className="text-sm text-green-600">{message}</p>}

            <button
              type="submit"
              disabled={isLoading}
              className="rounded bg-blue-600 px-4 py-2 font-medium text-white hover:bg-blue-700 disabled:opacity-50"
            >
              {isLoading ? "Registering..." : "Register"}
            </button>

            <button
              type="button"
              onClick={() => navigate("/login")}
              className="rounded bg-slate-700 px-4 py-2 font-medium text-white hover:bg-slate-800"
            >
              Back to Login
            </button>
          </form>
        </div>
      </div>
    </section>
  );
}
