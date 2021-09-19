FROM mcr.microsoft.com/mssql/server:2017-latest
#FROM mcr.microsoft.com/mssql/server# for window server
#FROM mcr.microsoft.com/mssql/server:2019-latest # for window server
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=Secret1234


USER root
RUN mkdir -p /data
RUN chown root /data
WORKDIR /data


COPY ./scripts/ /data
COPY ./CICD/data /data
COPY ./db/ /data


RUN chmod +x /data/init-db-off.sh
RUN ./init-db-off.sh
#ENTRYPOINT [ "/bin/bash", "import_data.sh" ]
#ENTRYPOINT [ "/bin/bash", "init-db-off.sh" ]
#ENTRYPOINT [ "/bin/bash", "initdb.sh" ]


CMD [ "/opt/mssql/bin/sqlservr" ]