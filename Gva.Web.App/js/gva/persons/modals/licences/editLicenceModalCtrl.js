/*global angular, _*/
(function (angular, _) {
  'use strict';

  function EditLicenceModalCtrl(
    $scope,
    $modalInstance,
    scModalParams,
    scModal,
    PersonLicences,
    licence
  ) {
    $scope.form = {};
    $scope.licence = licence;
    $scope.lotId = scModalParams.lotId;
    $scope.lastEdition = _.last($scope.licence.part.editions);
    $scope.edition = _.find($scope.licence.part.editions, function (edition) {
      return edition.index === scModalParams.editionIndex;
    });

    $scope.save = function () {
      return $scope.form.editLicenceForm.$validate().then(function () {
        if ($scope.form.editLicenceForm.$valid) {
          return PersonLicences.save({
              id: scModalParams.lotId,
              ind: scModalParams.licenceIndex
            }, $scope.licence)
          .$promise
          .then(function (savedLicence) {
            return $modalInstance.close(savedLicence);
          });
        }
      });
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };

    $scope.viewStatuses = function () {
      var params = {
        licence: $scope.licence,
        personId: scModalParams.lotId,
        licenceInd: scModalParams.licenceIndex
      };

      var modalInstance = scModal.open('licenceStatuses', params);

      return modalInstance.opened;
    };
  }

  EditLicenceModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'scModalParams',
    'scModal',
    'PersonLicences',
    'licence'
  ];

  EditLicenceModalCtrl.$resolve = {
    licence: [
      'PersonLicences',
      'scModalParams',
      function (PersonLicences, scModalParams) {
        return PersonLicences.get({
          id: scModalParams.lotId,
          ind: scModalParams.licenceIndex
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('EditLicenceModalCtrl', EditLicenceModalCtrl);
}(angular, _));
