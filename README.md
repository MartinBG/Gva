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
