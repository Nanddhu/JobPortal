import { Link } from "react-router-dom";
import BrandLogo from "./BrandLogo";

export default function AuthLayout({
  title,
  description,
  form,
  footerText,
  footerLink,
  footerLabel,
}) {
  return (
    <div className="page-shell auth-page">
      <div className="auth-card">
        <section className="auth-panel auth-panel--form">
          <div className="auth-panel__topbar">
            <Link to="/" className="back-chip" aria-label="Back to landing page">
              ←
            </Link>
            <BrandLogo compact />
          </div>

          <div className="auth-copy">
            <p className="eyebrow">Welcome to your next move</p>
            <h1>{title}</h1>
            <p>{description}</p>
          </div>

          {form}

          <p className="auth-footer">
            {footerText} <Link to={footerLink}>{footerLabel}</Link>
          </p>
        </section>

        <section className="auth-panel auth-panel--visual">
          <div className="visual-card">
            <p className="visual-card__eyebrow">Elevate your journey</p>
            <h2>
              A better way to explore jobs, build confidence, and move forward.
            </h2>
            <p>
              Stay focused with a simple portal designed for job seekers who want
              clarity, speed, and verified opportunities.
            </p>

            <div className="visual-feature-grid">
              <div className="visual-feature-card">
                <strong>Verified Jobs</strong>
                <span>Curated listings from trusted employers.</span>
              </div>
              <div className="visual-feature-card">
                <strong>Quick Access</strong>
                <span>Login or register and start applying faster.</span>
              </div>
            </div>

            <div className="visual-insight">
              <div className="visual-insight__metric">
                <strong>1200+</strong>
                <span>Openings available</span>
              </div>
              <div className="visual-insight__bars" aria-hidden="true">
                <span />
                <span />
                <span />
                <span />
              </div>
            </div>

            <div className="visual-note">
              Build your profile once, track opportunities smoothly, and keep your
              next career step organized.
            </div>
          </div>
        </section>
      </div>
    </div>
  );
}
