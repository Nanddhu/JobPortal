const API_BASE_URL =
  import.meta.env.VITE_API_BASE_URL || "https://localhost:7204/api";

async function request(path, options = {}) {
  const isFormData = options.body instanceof FormData;
  const token = localStorage.getItem("token");

  const response = await fetch(`${API_BASE_URL}${path}`, {
    method: options.method || "GET",
    headers: {
      ...(isFormData ? {} : { "Content-Type": "application/json" }),
      ...(token ? { Authorization: `Bearer ${token}` } : {}),
      ...(options.headers || {}),
    },
    body: options.body,
  });

  let data = null;

  try {
    data = await response.json();
  } catch {
    data = null;
  }

  if (!response.ok) {
    console.error("API ERROR:", data);
    throw new Error(data?.message || "API request failed");
  }

  return data;
}

function extractJobs(payload) {
  if (Array.isArray(payload)) return payload;
  if (Array.isArray(payload?.jobs)) return payload.jobs;
  if (Array.isArray(payload?.data)) return payload.data;
  return [];
}

function extractJob(payload) {
  return payload?.job || payload?.data || payload;
}

export async function signupUser(payload) {
  return request("/auth/register", {
    method: "POST",
    body: JSON.stringify(payload),
  });
}

export async function loginUser(payload) {
  const data = await request("/auth/login", {
    method: "POST",
    body: JSON.stringify(payload),
  });

  const token =
    data?.token ||
    data?.data?.token ||
    data?.jwtToken ||
    data?.accessToken;

  if (token) {
    localStorage.setItem("token", token);
  }

  return data;
}

export async function fetchJobs(keyword = "") {
  const query = keyword ? `?keyword=${encodeURIComponent(keyword)}` : "";
  const response = await request(`/jobs/search${query}`);
  return extractJobs(response);
}

export async function fetchJobById(jobId) {
  const response = await request(`/jobs/${jobId}`);
  return extractJob(response);
}

export async function applyToJob(payload) {
  return request(`/jobs/apply`, {
    method: "POST",
    body: payload,
  });
}

export { API_BASE_URL };
