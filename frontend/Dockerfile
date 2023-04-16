FROM node:16-alpine as angular-app
WORKDIR /app

COPY package.json /app
RUN npm install --silent
COPY . .
RUN npm run build

FROM nginx:alpine
COPY --from=angular-app app/dist/freelance-nfcontrol /usr/share/nginx/html/
COPY ./nginx.conf /etc/nginx/conf.d/default.conf