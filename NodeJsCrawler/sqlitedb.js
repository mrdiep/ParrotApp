var sqlite3 = require('sqlite3').verbose();
var db = new sqlite3.Database('./hopam7.db3');
var fs = require('fs');
module.exports = {
    addData: function(songData) {
        db.serialize(function() {
            var createTable = function() {
                var tableSql = fs.readFileSync('./table_schema.sqlite', 'utf8');
                db.run(tableSql);
                console.log('done created table');
            }

            var addSongMetadata = function() {
                let stmt = db.prepare('INSERT INTO "songMetadata"("id", "title", "rhythm", "chord", "author", "singer") VALUES (?,?,?,?,?,?);');
                for (let i = 0; i < songData.length; i++) {
                    let song = songData[i];
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

            function squash(arr){
                var tmp = [];
                for(var i = 0; i < arr.length; i++){
                    if(tmp.indexOf(arr[i]) == -1){
                    tmp.push(arr[i]);
                    }
                }
                return tmp;
            }

            var addSongVersions = function() {
                let stmt = db.prepare('INSERT INTO "versions"("id","songId", "chord", "content", "description", "star", "votes","updated","default") VALUES (?,?,?,?,?,?,?,?,?);');
                for (let i = 0; i < songData.length; i++) {
                    let song = songData[i];
                    var versions = songData[i].version;

                    for(var versionIndex = 0; versionIndex<versions.length;versionIndex++){
                        var version = versions[versionIndex];
                        console.log("add version for " + song.id);
                        if(version === undefined)
                            continue;

                        stmt.run(
                            version.versionId,
                            song.id,
                            version.chord,
                            version.content,
                            version.description,
                            version.star,
                            version.votes,
                            version.updated,
                            version.default
                        );
                    }
                }
                stmt.finalize();
                console.log('done added data');
            }

            //createTable();
            addSongMetadata();
            addSongVersions();
        });
    }
}