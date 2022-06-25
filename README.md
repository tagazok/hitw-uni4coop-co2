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

This estimation comes from this [article](https://youmatter.world/fr/regime-alimentaire-ecologique-vegetarien-omnivore/#:~:text=Les%20r%C3%A9gimes%20v%C3%A9g%C3%A9tariens%20et%20vegans,plus%20que%20le%20r%C3%A9gime%20vegan.) where they state that footprint by diet type are on average about 3.3t for meat lovers and 1.7t for vegeterian per year. This means that the difference between the 2 diets is about 1.6t per year. Considering 365 days a year, we estimated the difference that a meat lover would consume $\frac{1600}{365} = 4.38kg$ more than a vegeterian per day that we rounded to 4kg for simplicity.

2. *Taking public transport or bicycle would save 67% of your carbon footprint by car for the same distance.*

This estimation comes from [this article](https://www.bloomberg.com/news/articles/2021-03-31/switching-from-cars-to-bikes-cuts-commuting-emissions-by-67#xj4y7vzkg) stating:
> Switching From Cars to Bikes Cuts Commuting Emissions by 67%

We therefore consider the distance one would do by car, estimate the carbon footprint of this ride and consider 67% of this amount as carbon saved. We equally considered bicycle, bus, train, metro as those are pretty similar when dividing the impact by the number of passenger.

For instance, someone took public transportation for 20km. According to this [website](https://www.liberation.fr/checknews/2018/12/21/un-trajet-en-avion-est-il-vraiment-plus-polluant-qu-un-trajet-en-voiture-ou-en-train_1679761/)
> une voiture neuve, en 2015, émet 110 grammes de CO2 par kilomètre

So for this ride, the carbon footprint would be $20 \times 110 = 2.2kg$. We then consider 67% of this amount so $2.2 \times 0.67 = 1.5kg$ of CO2 saved. 

3. *Taking a shower instead of a bath would save 1.1kg of CO2.*

This estimation comes from this [website](https://www.m-habitat.fr/plomberie-et-eau/consommation-d-eau/quelle-est-la-consommation-en-eau-d-une-douche-902_A) where they state that:
> une douche consomme de 40 à 60 litres alors qu’un bain de 120 à 200 litres.

We rounded estimations for showers and bath at 50L of water for a shower and 150L of water for a bath. 
We also found on this [website](https://wint.ai/the-carbon-footprint-of-water/#:~:text=The%20energy%20requirement%2C%20combined%20with,the%20%E2%80%9Crespect%E2%80%9D%20it%20deserves.) that:
> on average, every cubic meter of water consumed generates 23lb (or 10.6Kg) of carbon emissions.

This means that 1000L of water consumes 10.6kg of CO2, therefore an average shower consumes $$\frac{50}{1000} \times 10600 = 530g$$ of CO2 and a bath $$\frac{150}{1000} \times 10600 = 1590g$$

 Therefore the amount of CO2 saved between a shower and a bath is the difference $1590 - 530 = 1060g$ that we rounded to 1.1kg for simplicity.

4. *Reuse platic bag instead of buying one saves 200g CO2.*

This estimation comes from [this article](https://timeforchange.org/plastic-bags-and-plastic-bottles-co2-emissions-during-their-lifetime/) stating that:
> For 5 plastic bags you get 1 kg of CO2

Considering 1 bag, we get $\frac{1000}{5} = 200g/bag$.

5. *Recycling everything that can be recycled at your home would saves 2kg of CO2.*

This estimation comes from [this article](https://changeit.app/blog/recycle-matters/) stating: 
> You could save up to 61kg of carbon emissions per month by recycling as much as possible.

Considering approximatively 30 days a month, $\frac{61}{30} = 2kg/day$.


6. Turn off your computer (no battery saver) when not using it would save around 200g of CO2 per day.

This estimation comes from this [website](http://content.time.com/time/specials/2007/environment/article/0,28804,1602354_1603074_1603535,00.html#:~:text=Compared%20with%20a%20machine%20left,just%2063%20kg%20a%20year.) stating that : 
> Shutting it *[your computer]* off would reduce the machine's CO2 emissions 83%, to just 63 kg a year. 

We estimated that $\frac{63}{365} = 0.172kg$ rounded up to 200g for simplicity.

7. *Turn down your thermostats by one degree saves 1kg CO2.* 

This estimation comes from this [website](https://www.count-us-in.org/en-gb/steps/dial-it-down/#:~:text=Turning%20down%20your%20thermostat%20by,thermostat%20has%20other%20benefits%20too.) where they state that:
> Turning down your thermostat by one degree can reduce your carbon pollution by up to 340kg *[each year]*

Considering 365 days in a year, this gives $\frac{340}{365} = 0.93kg/day$ that we rounded up to 1kg for simplicity.