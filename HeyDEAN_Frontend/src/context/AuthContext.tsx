import { createContext, useContext, useEffect, useState, type ReactNode } from "react";

/**
 * Title: AuthContext.tsx
 * Content: Context for handling login, logout, token generation and Authentication usage
 * Functions:
 *  - export AuthProvider: Gets token from JWT. Logs in users on token auth, saves token as localStorage. Logs out users and clears localStorage for tokens, etc.
 *  - login: Auths token from body as json, then in response sets tokens and others for localStorage
 *  - logout: Clears localStorage for token, sets local token/username to null.
 *  - useEffect username: Fetches stored username, and sets local username (randomGreeting)
 *  - useAuth: Checks for correct context usage.
 */

interface AuthContextType {
  token: string | null;
  username: string | null | undefined;
  login: (username: string, password: string) => Promise<boolean>;
  logout: () => void;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

interface AuthProviderProps {
  children: ReactNode;
}

export function AuthProvider({ children }: AuthProviderProps) {
  const [token, setToken] = useState<string | null>(
    localStorage.getItem("token")
  );
  const [username, setUsername] = useState<string | null | undefined>();

  useEffect(() => {
    const storedUsername = localStorage.getItem("username");
    if (storedUsername) {
      setUsername(storedUsername);
    }
  }, []);

  async function login(userName: string, password: string): Promise<boolean> {
    try {
      const res = await fetch("/api/auth/login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ userName, password }),
      });

      if (!res.ok) return false;

      const data = await res.json();
      console.log("Login Response:", data);
      console.log("data.username:", data.userName);  // ADD THIS
      console.log("typeof data.username:", typeof data.userName);  // ADD THIS

      localStorage.setItem("token", data.token);
      localStorage.setItem("userId", data.userId);
      localStorage.setItem("refreshToken", data.refreshToken);
      localStorage.setItem("username", data.userName);

      if (data.userName) {  // ADD THIS CHECK
        localStorage.setItem("username", data.userName);
        setUsername(data.userName);
        console.log("Username saved:", data.userName);  // ADD THIS
      } else {
        console.log("No username found in response");  // ADD THIS
        localStorage.setItem("username", "");
        setUsername("");
      }

      setToken(data.token);
      setUsername(data.userName);

      return true;
    } catch (err) {
      console.error("Login error:", err);
      return false;
    }
  }

  function logout() {
    localStorage.clear();
    setToken(null);
    setUsername(null);
  }

  return (
    <AuthContext.Provider value={{ token, login, username, logout }}>
      {children}
    </AuthContext.Provider>
  );
}

export function useAuth(): AuthContextType {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error("useAuth must be used inside AuthProvider");
  }
  return context;
}
