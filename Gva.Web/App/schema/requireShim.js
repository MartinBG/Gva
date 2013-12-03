window.require = function (module) {
  var match = /(\.\/)?(.+)/.exec(module);
  if (match) {
    return window[match[2]].exports;
  }
  return undefined;
}
window.getData = window.require;