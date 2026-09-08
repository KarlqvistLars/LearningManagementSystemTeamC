import type { User } from "../users/types";

export interface LoginResult {
  accessToken: string;
  expiresInMinutes: number;
  user: User;
}
