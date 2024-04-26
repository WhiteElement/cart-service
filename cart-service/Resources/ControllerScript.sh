#!/bin/bash

domain='http://localhost:5134'

#
# TEST
#
curl ${domain}/Test


#
# SHOPPINGLIST
#

#GetAll
#curl ${domain}/ShoppingList


#GetOne
#curl ${domain}/ShoppingList/1

#PostOne
#curl ${domain}/ShoppingList -d '{"name" : "Supermarkt" }' -H "Content-Type: application/json"







echo