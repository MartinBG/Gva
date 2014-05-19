gva
======

#### Build

1. Install [NodeJS](http://nodejs.org/)
2. Install `gulp` globally with `npm install -g gulp`
3. Install `bower` globally with `npm install -g bower`
4. In the js app folder `cd .\Gva.Web.App`
  * Install the required npm packages with `npm install`
  * Install the required bower components with `bower install`
  * Build the js app with `gulp`
5. Open the solution `Gva.sln` and run the web project `Gva.Web.Host`

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
1. Download and extract ODAC121012Xcopy_x64.zip from http://www.oracle.com/technetwork/database/windows/downloads/index-090165.html
2. Go to `.\path\to\extraction\folder`
3. Execute `install.bat all c:\oracle odac` from the Command Prompt (installation folder is of your choice)
4. Add the following to your PATH environment variable
`c:\oracle\;c:\oracle\bin\;`
5. Run Custom Tool `.\Gva.Database\CreateAll.tt`
6. Run the `.\Gva.MigrationTool` project
