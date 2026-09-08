// export function MainPage() {
//     return (
//         <>
//             <section className="min-h-screen bg-slate-100 px-6 py-20">
//                 <div className="mx-auto max-w-5xl">
//                     <h1 className="mb-6 text-5xl font-bold">
//                         Välkommen till Learning Management System
//                     </h1>
//                     <p className="text-xl leading-relaxed">
//                         Här kan du hitta information om kurser, utbildningar och arbetslivserfarenhet.
//                     </p>
//                 </div>
//             </section>
//         </>
//     );
// }

// for demo

import { useState } from "react";

import { apiFetch } from "../api/client";
import { useAuth } from "./auth/AuthContext";

interface CreateUserResponse {
  id: string;
  email: string;
  roleId: string;
  roleName: string;
}

export function MainPage() {
  const { user, logout } = useAuth();

  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [message, setMessage] = useState<string | null>(null);
  const [error, setError] = useState<string | null>(null);
  const [isLoading, setIsLoading] = useState(false);

  const isTeacher = user?.roleName === "Teacher";

  async function handleCreateTeacher(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault();

    setMessage(null);
    setError(null);
    setIsLoading(true);

    try {
      if (!user) {
        setError("You must be logged in.");
        return;
      }

      const response = await apiFetch<CreateUserResponse>("/users", {
        method: "POST",
        body: JSON.stringify({
          email,
          password,
          roleId: user.roleId,
        }),
      });

      if (!response.success) {
        setError(response.error.message);
        return;
      }

      setMessage(`Teacher account created for ${response.data.email}.`);

      setEmail("");
      setPassword("");
    } catch {
      setError("Unable to connect to the server.");
    } finally {
      setIsLoading(false);
    }
  }

  return (
    <section className="min-h-screen bg-slate-100 px-6 py-20 text-slate-900">
      <div className="mx-auto max-w-5xl">
        <div className="mb-10 text-center">
          <h1 className="mb-4 text-5xl font-bold !text-black">
            Hello, {user?.email}
          </h1>

          <p className="text-xl text-slate-700">
            Role:{" "}
            <span className="font-semibold text-slate-900">
              {user?.roleName}
            </span>
          </p>

          <button
            type="button"
            onClick={logout}
            className="mt-6 rounded bg-slate-700 px-4 py-2 text-white hover:bg-slate-800"
          >
            Log out
          </button>
        </div>

        {isTeacher && (
          <div className="mx-auto max-w-md rounded-lg bg-white p-6 shadow-lg">
            <h2 className="mb-3 text-2xl font-bold !text-slate-900">
              Create Teacher Account
            </h2>

            <p className="mb-6 text-slate-600">
              Since you are a teacher, you can create an account for another
              teacher.
            </p>

            <form
              onSubmit={handleCreateTeacher}
              className="flex flex-col gap-4"
            >
              <div>
                <label
                  htmlFor="teacher-email"
                  className="block text-sm font-medium text-slate-700"
                >
                  Email
                </label>

                <input
                  type="email"
                  id="teacher-email"
                  name="email"
                  value={email}
                  onChange={(event) => setEmail(event.target.value)}
                  required
                  className="mt-1 w-full rounded border border-slate-300 bg-white px-3 py-2 text-slate-900"
                />
              </div>

              <div>
                <label
                  htmlFor="teacher-password"
                  className="block text-sm font-medium text-slate-700"
                >
                  Password
                </label>

                <input
                  type="password"
                  id="teacher-password"
                  name="password"
                  value={password}
                  onChange={(event) => setPassword(event.target.value)}
                  required
                  className="mt-1 w-full rounded border border-slate-300 bg-white px-3 py-2 text-slate-900"
                />
              </div>

              {error && <p className="text-sm text-red-600">{error}</p>}

              {message && <p className="text-sm text-green-600">{message}</p>}

              <button
                type="submit"
                disabled={isLoading}
                className="rounded bg-blue-600 px-4 py-2 font-medium text-white hover:bg-blue-700 disabled:opacity-50"
              >
                {isLoading ? "Creating..." : "Create Teacher Account"}
              </button>
            </form>
          </div>
        )}
      </div>
    </section>
  );
}
