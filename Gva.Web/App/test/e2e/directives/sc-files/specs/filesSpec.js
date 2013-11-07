/*global protractor, describe, beforeEach, it, expect*/
(function (protractor) {
  'use strict';

  describe('Sc-files directive', function () {
    var ptor = protractor.getInstance(),
      filesDirectiveSpan,
      singleFileDirectiveSpan,
      multipleFilesBtnElem,
      singleFileBtnElem,
      noFilesBtnElem,
      openModalBtnElem;


    beforeEach(function () {
      ptor.get('#/test/files');
      ptor.waitForAngular();

      /*jshint quotmark:false */
      filesDirectiveSpan = ptor.findElement(
        protractor.By.xpath("//div[@class='form-control']/span"));
      singleFileDirectiveSpan = ptor.findElement(
        protractor.By.xpath("//div[@class='form-control']/a/span"));
      multipleFilesBtnElem = ptor.findElement(protractor.By.name('multipleFilesBtn'));
      singleFileBtnElem = ptor.findElement(protractor.By.name('singleFileBtn'));
      noFilesBtnElem = ptor.findElement(protractor.By.name('noFilesBtn'));
      openModalBtnElem = ptor.findElement(protractor.By.className('glyphicon-file'));
    });

    it('should set the text of the element to whatever file array is passed.', function () {

      var multipleFilesBtnElem = ptor.findElement(protractor.By.name('multipleFilesBtn'));
      var singleFileBtnElem = ptor.findElement(protractor.By.name('singleFileBtn'));
      var noFilesBtnElem = ptor.findElement(protractor.By.name('noFilesBtn'));

      filesDirectiveSpan.getText().then(function (value) {
        expect(value).toEqual('Няма прикачени файлове.');
      });

      multipleFilesBtnElem.click();
      filesDirectiveSpan.getText().then(function (value) {
        expect(value).toEqual('4 прикачени файла.');
      });

      singleFileBtnElem.click();
      singleFileDirectiveSpan.getText().then(function (value) {
        expect(value).toEqual('file1');
      });

      noFilesBtnElem.click();
      filesDirectiveSpan.getText().then(function (value) {
        expect(value).toEqual('Няма прикачени файлове.');
      });
    });

    it('should display files.', function () {
      /*jshint quotmark:false */

      openModalBtnElem.click().then(function () {
        ptor.findElement(
            protractor.By.xpath("//div[@class='modal-body ng-scope']/span[last()]")
          ).getText().then(function (value) {
            expect(value).toEqual('Няма прикачени файлове');
          });
      });
    });
  });
}(protractor));