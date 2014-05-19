/*global module, require*/
(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');

  module.exports = {
    person1Data: {
      lin: '11232',
      uin: '7005159385',
      firstName: 'Иван',
      firstNameAlt: 'Ivan',
      middleName: 'Иванов',
      middleNameAlt: 'Ivanov',
      lastName: 'Иванов',
      lastNameAlt: 'Ivanov',
      dateOfBirth: '1970-05-15T00:00',
      placeOfBirth: nomenclatures.get('cities', 'Sofia'),
      country: nomenclatures.get('countries', 'BG'),
      sex: nomenclatures.get('gender', 'male'),
      email: 'ivan@mail.bg',
      fax: '(02) 876 89 89',
      phone1: '0888 876 431',
      phone2: '0888 876 432',
      phone3: '0888 876 433',
      phone4: '0888 876 434',
      phone5: '0888 876 435'
    },
    person2Data: {
      lin: '12345',
      uin: '7903245888',
      firstName: 'Атанас',
      firstNameAlt: 'Atanas',
      middleName: 'Иванов',
      middleNameAlt: 'Ivanov',
      lastName: 'Иванов',
      lastNameAlt: 'Ivanov',
      dateOfBirth: '1979-03-24T00:00',
      placeOfBirth: nomenclatures.get('cities', 'Sofia'),
      country: nomenclatures.get('countries', 'BG'),
      sex: nomenclatures.get('gender', 'male'),
      email: 'atanas@mail.bg',
      fax: '(02) 876 89 89',
      phone1: '0888 876 431',
      phone2: '0888 876 432',
      phone3: '0888 876 433',
      phone4: '0888 876 434',
      phone5: '0888 876 435'
    },
    person3Data: {
      lin: '11111',
      uin: '6904245664',
      firstName: 'Петър',
      firstNameAlt: 'Petar',
      middleName: 'Петров',
      middleNameAlt: 'Petrov',
      lastName: 'Петров',
      lastNameAlt: 'Petrov',
      dateOfBirth: '1969-04-24T00:00',
      placeOfBirth: nomenclatures.get('cities', 'Sofia'),
      country: nomenclatures.get('countries', 'KWI'),
      sex: nomenclatures.get('gender', 'male'),
      email: 'petar@mail.bg',
      fax: '(02) 876 89 89',
      phone1: '0888 876 431',
      phone2: '0888 876 432',
      phone3: '0888 876 433',
      phone4: '0888 876 434',
      phone5: '0888 876 435'
    }
  };
})(typeof module === 'undefined' ? (this['person-data.sample'] = {}) : module);
