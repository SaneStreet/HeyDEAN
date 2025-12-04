import { useState, type FormEvent } from "react";
import { useAuth } from "../context/AuthContext";
import { useNavigate } from "react-router-dom";

export default function LoginPage() {
  const { login } = useAuth();
  const navigate = useNavigate();

  const [username, setUsername] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [error, setError] = useState<string>("");

  async function handleSubmit(e: FormEvent) {
    e.preventDefault();
    setError("");

    const success = await login(username, password);

    if (success) {
      navigate("/dean");
    } else {
      setError("Forkert brugernavn eller kodeord.");
      console.log(error);
    }
  }

  return (
    <div style={{ maxWidth: "300px", margin: "50px auto", textAlign: "center" }}>
      <h2 className="pb-2.5 text-3xl uppercase font-mono">Login</h2>

      <form onSubmit={handleSubmit}>
        <input
          type="text"
          placeholder="Username"
          id="username"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
          className="w-full border border-white/50 m-2.5 p-2.5 rounded flex"
        />

        <input
          type="password"
          placeholder="Password"
          id="password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          className="w-full border border-white/50 m-2.5 p-2.5 rounded flex"
        />

        <button
          type="submit"
          id="login"
          className="w-full m-2.5 p-2.5"
        >
          Login
        </button>
      </form>

      {error && <p className="text-red-500 mt-2.5">{error}</p>}
    </div>
  );
}
