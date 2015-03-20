/*global angular*/
(function (angular) {
  'use strict';

  function LicenceEditionDocModalCtrl(
    $scope,
    $modalInstance,
    PersonLicenceEditions,
    scModalParams
  ) {

    $scope.personId = scModalParams.personId;
    $scope.edition = scModalParams.edition;

    $scope.save = function () {
      return PersonLicenceEditions.save({
        id: scModalParams.personId,
        ind: scModalParams.licenceInd,
        index: scModalParams.editionInd,
        caseTypeId: scModalParams.caseTypeId
      }, $scope.edition)
        .$promise
        .then(function () {
          return $modalInstance.close();
        });
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };

  }

  LicenceEditionDocModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'PersonLicenceEditions',
    'scModalParams'
  ];

  angular.module('gva').controller('LicenceEditionDocModalCtrl', LicenceEditionDocModalCtrl);
}(angular));