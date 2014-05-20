var _ = require('lodash');
var glob = require('glob');
var minimatch = require('minimatch');
var glob2base = require('glob2base');
var path = require('path');

// expand all globs the way gulp would expland them synchronously
// a relative path is also added for convenience
// this is a direct port of glob-stream which gulp uses, but without asynchronicity and streams
// https://github.com/wearefractal/glob-stream/blob/master/index.js
module.exports = function(globs, opt) {
  /*jshint -W116*/
  /* enable one line if statements intentionally */
  var isMatch = function(file, pattern) {
    if (typeof pattern === 'string') return minimatch(file.path, pattern);
    if (pattern instanceof RegExp) return pattern.test(file.path);
    return true; // unknown glob type?
  };

  var isNegative = function(pattern) {
    if (typeof pattern !== 'string') return true;
    if (pattern[0] === '!') return true;
    return false;
  };

  var isPositive = function(pattern) {
    return !isNegative(pattern);
  };

  var unrelative = function(cwd, glob) {
    var mod = '';
    if (glob[0] === '!') {
      mod = glob[0];
      glob = glob.slice(1);
    }
    return mod+path.resolve(cwd, glob);
  };
  
  if (!opt) opt = {};
  if (!Array.isArray(globs)) globs = [globs];
  
  var positives = _.filter(globs, isPositive);
  var negatives = _.filter(globs, isNegative);
  
  if (positives.length === 0) throw new Error('Missing positive glob');
  
  if (!negatives) negatives = [];
  if (typeof opt.cwd !== 'string') opt.cwd = process.cwd();
  if (typeof opt.silent !== 'boolean') opt.silent = true;
  if (typeof opt.nonull !== 'boolean') opt.nonull = false;
  if (typeof opt.cwdbase !== 'boolean') opt.cwdbase = false;
  if (opt.cwdbase) opt.base = opt.cwd;  
  /*jshint +W116*/

  return _(positives)
    .map(function(ourGlob) {
      // remove path relativity to make globs make sense
      ourGlob = unrelative(opt.cwd, ourGlob);
      negatives = negatives.map(unrelative.bind(null, opt.cwd));

      // extract base path from glob
      var globber = new glob.Glob(ourGlob, opt);
      var basePath = opt.base ? opt.base : glob2base(globber);
      
      return _.map(glob.sync(ourGlob, opt), function (filename) {
        return {
          cwd: opt.cwd,
          base: basePath,
          path: path.resolve(opt.cwd, filename),
          relative: path.relative(basePath, filename)
        };
      });
    })
    .flatten(true)
    .filter(function(file) {    
      var matcha = isMatch.bind(null, file);
      
      return _.every(negatives, matcha);
    })
    .uniq('path')
    .value();
};