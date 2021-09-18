FROM mcr.microsoft.com/mssql/server
#FROM mcr.microsoft.com/mssql/server:2019-latest
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=Secret1234

COPY ./scripts/ /
COPY ./CICD/data /
COPY ./db/ /db

ENTRYPOINT [ "/bin/bash", "import_data.sh" ]
CMD [ "/opt/mssql/bin/sqlservr" ]