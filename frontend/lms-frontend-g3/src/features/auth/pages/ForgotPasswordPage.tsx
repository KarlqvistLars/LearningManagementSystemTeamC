import { useState } from "react";
import { Link } from "react-router";

import { FormTitle } from "../../../shared/components/FormTitle";
import { DisplayText } from "../../../shared/components/DisplayText";
import { FormLabel } from "../../../shared/components/FormLabel";
import { FormInput } from "../../../shared/components/FormInput";
import { FormButton } from "../../../shared/components/FormButton";
import { forgotPassword } from "../api/api";

export function ForgotPasswordPage() {
  const [email, setEmail] = useState("");
  const [message, setMessage] = useState<string | null>(null);
  const [error, setError] = useState<string | null>(null);
  const [isLoading, setIsLoading] = useState(false);

  async function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault();

    setMessage(null);
    setError(null);
    setIsLoading(true);

    try {
      await forgotPassword(email);

      setMessage(
        "If an account exists with this email, a reset link has been sent.",
      );
      setEmail("");
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

  const isSubmitted = message !== null;

  return (
    <section className="flex flex-1 items-center justify-center px-6 py-20">
      <div className="mx-auto flex w-full max-w-md flex-col gap-8">
        <div className="flex flex-col gap-3 rounded-lg border border-border bg-menu px-10 py-10">
          <div className="flex flex-col gap-2">
            <FormTitle title="Forgot your password?" />
            <DisplayText text="Enter your email address and we'll send you a link to reset your password." />
          </div>

          <div className="min-h-5">
            {error && <p className="text-sm text-error">{error}</p>}
            {message && <p className="text-sm text-green-600">{message}</p>}
          </div>

          <form onSubmit={handleSubmit} className="flex w-full flex-col gap-10">
            <div className="text-primary-display-text">
              <FormLabel htmlFor="email">Email</FormLabel>
              <FormInput
                type="email"
                id="email"
                name="email"
                placeholder="Enter your email"
                value={email}
                onChange={(event) => setEmail(event.target.value)}
                disabled={isSubmitted}
                required
              />
            </div>

            <div className="flex flex-col gap-3">
              <FormButton
                type="submit"
                disabled={isLoading || isSubmitted}
                className="bg-primary text-black"
              >
                {isLoading ? "Sending..." : "Send reset link"}
              </FormButton>

              <Link
                to="/login"
                className="self-center text-sm text-primary-display-text hover:text-primary"
              >
                Back to login
              </Link>
            </div>
          </form>
        </div>
      </div>
    </section>
  );
}
