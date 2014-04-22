/*global angular, _*/
(function (angular, _) {
  'use strict';

  function LicencesEditCtrl(
    $scope,
    $state,
    $stateParams,
    PersonLicence,
    licence
  ) {
    var originalLicence = _.cloneDeep(licence);
    $scope.licence = licence;
    $scope.editMode = null;
    $scope.lotId = $stateParams.id;

    $scope.backFromChild = false;

    if ($state.previous && $state.previous.includes[$state.current.name]) {
      $scope.backFromChild = true;
    }

    $scope.$watch('licence.part.editions | last', function (lastEdition) {
      $scope.currentEdition = lastEdition;
      $scope.lastEdition = lastEdition;
    });

    $scope.selectEdition = function (item) {
      $scope.currentEdition = item;
    };

    $scope.newEdition = function () {
      $scope.licence.part.editions.push({});

      $scope.editMode = 'edit';
    };

    $scope.editLastEdition = function () {
      $scope.editMode = 'edit';
    };

    $scope.deleteLastEdition = function () {
      $scope.licence.part.editions.pop();

      if ($scope.licence.part.editions.length === 0) {
        return PersonLicence
          .remove({ id: $stateParams.id, ind: $stateParams.ind })
          .$promise.then(function () {
            return $state.go('root.persons.view.licences.search');
          });
      }
      else {
        return PersonLicence.save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.licence)
          .$promise;
      }
    };

    $scope.save = function () {
      return $scope.editLicenceForm.$validate()
        .then(function () {
          if ($scope.editLicenceForm.$valid) {
            $scope.editMode = 'saving';
            $scope.backFromChild = false;
            return PersonLicence
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.licence).$promise
              .then(function () {
                $scope.editMode = null;
                $scope.backFromChild = false;
                originalLicence = _.cloneDeep($scope.licence);
              }, function () {
                $scope.editMode = 'edit';
              });
          }
        });
    };

    $scope.cancel = function () {
      $scope.licence = _.cloneDeep(originalLicence);
      $scope.editMode = null;
    };
  }

  LicencesEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonLicence',
    'licence'
  ];

  LicencesEditCtrl.$resolve = {
    licence: [
      '$stateParams',
      'PersonLicence',
      function ($stateParams, PersonLicence) {
        return PersonLicence.get($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('LicencesEditCtrl', LicencesEditCtrl);
}(angular, _));