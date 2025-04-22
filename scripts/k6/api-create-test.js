import { check } from 'k6';
import http from 'k6/http';
import exec from 'k6/execution';

// define configuration
export const options = {
  thresholds: {
    http_req_failed: ['rate<0.01'],
    http_req_duration: ['p(99)<1000'],
  },
};

// config for Keycloak
const keycloakConfig = {
  tokenUrl: 'http://localhost:8930/realms/my-realm/protocol/openid-connect/token',
  clientId: 'minimal-api-k6-client',
  clientSecret: '615M2X7zOywKE2nNWQSOC72quFjmvc3Q',
  username: 'StandardUser',
  password: 'password',
};

function getAccessToken() {
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
  return body.access_token;
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