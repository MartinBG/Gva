/*global angular, _*/
(function (angular, _) {
  'use strict';

  function LicenceEditionsEditRatingsCtrl(
    $scope,
    $state,
    $stateParams,
    PersonLicenceEditions,
    PersonRatings,
    currentLicenceEdition,
    licenceEditions,
    scMessage,
    scModal
  ) {

    $scope.currentLicenceEdition = currentLicenceEdition;
    $scope.isLast = _.last(licenceEditions).partIndex === currentLicenceEdition.partIndex;

    PersonRatings
      .getRatingsWithAllEditions({ id: $stateParams.id })
      .$promise
      .then(function (ratings) {
        $scope.includedRatings = _.map($scope.currentLicenceEdition.part.includedRatings,
        function (rating) {
          return _.find(ratings, { 
            ratingPartIndex: rating.ind,
            editionPartIndex: rating.index
          });
        });

        $scope.includedRatings = 
          _.map($scope.currentLicenceEdition.part.includedRatings, function (rating) {
            var includedRating = _.where(ratings, { 
              ratingPartIndex: rating.ind,
              editionPartIndex: rating.index
            })[0];
            includedRating.orderNum = rating.orderNum;

            return includedRating;
          });

        $scope.includedRatings = _.sortBy($scope.includedRatings, 'orderNum');
      });

    $scope.addRating = function () {
      var modalInstance = scModal.open('newRating', {
        lotId: $stateParams.id,
        appId: $stateParams.appId
      });

      modalInstance.result.then(function (newRating) {
        var lastOrderNum = 0,
          lastTraining = _.last($scope.includedRatings);
        if (lastTraining) {
          lastOrderNum = _.last($scope.includedRatings).orderNum;
        }

        PersonRatings.getRatingsWithAllEditions({ id: $stateParams.id })
          .$promise
          .then(function (ratings) {
            var rating = null;
            _.find(ratings, function (r) {
              if (r.ratingPartIndex === newRating.rating.partIndex &&
                r.editionPartIndex === newRating.edition.partIndex) {
                rating = r;
              }
            });

            rating.orderNum = ++lastOrderNum;
            $scope.includedRatings.push(rating);
            
            $scope.currentLicenceEdition.part.includedRatings =
              _.map($scope.includedRatings, function(rating) {
                return {
                  ind: rating.ratingPartIndex,
                  index: rating.editionPartIndex,
                  orderNum: rating.orderNum
                };
              });
          });

        $scope.save();
      });

      return modalInstance.opened;
    };

    $scope.addExistingRating = function () {
      var modalInstance = scModal.open('chooseRatings', {
        includedRatings: $scope.currentLicenceEdition.part.includedRatings,
        lotId: $stateParams.id
      });

      modalInstance.result.then(function (selectedRatings) {
        var lastOrderNum = 0,
          lastRating = _.last($scope.includedRatings);
        if (lastRating) {
          lastOrderNum = _.last($scope.includedRatings).orderNum;
        }

        _.forEach(selectedRatings, function(rating) {
          var newlyAddedRating = {
            orderNum: ++lastOrderNum,
            ind: rating.ratingPartIndex,
            index: rating.editionPartIndex
          };
          $scope.currentLicenceEdition.part.includedRatings.push(newlyAddedRating);

          rating.orderNum = newlyAddedRating.orderNum;
          rating.docDateValidFrom = rating.lastDocDateValidFrom;
          rating.docDateValidTo = rating.lastDocDateValidTo;

          $scope.includedRatings.push(rating);
        });

        $scope.save();
      });

      return modalInstance.opened;
    };

    $scope.removeRating = function (rating) {
      return scMessage('common.messages.confirmDelete')
        .then(function (result) {
          if (result === 'OK') {
            $scope.includedRatings = _.without($scope.includedRatings, rating);
            _.remove($scope.currentLicenceEdition.part.includedRatings,
              function(includedRating) {
                return rating.ratingPartIndex === includedRating.ind &&
                  rating.editionPartIndex === includedRating.index;
              });
            $scope.save();
          }
        });
    };

    $scope.changeOrder = function () {
      $scope.changeOrderMode = true;
    };

    $scope.saveOrder = function () {
      $scope.includedRatings = _.sortBy($scope.includedRatings, 'orderNum');
      $scope.changeOrderMode = false;
      $scope.currentLicenceEdition.part.includedRatings = [];
      _.forEach($scope.includedRatings, function (rating) {
        var changedRating = {
          orderNum: rating.orderNum,
          ind: rating.ratingPartIndex,
          index: rating.editionPartIndex
        };
        $scope.currentLicenceEdition.part.includedRatings.push(changedRating);
      });
      return $scope.save();
    }; 

    $scope.cancelChangeOrder = function () {
      $scope.changeOrderMode = false;
    };

    $scope.save = function () {
      return PersonLicenceEditions
        .save({
          id: $stateParams.id,
          ind: $stateParams.ind,
          index: $stateParams.index,
          caseTypeId: $scope.caseTypeId
        }, $scope.currentLicenceEdition)
        .$promise;
    };
  }

  LicenceEditionsEditRatingsCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonLicenceEditions',
    'PersonRatings',
    'currentLicenceEdition',
    'licenceEditions',
    'scMessage',
    'scModal'
  ];


  angular.module('gva')
    .controller('LicenceEditionsEditRatingsCtrl', LicenceEditionsEditRatingsCtrl);
}(angular, _));
