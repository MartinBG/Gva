'use strict';
/*global require, console*/
/*jshint -W097*/ // Use the function form of "use strict".
var gulp = require('gulp');

var utils = require('../gulp/utils');
var tasks = require('../gulp/tasks');

var config = {
  version: '1.0.0',
  msbuild: {
    projFile: '../Mosv.Web.Host/Mosv.Web.Host.csproj',
    iisAppName: 'Mosv',
    packageLocation: '../build/mosv/mosv%s.zip' //location is relative to the csproj file
  },
  outputDir: '../Mosv.Web.Host/App/',
  jsDir: 'js/',
  cssDir: 'css/',
  templatesDir: 'templates/',
  lint: function () {
    return gulp.src([
      'gulpfile.js',
      '../gulp/**/*.js',
      '../Common.Web.App/js/**/*.js',
      'js/**/*.js',
      'test/**/*.js'
    ]);
  },
  app: function () {
    return utils.uniqueQueue(
      gulp.src('js/app.js', { base: '.' }),
      gulp.src('../Common.Web.App/js/ems/ems.js', { base: '../Common.Web.App/' }),
      gulp.src('js/**/*.js', { base: '.' }),
      gulp.src('../Common.Web.App/js/**/*.js', { base: '../Common.Web.App/' })
    );
  },
  lib: function () {
    return gulp.src([
      'bower_components/jquery-modern/jquery.js',
      'bower_components/lodash/dist/lodash.js',
      'bower_components/select2/select2.js',
      'bower_components/angular/angular.js',
      'bower_components/angular-i18n/angular-locale_bg-bg.js',
      'bower_components/angular-l10n/build/l10n-with-tools.js',
      'bower_components/angular-resource/angular-resource.js',
      'bower_components/angular-ui-select2/src/select2.js',
      'bower_components/angular-ui-sortable/src/sortable.js',
      'bower_components/angular-ui-utils/modules/jq/jq.js',
      'bower_components/angular-bootstrap/ui-bootstrap-tpls.js',
      'bower_components/angular-ui-router/release/angular-ui-router.js',
      'bower_components/bootstrap-datepicker/js/bootstrap-datepicker.js',
      'bower_components/bootstrap-datepicker/js/locales/*.bg.js',
      'bower_components/jquery-ui/ui/jquery.ui.widget.js',
      'bower_components/jquery-ui/ui/jquery.ui.core.js',
      'bower_components/jquery-ui/ui/jquery.ui.mouse.js',
      'bower_components/jquery-ui/ui/jquery.ui.sortable.js',
      'bower_components/blueimp-file-upload/js/jquery.iframe-transport.js',
      'bower_components/blueimp-file-upload/js/jquery.fileupload.js',
      'bower_components/angular-mocks/angular-mocks.js',
      'bower_components/moment/moment.js',
      'bower_components/angular-scrollto/angular-scrollto.js',
      'bower_components/autofill-event/src/autofill-event.js',
      'bower_components/jquery-treetable/javascripts/src/jquery.treetable.js'
    ], { base: '.' });
  },
  lib_ie8: function () {
    return gulp.src([
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
      'bower_components/angular-ui-sortable/src/sortable.js',
      'bower_components/angular-ui-utils/modules/jq/jq.js',
      'bower_components/angular-bootstrap/ui-bootstrap-tpls.js',
      'bower_components/bootstrap/js/collapse.js',
      'bower_components/angular-ui-router/release/angular-ui-router.js',
      'bower_components/bootstrap-datepicker/js/bootstrap-datepicker.js',
      'bower_components/bootstrap-datepicker/js/locales/*.bg.js',
      'bower_components/jquery-ui/ui/jquery.ui.widget.js',
      'bower_components/jquery-ui/ui/jquery.ui.core.js',
      'bower_components/jquery-ui/ui/jquery.ui.mouse.js',
      'bower_components/jquery-ui/ui/jquery.ui.sortable.js',
      'bower_components/blueimp-file-upload/js/jquery.iframe-transport.js',
      'bower_components/blueimp-file-upload/js/jquery.fileupload.js',
      'bower_components/angular-mocks/angular-mocks.js',
      'bower_components/moment/moment.js',
      'bower_components/autofill-event/src/autofill-event.js'
    ], { base: '.' });
  },
  css: function () {
    return gulp.src([
      'bower_components/bootstrap/dist/css/bootstrap.css',
      'bower_components/font-awesome/css/font-awesome.css',
      'bower_components/select2/select2.css',
      'bower_components/select2-bootstrap-css/select2-bootstrap.css',
      'bower_components/bootstrap-datepicker/css/datepicker3.css',
      'bower_components/blueimp-file-upload/css/jquery.fileupload-ui.css',
      'bower_components/blueimp-file-upload/css/jquery.fileupload.css',
      'css/*.css'
    ], { base: '.' });
  },
  templates: {
    'common.templates': function () {
      return utils.uniqueQueue(
        gulp.src('js/common/**/*.html', { base: '.' }),
        gulp.src('../Common.Web.App/js/common/**/*.html', { base: '../Common.Web.App/' })
      );
    },
    'mosv.templates': function () {
      return gulp.src('js/mosv/**/*.html', { base: '.' });
    },
    'ems.templates': function () {
      return utils.uniqueQueue(
        gulp.src('js/ems/**/*.html', { base: '.' }),
        gulp.src('../Common.Web.App/js/ems/**/*.html', { base: '../Common.Web.App/' })
      );
    },
    'scaffolding.templates': function () {
      return gulp.src('../Common.Web.App/js/scaffolding/**/*.html', { base: '../Common.Web.App/' });
    }
  },
  assets: {
    '../fonts/': function (opts) {
      return utils.uniqueQueue(
        gulp.src('bower_components/bootstrap/dist/fonts/**', opts),
        gulp.src('bower_components/font-awesome/fonts/**', opts)
      );
    },
    './': function (opts) {
      return gulp.src('bower_components/select2/*.+(png|gif)', opts);
    },
    '../img/': function (opts) {
      return utils.uniqueQueue(
        gulp.src('bower_components/blueimp-file-upload/img/**', opts),
        gulp.src('img/**', opts)
      );
    }
  }
};

gulp.task('clean'          ,                    tasks.clean             (config));
gulp.task('lint'           ,                    tasks.lint              (config));
gulp.task('debug'          , ['lint', 'clean'], tasks.debug             (config));
gulp.task('bundled'        , ['lint', 'clean'], tasks.bundled           (config));
gulp.task('release'        , ['lint', 'clean'], tasks.release           (config));
gulp.task('design'         , ['lint', 'clean'], tasks.design            (config));
gulp.task('package-debug'  , ['bundled']      , tasks['package-debug']  (config));
gulp.task('package-release', ['release']      , tasks['package-release'](config));

gulp.on('err', function (e) {
  console.log(e.err.stack);
});

gulp.task('default', ['debug']);
