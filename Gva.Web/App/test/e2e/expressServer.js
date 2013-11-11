var express = require('express');
var app = express();
var files = {};
app.post('/file', function(req, res) {
  var fileKey = guid();
  //res.writeHead(200, { 'Content-Type' : 'application/json' });
  res.send(JSON.stringify(fileKey));
});
module.exports = app;


function guid() {
  return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
    var r = Math.random()*16|0, v = c == 'x' ? r : (r&0x3|0x8);
    return v.toString(16);
});
}