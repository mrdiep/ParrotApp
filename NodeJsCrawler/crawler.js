var _http = require('https');
_http.globalAgent.maxSockets = 2;

var jsdom = require('jsdom');
var fs = require('fs');
var jquery = fs.readFileSync("./libs/jquery.js", "utf-8");
let cheerio = require('cheerio')
let async = require('async');

module.exports = {
    get: function(id, callback, version) {
        let _ = this;
        try {

            if (version !== undefined) {
                version = '/' + version;
            }

            _http.get('https://hopamchuan.com/song/' + id + '/hihehe' + version, function(res) {
                try {
                    let body = '';
                    res.on('data', function(data) {
                        body += data;
                    });

                    res.on('end', function() {
                        _.parse(id, body, callback, version);
                    });
                } catch (err) {
                    console.log('==========' + id);
                    console.log(err)
                };

            });
        } catch (err) {
            console.log('==========' + id);
            console.log(err)
        };
    },

    parse: function(id, httpContent, callback, version) {
        let _ = this;
        jsdom.env({
            html: httpContent,
            src: [jquery],
            done: function(err, window) {
                try {
                    let $ = window.$;
                    let title = $('#song-title').text().trim();
                    let rythm = $('#display-rhythm').text().trim();
                    let chord = $('#song-key').text().trim();
                    let authors = $('#song-author').text().trim().split('         ').map(function(e) {
                        return e.trim()  
                    }).filter(function(e) {
                        return e !== ''
                    });
                    let singer = authors[0];
                    let author = authors[1];

                    let Q = cheerio.load($('#song-lyric')[0].innerHTML);
                    let content = '';
                    Q('.chord_lyric_line').each(function(i, value) {
                        content = content + '\r\n' + Q(this).text();
                    });

                    let downloadVersion = function() {
                        return new Promise(function(resolve, reject) {
                            if (version == undefined) {
                                let contentVersions = [];
                                function parseNumber(text){
                                    if(text===null || text===undefined|| text.trim()=='') return 0;
                                    var value = parseInt(text);
                                    if(value==NaN) return 0; else return value;
                                };

                                let versions = [];
                                Q = cheerio.load($('#version-select')[0].innerHTML);
                                Q('option').each(function(d, i) {
                                    let item = Q(this);
                                    versions.push({
                                        songId: id,
                                        description:item.attr('data-description'),
                                        star:parseNumber(item.attr('data-star')),
                                        votes: parseNumber(item.attr('data-votes')),
                                        urlValue: item.attr('value')
                                    });
                                });

                                let downloadVersionTasks = [];
                                if (versions.length > 1) {
                                    for (let i = 0; i < versions.length; i++) { //versions.length
                                        (function(versionInfo) {
                                            downloadVersionTasks.push(function(callback) {
                                                _.get(id, function(songData) {
                                                    contentVersions.push({
                                                        content: songData.content,
                                                        chord: songData.chord,
                                                        description: versionInfo.description,
                                                        star: versionInfo.star,
                                                        votes: versionInfo.votes
                                                    });
                                                    callback(null, songData);
                                                }, versionInfo.urlValue);
                                            });
                                        })(versions[i]);
                                    }

                                    async.parallel(downloadVersionTasks, function(err, results) {
                                        console.log('parallel done');
                                        resolve(contentVersions);
                                    });
                                } else {
                                    console.log('no version');
                                    resolve([]);
                                }

                            } else {
                                resolve(null);
                            }
                        });
                    }

                    downloadVersion().then(function(results) {
                        var songData = {
                            id: id,
                            title: title.trim(),
                            rythm: rythm.trim(),
                            chord: chord.trim(),
                            singer: singer.trim(),
                            author: author.trim(),
                            content: content.trim(),
                            version: results
                        };

                        if (results === null) {
                            delete songData.version;
                            delete songData.id;
                            delete songData.title;
                            delete songData.rythm;
                            delete songData.singer;
                            delete songData.author;
                        } else if(results.length==0){
                             delete songData.version;
                        }

                        console.log('download completed : ' + title + '      version: ' + version);
                        callback(songData);
                    });
                } catch (err) {
                    console.log('     ' + id);
                    console.log(err);
                    callback(null);
                }
            }
        });
    }
};