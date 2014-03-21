window.require = function (module) {
  var match = /(\.\/)?(.+)/.exec(module);
  if (match) {
    console.log(module)
    return window[match[2]].exports;
  }
  return undefined;
};
