var crawler = require('./crawler');
var database  = require('./database');

database.init(function(){
var ids=[];

for(var i=1;i<9354;i++){
    ids.push(i);
}

database.find('lyric',{}, function(c){
    c.each(function(err,doc){
        if(err===null && doc!==null){
            ids.splice(ids.indexOf(doc.id), 1);
        }
        else {
            doParse(ids);
        }

    })
});

var doParse = function(ids){
        for(var i=0;i<ids.length;i++){//9354
        crawler.get(ids[i], function(lyric){
            database.insert('lyric', lyric);
            //fs.writeFile('D:\aa.txt', lyric);
        });
    }
}
});

