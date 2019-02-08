var auto_id = 1;
//var X = db.ARTICLE_PRE.find({}, {"@mdate":0,"@key":0, "author":1, "title":1, "pages":1, "year":1, "volume":1, "journal":1}).toArray();
var X = db.ARTICLE.aggregate([{"$group": { "_id": {"volume":"$volume","journal":"$journal"}}}]).toArray();


for (var i = 0; i < X.length; i++) {
	var Y = db.ARTICLE.find({"volume":X[i]._id.volume,"journal":X[i]._id.journal},{}).sort({"year":-1}).toArray();
	var yr = Y[0].year;
	var row = {"_id":auto_id, "price":Math.floor((Math.random() * 11)+1)};
	var row1 = {"_id":auto_id, "Title":X[i]._id.journal, "Year_Publish":yr,"Volume":X[i]._id.volume};
        db.ITEM.insert(row);
	db.MAGAZINE.insert(row1);
        auto_id++;
}

var Z = db.ARTICLE.find().toArray();

for (var i = 0; i < Z.length; i++) {
	var Y = db.MAGAZINE.find({"Volume":Z[i].volume,"Title":Z[i].journal},{}).toArray();
	var page = Z[i].pages.split("-");
	var row = {"_id":Z[i]._id,"title":Z[i].title,"page_start":page[0],"page_end":page[1],"magazine_id":Y[0]._id};
	db.ARTI.insert(row);
}
