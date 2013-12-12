/*global module, require*/
/*jshint maxlen: false */
module.exports = function (grunt) {
  'use strict';

  var _ = require('lodash'),
      tv4 = require('tv4'),
      crypto = require('crypto');

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
    jsBundles: {
      '<%= buildDir %>/js/app.js': [
        'js/l10n/*.js',
        'js/*.js',
        'js/scaffolding/*.js',
        'js/scaffolding/**/*.js',
        'js/navigation/*.js',
        'js/navigation/**/*.js',
        'js/users/*.js',
        'js/users/**/*.js',
        'js/gva/*.js',
        'js/gva/**/*.js'
      ],
      '<%= buildDir %>/js/sample.data.js': [
        'schema/requireShim.js',
        'schema/nomenclatures.sample.js',
        'schema/person-data.sample.js',
        'schema/person-document-education.sample.js',
        'schema/person-document-employment.sample.js',
        'schema/person-document-id.sample.js',
        'schema/person-document-other.sample.js',
        'schema/person-document-training.sample.js',
        'schema/person-address.sample.js',
        'schema/person-status.sample.js',
        'schema/person.sample.js'
      ],
      '<%= buildDir %>/js/templates.js': [
        'build/templates/*.js'
      ],
      '<%= buildDir %>/js/lib.js': [
        'bower_components/jquery-modern/jquery.js',
        'bower_components/lodash/dist/lodash.js',
        'bower_components/select2/select2.js',
        'bower_components/angular/angular.js',
        'bower_components/angular-i18n/angular-locale_bg-bg.js',
        'bower_components/angular-l10n/build/l10n-with-tools.js',
        'bower_components/angular-resource/angular-resource.js',
        'bower_components/angular-ui-select2/src/select2.js',
        'bower_components/angular-ui-utils/modules/jq/jq.js',
        'bower_components/angular-bootstrap/dist/ui-bootstrap-tpls-0.7.0.js',
        'bower_components/bootstrap/js/collapse.js',
        'bower_components/angular-ui-router/release/angular-ui-router.js',
        'bower_components/bootstrap-datetimepicker/src/js/bootstrap-datetimepicker.js',
        'bower_components/bootstrap-datetimepicker/src/js/locales/*.bg.js',
        'bower_components/datatable/media/js/jquery.dataTables.js',
        'bower_components/datatablePlugins/integration/bootstrap/3/dataTables.bootstrap.js',
        'bower_components/jquery-ui/ui/jquery.ui.widget.js',
        'bower_components/blueimp-file-upload/js/jquery.iframe-transport.js',
        'bower_components/blueimp-file-upload/js/jquery.fileupload.js',
        'bower_components/angular-mocks/angular-mocks.js'
      ],
      '<%= buildDir %>/js/lib.ie8.js': [
        'bower_components/html5shiv/dist/html5shiv.js',
        'bower_components/respond/respond.src.js',
        'bower_components/jquery-legacy/jquery.js',
        'bower_components/lodash/dist/lodash.compat.js',
        'bower_components/es5-shim/es5-shim.js',
        'bower_components/select2/select2.js',
        'bower_components/angular/angular.js',
        'bower_components/angular-i18n/angular-locale_bg-bg.js',
        'bower_components/angular-l10n/build/l10n-with-tools.js',
        'bower_components/angular-resource/angular-resource.js',
        'bower_components/angular-ui-select2/src/select2.js',
        'bower_components/angular-ui-utils/modules/jq/jq.js',
        'bower_components/angular-bootstrap/dist/ui-bootstrap-tpls-0.7.0.js',
        'bower_components/bootstrap/js/collapse.js',
        'bower_components/angular-ui-router/release/angular-ui-router.js',
        'bower_components/bootstrap-datetimepicker/src/js/bootstrap-datetimepicker.js',
        'bower_components/bootstrap-datetimepicker/src/js/locales/*.bg.js',
        'bower_components/datatable/media/js/jquery.dataTables.js',
        'bower_components/datatablePlugins/integration/bootstrap/3/dataTables.bootstrap.js',
        'bower_components/jquery-ui/ui/jquery.ui.widget.js',
        'bower_components/blueimp-file-upload/js/jquery.iframe-transport.js',
        'bower_components/blueimp-file-upload/js/jquery.fileupload.js',
        'bower_components/angular-mocks/angular-mocks.js'
      ],
      '<%= buildDir %>/js/test.js': [
        'test/e2e/**/*.js',
        '!test/e2e/gvaBy.js',
        '!test/e2e/ptorConf.js',
        '!test/e2e/expressServer.js',
        '!test/e2e/spec/**/*.js',
        '!test/e2e/directives/**/specs/*.js'
      ]
    },
    cssBundles: {
      '<%= buildDir %>/css/styles.css': [
        'bower_components/bootstrap/dist/css/bootstrap.css',
        'bower_components/select2/select2.css',
        'bower_components/select2-bootstrap-css/select2-bootstrap.css',
        'bower_components/datatablePlugins/integration/bootstrap/3/dataTables.bootstrap.css',
        'bower_components/bootstrap-datetimepicker/build/css/bootstrap-datetimepicker.min.css',
        'bower_components/blueimp-file-upload/css/jquery.fileupload-ui.css',
        'bower_components/blueimp-file-upload/css/jquery.fileupload.css',
        'css/*.css'
      ]
    },
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
    clean: {
      build: {
        src: '<%= buildDir %>'
      }
    },
    cssmin: {
      css: {
        files: '<%= cssBundles %>'
      }
    },
    uglify: {
      options: {
        debug: true,
        mangle: false,
        compress: false,
        sourceMap: function(path) {
          var n = path.lastIndexOf('/'),
              filename = path.substring(n + 1);
          return 'build/js/' + filename.replace(/\.js/,'.js.map');
        },
        sourceMappingURL: function(path) {
          var n = path.lastIndexOf('/'),
              filename = path.substring(n + 1);
          return filename.replace(/\.js/,'.js.map');
        },
        sourceMapRoot: '../../'
      },
      js: {
        files: '<%= jsBundles %>'
      }
    },
    concat_sourcemap: {
      all: {
        options: {
          sourceRoot: '/',
          sourcesContent: true
        },
        files: ['<%= jsBundles %>', '<%= cssBundles %>']
      }
    },
    copy: {
      resources: {
        files: [
          { expand:true, src: '**'           , dest: '<%= buildDir %>/fonts/' , cwd: 'bower_components/bootstrap/fonts/' },
          { expand:true, src: '{*.png,*.gif}', dest: '<%= buildDir %>/css/'   , cwd: 'bower_components/select2/' },
          { expand:true, src: '**'           , dest: '<%= buildDir %>/images/', cwd: 'bower_components/datatablePlugins/integration/bootstrap/images/' },
          { expand:true, src: '**'           , dest: '<%= buildDir %>/img'    , cwd: 'bower_components/blueimp-file-upload/img/' },
          { expand:true, src: '**'           , dest: '<%= buildDir %>/img'    , cwd: 'img' }
        ]
      }
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
      persons: {
        src: ['js/gva/**/*.html'],
        dest: '<%= buildDir %>/templates/gva.js',
        module: 'gva.templates'
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
        files:['css/**', 'js/**', 'schema/**', 'test/**'],
        tasks:['html2js', 'concat_sourcemap', 'template']
      }
    },
    express: {
      server: {
        options: {
          hostname: 'localhost',
          port: 52560,
          bases: ['../', '../app/build/'],
          server: 'test/e2e/expressServer.js'
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
    },
    template: {
      options: {
        data: {
          md5: function(file) {
            return crypto.createHash('md5').update(grunt.file.read(file), 'utf8').digest('hex');
          }
        }
      },
      index: {
        files: {
          'build/index.html': ['index.html']
        }
      }
    }
  });
  
  grunt.loadNpmTasks('grunt-contrib-jshint');
  grunt.loadNpmTasks('grunt-contrib-watch');
  grunt.loadNpmTasks('grunt-html2js');
  grunt.loadNpmTasks('grunt-exec');
  grunt.loadNpmTasks('grunt-express');
  grunt.loadNpmTasks('grunt-contrib-cssmin');
  grunt.loadNpmTasks('grunt-contrib-uglify');
  grunt.loadNpmTasks('grunt-contrib-copy');
  grunt.loadNpmTasks('grunt-contrib-clean');
  grunt.loadNpmTasks('grunt-concat-sourcemap');
  grunt.loadNpmTasks('grunt-template');

  grunt.registerTask('debug',
    ['clean', 'jshint:source', 'html2js', 'concat_sourcemap', 'copy', 'template']);

  grunt.registerTask('test',
    ['clean', 'jshint:source', 'html2js', 'concat_sourcemap', 'copy', 'template', 'express', 'exec:protractor']);

  grunt.registerTask('release',
    ['clean', 'jshint:source', 'html2js', 'uglify', 'cssmin', 'copy', 'template']);

  grunt.registerTask('test-release',
    ['clean', 'jshint:source', 'html2js', 'uglify', 'cssmin', 'copy', 'template', 'express', 'exec:protractor']);
 
  grunt.registerTask('sv', ['jshint:schema', 'tv4']);

  grunt.registerTask('test-server', ['express', 'express-keepalive']);

  grunt.registerTask('default', ['debug']);
  
};
