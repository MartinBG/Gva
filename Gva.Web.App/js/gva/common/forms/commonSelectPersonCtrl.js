/*global angular*/
(function (angular) {
  'use strict';
  function CommonSelectPersonCtrl($scope, namedModal) {
    $scope.choosePerson = function () {
      var params = {
        uin: null,
        names: null
      };

      if ($scope.model.doc && $scope.model.doc.docCorrespondents.length > 0) {
        params.names = $scope.model.doc.docCorrespondents[0].bgCitizenLastName;
        params.uin = $scope.model.doc.docCorrespondents[0].bgCitizenUIN;
      }

      var modalInstance = namedModal.open('choosePerson', params, {
        persons: [
          'Persons',
          function (Persons) {
            return Persons.query(params).$promise;
          }
        ]
      });

      modalInstance.result.then(function (personId) {
        $scope.model.lot.id = personId;
      });

      return modalInstance.opened;
    };

    $scope.newPerson = function () {
      var person = {
        personData: {}
      };

      if ($scope.model.doc && $scope.model.doc.docCorrespondents.length > 0) {
        person.personData.uin = $scope.model.doc.docCorrespondents[0].bgCitizenUIN;
        person.personData.firstName = $scope.model.doc.docCorrespondents[0].bgCitizenFirstName;
        person.personData.lastName = $scope.model.doc.docCorrespondents[0].bgCitizenLastName;
      }

      var modalInstance = namedModal.open('newPerson', { person: person });

      modalInstance.result.then(function (personId) {
        $scope.model.lot.id = personId;
      });

      return modalInstance.opened;
    };
  }

  CommonSelectPersonCtrl.$inject = ['$scope', 'namedModal'];

  angular.module('gva').controller('CommonSelectPersonCtrl', CommonSelectPersonCtrl);
}(angular));
