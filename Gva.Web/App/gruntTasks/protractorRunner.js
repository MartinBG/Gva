/*global module, require, process*/
module.exports = function (grunt) {
  'use strict';

  var _ = require('lodash'),
      protractorRunner = require('../node_modules/protractor/lib/runner.js'),
      path = require('path');

  grunt.registerMultiTask('protractor', 'Protractor runner.', function() {
    var done = this.async(),
      options = this.options({
        configFilename: '',
        config: {}
      }),
      configFilePath,
      config;

    grunt.verbose.writeflags(options, 'Options');

    if (options.configFilename) {
      configFilePath = path.resolve(process.cwd(), options.configFilename);
      config = require(configFilePath).config;
      config.specFileBase = path.dirname(configFilePath);
    }

    _.merge(config, options.config);

    grunt.verbose.writeflags(config, 'Runner config');
    protractorRunner.addConfig(config);

    function fail(err) {
      grunt.log.error('Protractor failed.');
      grunt.log.error(err);
      done(false);
    }
    
    try {
      protractorRunner
        .runOnce()
        .then(function () {
          done();
        }, function (err) {
          fail(err);
        });
    } catch (err) {
      fail(err);
    }
  });
};
