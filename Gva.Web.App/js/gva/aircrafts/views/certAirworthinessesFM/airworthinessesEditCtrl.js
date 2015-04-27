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
    scModal,
    scMessage
  ) {
    function resetAw() {
      $scope.aw = _.cloneDeep(originalAw);
      if ($scope.aw.part.airworthinessCertificateType.alias === '15a' ||
        $scope.aw.part.airworthinessCertificateType.alias === '15b') {
        $scope.newReviewBtnText = 'aircrafts.editAirworthiness.newAmendment';
      } else {
        $scope.newReviewBtnText = 'aircrafts.editAirworthiness.newReview';
      }
    }

    function resetReviews() {
      $scope.reviews = _.cloneDeep(originalAw).part.reviews;
      if ($scope.reviews && $scope.reviews.length) {
        $scope.currentReview = $scope.reviews[$scope.reviews.length - 1];
      }
    }

    resetAw();
    resetReviews();

    $scope.isActiveReg = true;
    $scope.$watch('aw.part.registration', function() {
      if ($scope.aw.part.registration) {
        AircraftCertRegistrationsFM.get({
          id: $stateParams.id,
          ind: $scope.aw.part.registration.nomValueId
        })
        .$promise
        .then(function (reg) {
          $scope.isActiveReg = reg.part.status.code === '1' || reg.part.status.code === '2';
        });
      }
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

    $scope.disableNewReview = false;
    $scope.$watch('reviews.length', function () {
      if(($scope.aw.part.airworthinessCertificateType.alias === '15a' || 
        $scope.aw.part.airworthinessCertificateType.alias === '15b') && 
        $scope.reviews.length === 2) {
        $scope.disableNewReview = true;
      } else {
        $scope.disableNewReview = false;
      }
    });

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
      return $scope.airworthinessReviewForm.$validate()
        .then(function () {
          var aw;
          if ($scope.airworthinessReviewForm.$valid) {
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

    $scope.print = function () {
      var params = {
        lotId: $stateParams.id,
        partIndex: $stateParams.ind
      };

      var modalInstance = scModal.open('printAirworthiness', params);

      modalInstance.result.then(function (savedAirworthiness) {
        $scope.aw.part.stampNumber = savedAirworthiness.part.stampNumber;
      });

      return modalInstance.opened;
    };
  }

  CertAirworthinessesFMEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertAirworthinessesFM',
    'AircraftCertRegistrationsFM',
    'aircraftCertAirworthiness',
    'scModal',
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
