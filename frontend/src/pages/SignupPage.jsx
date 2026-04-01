import { useState } from "react";
import { Link } from "react-router-dom";
import { useNavigate } from "react-router-dom";
import { signupUser } from "../api";
import AuthLayout from "../components/AuthLayout";

export default function SignupPage() {
  const navigate = useNavigate();
  const [formData, setFormData] = useState({
    email: "",
    password: "",
    confirmPassword: "",
  });

  const passwordsMatch =
    !formData.confirmPassword || formData.password === formData.confirmPassword;

  const handleChange = (event) => {
    const { name, value } = event.target;
    setFormData((current) => ({ ...current, [name]: value }));
  };

  const handleSubmit = async (event) => {
    event.preventDefault();

    if (!passwordsMatch) return;

    try {
      await signupUser({
        email: formData.email,
        password: formData.password,
      });

      alert("User registered successfully ✅");

      navigate("/login");
    } catch (err) {
      console.error(err);
      alert("Signup failed ❌");
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
          placeholder="Create a password"
          value={formData.password}
          onChange={handleChange}
        />
      </label>

      <label className="field">
        <span>Confirm Password</span>
        <input
          type="password"
          name="confirmPassword"
          placeholder="Confirm your password"
          value={formData.confirmPassword}
          onChange={handleChange}
        />
      </label>

      {!passwordsMatch && (
        <p className="field-error">Passwords do not match.</p>
      )}

      <button type="submit" className="submit-btn" disabled={!passwordsMatch}>
        Sign Up
      </button>

      <p className="auth-note">
        By creating an account, you agree to our <Link to="/">Terms & Policy</Link>.
      </p>
    </form>
  );

  return (
    <AuthLayout
      title="Create your account"
      description="Join the portal to explore jobs, save opportunities, and apply in minutes."
      form={form}
      footerText="Already have an account?"
      footerLink="/login"
      footerLabel="Login"
    />
  );
}
