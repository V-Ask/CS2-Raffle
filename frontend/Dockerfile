FROM node:latest as build-stage
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build

FROM nginx:stable-alpine as production-stage
COPY nginx.conf /etc/nginx/nginx.conf

COPY --from=build-stage /app/dist /app
EXPOSE 80
CMD [ "nginx", "-g", "daemon off;" ]