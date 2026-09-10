import { createContext, useContext, useState, type ReactNode } from "react";

import { login as loginApi } from "./api/api";
import type { User } from "../users/types";

interface AuthContextType {
  user: User | null;
  isAuthenticated: boolean;
  isTeacher: boolean;
  isStudent: boolean;
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
  const isTeacher = user?.roleName === "TEACHER";
  const isStudent = user?.roleName === "STUDENT";

  async function login(
    email: string,
    password: string,
  ): Promise<string | null> {
    try {
      const result = await loginApi(email, password);

      localStorage.setItem("access_token", result.accessToken);
      localStorage.setItem("user", JSON.stringify(result.user));

      setUser(result.user);

      return null;
    } catch (error) {
      return error instanceof Error
        ? error.message
        : "Unable to connect to the server.";
    }
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
        isTeacher,
        isStudent,
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
