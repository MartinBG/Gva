/*global angular*/
(function (angular) {
  'use strict';

  function PersonsNewCtrl($scope, Person) {
    $scope.newPerson = {
      personData: {
        lin: undefined,
        uin: undefined,
        dateOfBirth: undefined,
        sexId: undefined,
        firstName: undefined,
        firstNameAlt: undefined,
        middleName: undefined,
        middleNameAlt: undefined,
        lastName: undefined,
        lastNameAlt: undefined,
        placeOfBirthId: undefined,
        countryId: undefined,
        email: undefined,
        fax: undefined,
        phone1: undefined,
        phone2: undefined,
        phone3: undefined,
        phone4: undefined,
        phone5: undefined
      },
      personAddress: {
        addressTypeId: undefined,
        valid: undefined,
        settlementId: undefined,
        address: undefined,
        addressAlt: undefined,
        postalCode: undefined,
        phone: undefined
      },
      personDocumentId: {
        personDocumentIdTypeId: undefined,
        valid: undefined,
        documentNumber: undefined,
        documentDateValidFrom: undefined,
        documentDateValidTo: undefined,
        documentPublisher: undefined
      }
    };

    $scope.save = function () {
      return Person.save($scope.newPerson).$promise;
    };
  }

  PersonsNewCtrl.$inject = ['$scope', 'Person'];

  angular.module('gva').controller('PersonsNewCtrl', PersonsNewCtrl);
}(angular));
