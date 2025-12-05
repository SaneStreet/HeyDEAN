import { useState, type FormEvent } from "react";
import { useAuth } from "../context/AuthContext";
import { Link, useNavigate } from "react-router-dom";

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

  async function handleRegisterClick() {
    navigate("/register");
  }

  return (
    <div className="max-w-80 m-auto mb-14 mt-14 text-center">
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

      {/* Register Button */}
        <div className="mt-6 text-center">
           <div className="h-px w-48 bg-linear-to-r from-transparent via-green-400 to-transparent mx-auto mb-4"></div>
          <p className="mb-4 mt-8">Don't have an account?</p>
          <button
            onClick={handleRegisterClick}
            className="w-full m-2.5 py-3 bg-white/10 border border-white/20  font-semibold rounded-lg hover:bg-white/20 transition-all duration-200"
          >
            Register
          </button>
        </div>
        {/* Alternative Link to /register */}
        <div className="mt-4 text-center">
          <Link 
            to="/register" 
            className="w-full m-2.5"
          >
            Or register here →
          </Link>
        </div>

      {error && <p className="text-red-500 mt-2.5">{error}</p>}
    </div>
  );
}
