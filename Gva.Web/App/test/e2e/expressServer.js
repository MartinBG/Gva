/*global require, module, console, it, expect*/

var express = require('express');
var fs = require('fs');
var app = express();
var files = {};

function generateGuid() {
  return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
    var r = Math.random()*16|0, v = c == 'x' ? r : (r&0x3|0x8);
    return v.toString(16);
  });
}

function processFile(fileKey, reqFile, res) {
  fs.readFile(reqFile.path, function (err, data) {
    files[fileKey] = data;
    fs.unlink(reqFile.path, function() {
      if (err) throw err;
      res.send(JSON.stringify({fileKey: fileKey}));
    });
  });
}

app.use(express.multipart());

app.post('/file', function(req, res) {
  var fileKey = generateGuid();
  var currentFile = req.files.files[0];

  if (currentFile.name === 'filesModalCtrl.js') {
    console.log('if');
    setTimeout(function(){
      processFile(fileKey, currentFile, res);
    }, 9000);
  } else {
    console.log('else');
    processFile(fileKey, currentFile, res);
  }
});
module.exports = app;

