#!/bin/bash


if [ "$1" = "-h" -o "$1" = "--help" ]
then
    printf "\nThis Script is used to load a file into MySQL followed by \nloading another file into Mongo DB into the collection AUTHOR \n\n"
    exit 1
fi
#fetching the creds for MySQL
printf "Step 1: MySQL:\n\n"
printf "\nMySQL Username: "
read user
printf "\nMySQL password: "
read -s pass
printf "\nMySQL Database name: "
read db
printf "\nThe SQL file to be processed (without the .sql extension): "
read file

#executing the new_tables
echo "Executing the SQL file $file.sql"
mysql --password=$pass -u "$user" $db < "$file.sql"
printf "Done Execution of file $file \n\n Moving to Mongo DB\n"

#fetching the creds for MongoDB
printf "Step 2: MongoDB:\n\n"
printf "\nMongo Username: "
read user
printf "\nMongodb password: "
read -s pass
printf "\nMongo Database name: "
read db
printf "\nThe JSON file to be processed (without the .json extension): "
read file
coll="AUTHOR"


printf "\nEntering Mongo DB\"\n-\n"

#creting intermediate collection and required collections
mongoimport -d "$db" -u "$user" -p "$pass" -c "ARTICLE_PRE" --file "$file.json"
mongo  $user -u "$user" -p "$pass" --eval "db.ARTICLE.drop()"
mongo  $user -u "$user" -p "$pass" --eval "db.$coll.drop()"
mongo  $user -u "$user" -p "$pass" --eval "db.WRITES.drop()"
mongo  $user -u "$user" -p "$pass" --eval "load('make_coll_ordparts.js')"
mongo  $user -u "$user" -p "$pass" --eval "db.ARTICLE_PRE.drop()"

printf "\n All done !\n\n"

