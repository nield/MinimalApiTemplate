#!/usr/bin/env bash
#
# Generates and trusts the local ASP.NET Core HTTPS development certificate
# used by the API container in docker-compose.
#
# The PFX is exported to ~/.aspnet/https, which docker-compose.override.yml
# mounts into the container. The password must match the one referenced by
# Kestrel__Certificates__Default__Password in docker-compose.override.yml.
#
# Run this once after cloning the repo (or whenever the cert expires).

set -euo pipefail

PASSWORD="${1:-devpassword}"
CERT_NAME="${2:-MinimalApiTemplate.Api.pfx}"

CERT_DIR="${HOME}/.aspnet/https"
CERT_PATH="${CERT_DIR}/${CERT_NAME}"

mkdir -p "${CERT_DIR}"

echo "Cleaning existing dev certificates..."
dotnet dev-certs https --clean

echo "Exporting dev certificate to ${CERT_PATH} ..."
dotnet dev-certs https -ep "${CERT_PATH}" -p "${PASSWORD}"

echo "Trusting dev certificate..."
dotnet dev-certs https --trust || echo "NOTE: '--trust' may be unsupported on Linux; trust the cert manually if needed."

echo "Done. Dev HTTPS certificate ready at ${CERT_PATH}"
