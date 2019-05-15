# todoapplite

Simple Todo project written in Asp.Net Core
# Try it out
Asp.Net Core 2.2 / Couchbase 

Two ways to run the project can be followed. The first one is that run from source code.
The following commands can be executed in order.

Couchbase must be installed on your machine. If you have different environment variables you can change from appsettings.json before the run the application. The bucket name must be "Todos".

git clone https://github.com/enginocal/todoapplite.git 
	
	cd src
	dotnet restore
	dotnet run

The other way is to pull from the docker hub.

	docker pull enginocal/engin.todoapplite
	docker run -p 5000:5000 engin.todoapplite
	

At first user must be register from /api/User/Register and then need to get token for authentication from /api/User/Login.
In order to enable the user to use the methods in TodoItemController,use Bearer tokenFromLoginMethod as Header Authorization
should be added.If the token is not added , 401 Unauthorized error will be taken.


The methods can be tested at the http://localhost:5000/swagger/index.html in your browser.
