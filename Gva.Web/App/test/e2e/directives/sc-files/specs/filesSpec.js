/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect, require) {
  'use strict';

  var _ = require('lodash'),
    ScFiles = require('../../../pageObjects/directives/scFiles.js');

  describe('Sc-files directive', function () {
    var ptor = protractor.getInstance(),
      multipleFilesBtnElem,
      singleFileBtnElem,
      noFilesBtnElem,
      openModalBtnElem,
      filesDir;

    beforeEach(function () {
      ptor.get('#/test/files');
      filesDir = new ScFiles(ptor.findElement(protractor.By.name('filesDir')), ptor);
      multipleFilesBtnElem = ptor.findElement(protractor.By.name('multipleFilesBtn'));
      singleFileBtnElem = ptor.findElement(protractor.By.name('singleFileBtn'));
      noFilesBtnElem = ptor.findElement(protractor.By.name('noFilesBtn'));
      openModalBtnElem = ptor.findElement(protractor.By.className('glyphicon-file'));
    });

    it('should set the text of the element to whatever file array is passed.', function () {
      filesDir.get().then(function (value) {
        expect(value).toEqual('Няма прикачени файлове.');
      });

      multipleFilesBtnElem.click();
      filesDir.get().then(function (value) {
        expect(value).toEqual('4 прикачени файла.');
      });

      singleFileBtnElem.click();
      filesDir.get().then(function (value) {
        expect(value).toEqual('file1');
      });

      noFilesBtnElem.click();
      filesDir.get().then(function (value) {
        expect(value).toEqual('Няма прикачени файлове.');
      });
    });

    it('should open modal.', function () {
      filesDir.openModal();
      filesDir.get().then(function (value) {
        expect(value).toEqual('Няма прикачени файлове.');
      });
    });

    it('should dismiss modal', function () {
      multipleFilesBtnElem.click();
      filesDir.openModal();
      filesDir.closeModal();
      ptor.findElements(protractor.By.className('modal-body'))
        .then(function (data) {
          expect(_.keys(data).length).toEqual(0);
          filesDir.get().then(function (value) {
            expect(value).toEqual('4 прикачени файла.');
          });
        });
    });


    it('should display file tree.', function () {
      multipleFilesBtnElem.click();
      filesDir.openModal();
      ptor.findElements(
        protractor.By.className('test-files-li')).then(function (data) {
          expect(_.keys(data).length).toEqual(3);
        });
      ptor.findElements(
        protractor.By.className('test-files-root-li')).then(function (data) {
          expect(_.keys(data).length).toEqual(2);
        });
    });

    it('should render download link inside modal.', function () {
      singleFileBtnElem.click();
      filesDir.openModal();
      ptor.findElement(
        protractor.By.className('test-download-link')).getText().then(function (value) {
          expect(value).toEqual('file1');
        });
    });

    it('should delete file', function () {
      singleFileBtnElem.click();
      filesDir.get().then(function (value) {
        expect(value).toEqual('file1');
      });

      filesDir.openModal();

      ptor.findElement(protractor.By.className('tree-image-close')).click()
        .then(function () {
          return ptor.findElement(
            protractor.By.className('test-no-files-span')).getText();
        })
        .then(function (value) {
          expect(value).toEqual('Няма прикачени файлове');
        });
    });

    it('should upload file.', function () {
      filesDir.openModal();
      filesDir.addFile('//filesCtrl.js');
      filesDir.uploadFiles();
      filesDir.get().then(function (value) {
        expect(value).toEqual('filesCtrl.js');
      });
    });

    it('should stop uploading.', function () {
      filesDir.openModal();
      filesDir.addFile('//filesCtrl.js');
      filesDir.addFile('//filesDirective.js');
      filesDir.addFile('//filesModalCtrl.js');
      filesDir.uploadFiles();
      ptor.findElement(protractor.By.className('test-icon-stop')).click()
      .then(function () {
        return ptor.wait(function () {
          return ptor.findElements(protractor.By.className('modal-body'))
            .then(function (elements) {
              return _.keys(elements).length === 0;
            });
        }, 5000);
      })
      .then(function () {
        filesDir.get().then(function (value) {
          expect(value).toEqual('2 прикачени файла.');
        });
      });
    });
  });
}(protractor, describe, beforeEach, it, expect, require));