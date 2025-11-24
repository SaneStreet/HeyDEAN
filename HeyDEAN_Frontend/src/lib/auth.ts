import {api} from "./api";

interface LoginResponse {
  success: boolean;
  token?: string;
  userId?: string;
  message?: string;
}

export async function loginRequest(username: string, password: string): Promise<LoginResponse> {
  try {
    const res = await api.post("/auth/login", { username, password });

    return {
      success: true,
      token: res.data.token,
      userId: res.data.userId,
    };
  } catch (err: any) {
    return {
      success: false,
      message: err.response?.data || "Login failed",
    };
  }
}
