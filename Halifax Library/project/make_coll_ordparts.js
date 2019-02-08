var auto_id = 1;
var auto = 1
//fetching the raw data
var X = db.ARTICLE_PRE.find().toArray();
for (var i = 0; i < X.length; i++) {
	//failsafe check to see if there are undefined values
	if(X[i].pages === undefined){
	  X[i].pages = {"ftext":"0-0"};
	}
	if(X[i].author === undefined){
	  continue;
	}
        var row = {"_id":auto_id, "author":X[i].author, "title":X[i].title.ftext, 
                   "pages":X[i].pages.ftext, "year":X[i].year.ftext, "volume":X[i].volume.ftext, "journal":X[i].journal.ftext};
	//making an entry int article table
        db.ARTICLE.insert(row);
        for(var j=0; j< X[i].author.length; j++){
                var name = X[i].author[j].ftext.split(" ")
                var fname = name [0];
                var lname = "";
                for(var z=1;z<name.length;z++){
                        lname += name[z];
                }
		//checking if the author already exists
                var exist = db.AUTHOR.find({"Aname":fname,"Lname":lname},{"_id":1}).toArray();
		// if author is new then insert an entry into author and writes collection
                  if(exist.length <= 0){
                        var email = fname+lname+"@gmail.com";
                        var row2 = {"_id":auto, "Aname":fname, "Lname":lname, "Email":email};
                        db.AUTHOR.insert(row2);
                        var row3 = {"Auid":auto,"Arid":auto_id};
                        db.WRITES.insert(row3);
                        auto++;
                }
                else {
			//if author exists then enters data only into the writes collection
                        var row3 = {"Auid":exist[0]._id,"Arid":auto_id};
                        db.WRITES.insert(row3);
                }
        }
        auto_id++;
}

