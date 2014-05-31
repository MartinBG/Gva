'use strict';
/*global require, module*/
/*jshint -W097*/ // Use the function form of "use strict".
var stream = require('stream');
var path = require('path');

var _ = require('lodash');
var streamqueue = require('streamqueue');
var uniqueStream = require('unique-stream');

var utils = {
  queue: function queue() {
    var streams = _.isArray(arguments[0]) ? arguments[0] : _.toArray(arguments);

    streams = _.map(streams, function (s) {
      return s.pipe(stream.PassThrough({ objectMode: true }));
    });

    return streamqueue.apply(null, [{ objectMode: true }].concat(streams));
  },
  uniqueQueue: function uniqueQueue() {
    var streams = _.isArray(arguments[0]) ? arguments[0] : _.toArray(arguments);
  
    return utils.queue(streams).pipe(uniqueStream(function (file) {
      return path.relative(file.base, file.path);
    }));
  }
};

module.exports = utils;