gva
======

#### Build

1. Install [NodeJS](http://nodejs.org/)
2. Install `grunt-cli` globally with `npm install -g grunt-cli`
3. Install `bower` globally with `npm install -g bower`
4. In the js app folder `cd .\Gva.Web\App`
  * Install the required npm packages with `npm install`
  * Install the required bower components with `bower install`
  * Build the `angular-bootstrap`   
    `cd .\bower_components\angular-bootstrap`  
    `npm install`  
    `grunt before-test`  
    `grunt after-test`  
    `cd ..\..\`
  * Build the js app with `grunt`
5. Open the solution `Gva.sln` and run the web project `Gva.Web`

#### Test

1. Make sure you have `java` accessible from your `PATH`
2. Run `grunt test`

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
