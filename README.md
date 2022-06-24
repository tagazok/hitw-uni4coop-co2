# Hack In The Wood (HITW) - Uni4Coop
This project was born at the [HITW 2022](https://www.hackinthewoods.be/), a festival for developers who
code for a better world.

We collaborated with [Uni4Coop](https://uni4coop.com/fr), a consortium of NGOs, on a project aiming at creating awareness of the ecological impact of air travel and then try to compensate for it.

Our application takes as input flights departure and destination in order to estimate environmental impact. We can then calculate several ways the person can compensate by for instance taking challenges such as going vegeterian for a certain amount of days, or taking public transports instead of their cars or financing projects to reduce other emissions or to sequester carbon.

# How to dev
 - install the Static Web App CLI ([download here](https://github.com/Azure/static-web-apps-cli))
 ```bash
 npm install -g @azure/static-web-apps-cli
 ```
 - install Angular ([download here](https://angular.io/guide/setup-local#install-the-angular-cli))
 ```bash
 npm install -g @angular/cli
 ```
  - Install .NET Core SDK ([download here](https://dotnet.microsoft.com/en-us/download))
```

# How to run
```bash
cd app
npm install
cd ..
swa start
```

# Calculation methods
Estimated amount of CO2 saved are extrapolated from different sources.

1. *Going vegetarian for one day would save 4kg of CO2.*

This estimation comes from this [article](https://youmatter.world/fr/regime-alimentaire-ecologique-vegetarien-omnivore/#:~:text=Les%20r%C3%A9gimes%20v%C3%A9g%C3%A9tariens%20et%20vegans,plus%20que%20le%20r%C3%A9gime%20vegan.) where they state that footprint by diet type are on average about 3.3t for meat lovers and 1.7t for vegeterian per year. This means that the difference between the 2 diets is about 1.6t per year. Considering 365 days a year, we estimated the difference that a meat lover would consume 1600/365 = 4.38kg per day that we rounded to 4kg for simplicity.

2. Taking public transport or bicycle would save 70% of your carbon footprint by car for the same distance
3. Taking a shower instead of a bath would save 1.1kg of CO2
4. Reuse platic bag instead of buying one saves 200g CO2
5. Recycling everything that can be recycled at your home would saves 2kg of CO2
6. Turn off all electrical devices (no battery saver) that you don't use and unplug 
everything that is charged saves 2kg of CO2
7. *Turn down your thermostats by one degree saves 1kg CO2.* 

This estimation comes from this [website](https://www.count-us-in.org/en-gb/steps/dial-it-down/#:~:text=Turning%20down%20your%20thermostat%20by,thermostat%20has%20other%20benefits%20too.) where they state that:
> Turning down your thermostat by one degree can reduce your carbon pollution by up to 340kg *[each year]*

Considering 365 days in a year, this gives 340/365 = 0.93kg/day that we rounded up to 1kg for simplicity.