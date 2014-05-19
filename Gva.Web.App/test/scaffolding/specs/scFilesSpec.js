/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect, require) {
  'use strict';

  describe('scFiles directive', function () {
    var ptor = protractor.getInstance(),
        Page = require('../pageObjects/testbeds/filesPO'),
        filesPage;

    beforeEach(function () {
      ptor.get('#/test/files');
      filesPage = new Page(ptor);
    });

    it('should set the text of the element to whatever file array is passed.', function () {
      expect(filesPage.filesDirective.get()).toEqual('Няма прикачени файлове.');

      filesPage.selectSingleFileWithDelay();
      expect(filesPage.filesDirective.get()).toEqual('file1');

      filesPage.selectMultipleFiles();
      expect(filesPage.filesDirective.get()).toEqual('4 прикачени файла.');

      filesPage.selectSingleFile();
      expect(filesPage.filesDirective.get()).toEqual('file1');
      expect(filesPage.singleFileDirective.get()).toEqual('file1');

      filesPage.deselectFiles();
      expect(filesPage.filesDirective.get()).toEqual('Няма прикачени файлове.');
      expect(filesPage.singleFileDirective.get()).toEqual('Няма прикачен файл.');
    });

    it('should properly update single file model value.', function () {
      filesPage.selectSingleFile();
      expect(filesPage.getSingleFileValue()).
        toEqual('{"key":"f111111111","relativePath":"","name":"file1"}');
    });

    it('should open multiple files modal.', function () {
      filesPage.filesDirective.openModal();
      expect(filesPage.filesDirective.isModalDisplayed()).toBe(true);
    });

    it('should open single file modal.', function () {
      filesPage.singleFileDirective.openModal();
      expect(filesPage.singleFileDirective.isModalDisplayed()).toBe(true);
    });

    it('should dismiss multiple files modal', function () {
      filesPage.singleFileDirective.openModal();
      filesPage.singleFileDirective.closeModal();
      expect(filesPage.singleFileDirective.isModalDisplayed()).toBe(false);
    });

    it('should dismiss single file modal', function () {
      filesPage.selectMultipleFiles();
      filesPage.filesDirective.openModal();
      filesPage.filesDirective.closeModal();

      expect(filesPage.filesDirective.isModalDisplayed()).toBe(false);
      expect(filesPage.filesDirective.get()).toEqual('4 прикачени файла.');
    });

    it('should display file tree.', function () {
      filesPage.selectMultipleFiles();

      filesPage.filesDirective.openModal();
      expect(filesPage.filesDirective.getFileTree())
        .toEqual([
          { '': [{ file1: [] }, { file2: [] }, { file3: [] }] },
          { file4: [] }
        ]);
    });

    it('should render download link inside modal.', function () {
      filesPage.selectMultipleFiles();

      filesPage.filesDirective.openModal();
      expect(filesPage.filesDirective.getFiles()).toEqual(['file1', 'file2', 'file3', 'file4']);
    });

    it('should delete file', function () {
      filesPage.selectMultipleFiles();
      expect(filesPage.filesDirective.get()).toEqual('4 прикачени файла.');

      filesPage.filesDirective.openModal();
      filesPage.filesDirective.deleteFile('file1');
      expect(filesPage.filesDirective.getFileTree())
        .toEqual([
          { '': [{ file2: [] }, { file3: [] }] },
          { file4: [] }
        ]);
    });

    it('should upload file.', function () {
      filesPage.filesDirective.openModal();
      filesPage.filesDirective.addFile();
      filesPage.filesDirective.uploadFiles().then(function () {
        expect(filesPage.filesDirective.get()).toEqual('ptorConf.js');
      });
    });
  });
}(protractor, describe, beforeEach, it, expect, require));