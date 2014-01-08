/*global module, require*/
module.exports = function (grunt) {
  'use strict';

  var _ = require('lodash'),
      tv4 = require('tv4');

  grunt.registerMultiTask('tv4', 'JSON Schema validation.', function () {
    var options = this.options({
      formats: {}
    }),
      hasErrors;

    grunt.verbose.writeflags(options, 'Options');
    tv4.addFormat(options.formats);

    this.files.forEach(function (f) {
      var schema;

      if (!grunt.file.exists(f.dest)) {
        grunt.fail.warn('Schema file "' + f.dest + '" not found.');
      } else {
        schema = grunt.file.readJSON(f.dest);

        f.src.filter(function (filepath) {
          if (!grunt.file.exists(filepath)) {
            grunt.fail.warn('Data file "' + filepath + '" not found.');
            return false;
          } else {
            return true;
          }
        })
        .forEach(function (filepath) {
          if (hasErrors) {
            return;
          }

          var data = require('../' + filepath),
              failed;

          _.forOwn(data, function (value, key) {
            if (!tv4.validate(value, schema)) {
              grunt.log.error(filepath);
              grunt.log.error('Error at item: ' + key + ', dataPath: ' + tv4.error.dataPath);
              grunt.log.error(tv4.error.message);
              failed = true;
              hasErrors = true;
            }
          });

          if (!failed) {
            grunt.log.ok(filepath);
          }
        });
      }
    });

    return hasErrors;
  });
};
