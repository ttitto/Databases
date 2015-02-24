use chat
db
chat

var message = {text: "sample message text", date: "2015-02-23", isRead: true, user:{username: "pesho", fullName: "Pesho Peshev", website: "http://www.dir.bg"}};
 db.messages.insert(message);
 db.messages.insert({text: "another sample message text", date: "2015-02-24", isRead: false, user:{username: "gosho", fullName: "Gosho Goshov", website: "http://www.abv.bg"}});
 show collections
 db.messages.find()
 db.messages.findOne()
 db.messages.find({isRead: true});
 
 ctrl + C
 
mongodump --out /data/backup/
 
 use chat
 db.dropDatabase()
 show dbs
 
 ctrl + C
// before running next row stop mongod with ctrl + C
mongorestore --dbpath /data/db /data/backup/chat