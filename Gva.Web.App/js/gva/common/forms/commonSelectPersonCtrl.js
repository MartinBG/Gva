/*global angular*/
(function (angular) {
  'use strict';
  function CommonSelectPersonCtrl($scope, scModal, scFormParams) {
    $scope.newClass = scFormParams.newClass;
    $scope.choosePerson = function () {
      var params = {
        uin: null,
        names: null
      };

      if ($scope.model.doc && $scope.model.doc.docCorrespondents.length > 0) {
        params.names = $scope.model.doc.docCorrespondents[0].bgCitizenLastName;
        params.uin = $scope.model.doc.docCorrespondents[0].bgCitizenUIN;
      }

      var modalInstance = scModal.open('choosePerson', params);

      modalInstance.result.then(function (person) {
        $scope.model.lot.id = person.id;
      });

      return modalInstance.opened;
    };

    $scope.newPerson = function () {
      var params = {
        uin: null,
        firstName: null,
        lastName: null,
        isApplicant: true
      };

      if ($scope.model.doc && $scope.model.doc.docCorrespondents.length > 0) {
        params.uin = $scope.model.doc.docCorrespondents[0].bgCitizenUIN;
        params.firstName = $scope.model.doc.docCorrespondents[0].bgCitizenFirstName;
        params.lastName = $scope.model.doc.docCorrespondents[0].bgCitizenLastName;
      }

      var modalInstance = scModal.open('newPerson', params);

      modalInstance.result.then(function (personId) {
        $scope.model.lot.id = personId;
      });

      return modalInstance.opened;
    };
  }

  CommonSelectPersonCtrl.$inject = ['$scope', 'scModal', 'scFormParams'];

  angular.module('gva').controller('CommonSelectPersonCtrl', CommonSelectPersonCtrl);
}(angular));
