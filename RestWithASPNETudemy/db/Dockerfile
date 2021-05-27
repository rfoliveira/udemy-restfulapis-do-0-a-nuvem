# o instrutor colocou esse arquivo do lado de fora do projeto, mas eu coloquei aqui para facilitar
FROM mysql:8.0.25
EXPOSE 3306

# para copiar os scripts da migrations para dentro do container
COPY ./db/migrations/ /home/database/

# para executar os scripts de migrations de dentro do container
COPY ./ci/init_database.sh /docker-entrypoint-initdb.d/init_database.sh