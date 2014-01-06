/*global protractor, describe, beforeEach, it, expect*/
(function (protractor, describe, beforeEach, it, expect) {
  'use strict';

  var inputFields,
    nomenclatures,
    dates,
    ind;

  inputFields =
    [
      {
        fieldName: 'firstName',
        validationSpanName: 'firstNameValidationSpan'
      },
      {
        fieldName: 'middleName',
        validationSpanName: 'middleNameValidationSpan'
      },
      {
        fieldName: 'lastName',
        validationSpanName: 'lastNameValidationSpan'
      },
      {
        fieldName: 'firstNameAlt',
        validationSpanName: 'firstNameAltValidationSpan'
      },
      {
        fieldName: 'middleNameAlt',
        validationSpanName: 'middleNameAltValidationSpan'
      },
      {
        fieldName: 'lastNameAlt',
        validationSpanName: 'lastNameAltValidationSpan'
      },
      {
        fieldName: 'address',
        validationSpanName: 'addressValidationSpan'
      },
      {
        fieldName: 'addressAlt',
        validationSpanName: 'addressAltValidationSpan'
      },
      {
        fieldName: 'documentNumber',
        validationSpanName: 'documentNumberValidationSpan'
      },
      {
        fieldName: 'documentPublisher',
        validationSpanName: 'documentPublisherValidationSpan'
      }
    ];

  nomenclatures =
    [
      {
        fieldName: 'settlement',
        nomenclatureModel: 'model.settlement',
        validationSpanName: 'settlementValidationSpan'
      },
      {
        fieldName: 'sex',
        nomenclatureModel: 'model.sex',
        validationSpanName: 'sexValidationSpan'
      },
      {
        fieldName: 'country',
        nomenclatureModel: 'model.country',
        validationSpanName: 'countryValidationSpan'
      },
      {
        fieldName: 'placeOfBirth',
        nomenclatureModel: 'model.placeOfBirth',
        validationSpanName: 'placeOfBirthValidationSpan'
      },
      {
        fieldName: 'addressType',
        nomenclatureModel: 'model.addressType',
        validationSpanName: 'addressTypeValidationSpan'
      },
      {
        fieldName: 'personDocumentIdType',
        nomenclatureModel: 'model.personDocumentIdType',
        validationSpanName: 'personDocumentIdTypeValidationSpan'
      }
    ];

  dates =
    [
      {
        fieldName: 'dateOfBirth',
        dateCss: 'div[name=dateOfBirth] > input',
        validationSpanName: 'dateOfBirthValidationSpan'
      },
      {
        fieldName: 'documentDateValidFrom',
        dateCss: 'div[name=documentDateValidFrom] > input',
        validationSpanName: 'documentDateValidFromValidationSpan'
      }
    ];

  describe('New person page', function () {
    var ptor = protractor.getInstance(),
      saveBtn,
      cancelBtn;

    beforeEach(function () {
      ptor.get('#/persons/new');

      saveBtn = ptor.findElement(protractor.By.name('saveBtn'));
      cancelBtn = ptor.findElement(protractor.By.name('cancelBtn'));
    });

    it('should update breadcrumb text', function () {
      /*jshint quotmark:false */
      ptor.findElement(protractor.By.xpath("//ul[@class='breadcrumb']/li[last()]"))
        .getText().then(function (text) {
          expect(text).toEqual('Ново физическо лице');
        });
    });

    it('should redirect to search page on cancel', function () {
      cancelBtn.click();
      ptor.getCurrentUrl().then(function (url) {
        expect(url).toEqual('http://localhost:52560/#/persons');
      });
    });


    function validateInputField(fieldName, validationSpanName) {
      return function () {
        expect(ptor.findElement(protractor.By.name(validationSpanName)).isDisplayed())
          .toBe(true);
        ptor.findElement(protractor.By.name(fieldName)).sendKeys('12.12.2012');
        expect(ptor.findElement(protractor.By.name(validationSpanName)).isDisplayed())
          .toBe(false);
      };
    }

    function validateNomenclature(nomenclatureModel, validationSpanName) {
      return function () {
        expect(ptor.findElement(protractor.By.name(validationSpanName)).isDisplayed())
          .toBe(true);
        ptor.findElement(protractor.By.nomenclature(nomenclatureModel)).click();
        ptor.findElement(protractor.By.nomenclature(nomenclatureModel).dropdownInput())
          .sendKeys(protractor.Key.ENTER);
        expect(ptor.findElement(protractor.By.name(validationSpanName)).isDisplayed())
          .toBe(false);
      };
    }

    function validateDate(dateCss, validationSpanName) {
      return function () {
        expect(ptor.findElement(protractor.By.name(validationSpanName)).isDisplayed())
          .toBe(true);
        ptor.findElement(protractor.By.css(dateCss)).sendKeys('01.01.1980\t');
        expect(ptor.findElement(protractor.By.name(validationSpanName)).isDisplayed())
          .toBe(false);
      };
    }
    for (ind = 0; ind < inputFields.length; ind++) {
      it('should validate ' + inputFields[ind].fieldName,
        validateInputField(inputFields[ind].fieldName, inputFields[ind].validationSpanName)
        );
    }

    for (ind = 0; ind < nomenclatures.length; ind++) {
      it('should validate ' + nomenclatures[ind].fieldName,
        validateNomenclature(
          nomenclatures[ind].nomenclatureModel,
          nomenclatures[ind].validationSpanName
        ));
    }

    for (ind = 0; ind < dates.length; ind++) {
      it('should validate ' + dates[ind].fieldName,
        validateDate(dates[ind].dateCss, dates[ind].validationSpanName)
        );
    }

    it('should validate personAddress.valid', function () {
      var form = ptor.findElement(protractor.By.name('personAddressForm'));
      expect(form.findElement(protractor.By.name('addressValidValidationSpan')).isDisplayed())
                .toBe(true);
      form.findElement(protractor.By.nomenclature('model.valid')).click();
      form.findElement(protractor.By.nomenclature('model.valid').dropdownInput())
        .sendKeys(protractor.Key.ENTER);
      expect(form.findElement(protractor.By.name('addressValidValidationSpan')).isDisplayed())
        .toBe(false);
    });

    it('should validate personDocumentId.valid', function () {
      var form = ptor.findElement(protractor.By.name('personDocumentIdForm'));
      expect(form.findElement(protractor.By.name('docValidValidationSpan')).isDisplayed())
                .toBe(true);
      form.findElement(protractor.By.nomenclature('model.valid')).click();
      form.findElement(protractor.By.nomenclature('model.valid').dropdownInput())
        .sendKeys(protractor.Key.ENTER);
      expect(form.findElement(protractor.By.name('docValidValidationSpan')).isDisplayed())
        .toBe(false);
    });

    it('should validate email', function () {
      expect(ptor.findElement(protractor.By.name('emailValidationSpan')).isDisplayed())
      .toBe(false);
      ptor.findElement(protractor.By.name('email')).sendKeys('email@gmail.');
      expect(ptor.findElement(protractor.By.name('emailValidationSpan')).isDisplayed())
      .toBe(true);

      ptor.findElement(protractor.By.name('email')).sendKeys('com');
      expect(ptor.findElement(protractor.By.name('emailValidationSpan')).isDisplayed())
      .toBe(false);
    });

    it('should validate lin', function () {
      expect(ptor.findElement(protractor.By.name('linValidationSpan')).isDisplayed())
      .toBe(false);

      ptor.findElement(protractor.By.name('lin')).sendKeys('11111');
      expect(ptor.findElement(protractor.By.name('linValidationSpan')).isDisplayed())
      .toBe(true);

      ptor.findElement(protractor.By.name('lin')).sendKeys('1');
      expect(ptor.findElement(protractor.By.name('linValidationSpan')).isDisplayed())
      .toBe(false);
    });

    it('should be able to create new person', function () {
      ptor.findElement(protractor.By.name('firstName')).sendKeys('Георги');
      ptor.findElement(protractor.By.name('middleName')).sendKeys('Георгиев');
      ptor.findElement(protractor.By.name('lastName')).sendKeys('Георгиев');
      ptor.findElement(protractor.By.name('firstNameAlt')).sendKeys('Georgi');
      ptor.findElement(protractor.By.name('middleNameAlt')).sendKeys('Georgiev');
      ptor.findElement(protractor.By.name('lastNameAlt')).sendKeys('Georgiev');
      ptor.findElement(protractor.By.name('lin')).sendKeys('33333');

      ptor.findElement(protractor.By.nomenclature('model.sex')).click();
      ptor.findElement(protractor.By.nomenclature('model.sex').dropdownInput())
        .sendKeys('мъж');
      ptor.findElement(protractor.By.nomenclature('model.sex').dropdownInput())
        .sendKeys(protractor.Key.ENTER);

      ptor.findElement(protractor.By.css('div[name=dateOfBirth] > input')).sendKeys('01.01.1980');

      ptor.findElement(protractor.By.nomenclature('model.placeOfBirth')).click();
      ptor.findElement(protractor.By.nomenclature('model.placeOfBirth').dropdownInput())
        .sendKeys('соф');
      ptor.findElement(protractor.By.nomenclature('model.placeOfBirth').dropdownInput())
        .sendKeys(protractor.Key.ENTER);

      ptor.findElement(protractor.By.nomenclature('model.country')).click();
      ptor.findElement(protractor.By.nomenclature('model.country').dropdownInput())
        .sendKeys('реп');
      ptor.findElement(protractor.By.nomenclature('model.country').dropdownInput())
        .sendKeys(protractor.Key.ENTER);

      ptor.findElement(protractor.By.nomenclature('model.addressType')).click();
      ptor.findElement(protractor.By.nomenclature('model.addressType').dropdownInput())
        .sendKeys('пост');
      ptor.findElement(protractor.By.nomenclature('model.addressType').dropdownInput())
        .sendKeys(protractor.Key.ENTER);

      var addressFrom = ptor.findElement(protractor.By.name('personAddressForm'));
      addressFrom.findElement(protractor.By.nomenclature('model.valid')).click();
      addressFrom.findElement(protractor.By.nomenclature('model.valid').dropdownInput())
        .sendKeys('да');
      addressFrom.findElement(protractor.By.nomenclature('model.valid').dropdownInput())
        .sendKeys(protractor.Key.ENTER);

      ptor.findElement(protractor.By.name('address')).sendKeys('Карнобат');
      ptor.findElement(protractor.By.name('addressAlt')).sendKeys('Karnobat');

      ptor.findElement(protractor.By.nomenclature('model.settlement')).click();
      ptor.findElement(protractor.By.nomenclature('model.settlement').dropdownInput())
        .sendKeys('софия');
      ptor.findElement(protractor.By.nomenclature('model.settlement').dropdownInput())
        .sendKeys(protractor.Key.ENTER);

      ptor.findElement(protractor.By.nomenclature('model.personDocumentIdType')).click();
      ptor.findElement(protractor.By.nomenclature('model.personDocumentIdType').dropdownInput())
        .sendKeys('лич');
      ptor.findElement(protractor.By.nomenclature('model.personDocumentIdType').dropdownInput())
        .sendKeys(protractor.Key.ENTER);

      var personDocumentIdForm = ptor.findElement(protractor.By.name('personDocumentIdForm'));
      personDocumentIdForm.findElement(protractor.By.nomenclature('model.valid')).click();
      personDocumentIdForm.findElement(protractor.By.nomenclature('model.valid').dropdownInput())
        .sendKeys('да');
      personDocumentIdForm.findElement(protractor.By.nomenclature('model.valid').dropdownInput())
        .sendKeys(protractor.Key.ENTER);

      ptor.findElement(protractor.By.name('documentNumber')).sendKeys('D-0001');
      ptor.findElement(protractor.By.css('div[name=documentDateValidFrom] > input'))
        .sendKeys('10.10.2010');
      ptor.findElement(protractor.By.name('documentPublisher')).sendKeys('Карнобат еър');

      saveBtn.click().then(function () {
        ptor.getCurrentUrl().then(function (url) {
          expect(url).toEqual('http://localhost:52560/#/persons');
        });
      });
    });

  });
}(protractor, describe, beforeEach, it, expect));