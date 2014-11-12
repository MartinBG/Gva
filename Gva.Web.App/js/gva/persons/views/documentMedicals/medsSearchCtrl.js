/*global angular, moment*/
(function (angular, moment) {
  'use strict';

  function DocumentMedicalsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    person,
    meds
  ) {
    $scope.person = person;
    $scope.medicals = meds;

    $scope.isExpiringDocument = function(item) {
      var today = moment(new Date()),
          difference = moment(item.part.documentDateValidTo).diff(today, 'days');

      return 0 <= difference && difference <= 30;
    };

    $scope.isExpiredDocument = function(item) {
      return moment(new Date()).isAfter(item.part.documentDateValidTo);
    };

    $scope.editDocumentMedical = function (medical) {
      return $state.go('root.persons.view.medicals.edit', {
        id: $stateParams.id,
        ind: medical.partIndex
      });
    };

    $scope.newDocumentMedical = function () {
      return $state.go('root.persons.view.medicals.new');
    };
  }

  DocumentMedicalsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'person',
    'meds'
  ];

  DocumentMedicalsSearchCtrl.$resolve = {
    meds: [
      '$stateParams',
      'PersonDocumentMedicals',
      function ($stateParams, PersonDocumentMedicals) {
        return PersonDocumentMedicals.query($stateParams).$promise;
      }
    ]
  };
  angular.module('gva').controller('DocumentMedicalsSearchCtrl', DocumentMedicalsSearchCtrl);
}(angular, moment));
