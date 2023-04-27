# Innoloft_Test

##FYI
* This project uses .net 6 as target. 
* Clone repository. 
* Make sure your current dotnet version is running the .net6 sdk.
* In project directory, run `docker-compose build` to build image.
* Run  `docker-compose up` to create container. 
* Create new connection in  `Mysql WorkBench` with `Hostname: host.docker.internal` and use `MYSQL_ROOT_PASSWORD` as password in docker-compose file.
* Test connection. 
* Delete migrations folder. Move to package manager console in Visual Studio, run  `dotnet ef migrations add first_migrations` and `dotnet ef database update` to create our tables and schemas.
* Run `localhost:5000` in browser to open swagger and test endpoints. or use postman. 


