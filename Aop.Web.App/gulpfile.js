'use strict';
/*global require*/
/*jshint -W069*/ // ['XXX'] is better written in dot notation
/*jshint -W097*/ // Use the function form of "use strict".
/*jshint maxlen:false*/
var path = require('path');

var gulp = require('gulp');
var plugins = require('gulp-load-plugins')();
var es = require('event-stream');
var streamqueue = require('streamqueue');

var outputDir = '../Aop.Web.Host/App/';
var jsDir = 'js/';
var cssDir = 'css/';
var templatesDir = 'templates/';

var streams = {
  js: {
    app: function () {
      return streamqueue({ objectMode: true, allowHalfOpen: false },
        gulp.src('js/app.js', { base: '.' }),
        gulp.src('../Common.Web.App/js/ems/*.js', { base: '../Common.Web.App/' }),
        gulp.src(['!js/app.js', 'js/**/*.js'], { base: '.' }),
        gulp.src(['!../Common.Web.App/js/ems/*.js', '../Common.Web.App/js/**/*.js'], { base: '../Common.Web.App/' })
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
        'bower_components/autofill-event/src/autofill-event.js'
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
    }
  },
  css: {
    styles: function () {
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
    }
  },
  templates: {
    common: function () {
      return streamqueue({ objectMode: true, allowHalfOpen: false },
        gulp.src('js/common/**/*.html', { base: '.' }),
        gulp.src('../Common.Web.App/js/common/**/*.html', { base: '../Common.Web.App/' })
      );
    },
    aop: function () {
      return gulp.src('js/aop/**/*.html', { base: '.' });
    },
    ems: function () {
      return streamqueue({ objectMode: true, allowHalfOpen: false },
        gulp.src('js/ems/**/*.html', { base: '.' }),
        gulp.src('../Common.Web.App/js/ems/**/*.html', { base: '../Common.Web.App/' })
      );
    },
    scaffolding: function () {
      return gulp.src('../Common.Web.App/js/scaffolding/**/*.html', { base: '../Common.Web.App/' });
    }
  },
  assets: {
    bootstrap: function () {
      return gulp.src('bower_components/bootstrap/dist/fonts/**', { base: '.' });
    },
    font_awesome: function () {
      return gulp.src('bower_components/font-awesome/fonts/**', { base: '.' });
    },
    select2: function () {
      return gulp.src('bower_components/select2/*.+(png|gif)', { base: '.' });
    },
    blueimp_file_upload: function () {
      return gulp.src('bower_components/blueimp-file-upload/img/**', { base: '.' });
    },
    img: function () {
      return gulp.src('img/**', { base: '.' });
    }
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
      '../Common.Web.App/js/**/*.js',
      'js/**/*.js',
      'test/**/*.js'
    ])
    .pipe(plugins.jshint())
    .pipe(plugins.jshint.reporter('jshint-stylish'))
    .on('error', plugins.util.log);
});

var js_raw = function() {
  return streamqueue({ objectMode: true, allowHalfOpen: false },
      streams.js['lib.js'],
      streams.js['lib.ie8.js'],
      streams.js['app.js']
    )
    .pipe(plugins.preprocess({ context: { DEBUG: true } }));
};

var js_bundled = function() {
  var pipeline = function (stream) {
    return streams.js[stream]
      .pipe(plugins.preprocess())
      .pipe(plugins.concatSourcemap(jsDir + stream, {
        sourceRoot: '/',
        sourcesContent: true
      }));
  };

  return es.merge(
      pipeline('lib.js'),
      pipeline('lib.ie8.js'),
      pipeline('app.js')
    );
};

var js_minified = function() {
  var pipeline = function (stream, useUglify) {
    return streams.js[stream]
      .pipe(plugins.preprocess())
      .pipe(plugins['if'](useUglify, plugins.uglify({
        mangle: false
      })))
      .pipe(plugins.concat(jsDir + stream));
  };

  return es.merge(
      pipeline('lib.js', false),
      pipeline('lib.ie8.js', false),
      pipeline('app.js', true)
    );
};

var css_raw = function() {
  return streams.css.styles();
};

var css_bundled = function() {
  return streams.css['styles.css']
    .pipe(plugins.concatSourcemap(cssDir + 'styles.css', {
      sourceRoot: '/',
      sourcesContent: true
    }));
};

var css_minified = function() {
  return streams.css['styles.css']
    .pipe(plugins.minifyCSS())
    .pipe(plugins.concat(cssDir + 'styles.css'));
};

var templates_raw = function() {
  return streamqueue({ objectMode: true, allowHalfOpen: false },
      streams.templates.common(),
      streams.templates.aop(),
      streams.templates.ems(),
      streams.templates.scaffolding()
    );
};

var templates_bundled = function() {
  var pipeline = function (stream) {
    return streams.templates[stream]
      .pipe(plugins.html2js({
        base: '.',
        outputModuleName: stream.replace(/\.js$/i, '') // remove .js extension
      }))
      .pipe(plugins.concat(templatesDir + stream));
  };

  return es.merge(
      pipeline('common.templates.js'),
      pipeline('aop.templates.js'),
      pipeline('ems.templates.js'),
      pipeline('scaffolding.templates.js')
    )
    .pipe(plugins.concat(templatesDir + 'templates.js'));
};

var assets_raw = function() {
  return streamqueue({ objectMode: true, allowHalfOpen: false },
      streams.assets.bootstrap(),
      streams.assets.font_awesome(),
      streams.assets.select2(),
      streams.assets.blueimp_file_upload(),
      streams.assets.img()
    );
};

var assets_relocated = function() {
  return es.merge(
      streams.assets['bootstrap'].pipe(gulp.dest(cssOutputDir + '/../fonts')),
      streams.assets['font-awesome'].pipe(gulp.dest(cssOutputDir + '/../fonts')),
      streams.assets['select2'].pipe(gulp.dest(cssOutputDir)),
      streams.assets['blueimp-file-upload'].pipe(gulp.dest(cssOutputDir + '/../img')),
      streams.assets['img'].pipe(gulp.dest(cssOutputDir + '/../img'))
    );
};

var inject = function (stream, tag) {
  return plugins.inject(stream, {
      starttag: '<!-- inject:' + tag + ':{{ext}} -->',
      transform: function (filepath, file) {
        var relativePath = '/' + path.relative(file.base, file.path).replace(/\\/g, '/');

        switch (path.extname(relativePath).slice(1)) {
          case 'css':
            return '<link rel="stylesheet" href="' + relativePath + '">';
          case 'js':
            return '<script src="' + relativePath + '"></script>';
        }
      },
      removeTags: true
    }
  );
};

//gulp.task('debug',         ['lint', 'js-raw'     , 'css-raw'     , 'templates-raw'    , 'assets-raw'      ], index({js: 'raw'    , css: 'raw'    , templates: 'none'   }));
//gulp.task('debug-bundled', ['lint', 'js-bundled' , 'css-bundled' , 'templates-bundled', 'assets-relocated'], index({js: 'bundled', css: 'bundled', templates: 'bundled'}));
//gulp.task('release',       ['lint', 'js-minified', 'css-minified', 'templates-bundled', 'assets-relocated'], index({js: 'bundled', css: 'bundled', templates: 'bundled'}));
//gulp.task('design', ['lint', 'js-minified', 'css-raw', 'templates-raw', 'assets-raw'], index({ js: 'bundled', css: 'raw', templates: 'none' }));

gulp.on('err', function (e) {
  console.log(e.err.stack);
});

gulp.task('debug', ['clean'], function () {

  var app_js = streams.js.app()
    .pipe(plugins.preprocess({ context: { DEBUG: true } }));

  var lib_js = streams.js.lib();
  var lib_ie8 = streams.js.lib_ie8();

  var css = css_raw();
  var templates = templates_raw();
  var assets = assets_raw();

  return es.merge(
    app_js,
    lib_js,
    lib_ie8,
    css,
    templates,
    assets,
    gulp.src('index.html')
      .pipe(inject(css, 'styles.css'))
      .pipe(inject(app_js, 'app.js'))
      .pipe(inject(lib_js, 'lib.js'))
      .pipe(inject(lib_ie8, 'lib.ie8.js'))
  )
  .pipe(gulp.dest(outputDir));
});

gulp.task('default', ['debug']);
