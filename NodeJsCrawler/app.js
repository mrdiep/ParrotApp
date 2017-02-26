var crawler = require('./crawler');
var database = require('./database');

var parseToMongoDb = function() {
    var ids = [];

    for (var i = 1; i < 9356; i++) {
        ids.push(i);
    }

    database.find('lyric', {}, function(c) {
        c.each(function(err, doc) {
            if (err === null && doc !== null) {
                ids.splice(ids.indexOf(doc.id), 1);
            } else {
                doParse(ids);
            }

        })
    });

    var doParse = function(ids) {
        for (var i = 0; i < ids.length; i++) { 
            crawler.get(ids[i], function(lyric) {
                database.insert('lyric', lyric);
            });
        }
    }
};


var mongoDbToSqlite = function() {
    database.find('lyric', {}, function(c) {
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

//database.init(parseToMongoDb);
database.init(mongoDbToSqlite);