# Example Application

## Tech Stack

### Front End
* Angular.js 1.x
* Gulp
* Bootstrap LESS

### Server Side
* ASP.NET 5
* MongoDB
* SignalR (TODO)
* RabbitMQ (TODO)

### Testing
* xUnit 
* Karma (JS Test Runner) (TODO)
* Jasmine (JS Test Framework) (TODO)
* Protractor (E2E Framework) (TODO)

## Software Quality Goals

* Setup Server Unit Tests
* Setup JS Unit Tests
* Setup E2E Tests
* Setup Code Coverage Reporting
* Setup JS Linting
* Setup C# Linting (Static Analysis)



## Setup (OSX)
```
$ dnvm upgrade
$ source restore.sh
$ source build.sh
```
## Setup (Windows)

```
> dnvm upgrade
> restore
> build
```

## Run (OSX)
In the .DevSetup project
```
$ dnx run
```

In the .Web project
```
$ dnx kestrel
```

## Run (Windows)
In the .DevSetup project
```
> dnx run
```

In the .Web project
```
> dnx kestrel
```


## Test
In the .UnitTest project
```
$ dnx test
```