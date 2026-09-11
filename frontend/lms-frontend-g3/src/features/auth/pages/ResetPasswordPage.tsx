import { useState } from "react";
import { Link } from "react-router";

import { resetPassword } from "../api/api";
import { FormTitle } from "../../../shared/components/FormTitle";
import { DisplayText } from "../../../shared/components/DisplayText";
import { FormLabel } from "../../../shared/components/FormLabel";
import { FormInput } from "../../../shared/components/FormInput";
import { Button } from "../../../shared/components/Button";

export function ResetPasswordPage() {
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");

  const [message, setMessage] = useState<string | null>(null);
  const [error, setError] = useState<string | null>(null);
  const [isLoading, setIsLoading] = useState(false);

  const token = new URLSearchParams(window.location.search).get("token");

  async function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault();

    setMessage(null);
    setError(null);

    if (!token) {
      setError("Invalid or missing reset token.");
      return;
    }

    if (password !== confirmPassword) {
      setError("Passwords do not match.");
      return;
    }

    setIsLoading(true);

    try {
      await resetPassword(token, password);

      setMessage("Your password has been reset successfully.");

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
    <section className="flex flex-1 items-center justify-center px-6 py-20">
      <div className="mx-auto flex w-full max-w-md flex-col gap-8">
        <div className="flex flex-col gap-3 rounded-lg border border-border bg-menu px-10 py-10">
          <div className="flex flex-col gap-2">
            <FormTitle title="Reset your password" />
            <DisplayText text="Enter your new password below." />
          </div>

          <div className="min-h-5">
            {error && <p className="text-sm text-error">{error}</p>}
            {message && <p className="text-sm text-green-600">{message}</p>}
          </div>

          <form onSubmit={handleSubmit} className="flex w-full flex-col gap-10">
            <div className="flex w-full flex-col gap-5">
              <div className="text-primary-display-text">
                <FormLabel htmlFor="password">New Password</FormLabel>
                <FormInput
                  type="password"
                  id="password"
                  name="password"
                  placeholder="Enter your new password"
                  value={password}
                  onChange={(event) => setPassword(event.target.value)}
                  disabled={message !== null}
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
                  placeholder="Repeat your new password"
                  value={confirmPassword}
                  onChange={(event) => setConfirmPassword(event.target.value)}
                  disabled={message !== null}
                  required
                />
              </div>
            </div>

            <div className="flex flex-col gap-3">
              <Button
                type="submit"
                disabled={isLoading || message !== null}
                variant="form"
                color="create"
              >
                {isLoading ? "Resetting password..." : "Reset password"}
              </Button>

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
