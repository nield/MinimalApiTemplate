// import necessary modules
import { check } from 'k6';
import http from 'k6/http';
import exec from 'k6/execution';

// define configuration
export const options = {
  // define thresholds
  thresholds: {
    http_req_failed: ['rate<0.01'], // http errors should be less than 1%
    http_req_duration: ['p(99)<1000'], // 99% of requests should be below 1s
  },
};

export default function () {
  // define URL and request body
  const url = 'https://localhost:8000/api/v1/todos';
  const payload = JSON.stringify({
    title: 'load-test-' + exec.vu.idInTest,
    note: exec.scenario.name,
    tags: [
        "load-test"
    ]
  });
  const params = {
    headers: {
      'Content-Type': 'application/json',
    },
  };

  // send a post request and save response as a variable
  const res = http.post(url, payload, params);

  // check that response is 201
  check(res, {
    'response code was 201': (res) => res.status == 201,
  });
}