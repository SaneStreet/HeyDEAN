import {api} from "./api";

/**
 * Title: auth.ts
 * Content: Authentication response for logging in users
 * Functions:
 *  - loginRequest: requests the username and password for the auth/login endpoint, which returns a resposne if true
 */


interface LoginResponse {
  success: boolean;
  token?: string;
  userId?: string;
  message?: string;
  userName?: string;
}

export async function loginRequest(username: string, password: string): Promise<LoginResponse> {
  try {
    const res = await api.post("/auth/login", { username, password });

    return {
      success: true,
      token: res.data.token,
      userId: res.data.userId,
      userName: res.data.userName,
    };
  } catch (err: any) {
    return {
      success: false,
      message: err.response?.data || "Login failed",
    };
  }
}
