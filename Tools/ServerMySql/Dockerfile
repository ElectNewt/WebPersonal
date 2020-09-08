FROM mysql:5.6

ENV MYSQL_DATABASE webpersonal
ENV MYSQL_ROOT_PASSWORD=test

## todos los scripts en  docker-entrypoint-initdb.d/ se ejecutan automaticamente
COPY ./Database/ ./docker-entrypoint-initdb.d/