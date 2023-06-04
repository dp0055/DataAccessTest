# In Visual Studio Code hit CTRL+SHFT+V to show markdown preview!

## Main Task
Implement a simple data query application that leverages the DataReturner class and the underlying data sets that have been provided. 

The data files are contained within the Data folder and show the following:

1. film_data.csv -> A list of users and their favourite films

Schema

|id |first_name|last_name|email|city|film
|--|---|----|----|---|---|

1. cars.json -> a list of cars that links to the users under film_data

Schema

|id|carsArray|
|--|---|

The DataReturner class takes, as an argument, a filter model that allows you to filter for a set of car names and film titles beginning with a certain letter.

For example, a filter model can comprise of films beginning with the letter 'B' and all car makes called 'Ford'. 

The application itself can be any of the following:
- Web API
- Console Application
- Unittest

## Aspects to consider
1. Memory profile / expected load when querying millions of data points
2. Clear testing framework
