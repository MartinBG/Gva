/*global angular, $*/
(function (angular, $) {
  'use strict';

  function PrintLicenceModalCtrl(
    $scope,
    $modalInstance,
    PersonLicenceEditions,
    scModalParams,
    licenceEdition
  ) {
    $scope.form = {};
    $scope.editMode = null;

    $scope.isLastEdition = scModalParams.isLastEdition;
    $scope.model = {
      stampNumber: licenceEdition.part.stampNumber,
      lotId: scModalParams.lotId,
      index: scModalParams.index,
      editionIndex: scModalParams.editionIndex,
      isFclOrPart66: scModalParams.isFclOrPart66,
      paperId: licenceEdition.part.paperId,
      hasNoNumber: licenceEdition.part.hasNoNumber
    };

    $scope.save = function () {
      return $scope.form.printLicenceForm.$validate().then(function () {
        if ($scope.form.printLicenceForm.$valid) {
          licenceEdition.part.stampNumber = $scope.model.stampNumber;
          licenceEdition.part.paperId = $scope.model.paperId;
          licenceEdition.part.hasNoNumber = $scope.model.hasNoNumber;

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

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };

    $scope.setNoNumber = function (event) {
      $scope.model.hasNoNumber =
        $(event.target).is(':checked') ? true : false;
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
}(angular, $));
