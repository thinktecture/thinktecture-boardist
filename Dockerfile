# Build Env
FROM node:10-alpine AS build-env
WORKDIR /app

COPY package.json ./
COPY package-lock.json ./

RUN npm i

COPY projects ./projects
COPY angular.json ./
COPY tsconfig.json ./
COPY tslint.json ./

RUN npm run build

# Prod Env
FROM nginx:alpine
WORKDIR /app

COPY --from=build-env /app/dist/frontend /usr/share/nginx/html
