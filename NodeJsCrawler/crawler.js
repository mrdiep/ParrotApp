var _http = require('https');
_http.globalAgent.maxSockets = 3;

var jsdom = require('jsdom');
var fs = require('fs');
var jquery = fs.readFileSync("./libs/jquery.js", "utf-8");
let cheerio = require('cheerio')
let async = require('async');

                                function parseNumber(text){
                                    if(text===null || text===undefined|| text.trim()=='') return 0;
                                    var value = parseInt(text);
                                    if(value===NaN) 
                                    return 0;
                                     else 
                                     return value;
                                };
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
                    let singer = $('#song-author .author-item:first').text();
                    let author = $('#song-author .author-item:last').text();
                    let star = parseNumber($('#contribute-rating-control').attr('data-stars'));
                    let votes = parseNumber($('.contribute-rate-desc').text().trim().replace(' người bình chọn',''));
                    let updated = $('#song-date-time').text().trim().split('\n')[0].replace('Cập nhật ngày ','').replace(' tháng ','-').replace(', ','-');
                    let postUser =$('.song-poster-username a').attr('href').split('/')[5];

                    let Q = cheerio.load($('#song-lyric')[0].innerHTML);
                    let content = '';
                    Q('.chord_lyric_line').each(function(i, value) {
                        content = content + '\r\n' + Q(this).text();
                    });

                    let downloadVersion = function() {
                        return new Promise(function(resolve, reject) {
                            if (version == undefined) {
                                let contentVersions = [];
                                let versions = [];
                                Q = cheerio.load($('#version-list')[0].innerHTML);
                                Q('#other-versions .version-link').each(function(d, i) {
                                    let item = Q(this);
                                    versions.push({
                                        songId: id,
                                        versionId:id+"000".substring(0, 3- ((d+1)+'').length) + (d+1),
                                        //description:item.attr('data-description'),
                                        //star:parseNumber(item.attr('data-star')),
                                        //votes: parseNumber(item.attr('data-votes')),
                                        postUser: item.attr('href').split('/')[6],
                                        url:item.attr('href')
                                    });
                                });

                                let downloadVersionTasks = [];
                                if (versions.length > 1) {
                                    for (let i = 0; i < versions.length; i++) {
                                        (function(versionInfo) {
                                            downloadVersionTasks.push(function(callback) {
                                                _.get(id, function(songData) {
                                                    contentVersions.push({
                                                        content: songData.content,
                                                        chord: songData.chord,
                                                        description: songData.description,
                                                        star: songData.star,
                                                        votes: songData.votes,
                                                        updated:songData.updated,
                                                        postUser:versionInfo.postUser,
                                                        versionId : versionInfo.versionId,
                                                        url:versionInfo.url
                                                    });
                                                    callback(null, songData);
                                                }, versionInfo.postUser);
                                            });
                                        })(versions[i]);
                                    }

                                    async.parallel(downloadVersionTasks, function(err, results) {
                                        console.log('parallel done');
                                        resolve({type: 'success', versions: contentVersions});
                                    });
                                } else {
                                    console.log('no version');
                                    resolve({type:'no-version'});
                                }

                            } else {
                                resolve({type:'no-resolve'});
                            }
                        });
                    }

                    downloadVersion().then(function(results) {
                        if(singer==undefined)
                            singer='';
                         if(author==undefined)
                            author='';
                         
                        var songData = {
                            id: id,
                            title: title.trim(),
                            rythm: rythm.trim(),
                            chord: chord.trim(),
                            singer: singer.trim(),
                            author: author.trim(),
                            content: content.trim(),
                            description:content.trim(),
                            description: content.trim().substring(0,50).replace('\n',' ').replace('\r',' '),
                            votes:votes,
                            star:star,
                            updated:updated,
                        };

                        function getDefaultSongVersion(){
                            return [{
                                    chord: chord.trim(),
                                    content: content.trim(),
                                    description: content.trim().substring(0,50).replace('\n',' ').replace('\r',' '),
                                    star: star,
                                    votes: votes,
                                    updated:updated,
                                    postUser:postUser,
                                    versionId:id+'000',
                                    default:true
                             }];
                        }
                        function updateVersionDefault(){
                            for(var i=0;i<results.versions.length;i++){
                                if(results.versions[i].postUser===postUser){
                                    results.versions[i].default = true;
                                }
                                else {
                                    results.versions[i].default = false;
                                }
                            }

                            return results.versions;

                        }
                        if (results.type === 'no-resolve') {
                            delete songData.version;
                            delete songData.id;
                            delete songData.title;
                            delete songData.rythm;
                            delete songData.singer;
                            delete songData.author;

                        } else if(results.type=='no-version'){
                            delete songData.content;
                            delete songData.star;
                            delete songData.votes;
                            delete songData.description;
                            delete songData.updated;

                             songData.version = getDefaultSongVersion();
                        } else if(results.type=='success'){
                            delete songData.content;
                            delete songData.star;
                            delete songData.votes;
                            delete songData.description;
                            delete songData.updated;

                            //removeVersionHasContent(content);

                            songData.version = updateVersionDefault();
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