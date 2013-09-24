/*global module:false*/
module.exports = function (grunt) {
  'use strict';

  // Project configuration.
  grunt.initConfig({
    buildDir: 'build',
    jshint: {
      options: {
        jshintrc: '.jshintrc'
      },
      all: [
        'gruntfile.js',
        'js/**/*.js',
        'test/**/*.js'
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
        command: 'protractor test/e2e/ptorConf.js'
      }
    }
  });
  
  grunt.loadNpmTasks('grunt-contrib-jshint');
  grunt.loadNpmTasks('grunt-contrib-watch');
  grunt.loadNpmTasks('grunt-html2js');
  grunt.loadNpmTasks('grunt-exec');
  grunt.loadNpmTasks('grunt-express');

  grunt.registerTask('debug', ['jshint', 'html2js' ]);
  grunt.registerTask('default', [ 'debug' ]);
  grunt.registerTask('test', [ 'debug', 'express', 'exec']);
};
