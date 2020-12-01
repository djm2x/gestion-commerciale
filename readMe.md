# App web 
[lien]:(https://aspme.herokuapp.com)

## Le projet à éte developpé par les technologies suivantes : 
[ASP.net Core 3.1 SDK] :(https://dotnet.microsoft.com/download)

[Node js et NPM] :(https://nodejs.org/en/download/) & [Angular 8](https://cli.angular.io/) : 

## Les étapes d'instalation : 

### la commande pour récuperer le repository du git est : 
```
get clone  https://github.com/djm2x/brik-afric.git
```

### Pour installer les dependences éxécuter la commande suivante : 
```
npm run install
```

### pour le lancer le back-end asp.net core restful api éxécuter la commande suivante : 
```
npm run b
```

### pour lancer le projet front-end angular éxécuter la commande suivante :  
```
npm run f 
```

### configuration deheroku pour le deploiment de l'application

- Crée un compte sur [Heroku](https://www.heroku.com/)
- Dans le Dashboard cliquer sur new => `create a new app`
- Attribuer un nom à l'application et choisissez  la région la plus proche de vous => `create app`
- Dans l'ongle Settings -> `add Buildpacks` : https://github.com/anuraj/dotnetcore-buildpack (pour qui Heroku support ASP.NET Core)
- Dans l'ongle `Deploy`, choisir comme `Deployment method` GitHub, connectez-vous après avoir choisie votre repo est cliqué sur `connect` -> `Enable Automatic Deploy`
- Pour genere la base de donne install ce package de entity framwork core
```
dotnet tool install --global dotnet-ef
```


### pour deployer le projet sur Heroku : 
Cette commande au dessous (plus de detail dans package.json -> scripts) va pushe les changement dans votre repo, puis Heroku recoit les action faite sur votre repo alors ila copy et builder votre app , et le site sera actualiser
```
npm run ci 
```

