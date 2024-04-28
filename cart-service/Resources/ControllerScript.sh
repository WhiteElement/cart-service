#!/bin/bash

domain='http://localhost:5000'

###############
# TEST
###############

#curl ${domain}/Test


###############
# SHOPPINGLIST
###############

#GetAll
curl ${domain}/ShoppingList

#GetOne
#curl ${domain}/ShoppingList/1

#PostOne
#curl ${domain}/ShoppingList -d '{"name" : "Supermarkt" }' -H "Content-Type: application/json"
#curl ${domain}/ShoppingList -d '{"name" : "Supermarkt", "Id" : 5 }' -H "Content-Type: application/json"



###############
# SHOPPINGITEM
###############

#GetAll
#curl ${domain}/ShoppingItem

#PostOneToShoppingList
#curl ${domain}/ShoppingList -d '{"name" : "Supermarkt1" }' -H "Content-Type: application/json"
#curl ${domain}/ShoppingItem/1 -d '{"name" : "Erdnussbutter"}' -H "Content-Type: application/json"

#DeleteOne
#        //TODO
#        // keine Id angegeben
#        // item mit der Id gibt es nicht
#        // prüfen ob wirklich gelöscht

#curl ${domain}/ShoppingList -d '{"name" : "Supermarkt1" }' -H "Content-Type: application/json"
#ech
#curl ${domain}/ShoppingItem/1 -d '{"name" : "isfds"}' -H "Content-Type: application/json"
#echo
#curl ${domain}/ShoppingItem/1 -d '{"name" : "dsfds"}' -H "Content-Type: application/json"
#echo
#curl ${domain}/ShoppingItem/1 -d '{"name" : "fsfds"}' -H "Content-Type: application/json"
#echo
#curl ${domain}/ShoppingItem/1 -d '{"name" : "wsfds"}' -H "Content-Type: application/json"
#echo
#curl ${domain}/ShoppingItem
#curl ${domain}/ShoppingItem/4 -X DELETE 
echo