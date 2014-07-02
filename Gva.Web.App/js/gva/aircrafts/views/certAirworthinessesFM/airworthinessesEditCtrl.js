/*global angular,_*/
(function (angular) {
  'use strict';

  function CertAirworthinessesFMEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertAirworthinessesFM,
    aircraftCertAirworthiness,
    inspectorType
  ) {
    var originalAirworthiness = _.cloneDeep(aircraftCertAirworthiness);

    $scope.isEdit = true;
    $scope.aw = aircraftCertAirworthiness;
    $scope.editMode = null;

    $scope.$watch('aw.part.reviews | last', function (lastReview) {
      $scope.currentReview = lastReview;
      $scope.lastReview = lastReview;
    });

    $scope.selectReview = function (item) {
      $scope.currentReview = item;
    };

    $scope.newReview = function () {
      $scope.aw.part.reviews.push({
        amendment1: null,
        amendment2: null
      });

      $scope.editMode = 'editReview';
    };

    $scope.newAmendment = function () {
      if (!$scope.lastReview.amendment1) {
        $scope.lastReview.amendment1 = {
          inspector: {
            inspectorType: inspectorType
          }
        };
      } else if (!$scope.lastReview.amendment2) {
        $scope.lastReview.amendment2 = {
          inspector: {
            inspectorType: inspectorType
          }
        };
      }

      $scope.editMode = 'editReview';
    };

    $scope.editLastReview = function () {
      $scope.editMode = 'editReview';
    };

    $scope.deleteLastReview = function () {
      $scope.aw.part.reviews.pop();
      return AircraftCertAirworthinessesFM.save(
        { id: $stateParams.id, ind: $stateParams.ind },
        $scope.aw
      )
      .$promise.then(function () {
        originalAirworthiness = _.cloneDeep($scope.aw);
      });
    };

    $scope.deleteAirworthiness = function () {
      return AircraftCertAirworthinessesFM
        .remove({ id: $stateParams.id, ind: $stateParams.ind })
        .$promise.then(function () {
          return $state.go('root.aircrafts.view.airworthinessesFM.search');
        });
    };

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.editReview = function () {
      $scope.editMode = 'editReview';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.aw = _.cloneDeep(originalAirworthiness);
    };

    $scope.save = function () {
      return $scope.editCertAirworthinessForm.$validate()
      .then(function () {
        if ($scope.editCertAirworthinessForm.$valid) {
          return AircraftCertAirworthinessesFM
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.aw)
            .$promise
            .then(function () {
              return $state.go('root.aircrafts.view.airworthinessesFM.search');
            });
        }
      });
    };
  }

  CertAirworthinessesFMEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertAirworthinessesFM',
    'aircraftCertAirworthiness',
    'inspectorType'
  ];

  CertAirworthinessesFMEditCtrl.$resolve = {
    aircraftCertAirworthiness: [
      '$stateParams',
      'AircraftCertAirworthinessesFM',
      function ($stateParams, AircraftCertAirworthinessesFM) {
        return AircraftCertAirworthinessesFM.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ],
    inspectorType: [
      'Nomenclatures',
      function (Nomenclatures) {
        return Nomenclatures.get({
          alias: 'inspectorTypes',
          valueAlias: 'examiner'
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertAirworthinessesFMEditCtrl', CertAirworthinessesFMEditCtrl);
}(angular));