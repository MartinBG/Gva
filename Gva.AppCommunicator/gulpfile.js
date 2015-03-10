'use strict';
/*global require, console*/
/*jshint -W097*/ // Use the function form of "use strict".
var gulp = require('gulp'),
    utils = require('../gulp/utils'),
    tasks = require('../gulp/tasks'),
    config;

config = {
  version: '1.0.0',
  msbuild: {
    projFile: '../Gva.AppCommunicator/Gva.AppCommunicator.csproj',
    iisAppName: 'AppCommunicator',
    packageLocation: '../build/gva_appcomm/gva_appcomm%s.zip' //location is relative to the csproj file
  },
};

gulp.task('package-debug'            , []              , tasks['package-debug']    (config));
gulp.task('package-release'          , []              , tasks['package-release']  (config));

gulp.on('err', function (e) {
  console.log(e.err.stack);
});

gulp.task('default', ['package-debug']);
