var sqlite3 = require('sqlite3').verbose();
var db = new sqlite3.Database('./hopam3.db3');
var fs = require('fs');
module.exports = {
    addData: function(songData) {
        db.serialize(function() {
            var createTable = function() {
                var tableSql = fs.readFileSync('./table_schema.sqlite', 'utf8');
                db.run(tableSql);
                console.log('done created table');
            }

            var add3 = function() {
                var stmt = db.prepare('INSERT INTO "song"("id", "title", "rhythm", "chord", "author", "singer") VALUES (?,?,?,?,?,?);');
                for (var i = 0; i < songData.length; i++) {
                    var song = songData[i];
                    console.log(song.id);
                    stmt.run(
                        song.id,
                        song.title,
                        song.rythm,
                        song.chord,
                        song.author,
                        song.singer
                    );
                }
                stmt.finalize();
                console.log('done added data');
            }

            createTable();
            add3();
        });
    }
}