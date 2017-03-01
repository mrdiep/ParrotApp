var crawler = require('./crawler');
var database = require('./database');
var async = require('async');
let tableName = 'lyric4';

var parseToMongoDb = function() {
    var ids = [];

    for (var i = 1; i < 9356; i++) {
        ids.push(i);
    }
    
    database.find(tableName, {}, function(c) {
        c.each(function(err, doc) {
            if (err === null && doc !== null) {
                ids.splice(ids.indexOf(doc.id), 1);
            } else {
                doParse(ids);
            }
        })
    });

    var doParse = function(ids) {
        if (ids.length == 0) {
            database.close();
            return;
        }

        var taskAsync = [];

        function handlerInsert(lyricData, callback) {
            if (lyricData === null) {
                callback(null, 'err');
            } else {
                database.insert(tableName, lyricData);
                console.log('inserted, prepare call callback');
                callback(null, 'done');
            }
        };
        (function(songId) {
            taskAsync.push(function(callback) {
                console.log('start get songId:' + songId);

                crawler.get(songId, function(lyricData) {
                    handlerInsert(lyricData, callback);
                });
            });
        })(ids[0]);
        console.log('total='+ids.length);
        for (var i = 1; i < ids.length; i++) {
            (function(songId) {
                taskAsync.push(function(value, callback) {
                    console.log('start get songId:' + songId);

                    crawler.get(songId, function(lyricData) {
                        handlerInsert(lyricData, callback);
                    });
                });
            })(ids[i]);
        }

        setTimeout(function() {
            console.log('task count: ' + taskAsync.length);
            async.waterfall(taskAsync, function(err, results) {
                console.log('done waterfall---' + err + results);
                database.close();
            });
        }, 2000);

    }
};


var mongoDbToSqlite = function() {
    database.find(tableName, {}, function(c) {
        var songData = [];
        c.each(function(err, doc) {
            if (err === null && doc !== null) {
                songData.push(doc);
            } else {
                var sqlite = require('./sqlitedb');
                console.log('doneeee');
                sqlite.addData(songData);
            }
        })
    });
}
// for(var i=9;i<=20;i++){
// crawler.get(50, function(lyric) {
//      console.log('======================');
//      console.log(lyric);
// });
// }

//database.init(parseToMongoDb);
database.init(mongoDbToSqlite);