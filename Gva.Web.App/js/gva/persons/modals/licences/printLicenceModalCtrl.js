/*global angular*/
(function (angular) {
  'use strict';

  function PrintLicenceModalCtrl(
    $scope,
    $modalInstance,
    PersonLicenceEditions,
    scModalParams,
    licenceEdition
  ) {
    $scope.form = {};
    $scope.isLastEdition = scModalParams.isLastEdition;
    $scope.model = {
      stampNumber: licenceEdition.part.stampNumber,
      stampNumberReadonly: !!licenceEdition.part.stampNumber,
      lotId: scModalParams.lotId,
      index: scModalParams.index,
      editionIndex: scModalParams.editionIndex
    };

    $scope.save = function () {
      return $scope.form.printLicenceForm.$validate().then(function () {
        if ($scope.form.printLicenceForm.$valid) {
          licenceEdition.part.stampNumber = $scope.model.stampNumber;

          return PersonLicenceEditions.save({
            id: scModalParams.lotId,
            ind: scModalParams.index,
            index: scModalParams.editionIndex
          }, licenceEdition).$promise.then(function (savedEdition) {
            return $modalInstance.close(savedEdition);
          });
        }
      });
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };
  }

  PrintLicenceModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'PersonLicenceEditions',
    'scModalParams',
    'licenceEdition'
  ];

  PrintLicenceModalCtrl.$resolve = {
    licenceEdition: [
      'scModalParams',
      'PersonLicenceEditions',
      function resolveDocs(scModalParams, PersonLicenceEditions) {
        return PersonLicenceEditions.get({
          id: scModalParams.lotId,
          ind: scModalParams.index,
          index: scModalParams.editionIndex
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('PrintLicenceModalCtrl', PrintLicenceModalCtrl);
}(angular));
