var _http = require('https');
_http.globalAgent.maxSockets = 5;

var jsdom = require('jsdom');
var fs = require('fs');
var jquery = fs.readFileSync("./libs/jquery.js", "utf-8");
let cheerio = require('cheerio')

module.exports = {
    get: function(id, callback, version) {
        var _ = this;
        try{

            if(version!==undefined){
                version = '/' + version;
            }

            _http.get('https://hopamchuan.com/song/' + id + '/hihehe' + version, function(res) {
                try{
                    var body = '';
                    res.on('data', function(data) {
                        body += data;
                    });

                    res.on('end', function() {
                        _.parse(id, body, callback, version);
                    });
                }catch(err){console.log('==========' + id);console.log(err)};
            
            });
        }catch(err){console.log('==========' + id);console.log(err)};
    },

    parse: function(id, httpContent, callback, version) {
        var _ = this;
        jsdom.env({
            html: httpContent,
            src: [jquery],
            done: function(err, window) {
                try{
                    var $ = window.$;
                    var title = $('#song-title').text().trim();
                    var rythm = $('#display-rhythm').text().trim();
                    var chord  =$('#song-key').text().trim();
                    var authors = $('#song-author').text().trim().split('         ').map(function(e){return e.trim()}).filter(function(e){return e!==''});
                    var singer = authors[0];
                    var author = authors[1];                
                    var Q = cheerio.load($('#song-lyric')[0].innerHTML);
                    var content='';
                    Q('.chord_lyric_line').each(function(i,value){
                        content = content + '\r\n' + Q(this).text();
                    });

                    if(version == undefined){
                        var version = [];    
                        Q('#version-select-box option').each(function(d,i){
                            version.push({
                                songId:id,
                                description:'',
                                star:'',
                                votes:''
                            });
                        });
                    }

                    callback({id: id, title: title, rythm:rythm, chord: chord,singer:singer, author: author,  content: content});

                }catch(err){
                    console.log('     ' + id);
                    console.log(err);
                }
                
            }
        });
    }
};