/*global module, require*/
module.exports = function (grunt) {
  'use strict';

  var _ = require('lodash'),
      tv4 = require('tv4');

  grunt.registerMultiTask('tv4', 'JSON Schema validation.', function() {
    var options = this.options({
        formats: {}
      }),
      hasErrors;

    grunt.verbose.writeflags(options, 'Options');
    tv4.addFormat(options.formats);

    this.files.forEach(function(f) {
      var schema;

      if (!grunt.file.exists(f.dest)) {
        grunt.fail.warn('Schema file "' + f.dest + '" not found.');
      } else {
        schema = grunt.file.readJSON(f.dest);

        f.src.filter(function(filepath) {
          if (!grunt.file.exists(filepath)) {
            grunt.fail.warn('Data file "' + filepath + '" not found.');
            return false;
          } else {
            return true;
          }
        })
        .forEach(function(filepath) {
          if (hasErrors) {
            return;
          }
        
          var data = require('./' + filepath),
              failed;

          _.forOwn(data, function(value, key) {
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

  // Project configuration.
  grunt.initConfig({
    buildDir: 'build',
    jshint: {
      options: {
        jshintrc: '.jshintrc'
      },
      source: [
        'gruntfile.js',
        'js/**/*.js',
        'test/**/*.js'
      ],
      schema: [
        'schema/**/*.js'
      ]
    },
    html2js: {
      options: {
        base: 'js'
      },
      navigation: {
        src: [ 'js/navigation/**/*.html' ],
        dest: '<%= buildDir %>/templates/navigation.js',
        module: 'navigation.templates'
      },
      users: {
        src: [ 'js/users/**/*.html' ],
        dest: '<%= buildDir %>/templates/users.js',
        module: 'users.templates'
      },
      scaffolding: {
        src: ['js/scaffolding/**/*.html'],
        dest: '<%= buildDir %>/templates/scaffolding.js',
        module: 'scaffolding.templates'
      },
      directiveTests: {
        src: ['test/e2e/directives/**/**/*.html'],
        dest: '<%= buildDir %>/templates/directive-tests.js',
        module: 'directive-tests.templates'
      }
    },
    watch:{
      html: {
        files:['js/**/*.html'],
        tasks:['html2js']
      }
    },
    express: {
      server: {
        options: {
          hostname: 'localhost',
          port: 52560,
          bases: ['../', '../app']
        }
      }
    },
    exec: {
      protractor: {
        cmd: 'protractor.cmd ../../test/e2e/ptorConf.js',
        cwd: './node_modules/.bin/'
      }
    },
    tv4: {
      options: {
        formats: {
          'date-time': function (data) {
            if (isNaN(Date.parse(data))) {
              return 'must be a date string in ISO 8601 format';
            }
            return null;
          }
        }
      },
      schemas: {
        files: {
          'schema/person-data.json': 'schema/person-data.sample.js',
          'schema/person-address.json': 'schema/person-address.sample.js',
          'schema/person-status.json': 'schema/person-status.sample.js',
          'schema/person-document-employment.json': 'schema/person-document-employment.sample.js',
          'schema/person-document-education.json': 'schema/person-document-education.sample.js',
          'schema/person-document-id.json': 'schema/person-document-id.sample.js',
          'schema/person-document-other.json': 'schema/person-document-other.sample.js',
          'schema/person-document-training.json': 'schema/person-document-training.sample.js'
        }
      }
    }
  });
  
  grunt.loadNpmTasks('grunt-contrib-jshint');
  grunt.loadNpmTasks('grunt-contrib-watch');
  grunt.loadNpmTasks('grunt-html2js');
  grunt.loadNpmTasks('grunt-exec');
  grunt.loadNpmTasks('grunt-express');

  grunt.registerTask('debug', ['jshint:source', 'html2js' ]);
  grunt.registerTask('test', ['debug', 'express', 'exec:protractor']);
  grunt.registerTask('sv', ['jshint:schema', 'tv4']);

  grunt.registerTask('default', ['debug']);
  
};
