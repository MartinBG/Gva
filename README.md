gva
======

#### Build

1. Install [NodeJS](http://nodejs.org/)
2. Install `grunt-cli` globally with `npm install -g grunt-cli`
3. Install `bower` globally with `npm install -g bower`
4. In the js app folder `cd .\Gva.Web\App`
  * Install the required npm packages with `npm install`
  * Install the required bower components with `bower install`
  * Build the js app with `grunt`
5. Open the solution `Gva.sln` and run the web project `Gva.Web`

#### Test

##### To run the tests
1. Make sure you have `java` accessible from your `PATH`
2.1 Download chormedriver for selenium from http://chromedriver.storage.googleapis.com/index.html?path=2.8/
2.2 Download IEDriverServer from https://code.google.com/p/selenium/downloads/detail?name=IEDriverServer_Win32_2.39.0.zip
3. Download selenium server from https://code.google.com/p/selenium/downloads/detail?name=selenium-server-standalone-2.39.0.jar
4. Add a new environment variable `SELENIUM_PATH` with value -  the path to the folder in which you have put selenium server and chormedriver
5. Run `grunt test` (`grunt test-ie` for IE)
6. Add a new environment variable `GVA_PATH` with value -  the path to the folder containing the project

##### To debug the tests
1. Install `node-inspector` globally with `npm install -g node-inspector`
2. Run the tests with
`node --debug-brk c:\Users\username\AppData\Roaming\npm\node_modules\grunt-cli\bin\grunt test`
where `username` is your current user
3. Run `node-inspector` in a separate command prompt
4. Open the url that that inspector is showing in Chrome(e.g. http://127.0.0.1:8080/debug?port=5858)
5. See [Debugging Protractor Tests](https://github.com/angular/protractor/blob/master/docs/debugging.md) for more info.

#### Migration tool
1. Install `Instant Client Package - Basic 12.1` and `Instant Client Package - SDK 12.1` from http://www.oracle.com/technetwork/database/features/instant-client/index-097480.html
2. Add the following environtment variables
```bat
OCI_INCLUDE_DIR=C:\instantclient_12_1\sdk\include
OCI_LIB_DIR=C:\instantclient_12_1\sdk\lib\msvc\vc11
OCI_VERSION=12
Path=...;c:\instantclient_12_1\vc11;c:\instantclient_12_1
```
3. Install [Python 2.7.3](http://www.python.org/download/releases/2.7.3/#download)
4. Add the following to your PATH environment variable
`C:\Python27;C:\Python27\Lib\site-packages\;C:\Python27\Scripts\;`
5. Set msvs_version in npm with `npm config set msvs_version 2012`
6. Add a new environment variable `NLS_LANG` with value `_.UTF8`
7. Go to `.\Gva.Database\Migration`
8. Install npm packages with `npm install`
9. Run `node migration.js`
