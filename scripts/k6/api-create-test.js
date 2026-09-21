import { check } from 'k6';
import http from 'k6/http';
import exec from 'k6/execution';

// define configuration
export const options = {
  scenarios: {
    create_todos: {
      executor: 'ramping-vus',
      startVUs: 0,
      stages: [
        { duration: '30s', target: 50 },
        { duration: '1m', target: 50 },
        { duration: '10s', target: 0 },
      ],
    },
  },
  thresholds: {
    http_req_failed: ['rate<0.01'],
    http_req_duration: ['p(99)<1000'],
  },
  // Reuse TCP/TLS connections and skip cert validation for localhost
  noConnectionReuse: false,
  insecureSkipTLSVerify: true,
};

// config for Keycloak
const keycloakConfig = {
  tokenUrl: 'https://localhost:8930/realms/my-realm/protocol/openid-connect/token',
  clientId: 'minimal-api-k6-client',
  clientSecret: '615M2X7zOywKE2nNWQSOC72quFjmvc3Q',
  username: 'StandardUser',
  password: 'password',
};

function requestToken() {
  const payload = {
    grant_type: 'password',
    client_id: keycloakConfig.clientId,
    username: keycloakConfig.username,
    password: keycloakConfig.password,
  };

  // Include client_secret if applicable
  if (keycloakConfig.clientSecret) {
    payload.client_secret = keycloakConfig.clientSecret;
  }

  const headers = { 'Content-Type': 'application/x-www-form-urlencoded' };

  const res = http.post(
    keycloakConfig.tokenUrl,
    Object.entries(payload).map(([k, v]) => `${k}=${encodeURIComponent(v)}`).join('&'),
    { headers }
  );

  const body = res.json();
  return {
    accessToken: body.access_token,
    // Refresh 30s before actual expiry to be safe
    expiresAt: Date.now() + (body.expires_in - 30) * 1000,
  };
}

// One-time setup: validates auth before the run starts
export function setup() {
  requestToken();
}

// Per-VU cached token, refreshed only when near expiry
let cached = null;

function getAccessToken() {
  if (!cached || Date.now() >= cached.expiresAt) {
    cached = requestToken();
  }
  return cached.accessToken;
}

export default function () {
  const token = getAccessToken();

  const url = 'https://localhost:8901/api/v1/todos';
  const payload = JSON.stringify({
    title: 'load-test-' + exec.vu.idInTest,
    note: exec.scenario.name,
    tags: ["load-test"]
  });

  const params = {
    headers: {
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`,
    },
  };

  const res = http.post(url, payload, params);

  check(res, {
    'response code was 201': (res) => res.status === 201,
  });
}