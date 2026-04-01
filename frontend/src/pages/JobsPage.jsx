import { useEffect, useState } from "react";
import { Link, useLocation } from "react-router-dom";
import { fetchJobs } from "../api";
import BrandLogo from "../components/BrandLogo";

export default function JobsPage() {
  const location = useLocation();
  const [keyword, setKeyword] = useState("");
  const [jobs, setJobs] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState("");

  useEffect(() => {
    let isMounted = true;

    async function loadJobs() {
      try {
        setIsLoading(true);
        setError("");
        const data = await fetchJobs();

        if (isMounted) {
          setJobs(data);
        }
      } catch (err) {
        if (isMounted) {
          setError(err.message || "Failed to load jobs.");
        }
      } finally {
        if (isMounted) {
          setIsLoading(false);
        }
      }
    }

    loadJobs();

    return () => {
      isMounted = false;
    };
  }, []);

  const handleSearch = async () => {
    try {
      setIsLoading(true);
      setError("");

      const searchValue = keyword.trim() || "developer";
      const data = await fetchJobs(searchValue);
      setJobs(data);
    } catch (err) {
      setError(err.message || "Failed to search jobs.");
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className="page-shell">
      <header className="hero-header jobs-page__header">
        <BrandLogo />
        <nav className="hero-nav">
          <Link to="/" className="nav-btn nav-btn--ghost">
            Home
          </Link>
          <Link to="/login" className="nav-btn nav-btn--solid">
            Logout
          </Link>
        </nav>
      </header>

      <section className="jobs-page__hero">
        <p className="eyebrow">Dashboard</p>
        <h1>Your job listings dashboard</h1>
        <p className="jobs-section__lead">
          Search job postings by keyword and browse openings fetched from your
          backend API.
        </p>
      </section>

      <section className="jobs-section jobs-section--dashboard">
        {location.state?.appliedJobTitle && (
          <div className="jobs-success">
            Application submitted for {location.state.appliedJobTitle}.
          </div>
        )}

        <div className="jobs-section__header">
          <div>
            <p className="eyebrow">Search postings</p>
            <h2>Find jobs by keyword.</h2>
          </div>

          <form className="jobs-search" onSubmit={(event) => event.preventDefault()}>
            <input
              type="text"
              placeholder="Search jobs by keyword"
              value={keyword}
              onChange={(event) => setKeyword(event.target.value)}
              onKeyDown={(event) => {
                if (event.key === "Enter") handleSearch();
              }}
            />
            <button type="button" className="jobs-search__button" onClick={handleSearch}>
              Search
            </button>
          </form>
        </div>

        {error && <p className="jobs-error">{error}</p>}

        {isLoading && <p className="jobs-loading">Loading job postings...</p>}

        {!isLoading && !error && (
          <div className="jobs-grid">
            {jobs.map((job) => (
              <Link
                key={job.id}
                to={`/jobs/${job.id}`}
                state={{ job }}
                className="job-card job-card--link"
              >
                <div className="job-card__top">
                  <div>
                    <p className="job-card__company">{job.company || "Unknown Company"}</p>
                    <h3>{job.title || "No title"}</h3>
                  </div>
                  <span className="job-card__type">{job.type}</span>
                </div>

                <div className="job-card__meta">
                  <span>{job.location || "Unknown location"}</span>
                  <span>{job.salary || "Not disclosed"}</span>
                </div>

                <div className="job-card__tags">
                  {(job.tags || []).map((tag) => (
                    <span key={tag}>{tag}</span>
                  ))}
                </div>
              </Link>
            ))}
          </div>
        )}

        {!isLoading && !error && jobs.length === 0 && (
          <p className="jobs-empty">
            No job postings matched your keyword. Try another search term.
          </p>
        )}
      </section>
    </div>
  );
}
