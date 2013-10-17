(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');
  
  module.exports = {
    personData1: {
      lin: '11232',
      uin: '6101033765',
      firstName: 'Иван',
      firstNameAlt: 'Ivan',
      middleName: 'Иванов',
      middleNameAlt: 'Ivanov',
      lastName: 'Иванов',
      lastNameAlt: 'Ivanov',
      dateOfBirth: '1961-04-04T00:00',
      placeOfBirthId: nomenclatures.getId('cities', 'Sofia'),
      countryId: nomenclatures.getId('countries', 'Bulgaria'),
      sexId: nomenclatures.getId('sex', 'male'),
      email: 'ivan@mail.bg',
      fax: '(02) 876 89 89',
      phone1: '0888 876 431',
      phone2: '0888 876 432',
      phone3: '0888 876 433',
      phone4: '0888 876 434',
      phone5: '0888 876 435'
    }
  };
})(typeof module === 'undefined' ? (this['person-data.sample'] = {}) : module);