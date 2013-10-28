/*global exports*/
exports.config = {
  seleniumServerJar: '../../test/e2e/assets/selenium/selenium-server-standalone-2.35.0.jar',
  chromeDriver: '../../test/e2e/assets/selenium/chromedriver',
  specs: ['spec/*.js', 'directives/specs/*.js'],
  capabilities: {
    'browserName': 'chrome'
  },
  baseUrl: 'http://localhost:52560',
  rootElement: 'body',

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
