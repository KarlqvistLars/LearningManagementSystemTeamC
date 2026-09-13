import type { ApiResponse } from "../../../api/types";
import { apiFetch } from "../../../api/client";
import type {
  LoginResult,
  RegisterUserResponse,
  RegisterRequest,
} from "../types";

export async function login(
  email: string,
  password: string,
): Promise<LoginResult> {
  const result: ApiResponse<LoginResult> = await apiFetch<LoginResult>(
    "/auth/login",
    {
      method: "POST",
      body: JSON.stringify({
        email,
        password,
      }),
    },
  );

  if (!result.success) {
    throw new Error(result.error?.message || "Failed to login");
  }

  return result.data;
}

export async function register(
  request: RegisterRequest,
): Promise<RegisterUserResponse> {
  const result: ApiResponse<RegisterUserResponse> =
    await apiFetch<RegisterUserResponse>("/auth", {
      method: "POST",
      body: JSON.stringify(request),
    });

  if (!result.success) {
    throw new Error(result.error?.message || "Failed to register");
  }

  return result.data;
}

export async function forgotPassword(email: string): Promise<void> {
  const result: ApiResponse<void> = await apiFetch<void>(
    "/auth/forgot-password",
    {
      method: "POST",
      body: JSON.stringify({
        email,
      }),
    },
  );

  if (!result.success) {
    throw new Error(
      result.error?.message || "Failed to send password reset email",
    );
  }
}

export async function resetPassword(
  token: string,
  newPassword: string,
): Promise<void> {
  const result: ApiResponse<string> = await apiFetch<string>(
    "/auth/reset-password",
    {
      method: "POST",
      body: JSON.stringify({
        token,
        newPassword,
      }),
    },
  );

  if (!result.success) {
    throw new Error(result.error?.message || "Failed to reset password");
  }
}
