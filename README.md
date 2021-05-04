# world-cities-api

## How to create a free city world api

When working with microservice you will find yourself with various apps that need users to select a country or a city.

There are a lot of providers for such services out there, this however is an in-house solution you can spin in a couple of seconds.

The dataset is based on [Juanmah's](https://www.kaggle.com/juanmah/world-cities) work, but slightly striped.

How it works.
* create an azure function app in azure.
* read the csv.
* respond with the requested data.


### Let get started: 

1. clone repository.
1. create a a function app.
1. pulbish to function to the above.
   1. When publishing make sure the option run from a package is false or 0. This makes wwwroot folder in Kudu readonly as the [docs] (https://docs.microsoft.com/en-us/azure/azure-functions/run-functions-from-deployment-package) state.
   1. Create a folder, "Data" , using Kudu in the wwwwroot folder of the function.
   1. Upload your csv file.
1. test your function.


## Expected output
1. without parameter country you will get a list of all countries in the world. 
1. with a paramter you will get a list of all countries in the world.


