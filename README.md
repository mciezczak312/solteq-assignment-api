# Employee Mangement System  - Solteq Assignment
[![Build Status](https://travis-ci.org/mciezczak312/solteq-assignment-api.svg?branch=master)](https://travis-ci.org/mciezczak312/solteq-assignment-api)

This repository contains .net core API for EMS (employee management system) for a recruitment assignment from solteq.
You can find a repository with frontend (Angular) app [here](https://github.com/mciezczak312/solteq-assignment-frontend) 
Docker images from the master branch are deployed to my VPS. 

Demo: solteq-assignment.mciezczak.pl

(l: admin p: admin)

# System architecture

![archDiagram](https://images2.imgbox.com/b7/95/AkZfj7qN_o.png)

My system consists of two main components 
 - .NET Core 2.1 API
 - Angular frontend application

Additionally, there is MySql container for a database, adminer container for managing MySQL and swagger UI container for API documentation.
Everything is built on Docker.

# Database design
![databaseDiagram](https://images2.imgbox.com/06/0a/52G0Wlbg_o.jpg)
This is a very simplified database for employee management system. I am aware that in a real system like this database would be much more complicated but I think this is enough to fulfill the assignment requirements. I included both one-to-many and many-to-many relationships and full-text indexes. 

# Running locally
Because my system is based on docker it is very easy to run it locally. You need docker and docker-compose. In deploy folder, you can find a docker-compose file. To run it just use:

```sh
$ docker-compose up
```
Then you can go to localhost and open frontend app.
Docker will expose others ports to.
- 9000 for .net API
- 9001 for adminer
- 9002 for swagger UI

Then you need to import database. The dump file is also in deploy folder.


# CI
Before every pull request or change on any branch, Travis CI pipeline triggers builds docker images and run tests. For the frontend, I also added codebeat that checks the style of code.
For every change on the master branch, Travis also pushes new Docker images to my docker hub repos and tags them as latest.

# User authentication
For user authentication, I implemented a simple password-grant flow. In API there is an endpoint that accepts user login and password. If the provided data is valid it returns JWT token signed by application secret. Every subsequent call to API has to include this token in the Authorization header.

![text](https://images2.imgbox.com/c9/e7/qN4BTV7v_o.png)

## Tech
Tech used both in frontend and backend.


* [Angular](https://angular.io/)
* [ngx-rocket template](https://github.com/ngx-rocket/generator-ngx-rocket) - awesome boilerplate tamplete for angular apps
* [ngx-datatable](http://swimlane.github.io/ngx-datatable/) - cool datatable for angular
* [Twitter Bootstrap](https://ng-bootstrap.github.io/#/home) - UI boilerplate
* [.net core 2.1](https://www.microsoft.com/net)
* [Dapper](https://github.com/StackExchange/Dapper) - Micro ORM.
* [Automapper](http://automapper.org/) Library to automaticly map objects


