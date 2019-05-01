"use strict";

var gulp = require("gulp"),
    fs = require("fs"),
    util = require('gulp-util'),
    request = require('request'),
    path = require('path'),
    cheerio = require('cheerio');


var settingsFileName = "manifest.json",
credentials = 'Basic c211cmdvOnNtdXJnb3Bhc3MxMjM0Kg==', //ojo es mi usuario chappal
settingsTransformPath = path.resolve(__dirname) + "/",
upcomingVersion = '',
urlUnfuddle = 'https://sandinas.unfuddle.com/api/v1/projects/1',
endpoint = 'milestones/upcoming.xml',
urlEndpoint = urlUnfuddle +'/' + endpoint

gulp.task("default", ['settings:compile']);

gulp.task("settings:compile", function () {

    var options = {
        url: urlEndpoint,
        headers: {
            'Authorization': credentials,
            'Accept': 'application/xml'
        }
    };

    function callback(error, response, body) {
        if (!error && response.statusCode === 200) {
            var info = body
            var $ = cheerio.load(body, {
                xmlMode: true
            });
            upcomingVersion = $('title').text()
            var newSettings = require(settingsTransformPath + settingsFileName);
            var environmentSettings = require(settingsTransformPath + settingsFileName);
            for (var prop in newSettings) {
                if (environmentSettings.hasOwnProperty(prop) && prop === "version") {
                    newSettings[prop] = upcomingVersion;

                    util.log('Se seteo correctamente la version ' + upcomingVersion);
                }
            }

            return newFile(settingsFileName, JSON.stringify(newSettings))
             .pipe(gulp.dest(settingsTransformPath));
        } else {

            util.log("ERROR");
        }
    }

    request(options, callback);

});


function newFile(name, contents) {
    //uses the node stream object
    var readableStream = require('stream').Readable({ objectMode: true });
    //reads in our contents string
    readableStream._read = function () {
        this.push(new util.File({ cwd: "", base: "", path: name, contents: new Buffer(contents) }));
        this.push(null);
    }
    return readableStream;
};
