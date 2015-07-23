/*global angular*/
(function (angular) {
  'use strict';

  function LicencesViewCtrl(
    $scope,
    $state,
    $stateParams,
    l10n,
    scModal,
    PersonLicences,
    licence
  ) {
    $scope.licence = licence;
    $scope.lotId = $stateParams.id;
    $scope.title = l10n.get('persons.licenceView.title');
    $scope.newEdition = function () {
      return $state.go('root.persons.view.licences.view.editions.new');
    };

    $scope.viewStatuses = function () {
      var params = {
        licence: $scope.licence,
        personId: $stateParams.id,
        licenceInd: $stateParams.ind
      };

      var modalInstance = scModal.open('licenceStatuses', params);

      modalInstance.result.then(function () {
        $state.reload();
      });

      return modalInstance.opened;
    };
  }

  LicencesViewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'l10n',
    'scModal',
    'PersonLicences',
    'licence'
  ];

  LicencesViewCtrl.$resolve = {
    licence: [
      '$stateParams',
      'PersonLicences',
      function ($stateParams, PersonLicences) {
        return PersonLicences.getLicenceData($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('LicencesViewCtrl', LicencesViewCtrl);
}(angular));
