export default function BrandLogo({ compact = false }) {
  return (
    <div className={`brand ${compact ? "brand--compact" : ""}`}>
      <div className="brand__mark">
        <span className="brand__dot brand__dot--one" />
        <span className="brand__dot brand__dot--two" />
      </div>
      <div>
        <p className="brand__name">Elevate Jobs</p>
        {!compact && <p className="brand__tag">Hire brighter. Grow faster.</p>}
      </div>
    </div>
  );
}
