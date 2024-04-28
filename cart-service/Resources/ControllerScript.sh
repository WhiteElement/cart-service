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
#curl ${domain}/ShoppingList

#GetOne
#curl ${domain}/ShoppingList/1

#PostOne
#curl ${domain}/ShoppingList -d '{"name" : "Supermarkt" }' -H "Content-Type: application/json"
#curl ${domain}/ShoppingList -d '{"name" : "Supermarkt", "Id" : 5 }' -H "Content-Type: application/json"


#PatchOne
#=========
#curl ${domain}/ShoppingList -X PATCH -d '{"Id" : 1, "Name" : "Supermarkt"}' -H "Content-Type: application/json"
#curl ${domain}/ShoppingList -X PATCH -d '{"Name" : "Pfannkuchen"}' -H "Content-Type: application/json"


###############
# SHOPPINGITEM
###############

#GetAll
#curl ${domain}/ShoppingItem

#PostOneToShoppingList
#curl ${domain}/ShoppingList -d '{"name" : "Supermarkt1" }' -H "Content-Type: application/json"
#curl ${domain}/ShoppingItem/1 -d '{"name" : "Erdnussbutter"}' -H "Content-Type: application/json"
#curl ${domain}/ShoppingItem/1 -d '{"name" : "Butter"}' -H "Content-Type: application/json"
#curl ${domain}/ShoppingItem/1 -d '{"name" : "Klopapier"}' -H "Content-Type: application/json"
#curl ${domain}/ShoppingItem/1 -d '{"name" : "Wasser"}' -H "Content-Type: application/json"

#DeleteOne

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

#DeleteMultiple
#curl ${domain}/ShoppingItem/multiple -X DELETE -d [8] -H "Content-Type: application/json"

#PatchOne
#=========
#curl ${domain}/ShoppingItem -X PATCH -d '{"Id" : 9, "Name" : "Pfannkuchen"}' -H "Content-Type: application/json"
#curl ${domain}/ShoppingItem -X PATCH -d '{"Name" : "Pfannkuchen"}' -H "Content-Type: application/json"
#curl ${domain}/ShoppingItem -X PATCH -d '{"Id" : 3, "Name" : "Pfannkuchen"}' -H "Content-Type: application/json"
#curl ${domain}/ShoppingItem -X PATCH -d '{"Id" : 10, "Name" : "Mineralwasser"}' -H "Content-Type: application/json"

echo