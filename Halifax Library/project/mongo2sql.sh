#!/bin/bash

#fetching the creds for MongoDB
printf "\nStep 1: MongoDB:"
printf "\nMongo Username: "
read user
printf "\nMongodb password: "
read -s pass
printf "\nMongo Database name: "
read db


printf "\nEntering Mongo DB\"\n-\n"

mongo  "$db" -u "$user" -p "$pass" --eval "load('make_coll_ordparts1.js')"

#Extracting the required data from mongo in csv
mongoexport -d "$db" -u "$user" -p "$pass" -c AUTHOR --type=csv --out author.csv --fields _id,Aname,Lname,Email
mongoexport -d "$db" -u "$user" -p "$pass" -c ITEM --type=csv --out id.csv --fields _id,price
mongoexport -d "$db" -u "$user" -p "$pass" -c MAGAZINE --type=csv --out magazine.csv --fields _id,Title,Year_Publish,Volume
mongoexport -d "$db" -u "$user" -p "$pass" -c ARTI --type=csv --out arti.csv --fields _id,title,page_start,page_end,magazine_id
mongoexport -d "$db" -u "$user" -p "$pass" -c WRITES --type=csv --out exists.csv --fields Auid,Arid

#clean up of all intermediate collections
mongo  "$db" -u "$user" -p "$pass" --eval "db.ITEM.drop()"
mongo  "$db" -u "$user" -p "$pass" --eval "db.MAGAZINE.drop()"
mongo  "$db" -u "$user" -p "$pass" --eval "db.ARTI.drop()"
mongo  "$db" -u "$user" -p "$pass" --eval "db.WRITES.drop()"

#mysql -u "$user" --password=$pass "$db" -e "truncate table WRITES"
#mysql -u "$user" --password=$pass "$db" -e "truncate table AUTHOR"
#mysql -u "$user" --password=$pass "$db" -e "truncate table ARTICLE"
#mysql -u "$user" --password=$pass "$db" -e "truncate table MAGAZINE"
#mysql -u "$user" --password=$pass "$db" -e "truncate table ITEM"

#fetching the creds for MySQL
printf "Step 2: MySQL:\n\n"
printf "\nMySQL Username: "
read user
printf "\nMySQL password: "
read -s pass
printf "\nMySQL Database name: "
read db

#importing the csv data into corresponding tables in MySQL
mysql -u "$user" --password=$pass "$db" -e "load data local infile 'author.csv' into table AUTHOR fields terminated by ',' lines terminated by '\n' IGNORE 1 LINES (Author_ID,A_Name,A_Last,Email)"
mysql -u "$user" --password=$pass "$db" -e "load data local infile 'id.csv' into table ITEM fields terminated by ',' lines terminated by '\n' (ID,Price)"
mysql -u "$user" --password=$pass "$db" -e "load data local infile 'magazine.csv' into table MAGAZINE fields terminated by ',' lines terminated by '\n' (Maganize_ID,Title,Year_Publish,Volume)"
mysql -u "$user" --password=$pass "$db" -e "load data local infile 'arti.csv' into table ARTICLE fields terminated by ',' lines terminated by '\n' (Article_id,Title,Page_Start,Page_End,Magazine_Id)"
mysql -u "$user" --password=$pass "$db" -e "load data local infile 'exists.csv' into table WRITES fields terminated by ',' lines terminated by '\n' (Author_ID,Article_ID)"

#creating the index for the required tables
mysql -u "$user" --password=$pass "$db" -e "Create Index ITEM_ID_INDEX on ITEM (ID)"

mysql -u "$user" --password=$pass "$db" -e "Create Index TRANSACTION_Trxn_Number_INDEX on TRANSACTION (Trxn_Number)"

mysql -u "$user" --password=$pass "$db" -e "Create Index TRANSACION_DETAILS_Trxn_Number_INDEX on TRANSACION_DETAILS (Trxn_Number)"

mysql -u "$user" --password=$pass "$db" -e "Create Index CUSTOMER_C_NAME_INDEX on CUSTOMER (C_Name)"

#clean up of the csv
rm author.csv id.csv magazine.csv exists.csv arti.csv
printf "\nAll Done!\n\n"
