var sqlite3 = require('sqlite3').verbose();
var db = new sqlite3.Database(':memory:');
var fs  =require('fs');
module.exports = {
    init:function(completedCallback){
        db.serialize(function() {
            //db.run(fs.readdirSync('./table_schema.sqlite'));
            
            //var stmt = db.prepare("INSERT INTO song VALUES (?,?,?,?,?,?)");
            //for (var i = 0; i < 10; i++) {
            //    stmt.run()
            //}
            //stmt.finalize();
            
            //db.each("SELECT id FROM song", function(err, row) {
            //    console.log(row.id + ": " + row.info);
            //});
        });
    }
}