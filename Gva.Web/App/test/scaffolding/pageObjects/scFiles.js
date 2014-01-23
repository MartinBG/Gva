﻿/*global module, by, require*/
(function (module, by, require){
  'use strict';

  var Q = require('q'),
      _ = require('lodash');

  function ScFiles(element, context) {
    this.element = element;
    this.context = context;
  }

  ScFiles.prototype.get = function () {
    return this.element.findElement(by.className('form-control')).then(function (control) {
      return control.getText();
    });
  };

  ScFiles.prototype.openModal = function () {
    return this.element.findElement(by.className('glyphicon-file')).then(function (openBtn) {
      openBtn.click();
    });
  };

  ScFiles.prototype.closeModal = function () {
    return this.context.findElement(by.className('test-button-close')).then(function (closeBtn) {
      return closeBtn.click();
    });
  };

  ScFiles.prototype.addFile = function (text) {
    return this.context.findElement(by.className('test-file-upload-button')).then(function (elem) {
      elem.sendKeys(text);
    });
  };

  ScFiles.prototype.deleteFile = function (fileName) {
    return this.context.findElement(by.className('scf-' + fileName)).then(function (file) {
      return file.findElement(by.className('tree-image-close')).then(function (deleteBtn) {
        deleteBtn.click();
      });
    });
  };

  ScFiles.prototype.uploadFiles = function () {
    return this.context.findElement(by.className('btn-primary')).then(function (uploadBtn) {
      uploadBtn.click();
    });
  };

  ScFiles.prototype.getFiles = function () {
    return this.context.findElements(by.className('test-download-link')).then(function (files) {
      return Q.all(_.map(files, function (file) {
        return file.getText();
      })).then(function (fileNames) {
        return _.filter(fileNames, function (fileName) {
          return fileName !== '';
        });
      });
    });
  };

  ScFiles.prototype.getFileTree = function () {
    var makeNode = function (element) {
      var obj = {};

      return element.findElements(by.css('ul > li')).then(function (childs) {
        return Q.all(_.map(childs, makeNode)).then(function (mappedChilds) {
          var nameElemPromise = mappedChilds.length === 0 ?
            element.findElement(by.tagName('a')) :
            element.findElement(by.css('span:last-child'));

          return nameElemPromise.then(function (nameElem) {
            return nameElem.getText().then(function (name) {
              obj[name] = mappedChilds;
              return obj;
            });
          });
        });
      });
    };

    return this.context.findElements(by.className('test-files-root-li')).then(function (rootFiles) {
      return Q.all(_.map(rootFiles, makeNode)).then(function (tree) {
        return tree;
      });
    });
  };

  ScFiles.prototype.isModalDismissed = function () {
    return this.context.findElements(by.className('modal-body')).then(function (elements) {
      return elements.length === 0;
    });
  };

  module.exports = ScFiles;
}(module, by, require));