cur_dir				:= `pwd` 
project_name		:= CityManager
data_project		:= src/${project_name}.Data
startup_project		:= src/${project_name}.Api
tests				:= tests
 
clean:
	dotnet clean

restore:
	dotnet restore

build:
	dotnet build 

watch:	
	dotnet watch --project ${startup_project} run

run:
	dotnet run --project ${startup_project}

test:$(tests)/*
	for file in $^ ; do dotnet test $${file} ; done

reload: clean restore

add-migration:
	dotnet ef migrations add $(name) -s ${startup_project} --project ${data_project}

remove-migration:
	dotnet ef migrations remove -s ${startup_project} -p ${data_project}

update-database:
	dotnet ef database update -s ${startup_project} -p ${data_project} $(name)

truncate-database:
	dotnet ef database update -s ${startup_project} -p ${data_project} 0

reset-migrations: truncate_database remove_migration

start-postgres:
	docker-compose -f docker-compose.postgres.yml up

start-on-docker:
	docker-compose up --build

stop-docker:
	docker-compose down