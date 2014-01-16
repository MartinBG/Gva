/*global module, require*/
module.exports = function (grunt) {
  'use strict';

  var _ = require('lodash'),
      crypto = require('crypto');

  grunt.registerMultiTask('bundle', 'Process template with bundles.', function() {
    // Merge task-specific and/or target-specific options with these defaults:
    var options = this.options({
      'appPath': '',
      'debug': false,
      'data': {}
    });

    var data = typeof options.data === 'function' ?
      options.data() :
      options.data;

    var bundles = {};

    options.bundles.forEach(function (bundleGroup) {
      _.forOwn(bundleGroup, function (files, bundle) {
        bundles[grunt.config.process(bundle)] = grunt.file.expand(files);
      });
    });

    var templateContext =   {
      md5: function (file) {
        return crypto.createHash('md5').update(grunt.file.read(file), 'utf8').digest('hex');
      },
      jsBundle: function (bundle) {
        return templateContext.bundle(bundle, function (file) {
          return '<script src="' + options.appPath + '/' + file + '?_h=' +
            templateContext.md5(file) + '"></script>';
        });
      },
      cssBundle: function (bundle) {
        return templateContext.bundle(bundle, function (file) {
          return '<link href="' + options.appPath + '/' + file + '?_h=' +
            templateContext.md5(file) + '" rel="stylesheet" />';
        });
      },
      bundle: function (bundle, template) {
        if (options.debug) {
          if (!bundles[bundle]) {
            grunt.log.warn('Bundle `' + bundle + '` not found.');
            return '';
          }

          return _(bundles[bundle]).map(template).reduce(function (a, b) {
            return a + '\r\n' + b;
          });
        }
        else {
          return template(bundle);
        }
      }
    };

    // Iterate over all specified file groups.
    this.files.forEach(function(file) {
      // Concat specified files.
      var src = file.src.filter(function(filePath) {
        // Warn on and remove invalid source files.
        if (!grunt.file.exists(filePath)) {
          grunt.log.warn('Source file `' + filePath + '` not found.');
          return false;
        } else {
          return true;
        }
      });
      if (!src.length) {
        grunt.log.warn('Destination `' +
          file.dest + '` not written because `src` files were empty.');
        return;
      }
      var template = src.map(function(filePath) {
        // Read file source.
        return grunt.file.read(filePath);
      }).join('\n');

      var result = grunt.template.process(template, {
        'data': _.assign(templateContext, data)
      });

      // Write the destination file
      grunt.file.write(file.dest, result);

      // Print a success message
      grunt.log.writeln('File `' + file.dest + '` created.');
    });
  });
};
