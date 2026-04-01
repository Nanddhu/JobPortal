import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { loginUser } from "../api";
import AuthLayout from "../components/AuthLayout";

export default function LoginPage() {
  const navigate = useNavigate();
  const [formData, setFormData] = useState({
    email: "",
    password: "",
  });

  const handleChange = (event) => {
    const { name, value } = event.target;
    setFormData((current) => ({ ...current, [name]: value }));
  };

  const handleSubmit = async (event) => {
    event.preventDefault();

    try {
      const data = await loginUser({
        email: formData.email,
        password: formData.password,
      });

      const token =
        data?.token ||
        data?.data?.token ||
        data?.jwtToken ||
        data?.accessToken;

      if (!token) {
        throw new Error("Login succeeded but token was not returned by the backend.");
      }

      localStorage.setItem("token", token);
      navigate("/jobs");
    } catch (err) {
      console.error(err);
      alert(err.message || "Login failed");
    }
  };

  const form = (
    <form className="auth-form" onSubmit={handleSubmit}>
      <label className="field">
        <span>Email</span>
        <input
          type="email"
          name="email"
          placeholder="Enter your email"
          value={formData.email}
          onChange={handleChange}
        />
      </label>

      <label className="field">
        <span>Password</span>
        <input
          type="password"
          name="password"
          placeholder="Enter your password"
          value={formData.password}
          onChange={handleChange}
        />
      </label>

      <button type="submit" className="submit-btn">
        Login
      </button>

      <p className="auth-note">
        New here? <Link to="/signup">Create an account</Link>.
      </p>
    </form>
  );

  return (
    <AuthLayout
      title="Welcome back"
      description="Log in to continue your job search and track your latest applications."
      form={form}
      footerText="Need an account?"
      footerLink="/signup"
      footerLabel="Sign Up"
    />
  );
}
