import { createContext, useContext, useState, type ReactNode } from "react";

import { apiFetch } from "../../api/client";
import type { LoginResult } from "./types";
import type { User } from "../users/types";

interface AuthContextType {
  user: User | null;
  isAuthenticated: boolean;
  login: (email: string, password: string) => Promise<string | null>;
  logout: () => void;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

interface AuthProviderProps {
  children: ReactNode;
}

export function AuthProvider({ children }: AuthProviderProps) {
  const [user, setUser] = useState<User | null>(() => {
    const storedUser = localStorage.getItem("user");

    return storedUser ? JSON.parse(storedUser) : null;
  });

  const isAuthenticated = user !== null;

  async function login(
    email: string,
    password: string,
  ): Promise<string | null> {
    const response = await apiFetch<LoginResult>("/auth/login", {
      method: "POST",
      body: JSON.stringify({
        email,
        password,
      }),
    });

    if (!response.success) {
      return response.error.message;
    }

    localStorage.setItem("access_token", response.data.accessToken);

    localStorage.setItem("user", JSON.stringify(response.data.user));

    setUser(response.data.user);

    return null;
  }

  function logout() {
    localStorage.removeItem("access_token");
    localStorage.removeItem("user");

    setUser(null);
  }

  return (
    <AuthContext.Provider
      value={{
        user,
        isAuthenticated,
        login,
        logout,
      }}
    >
      {children}
    </AuthContext.Provider>
  );
}

export function useAuth() {
  const context = useContext(AuthContext);

  if (!context) {
    throw new Error("useAuth must be used inside AuthProvider");
  }

  return context;
}
