import type { User } from "../users/types/types";

export interface LoginResult {
  accessToken: string;
  expiresInMinutes: number;
  user: User;
}

export interface RegisterUserResponse {
  id: string;
  email: string;
  roleId: string;
  roleName: string;
}

export interface RegisterRequest {
  email: string;
  firstName: string;
  lastName: string;
  password: string;
}
