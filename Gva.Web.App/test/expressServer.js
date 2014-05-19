/*global require, module, setTimeout*/
(function (require, module, setTimeout) {
  'use strict';
  var express = require('express');
  var fs = require('fs');
  var app = express();
  var files = {};

  function generateGuid() {
    /*jshint bitwise: false*/
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
      var r = Math.floor(Math.random() * 16),
          v = c === 'x' ? r : (r&0x3|0x8);
      return v.toString(16);
    });
  }

  function processFile(fileKey, reqFile, res) {
    fs.readFile(reqFile.path, function (err, data) {
      files[fileKey] = data;
      fs.unlink(reqFile.path, function() {
        if (err) { throw err; }
        res.send(JSON.stringify({fileKey: fileKey}));
      });
    });
  }

  app.use(express.multipart());

  app.post('/api/file', function(req, res) {
    var fileKey = generateGuid(),
      currentFile = req.files.files[0];
    
    if (currentFile.name === 'filesDirective.js') {
      setTimeout(function() {
        processFile(fileKey, currentFile, res);
      }, 3000);
    } else {
      processFile(fileKey, currentFile, res);
    }
  });
  
  app.get('/api/file', function(req, res) {
    var fileName = req.query.fileName,
      fileKey = req.query.fileKey,
      file = files[fileKey];
    
    res.setHeader('Content-disposition', 'attachment; filename=' + fileName);
    res.setHeader('Content-type', 'application/octet-stream');
    res.send(file);
  });
  module.exports = app;
}(require, module, setTimeout));

