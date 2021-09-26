FROM node:latest as build

WORKDIR /app
COPY ./ /app

RUN npm install

RUN npm run build


###
FROM nginx:latest

COPY --from=build /app/dist/sena-front /usr/share/nginx/html
COPY nginx.conf /etc/nginx/

EXPOSE 4200

ENTRYPOINT ["nginx", "-g", "daemon off;"]