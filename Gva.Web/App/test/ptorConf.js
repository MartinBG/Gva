/*global exports, require, global, process*/
(function (exports, require, global, process) {
  'use strict';

  exports.config = {
    seleniumServerJar: process.env.SELENIUM_PATH + 'selenium-server-standalone-2.39.0.jar',
    specs: [
      'test/common/specs/*.js',
      'test/gva/persons/specs/**/*.js',
      'test/gva/directives/specs/*.js',
      'test/scaffolding/specs/*.js'
    ],
    baseUrl: 'http://localhost:52560',
    rootElement: 'body',
    onPrepare: function() {
      var GvaBy = require('./gvaBy');

      global.protractor.By = new GvaBy();

      global.browser.driver.manage().window().maximize();
    },
    // ----- Options to be passed to minijasminenode -----
    jasmineNodeOpts: {
      // onComplete will be called just before the driver quits.
      onComplete: null,
      // If true, display spec names.
      isVerbose: true,
      // If true, print colors to the terminal.
      showColors: true,
      // If true, include stack traces in failures.
      includeStackTrace: true,
      // Default time to wait in ms before a test fails.
      defaultTimeoutInterval: 10000
    }
  };
}(exports, require, global, process));
