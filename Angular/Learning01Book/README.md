# Introduction 
Following Pro Angular 5th book to learn angular.

The github repository location of the book: 
https://github.com/Apress/pro-angular-5ed

## Links

- Google icons https://fonts.google.com/icons

## Node commands

- npm version
```
npm -v
```

- Installing the Angular Development Package
```
npm install --global @angular/cli@13.0.3
```

- Creating a New Angular Project

```
ng new todo --routing false --style css --skip-git --skip-tests
```

- Starting the Angular Development Tools

```
ng serve
```

Another option
```
ng serve --open
```


- Adding the Angular Material Package

```
ng add @angular/material@13.0.2 --defaults
```

- Installing the Bootstrap CSS Package (run the command inside the folder project)

```
npm install bootstrap@5.1.3
```

-  Changing the Application Configuration Using PowerShell

```
Set-ExecutionPolicy Bypass -Scope Process -Force;
ng config projects.Primer.architect.build.options.styles '[""src/styles.css"", ""node_modules/bootstrap/dist/css/bootstrap.min.css""]'
```

