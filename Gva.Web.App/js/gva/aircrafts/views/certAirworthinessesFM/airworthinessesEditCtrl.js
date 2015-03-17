/*global angular,_*/
(function (angular) {
  'use strict';

  function CertAirworthinessesFMEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertAirworthinessesFM,
    AircraftCertRegistrationsFM,
    originalAw,
    scMessage
  ) {

    function resetAw() {
      $scope.aw = _.cloneDeep(originalAw);
    }

    function resetReviews() {
      $scope.reviews = _.cloneDeep(originalAw).part.reviews;
      if ($scope.reviews && $scope.reviews.length) {
        $scope.currentReview = $scope.reviews[$scope.reviews.length - 1];
      }
    }

    resetAw();
    resetReviews();

    $scope.disableNewAmmendment = false;
    $scope.$watch('aw.part.registration', function() {
      AircraftCertRegistrationsFM.get({
          id: $stateParams.id,
          ind: $scope.aw.part.registration.nomValueId
        })
      .$promise
      .then(function (reg) {
        $scope.disableNewAmmendment = reg.part.status.code !== '1' && reg.part.status.code !== '2';
      });
    });

    $scope.isEditAw = false;
    $scope.isEditReview = false;
    $scope.lotId = $stateParams.id;

    //aw
    $scope.editAw = function () {
     $scope.isEditAw = true;
    };

    $scope.cancelAw = function () {
      $scope.isEditAw = false;
      resetAw();
    };

    $scope.deleteAw = function () {
      return scMessage('common.messages.confirmDelete')
      .then(function (result) {
        if (result === 'OK') {
          return AircraftCertAirworthinessesFM
          .remove({ id: $stateParams.id, ind: $stateParams.ind })
          .$promise.then(function () {
            return $state.go('root.aircrafts.view.airworthinessesFM.search');
          });
        }
      });
    };

    $scope.saveAw = function () {
      return $scope.aircraftCertAirworthinessForm.$validate()
      .then(function () {
        if ($scope.aircraftCertAirworthinessForm.$valid) {
          return AircraftCertAirworthinessesFM
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.aw)
            .$promise
            .then(function () {
              originalAw = $scope.aw;
              $scope.isEditAw = false;
              resetAw();
            });
        }
      });
    };

    //review
    $scope.selectReview = function (item) {
      $scope.currentReview = item;
    };

    $scope.newReview = function () {
      $scope.reviews.push({
        inspector: {}
      });
      $scope.currentReview = $scope.reviews[$scope.reviews.length - 1];

      $scope.isEditReview = true;
    };

    $scope.newAmendment = function () {
      if (!$scope.aw.part.form15Amendments || !$scope.aw.part.form15Amendments.amendment1) {
        $scope.aw.part.form15Amendments = {
          amendment1 : {
            inspector: {}
          }
        };
      } else if (!$scope.aw.part.form15Amendments.amendment2) {
        $scope.aw.part.form15Amendments.amendment2 = {
          inspector: {}
        };
      }
    };

    $scope.editReview = function () {
      $scope.isEditReview = true;
    };

    $scope.cancelReview = function () {
      $scope.isEditReview = false;
      resetReviews();
    };

    $scope.deleteReview = function (review) {
      var aw = _.cloneDeep(originalAw);

      //not modifying the $scope.reviews in case something fails
      aw.part.reviews = $scope.reviews.slice();
      aw.part.reviews.splice(aw.part.reviews.indexOf(review), 1);

      return AircraftCertAirworthinessesFM
        .save({ id: $stateParams.id, ind: $stateParams.ind }, aw)
        .$promise
        .then(function () {
          originalAw = aw;
          resetReviews();
        });
    };

    $scope.saveReview = function () {
      var actAlias = $scope.aw.part.airworthinessCertificateType.alias,
          form;
      if (actAlias === 'directive8' || actAlias === 'vla' || actAlias === 'unknown') {
        form = $scope.airworthinessReviewOtherForm;
      }

      return form.$validate()
        .then(function () {
          var aw;
          if (form.$valid) {
            aw = _.cloneDeep(originalAw);
            aw.part.reviews = $scope.reviews;

            return AircraftCertAirworthinessesFM
              .save({ id: $stateParams.id, ind: $stateParams.ind }, aw)
              .$promise
              .then(function () {
                originalAw = aw;
                $scope.isEditReview = false;
                resetReviews();
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
    'AircraftCertRegistrationsFM',
    'aircraftCertAirworthiness',
    'scMessage'
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
    ]
  };
  
  angular.module('gva').controller('CertAirworthinessesFMEditCtrl', CertAirworthinessesFMEditCtrl);
}(angular));
