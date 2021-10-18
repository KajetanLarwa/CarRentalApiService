# CarRentalApiService

## Database configuration:
* To set database credentials you have to execute (Required to connect with DB):
    * ```dotnet user-secrets set "DevelopDB:Login" "{dev username}" ```
    * ```dotnet user-secrets set "DevelopDB:Password" "{dev password}" ```

## For development You can either:
* Run each part separately (SQLServer local instance)
* Use ```docker-compose up``` (it will setup api and database)

## Working with GIT
* Each task should be one commit, commit message schema: 'CRA-{azure devops id}': {Shortly what this change is}
* A single pull request can consist of one commit (task) or the whole user story (the whole user story is preferred)
* Each branch which contains task/user story should be named 'feature/CRA-{azure devops id}'
* Pull requests should be made to master
* When You are implementing code review amend you commit and force push
