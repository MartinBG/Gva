/*global angular, _*/
(function (angular, _) {
  'use strict';

  function CertAirworthinessesFMNewCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertAirworthinessesFM,
    actF25,
    art15A
  ) {
    $scope.aw = {
      part: {
        airworthinessCertificateType: actF25
      },
      applications: []
    };

    $scope.reviewForm15 = {
      airworthinessReviewType: art15A,
      inspector: {}
    };

    $scope.reviewOther = {
      inspector: {}
    };

    $scope.save = function () {
      return $scope.newCertAirworthinessForm.$validate()
        .then(function () {
          var aw = $scope.aw,
              actAlias;

          if ($scope.newCertAirworthinessForm.$valid) {
            actAlias = aw.part.airworthinessCertificateType.alias;
            if (actAlias !== 'special') {
              //clone the scope object to keep it clean in case something fails
              aw = _.cloneDeep($scope.aw);

              aw.part.reviews = [];

              if (actAlias === 'f24' || actAlias === 'f25') {
                aw.part.reviews.push($scope.reviewForm15);
              } else if (actAlias === 'directive8' || actAlias === 'vla') {
                aw.part.reviews.push($scope.reviewOther);
              }
            }

            return AircraftCertAirworthinessesFM
              .save({ id: $stateParams.id }, aw)
              .$promise
              .then(function () {
                return $state.go('root.aircrafts.view.airworthinessesFM.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.aircrafts.view.airworthinessesFM.search');
    };
  }

  CertAirworthinessesFMNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertAirworthinessesFM',
    'actF25',
    'art15A'
  ];

  CertAirworthinessesFMNewCtrl.$resolve = {
    actF25: [
      'Nomenclatures',
      function (Nomenclatures) {
        return Nomenclatures.get({
          alias: 'airworthinessCertificateTypes',
          valueAlias: 'f25'
        }).$promise;
      }
    ],
    art15A: [
      'Nomenclatures',
      function (Nomenclatures) {
        return Nomenclatures.get({
          alias: 'airworthinessReviewTypes',
          valueAlias: '15a'
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertAirworthinessesFMNewCtrl', CertAirworthinessesFMNewCtrl);
}(angular, _));
