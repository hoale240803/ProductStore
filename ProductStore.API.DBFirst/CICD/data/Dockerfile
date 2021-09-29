FROM mcr.microsoft.com/mssql/server:2017-CU24-ubuntu-16.04
#FROM mcr.microsoft.com/mssql/server:2017-latest
#FROM mcr.microsoft.com/mssql/server# for window server
#FROM mcr.microsoft.com/mssql/server:2019-latest # for window server
ENV ACCEPT_EULA=Y
ENV MSSQL_SA_PASSWORD=Secret1234


#USER root
#RUN mkdir -p /data
#RUN chown root /data
#WORKDIR /data

# Create app directory
USER root
RUN mkdir -p /usr/src/data
RUN chown root /usr/src/data
WORKDIR /usr/src/data

# Copy initialization scripts
COPY ./scripts/ /usr/src/data
COPY ./CICD/data /usr/src/data
COPY ./db/ /usr/src/data

# Grant permissions for the run-initialization script to be executable
RUN chmod +x /usr/src/data/initdb.sh

# Run Microsoft SQl Server and initialization script (at the same time)
# Note: If you want to start MsSQL only (without initialization script) you can comment bellow line out, CMD entry from base image will be taken
CMD /bin/bash /usr/src/data/entrypoint.sh

#RUN /opt/mssql/bin/sqlservr --accept-eula \ 
    #&& /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P 'Secret1234' -i /opt/mssql-scripts/store.sql

#CMD /opt/mssql-tools/bin/sqlcmd -S localhost,1433 -U SA -P Secret1234 -i "/usr/src/data/store.sql" -o "/var/opt/mssql/database/store.log"

#RUN /opt/mssql/bin/sqlservr --accept-eula & sleep 90 \
    #&& /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P 'Secret1234' -i /opt/mssql-scripts/store.sql \
    #&& pkill sqlservr 

#RUN ./initdb.sh
#ENTRYPOINT [ "/bin/bash", "/data/import_data.sh" ]
#ENTRYPOINT [ "/bin/bash", "/data/init-db-off.sh" ]
#ENTRYPOINT [ "/bin/bash", "/data/initdb.sh" ]
#



#ENTRYPOINT ["/data/initdb.sh"]


#CMD [ "/opt/mssql/bin/sqlservr" ]