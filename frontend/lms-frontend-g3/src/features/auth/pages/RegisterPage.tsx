import { useState } from "react";

import { register } from "../api/api";
import { AuthTabs } from "../components/AuthTabs";
import { FormTitle } from "../../../shared/components/FormTitle";
import { DisplayText } from "../../../shared/components/DisplayText";
import { FormLabel } from "../../../shared/components/FormLabel";
import { FormInput } from "../../../shared/components/FormInput";
import { FormButton } from "../../../shared/components/FormButton";

export function RegisterPage() {
  const [email, setEmail] = useState("");
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");

  const [message, setMessage] = useState<string | null>(null);
  const [error, setError] = useState<string | null>(null);
  const [isLoading, setIsLoading] = useState(false);

  async function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault();

    setMessage(null);
    setError(null);

    if (password !== confirmPassword) {
      setError("Passwords do not match.");
      return;
    }

    setIsLoading(true);

    try {
      const result = await register({
        email,
        firstName,
        lastName,
        password,
      });

      setMessage(`Student account created for ${result.email}.`);

      setEmail("");
      setFirstName("");
      setLastName("");
      setPassword("");
      setConfirmPassword("");
    } catch (error) {
      setError(
        error instanceof Error
          ? error.message
          : "Unable to connect to the server.",
      );
    } finally {
      setIsLoading(false);
    }
  }

  return (
    <section className="flex flex-1 flex-col items-center justify-center px-6 py-20">
      <div className="mx-auto flex w-full max-w-md flex-col gap-8">
        <AuthTabs active="register" />

        <div className="flex flex-col gap-3 rounded-lg border border-border bg-menu px-10 py-10">
          <div className="flex flex-col gap-2">
            <FormTitle title="Create account" />
            <DisplayText text="Join the LMS platform" />
          </div>

          <div className="min-h-5">
            {error && <p className="text-sm text-error">{error}</p>}
            {message && <p className="text-sm text-green-600">{message}</p>}
          </div>

          <form onSubmit={handleSubmit} className="flex w-full flex-col gap-10">
            <div className="flex w-full flex-col gap-5">
              <div className="text-primary-display-text">
                <FormLabel htmlFor="firstName">First Name</FormLabel>
                <FormInput
                  type="text"
                  id="firstName"
                  name="firstName"
                  placeholder="Enter your first name"
                  value={firstName}
                  onChange={(event) => setFirstName(event.target.value)}
                  required
                />
              </div>

              <div className="text-primary-display-text">
                <FormLabel htmlFor="lastName">Last Name</FormLabel>
                <FormInput
                  type="text"
                  id="lastName"
                  name="lastName"
                  placeholder="Enter your last name"
                  value={lastName}
                  onChange={(event) => setLastName(event.target.value)}
                  required
                />
              </div>

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

              <div className="text-primary-display-text">
                <FormLabel htmlFor="confirmPassword">
                  Confirm Password
                </FormLabel>
                <FormInput
                  type="password"
                  id="confirmPassword"
                  name="confirmPassword"
                  placeholder="Repeat your password"
                  value={confirmPassword}
                  onChange={(event) => setConfirmPassword(event.target.value)}
                  required
                />
              </div>
            </div>

            <FormButton
              type="submit"
              disabled={isLoading}
              className="bg-primary text-black"
            >
              {isLoading ? "Creating account..." : "Create account"}
            </FormButton>
          </form>
        </div>
      </div>
    </section>
  );
}
