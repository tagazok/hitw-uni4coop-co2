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

1. Going vegetarian for one day would save 3 kg of CO2
2. Taking public transport or bicycle would save 70% of your carbon footprint by car for the same distance
3. Taking a shower instead of a bath would save 1.1 kg of CO2
4. Reuse platic bag instead of buying one saves 200g CO2
5. Recycling everything that can be recycled at your home would saves 2kg of CO2
6. Turn off all electrical devices (no battery saver) that you don't use and unplug 
everything that is charged saves 2 kg of CO2
7. Turn down your thermostats by one degree saves 1 kg CO2