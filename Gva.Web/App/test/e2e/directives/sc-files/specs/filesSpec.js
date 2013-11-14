/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect, require) {
  'use strict';

  var _ = require('lodash');

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

      filesDirectiveSpan = ptor.findElement(protractor.By.className('test-files-span'));
      singleFileDirectiveSpan = ptor.findElement(protractor.By.className('test-single-file-span'));
      multipleFilesBtnElem = ptor.findElement(protractor.By.name('multipleFilesBtn'));
      singleFileBtnElem = ptor.findElement(protractor.By.name('singleFileBtn'));
      noFilesBtnElem = ptor.findElement(protractor.By.name('noFilesBtn'));
      openModalBtnElem = ptor.findElement(protractor.By.className('glyphicon-file'));
    });

    it('should set the text of the element to whatever file array is passed.', function () {
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

    it('should open modal.', function () {
      openModalBtnElem.click().then(function () {
        ptor.findElement(
            protractor.By.className('test-no-files-span')
          ).getText().then(function (value) {
            expect(value).toEqual('Няма прикачени файлове');
          });
      });
    });

    it('should dismiss modal', function () {
      multipleFilesBtnElem.click();

      openModalBtnElem.click().then(function () {
        return ptor.findElement(protractor.By.className('btn-default'))
          .click();
      })
      .then(function () {
        ptor.findElements(protractor.By.className('modal-body'))
          .then(function (data) {
            expect(_.keys(data).length).toEqual(0);
            filesDirectiveSpan.getText().then(function (value) {
              expect(value).toEqual('4 прикачени файла.');
            });
          });
      });
    });

    it('should display file tree.', function () {
      multipleFilesBtnElem.click();
      openModalBtnElem.click().then(function () {
        ptor.findElements(
          protractor.By.className('test-files-li')).then(function (data) {
            expect(_.keys(data).length).toEqual(3);
          });
        ptor.findElements(
          protractor.By.className('test-files-root-li')).then(function (data) {
            expect(_.keys(data).length).toEqual(2);
          });
      });
    });

    it('should render download link inside modal.', function () {
      singleFileBtnElem.click();
      openModalBtnElem.click().then(function () {
        ptor.findElement(
          protractor.By.className('test-download-link')).getText().then(function (value) {
            expect(value).toEqual('file1');
          });
      });
    });

    it('should delete file', function () {
      singleFileBtnElem.click();
      singleFileDirectiveSpan.getText().then(function (value) {
        expect(value).toEqual('file1');
      });
      openModalBtnElem
        .click()
        .then(function () {
          return ptor.findElement(protractor.By.className('tree-image-close')).click();
        })
        .then(function () {
          return ptor.findElement(
            protractor.By.className('test-no-files-span')).getText();
        })
        .then(function (value) {
          expect(value).toEqual('Няма прикачени файлове');
        });
    });

    it('should upload file.', function () {
      openModalBtnElem
        .click()
        .then(function () {
          return ptor.findElement(protractor.By.className('test-file-upload-button'))
            .sendKeys('//filesCtrl.js');
        })
        .then(function () {
          return ptor.findElement(protractor.By.className('test-file-name')).getText();
        })
        .then(function (value) {
          expect(value).toEqual('filesCtrl.js');
        })
        .then(function () {
          return ptor.findElement(protractor.By.className('btn-primary')).click();
        })
        .then(function () {
          return singleFileDirectiveSpan.getText();
        })
        .then(function (value) {
          expect(value).toEqual('filesCtrl.js');
        });
    });

    it('should stop uploading.', function () {
      openModalBtnElem.click()
        .then(function () {
          return ptor.findElement(protractor.By.className('test-file-upload-button'))
             .sendKeys('//filesCtrl.js');
        })
        .then(function () {
          return ptor.findElement(protractor.By.className('test-file-upload-button'))
             .sendKeys('//filesDirective.js');
        })
        .then(function () {
          return ptor.findElement(protractor.By.className('test-file-upload-button'))
             .sendKeys('//filesModalCtrl.js');
        })
        .then(function () {
          return ptor.findElement(protractor.By.className('btn-primary')).click();
        })
        .then(function () {
          return ptor.findElement(protractor.By.className('test-icon-stop')).click();
        })
        .then(function () {
          return ptor.wait(function () {
            return ptor.findElements(protractor.By.className('modal-body'))
              .then(function (elements) {
                return _.keys(elements).length === 0;
              });
          }, 5000);
        })
        .then(function () {
          filesDirectiveSpan.getText().then(function (value) {
            expect(value).toEqual('2 прикачени файла.');
          });
        });
    });
  });
}(protractor, describe, beforeEach, it, expect, require));