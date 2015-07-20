/*global angular, $*/
(function (angular, $) {
  'use strict';

  function PrintRatingEditionModalCtrl(
    $scope,
    $modalInstance,
    PersonLicenceEditions,
    scModalParams
  ) {
    $scope.model = {
      lotId: scModalParams.lotId,
      licenceIndex: scModalParams.licenceIndex,
      licenceEditionIndex: scModalParams.licenceEditionIndex,
      ratingIndex: scModalParams.ratingIndex,
      ratingEditionIndex: scModalParams.ratingEditionIndex,
      noNumber: scModalParams.existingEntry.noNumber,
      hasFileId: scModalParams.existingEntry.hasFileId
    };

    $scope.save = function () {
      return PersonLicenceEditions.setRatingEditionLicenceStatus({
        id: $scope.model.lotId,
        ind: $scope.model.licenceIndex,
        index: $scope.model.licenceEditionIndex,
        ratingIndex: $scope.model.ratingIndex,
        ratingEditionIndex: $scope.model.ratingEditionIndex,
        noNumber: $scope.model.noNumber
      }, $scope.model.existingEntry).$promise
        .then(function () {
          return $modalInstance.close();
        });
    };

    $scope.markAsPrinted = function () {
      $scope.model.hasFileId = true;
    };

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };

    $scope.setNoNumber = function (event) {
      $scope.model.noNumber =
        $(event.target).is(':checked') ? true : false;
    };
  }

  PrintRatingEditionModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'PersonLicenceEditions',
    'scModalParams'
  ];

  angular.module('gva').controller('PrintRatingEditionModalCtrl', PrintRatingEditionModalCtrl);
}(angular, $));
