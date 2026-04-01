import { Link } from "react-router-dom";
import BrandLogo from "../components/BrandLogo";

const highlights = [
  "Trusted job posts from modern companies",
  "Simple applications with quick profile setup",
  "Smart matches for freshers and experienced talent",
];

const metrics = [
  { label: "Active Jobs", value: "1200+" },
  { label: "Hiring Companies", value: "480+" },
  { label: "Fast Applications", value: "24/7" },
];

export default function LandingPage() {
  return (
    <div className="page-shell">
      <header className="hero-header">
        <BrandLogo />
        <nav className="hero-nav">
          <Link to="/login" className="nav-btn nav-btn--ghost">
            Login
          </Link>
          <Link to="/signup" className="nav-btn nav-btn--solid">
            Sign Up
          </Link>
        </nav>
      </header>

      <main className="hero">
        <section className="hero__content">
          <p className="eyebrow">Job portal for modern careers</p>
          <h1>Discover the right job, build your future, and connect faster.</h1>
          <p className="hero__lead">
            Elevate Jobs helps job seekers explore verified openings, follow
            hiring trends, and apply with confidence from one clean platform.
          </p>

          <div className="hero__actions">
            <Link to="/signup" className="nav-btn nav-btn--solid nav-btn--large">
              Create Account
            </Link>
            <Link to="/login" className="nav-btn nav-btn--ghost nav-btn--large">
              Login
            </Link>
          </div>

          <div className="hero__highlights">
            {highlights.map((item) => (
              <div key={item} className="highlight-card">
                <span className="highlight-card__icon">*</span>
                <p>{item}</p>
              </div>
            ))}
          </div>
        </section>

        <section className="hero__visual">
          <div className="hero-showcase">
            <div className="hero-showcase__glow hero-showcase__glow--one" aria-hidden="true" />
            <div className="hero-showcase__glow hero-showcase__glow--two" aria-hidden="true" />

            <div className="hero-showcase__panel">
              <div className="hero-showcase__intro">
                <p className="hero-showcase__eyebrow">Career growth</p>
                <h2>One smart place to find jobs, companies, and your next move.</h2>
                <p className="hero-showcase__copy">
                  Explore verified opportunities, apply quickly, and stay focused
                  with a simple experience built for job seekers.
                </p>
              </div>

              <div className="hero-showcase__metrics">
                {metrics.map((metric) => (
                  <div key={metric.label} className="metric-card">
                    <strong>{metric.value}</strong>
                    <span>{metric.label}</span>
                  </div>
                ))}
              </div>
            </div>
          </div>
        </section>
      </main>
    </div>
  );
}
