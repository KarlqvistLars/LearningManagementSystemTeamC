import { useState } from "react";
import { Navigate, useNavigate, Link } from "react-router";

import { useAuth } from "../AuthContext";
import { AuthTabs } from "../components/AuthTabs";
import { FormTitle } from "../../../shared/components/FormTitle";
import { DisplayText } from "../../../shared/components/DisplayText";
import { FormLabel } from "../../../shared/components/FormLabel";
import { FormInput } from "../../../shared/components/FormInput";
import { FormButton } from "../../../shared/components/FormButton";

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
    <section className="flex flex-1 items-center justify-center px-6 py-20">
      <div className="mx-auto w-full max-w-md flex flex-col gap-8">
        <AuthTabs active="login" />

        <div className="flex flex-col gap-3 px-10 py-10 bg-menu rounded-lg border border-border">
          <div className="flex flex-col gap-2">
            <FormTitle title="Welcome back" />
            <DisplayText text="Sign in to your account" />
          </div>

          <div className="min-h-5">
            {error && <p className="text-sm text-error">{error}</p>}
          </div>

          <form onSubmit={handleSubmit} className="flex w-full flex-col gap-10">
            <div>
              <div className="text-primary-display-text">
                <FormLabel htmlFor="email">Email</FormLabel>
                <FormInput
                  type="email"
                  id="email"
                  name="email"
                  placeholder="Enter your email"
                  value={email}
                  onChange={(event) => setEmail(event.target.value)}
                  required
                />
              </div>

              <div className="text-primary-display-text">
                <FormLabel htmlFor="password">Password</FormLabel>
                <FormInput
                  type="password"
                  id="password"
                  name="password"
                  placeholder="Enter your password"
                  value={password}
                  onChange={(event) => setPassword(event.target.value)}
                  required
                />
              </div>
            </div>

            <div className="flex flex-col gap-3">
              <FormButton
                type="submit"
                disabled={isLoading}
                className="bg-primary text-black"
              >
                {isLoading ? "Logging in..." : "Login"}
              </FormButton>

              <Link
                to="/forgot-password"
                className="self-center text-sm text-primary-display-text hover:text-primary"
              >
                Forgot password?
              </Link>
            </div>
          </form>
        </div>
      </div>
    </section>
  );
}
