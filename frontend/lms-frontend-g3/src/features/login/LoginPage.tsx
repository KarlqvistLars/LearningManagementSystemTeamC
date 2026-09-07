// export function LoginPage() {
//     return (
//         <section className="min-h-screen bg-slate-100 px-6 py-20">
//             <div className="mx-auto max-w-5xl">
//                 <h1 className="mb-6 text-4xl font-bold">
//                     Login
//                 </h1>
//                 <p className="text-lg">
//                     Här placeras inloggningsformuläret.
//                 </p>
//                 <form className="flex flex-col mt-6 *:gap-4 ">
//                     <div className="mb-4">
//                         <label htmlFor="username" className="block text-sm font-medium text-gray-700">
//                             Username</label>
//                         <input
//                             type="text"
//                             id="username"
//                             name="username"
//                             className="bg-white"
//                         />
//                         <label htmlFor="password" className="block text-sm font-medium text-gray-700">
//                             Password</label>
//                         <input
//                             type="password"
//                             id="password"
//                             name="password"
//                             className="bg-white"
//                         />
//                     </div>
//                 </form>
//             </div>
//         </section>
//     );
// }

// for demo

import { useState } from "react";
import { Navigate, useNavigate } from "react-router";

import { useAuth } from "../auth/AuthContext";

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
    <section className="min-h-screen bg-slate-100 px-6 py-20">
      <div className="mx-auto max-w-5xl">
        <h1 className="mb-6 text-center text-4xl font-bold !text-black">
          Login
        </h1>

        <p className="text-center text-lg text-black">
          Här placeras inloggningsformuläret.
        </p>

        <form
          onSubmit={handleSubmit}
          className="mx-auto mt-6 flex max-w-md flex-col gap-4"
        >
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
              className="w-full bg-white text-black"
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
              className="w-full bg-white text-black"
            />
          </div>

          {error && <p className="text-sm text-red-600">{error}</p>}

          <button
            type="submit"
            disabled={isLoading}
            className="rounded bg-blue-600 px-4 py-2 font-medium text-white hover:bg-blue-700 disabled:opacity-50"
          >
            {isLoading ? "Logging in..." : "Login"}
          </button>

          <button
            type="button"
            onClick={() => navigate("/register")}
            className="rounded bg-slate-700 px-4 py-2 font-medium text-white hover:bg-slate-800"
          >
            Register
          </button>
        </form>
      </div>
    </section>
  );
}
