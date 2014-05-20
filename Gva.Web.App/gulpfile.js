'use strict';
/*global process, require, console*/
/*jshint -W069*/ // ['XXX'] is better written in dot notation
/*jshint -W097*/ // Use the function form of "use strict".
/*jshint maxlen:false*/
var fs = require('fs');

var _ = require('lodash');
var crypto = require('crypto');

var gulp = require('gulp');
var plugins = require('gulp-load-plugins')();
var es = require('event-stream');

// expand all globs the way gulp would expland them synchronously
// a relative path is also added for convenience
// this is a direct port of glob-stream which gulp uses, but without asynchronicity and streams
// https://github.com/wearefractal/glob-stream/blob/master/index.js
var globMultipleSync = function(globs, opt) {
  var _ = require('lodash');
  var glob = require('glob');
  var minimatch = require('minimatch');
  var glob2base = require('glob2base');
  var path = require('path');
  
  /*jshint -W116*/
  /* enable one line if statements intentionally */
  var isMatch = function(file, pattern) {
    if (typeof pattern === 'string') return minimatch(file.path, pattern);
    if (pattern instanceof RegExp) return pattern.test(file.path);
    return true; // unknown glob type?
  };

  var isNegative = function(pattern) {
    if (typeof pattern !== 'string') return true;
    if (pattern[0] === '!') return true;
    return false;
  };

  var isPositive = function(pattern) {
    return !isNegative(pattern);
  };

  var unrelative = function(cwd, glob) {
    var mod = '';
    if (glob[0] === '!') {
      mod = glob[0];
      glob = glob.slice(1);
    }
    return mod+path.resolve(cwd, glob);
  };
  
  if (!opt) opt = {};
  if (!Array.isArray(globs)) globs = [globs];
  
  var positives = _.filter(globs, isPositive);
  var negatives = _.filter(globs, isNegative);
  
  if (positives.length === 0) throw new Error('Missing positive glob');
  
  if (!negatives) negatives = [];
  if (typeof opt.cwd !== 'string') opt.cwd = process.cwd();
  if (typeof opt.silent !== 'boolean') opt.silent = true;
  if (typeof opt.nonull !== 'boolean') opt.nonull = false;
  if (typeof opt.cwdbase !== 'boolean') opt.cwdbase = false;
  if (opt.cwdbase) opt.base = opt.cwd;  
  /*jshint +W116*/

  return _(positives)
    .map(function(ourGlob) {
      // remove path relativity to make globs make sense
      ourGlob = unrelative(opt.cwd, ourGlob);
      negatives = negatives.map(unrelative.bind(null, opt.cwd));

      // extract base path from glob
      var globber = new glob.Glob(ourGlob, opt);
      var basePath = opt.base ? opt.base : glob2base(globber);
      
      return _.map(glob.sync(ourGlob, opt), function (filename) {
        return {
          cwd: opt.cwd,
          base: basePath,
          path: path.resolve(opt.cwd, filename),
          relative: path.relative(basePath, filename)
        };
      });
    })
    .flatten(true)
    .filter(function(file) {    
      var matcha = isMatch.bind(null, file);
      
      return _.every(negatives, matcha);
    })
    .uniq('path')
    .value();
};

// a fork of jshint-stylish with some minor changes (changed colors and added error codes)
// https://github.com/sindresorhus/jshint-stylish/blob/master/stylish.js
var stylishReporter = function (result, config, options) {
  var chalk = require('chalk');
  var table = require('text-table');

  var total = result.length;
  var ret = '';
  var headers = [];
  var prevfile;

  options = options || {};

  ret += table(result.map(function (el, i) {
    var err = el.error;
    // E: Error, W: Warning, I: Info
    var isError = err.code && err.code[0] === 'E';

    var line = [
      '',
      chalk.gray('line ' + err.line),
      chalk.gray('col ' + err.character),
      isError ? chalk.red(err.code + ' ' + err.reason) : chalk.cyan(err.code + ' ' + err.reason)
    ];

    if (el.file !== prevfile) {
      headers[i] = el.file;
    }

    if (options.verbose) {
      line.push(chalk.gray('(' + err.code + ')'));
    }

    prevfile = el.file;

    return line;
  }), {
    stringLength: function (str) {
      return chalk.stripColor(str).length;
    }
  }).split('\n').map(function (el, i) {
    return headers[i] ? '\n' + chalk.underline(headers[i]) + '\n' + el : el;
  }).join('\n') + '\n\n';

  if (total > 0) {
    /*jshint -W101*/
    ret += chalk.red((process.platform !== 'win32' ? '? ' : '') + total + ' problem' + (total === 1 ? '' : 's'));
    /*jshint +W101*/
  } else {
    ret += chalk.green((process.platform !== 'win32' ? '? ' : '') + 'No problems');
    ret = '\n' + ret.trim();
  }

  console.log(ret + '\n');
};

var outputDir = '../Gva.Web.Host/App';
var jsOutputDir = outputDir + '/js';
var cssOutputDir = outputDir + '/css';
var templatesOutputDir = outputDir + '/templates';

var bundles = {
  js: {
    'app.js': [
      'js/*.js',
      'js/scaffolding/*.js',
      'js/scaffolding/**/*.js',
      'js/common/*.js',
      'js/common/**/*.js',
      'js/gva/*.js',
      'js/gva/**/*.js',
      'js/ems/*.js',
      'js/ems/**/*.js'
    ],
    'sample.data.js': [
      'schema/requireShim.js',
      'schema/nomenclatures/*.js',
      'schema/nomenclatures.sample.js',
      'schema/person-data.sample.js',
      'schema/person-document-education.sample.js',
      'schema/person-document-employment.sample.js',
      'schema/person-document-id.sample.js',
      'schema/person-document-med.sample.js',
      'schema/person-document-other.sample.js',
      'schema/person-document-training.sample.js',
      'schema/person-document-checks.sample.js',
      'schema/person-address.sample.js',
      'schema/person-flyingExperience.sample.js',
      'schema/person-status.sample.js',
      'schema/person.sample.js',
      'schema/person-rating-edition.sample.js',
      'schema/person-rating.sample.js',
      'schema/person-document-exam.sample.js',
      'schema/person-document-application.sample.js',
      'schema/aircrafts/aircraft-data.sample.js',
      'schema/aircrafts/aircraft-dataapex.sample.js',
      'schema/aircrafts/aircraft-cert-registrations.sample.js',
      'schema/aircrafts/aircraft-cert-registrationsfm.sample.js',
      'schema/aircrafts/aircraft-cert-smods.sample.js',
      'schema/aircrafts/aircraft-cert-marks.sample.js',
      'schema/aircrafts/aircraft-cert-airworthinesses.sample.js',
      'schema/aircrafts/aircraft-cert-airworthinessesfm.sample.js',
      'schema/aircrafts/aircraft-cert-noises.sample.js',
      'schema/aircrafts/aircraft-cert-noisesfm.sample.js',
      'schema/aircrafts/aircraft-cert-permitstofly.sample.js',
      'schema/aircrafts/aircraft-cert-radios.sample.js',
      'schema/aircrafts/aircraft-document-debts.sample.js',
      'schema/aircrafts/aircraft-document-debtsfm.sample.js',
      'schema/aircrafts/aircraft-document-other.sample.js',
      'schema/aircrafts/aircraft-inspections.sample.js',
      'schema/aircrafts/aircraft-document-occurrences.sample.js',
      'schema/aircrafts/aircraft-maintenance.sample.js',
      'schema/aircrafts/aircraft-document-owner.sample.js',
      'schema/aircrafts/aircraft-parts.sample.js',
      'schema/aircrafts/aircraft-document-application.sample.js',
      'schema/organizations/organization-data.sample.js',
      'schema/organizations/organization-address.sample.js',
      'schema/organizations/organization-cert-airportoperator.sample.js',
      'schema/organizations/organization-auditplan.sample.js',
      'schema/organizations/organization-staff-managment.sample.js',
      'schema/organizations/organization-document-other.sample.js',
      'schema/organizations/organization-cert-groundserviceoperator.sample.js',
      'schema/organizations/organization-cert-groundserviceoperatorssnooperational.sample.js',
      'schema/organizations/organization-inspections.sample.js',
      'schema/organizations/organization-approval.sample.js',
      'schema/organizations/organization-amendment.sample.js',
      'schema/organizations//organization-staff-examiner.sample.js',
      'schema/organizations/organization-recommendation.sample.js',
      'schema/organizations/organization-reg-ground-service-operator.sample.js',
      'schema/organizations/organization-reg-airport-operator.sample.js',
      'schema/airports/airport-data.sample.js',
      'schema/airports/airport-document-other.sample.js',
      'schema/airports/airport-document-owner.sample.js',
      'schema/airports/airport-cert-operational.sample.js',
      'schema/airports/airport-document-application.sample.js',
      'schema/airports/airport-inspections.sample.js'
    ],
    'templates.js': [
      'build/templates/*.js'
    ],
    'lib.js': [
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
      'bower_components/autofill-event/src/autofill-event.js'
    ],
    'lib.ie8.js': [
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
    ],
    'test.js': [
      'test/delay.js',
      'test/e2eMocksSetup.js',
      'test/httpBackendConfiguratorProvider.js',
      'test/common/mocks/*.js',
      'test/ems/corrs/mocks/*.js',
      'test/ems/docs/mocks/*.js',
      'test/gva/common/mocks/*.js',
      'test/gva/applications/mocks/*.js',
      'test/gva/persons/mocks/*.js',
      'test/gva/aircrafts/mocks/*.js',
      'test/gva/organizations/mocks/*.js',
      'test/gva/airports/mocks/*.js',
      'test/scaffolding/testbeds/states.js',
      'test/scaffolding/testbeds/*.js',
      'test/gva/directives/testbeds/states.js',
      'test/gva/directives/testbeds/*.js'
    ]
  },
  css: {
    'styles.css': [
      'bower_components/bootstrap/dist/css/bootstrap.css',
      'bower_components/font-awesome/css/font-awesome.css',
      'bower_components/select2/select2.css',
      'bower_components/select2-bootstrap-css/select2-bootstrap.css',
      'bower_components/bootstrap-datepicker/css/datepicker3.css',
      'bower_components/blueimp-file-upload/css/jquery.fileupload-ui.css',
      'bower_components/blueimp-file-upload/css/jquery.fileupload.css',
      'css/*.css'
    ]
  },
  templates: {
    'common.templates.js': [
      'js/common/**/*.html'
    ],
    'gva.templates.js': [
      'js/gva/**/*.html'
    ],
    'ems.templates.js': [
      'js/ems/**/*.html'
    ],
    'scaffolding.templates.js': [
      'js/scaffolding/**/*.html'
    ]
  },
  assets: {
    'bootstrap': [
      'bower_components/bootstrap/dist/fonts/**'
    ],
    'font-awesome': [
      'bower_components/font-awesome/fonts/**'
    ],
    'select2': [
      'bower_components/select2/*.+(png|gif)'
    ],
    'blueimp-file-upload': [
      'bower_components/blueimp-file-upload/img/**'
    ],
    'img': [
      'img/**'
    ]
  }
};

gulp.task('clean', function () {
  return gulp
    .src(outputDir, {read: false})
    .pipe(plugins.clean({ force: true }));
});

gulp.task('lint', function() {
  return gulp
    .src([
      'gulpfile.js',
      'js/**/*.js',
      'test/**/*.js'
    ])
    .pipe(plugins.jshint())
    .pipe(plugins.jshint.reporter(stylishReporter))
    .on('error', plugins.util.log);
});

gulp.task('js-raw', ['clean'], function() {
  return gulp
    .src(_.union(
      bundles.js['lib.js'],
      bundles.js['lib.ie8.js'],
      bundles.js['app.js']
    ), { base: '.' })
    .pipe(plugins.preprocess({ context: { DEBUG: true } }))
    .pipe(gulp.dest(outputDir));
});

gulp.task('js-bundled', ['clean'], function() {
  var pipeline = function (file) {
    return gulp
      .src(bundles.js[file])
      .pipe(plugins.preprocess())
      .pipe(plugins.concatSourcemap(file, {
        sourceRoot: '/',
        sourcesContent: true
      }));
  };

  return es.merge(
      pipeline('lib.js'),
      pipeline('lib.ie8.js'),
      pipeline('app.js')
    )
    .pipe(gulp.dest(jsOutputDir));
});

gulp.task('js-minified', ['clean'], function() {
  var pipeline = function (file, useUglify) {
    return gulp
      .src(bundles.js[file])
      .pipe(plugins.preprocess())
      .pipe(plugins['if'](useUglify, plugins.uglify({
        mangle: false
      })))
      .pipe(plugins.concat(file));
  };

  return es.merge(
      pipeline('lib.js', false),
      pipeline('lib.ie8.js', false),
      pipeline('app.js', true)
    )
    .pipe(gulp.dest(jsOutputDir));
});

gulp.task('css-raw', ['clean'], function() {
  return gulp
    .src(bundles.css['styles.css'], {base: '.'})
    .pipe(gulp.dest(outputDir));
});

gulp.task('css-bundled', ['clean'], function() {
  return gulp
    .src(bundles.css['styles.css'])
    .pipe(plugins.concatSourcemap('styles.css', {
      sourceRoot: '/',
      sourcesContent: true
    }))
    .pipe(gulp.dest(cssOutputDir));
});

gulp.task('css-minified', ['clean'], function() {
  return gulp
    .src(bundles.css['styles.css'])
    .pipe(plugins.minifyCSS())
    .pipe(plugins.concat('styles.css'))
    .pipe(gulp.dest(cssOutputDir));
});

gulp.task('templates-raw', ['clean'], function() {
  return gulp
    .src(_.union(
      bundles.templates['common.templates.js'],
      bundles.templates['gva.templates.js'],
      bundles.templates['ems.templates.js'],
      bundles.templates['scaffolding.templates.js']
    ), {base: '.'})
    .pipe(gulp.dest(outputDir));
});

gulp.task('templates-bundled', ['clean'], function() {
  var pipeline = function (file) {
    return gulp
      .src(bundles.templates[file])
      .pipe(plugins.html2js({
        base: '.',
        outputModuleName: file.replace(/\.js$/i, '') // remove .js extension
      }))
      .pipe(plugins.concat(file));
  };

  return es.merge(
      pipeline('common.templates.js'),
      pipeline('gva.templates.js'),
      pipeline('ems.templates.js'),
      pipeline('scaffolding.templates.js')
    )
    .pipe(plugins.concat('templates.js'))
    .pipe(gulp.dest(templatesOutputDir));
});

gulp.task('assets-raw', ['clean'], function() {
  return gulp
    .src(_.union(
      bundles.assets['bootstrap'],
      bundles.assets['font-awesome'],
      bundles.assets['select2'],
      bundles.assets['blueimp-file-upload'],
      bundles.assets['img']
    ), {base: '.'})
    .pipe(gulp.dest(outputDir));
});

gulp.task('assets-relocated', ['clean'], function() {
  return es.merge(
      gulp.src(bundles.assets['bootstrap']).pipe(gulp.dest(cssOutputDir + '/../fonts')),
      gulp.src(bundles.assets['font-awesome']).pipe(gulp.dest(cssOutputDir + '/../fonts')),
      gulp.src(bundles.assets['select2']).pipe(gulp.dest(cssOutputDir)),
      gulp.src(bundles.assets['blueimp-file-upload']).pipe(gulp.dest(cssOutputDir + '/../img')),
      gulp.src(bundles.assets['img']).pipe(gulp.dest(cssOutputDir + '/../img'))
    );
});

var index = function(options) {
  var md5 = function (file) {
    return crypto.createHash('md5').update(fs.readFileSync(file), 'utf8').digest('hex');
  };

  var getBundle = function(type) {
    var bundleNames = Array.prototype.slice.call(arguments, 1);
    
    if (options[type] === 'raw') {
      var globs = _(bundleNames)
        .map(function (name) {
          return bundles[type][name];
        })
        .flatten(true)
        .value();

      return globMultipleSync(globs, {base: '.'}).map(function (file) {
        return {
          path: '/' + file.relative.replace(/\\/g, '/'),
          hash: md5(file.path)
        };
      });
    } else if (options[type] === 'bundled') {
      return _(bundleNames)
        .map(function (name) {
          var bundlePath = '/' + type + '/' + name;          
          return {
            path: bundlePath,
            hash: md5(outputDir + '/' + bundlePath)
          };
        });
    } else if (options[type] === 'none') {
      return [];
    } else {
      throw new Error('Unknown bundling type: ' + options[type]);
    }
  };

  return function () {
    return gulp
      .src('index.html')
      .pipe(plugins.template({
        fullVersion: '',
        jsBundle: _.partial(getBundle, 'js'),
        cssBundle: _.partial(getBundle, 'css'),
        templatesBundle: _.partial(getBundle, 'templates')
      }))
      .pipe(gulp.dest(outputDir));
  };
};

gulp.task('debug',         ['lint', 'js-raw'     , 'css-raw'     , 'templates-raw'    , 'assets-raw'      ], index({js: 'raw'    , css: 'raw'    , templates: 'none'   }));
gulp.task('debug-bundled', ['lint', 'js-bundled' , 'css-bundled' , 'templates-bundled', 'assets-relocated'], index({js: 'bundled', css: 'bundled', templates: 'bundled'}));
gulp.task('release',       ['lint', 'js-minified', 'css-minified', 'templates-bundled', 'assets-relocated'], index({js: 'bundled', css: 'bundled', templates: 'bundled'}));
gulp.task('design',        ['lint', 'js-minified', 'css-raw'     , 'templates-raw'    , 'assets-raw'      ], index({js: 'bundled', css: 'raw'    , templates: 'none'   }));

gulp.task('default', ['debug']);