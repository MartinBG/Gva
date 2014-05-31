'use strict';
/*global require, module*/
/*jshint -W097*/ // Use the function form of "use strict".
var path = require('path');

var _ = require('lodash');
var gulp = require('gulp');
var plugins = require('gulp-load-plugins')();
var es = require('event-stream');

var utils = require('./utils');

function rawTemplates(templates) {
  return utils.uniqueQueue(_.map(_.values(templates), function (v) {
    return v();
  }));
}

function bundleTemplates(templates, file) {
  return es.merge.apply(null, _.map(_.keys(templates), function (key) {
    return templates[key]()
        .pipe(plugins.html2js({
          outputModuleName: key
        }));
  }))
  .pipe(plugins.concat(file));
}

function rawAssets(assets) {
  return utils.uniqueQueue(_.map(_.values(assets), function (v) {
    return v({ base: '.' });
  }));
}

var relocateAssets = function (assets, cssDir) {
  return es.merge.apply(null, _.map(_.keys(assets), function (key) {
    return assets[key]({})
      .pipe(plugins.rename(function (path) {
        path.dirname = cssDir + key;
      }));
  }));
};

function inject(stream, tag) {
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
  });
}

function build(config, opts) {
  var appJs =
    config.app()
    .pipe(plugins.preprocess({ context: opts.bundleTemplates ? {} : { DEBUG: true } }))
    .pipe(plugins['if'](opts.minifyJs, plugins.uglify({ mangle: false })))
    .pipe(plugins['if'](opts.bundleJs, plugins.concat(config.jsDir + 'app.js')));

  var libJs =
    config.lib()
    .pipe(plugins['if'](opts.minifyJs, plugins.uglify({ mangle: false })))
    .pipe(plugins['if'](opts.bundleJs, plugins.concat(config.jsDir + 'lib.js')));

  var libIe8Js =
    config.lib_ie8()
    .pipe(plugins['if'](opts.minifyJs, plugins.uglify({ mangle: false })))
    .pipe(plugins['if'](opts.bundleJs, plugins.concat(config.jsDir + 'lib.ie8.js')));

  var css =
    config.css()
    .pipe(plugins.preprocess({ context: opts.bundleTemplates ? {} : { DEBUG: true } }))
    .pipe(plugins['if'](opts.minifyCss, plugins.minifyCss()))
    .pipe(plugins['if'](opts.bundleCss, plugins.concat(config.cssDir + 'styles.css')));

  var templates = opts.bundleTemplates ?
    bundleTemplates(config.templates, config.templatesDir + 'templates.js') :
    rawTemplates(config.templates);

  var assets = opts.relocateAssets ?
    relocateAssets(config.assets, config.cssDir) :
    rawAssets(config.assets);

  return utils.uniqueQueue(
    appJs,
    libJs,
    libIe8Js,
    css,
    templates,
    assets,
    gulp.src('index.html')
      .pipe(inject(appJs, 'app.js'))
      .pipe(inject(libJs, 'lib.js'))
      .pipe(inject(libIe8Js, 'lib.ie8.js'))
      .pipe(inject(css, 'styles.css'))
      .pipe(plugins['if'](opts.bundleTemplates, inject(templates, 'templates.js')))
  )
  .pipe(gulp.dest(config.outputDir));
}

var tasks = {
  clean: function (config) {
    return gulp
      .src(config.outputDir, { read: false })
      .pipe(plugins.clean({ force: true }));
  },
  lint: function (config) {
    return config.lint()
      .pipe(plugins.jshint())
      .pipe(plugins.jshint.reporter('jshint-stylish'))
      .on('error', plugins.util.log);
  },
  debug: function (config) {
    return build(config, {});
  },
  'debug-bundled': function (config) {
    return build(config, {
      bundleJs: true,
      bundleCss: true,
      bundleTemplates: true,
      relocateAssets: true
    });
  },
  release: function (config) {
    return build(config, {
      minifyJs: true,
      bundleJs: true,
      minifyCss: true,
      bundleCss: true,
      bundleTemplates: true,
      relocateAssets: true
    });
  },
  design: function (config) {
    return build(config, {
      minifyJs: true,
      bundleJs: true
    });
  }
};

module.exports = tasks;