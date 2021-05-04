# World Cities REST API

## How to create a free city world api

When working with microservices, you will find yourself with various apps that need users to select a country or a city.

There are a lot of providers for such services out there, this however is an in-house solution that you can spin in a couple of seconds.

The dataset is based on [Juanmah's](https://www.kaggle.com/juanmah/world-cities) work, but slightly striped.

### How it works.
* create an azure function app in Azure.
* read the csv.
* respond with the requested data.


### Let get started: 

1. clone repository.
1. create a a function app.
1. pulbish to function to the above.
   1. When publishing make sure the option run from a package is false or 0. This makes wwwroot folder in Kudu readonly as the [docs](https://docs.microsoft.com/en-us/azure/azure-functions/run-functions-from-deployment-package) state.
   1. Create a folder, "Data" , using Kudu in the wwwwroot folder of the function.
   1. Upload your csv file to the Data folder in Kudu.
1. test your function.


### Expected output
1. without parameter country you will get a list of all countries in the world. 
    ![request without parameter](/output/countries.jpg)
2. with a paramter you will get a list of cities in that given country.
   ![request with parameter](/output/country.jpg)

### Questions
1. Is the data upto date? *Yes, cities take time to form. If a city is missing please let me know so that I can update the dataset.*
2. Can the data be improved *Yes, if the need arises we could add support for continent and other related data.*
3. How is the performance? *It is just fine, check the response times in the pictures above.*
4. I want a faster response, can that be done? *Yes, strip the file and remove fields that you don't need.*
5. Kudu cannot gives me a ERROR 409 Conflict ! *Set WEBSITE_RUN_FROM_PACKAGE to 0 in your function app configuration in Azure. This app service feature makes the wwwroot folder readonly as referenced above.*
6. Well my local solution is not working ! *Azure and a local development enviromrmnt have diffirent file structures, for debugging puposes change the root directory line to :   var rootDirectory = Path.GetFullPath(Path.Combine(binDirectory, "..","..","..",".."))*;
