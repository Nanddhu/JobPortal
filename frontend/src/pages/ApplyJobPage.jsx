import { useState } from "react";
import { Link, useLocation, useNavigate, useParams } from "react-router-dom";
import { applyToJob } from "../api";
import BrandLogo from "../components/BrandLogo";

export default function ApplyJobPage() {
  const { jobId } = useParams();
  const location = useLocation();
  const navigate = useNavigate();
  const [resumeName, setResumeName] = useState("");
  const [resumeFile, setResumeFile] = useState(null);
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [submitError, setSubmitError] = useState("");
  const job = location.state?.job;

  const handleResumeChange = (event) => {
    const file = event.target.files?.[0];
    setResumeFile(file || null);
    setResumeName(file ? file.name : "");
  };

  const handleApply = async (event) => {
    event.preventDefault();

    try {
      setIsSubmitting(true);
      setSubmitError("");

      const formData = new FormData();
      formData.append("jobTitle", job.title || "");
      formData.append("company", job.company || "");

      if (resumeFile) {
        formData.append("resume", resumeFile);
      }

      await applyToJob(formData);

      navigate("/jobs", {
        state: {
          appliedJobTitle: job?.title ?? "Job",
        },
      });
    } catch (err) {
      setSubmitError(err.message || "Failed to submit application.");
    } finally {
      setIsSubmitting(false);
    }
  };

  if (!job) {
    return (
      <div className="page-shell">
        <header className="hero-header jobs-page__header">
          <BrandLogo />
          <nav className="hero-nav">
            <Link to="/jobs" className="nav-btn nav-btn--solid">
              Back to Jobs
            </Link>
          </nav>
        </header>

        <section className="jobs-page__hero">
          <p className="eyebrow">Application</p>
          <h1>Job not found</h1>
          <p className="jobs-section__lead">
            This job posting was opened without job details. Please go back to the
            jobs page and open it again.
          </p>
        </section>
      </div>
    );
  }

  return (
    <div className="page-shell">
      <header className="hero-header jobs-page__header">
        <BrandLogo />
        <nav className="hero-nav">
          <Link to="/jobs" className="nav-btn nav-btn--ghost">
            Back to Jobs
          </Link>
        </nav>
      </header>

      <main className="apply-layout">
        <section className="apply-card">
          <p className="eyebrow">Apply now</p>
          <h1>{job.title}</h1>
          <p className="apply-card__company">
            {job.company} | {job.location}
          </p>

          <div className="apply-card__meta">
            <span>{job.type}</span>
            <span>{job.salary}</span>
          </div>

          <div className="job-card__tags">
            {(job.tags || []).map((tag) => (
              <span key={tag}>{tag}</span>
            ))}
          </div>

          <form className="apply-form" onSubmit={handleApply}>
            {submitError && <p className="jobs-error">{submitError}</p>}

            {!resumeFile && (
              <p className="apply-form__hint">
                Upload your resume before submitting the application.
              </p>
            )}

            <label className="field">
              <span>Upload Resume</span>
              <input
                id="resume-upload"
                className="resume-upload__input"
                type="file"
                accept=".pdf,.doc,.docx"
                onChange={handleResumeChange}
              />
              <label htmlFor="resume-upload" className="resume-upload">
                <span className="resume-upload__button">Choose Resume</span>
                <span className="resume-upload__text">
                  {resumeName || "PDF, DOC, or DOCX up to your preferred size"}
                </span>
              </label>
            </label>

            {resumeName && (
              <p className="apply-form__file">Selected file: {resumeName}</p>
            )}

            <button type="submit" className="submit-btn" disabled={isSubmitting}>
              {isSubmitting ? "Submitting..." : "Apply"}
            </button>
          </form>
        </section>

        <aside className="apply-sidecard">
          <p className="visual-card__eyebrow">Posting details</p>
          <h2>Complete your application in one step.</h2>
          <p>
            Upload your resume and submit your application. After applying, you
            will be redirected back to the job postings page.
          </p>
          <div className="visual-feature-grid">
            <div className="visual-feature-card">
              <strong>Role</strong>
              <span>{job.title}</span>
            </div>
            <div className="visual-feature-card">
              <strong>Company</strong>
              <span>{job.company}</span>
            </div>
          </div>
        </aside>
      </main>
    </div>
  );
}
