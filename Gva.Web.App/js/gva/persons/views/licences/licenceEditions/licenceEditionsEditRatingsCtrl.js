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
    includedRatings,
    scMessage,
    scModal
  ) {

    $scope.currentLicenceEdition = currentLicenceEdition;
    $scope.isLast = _.last(licenceEditions).partIndex === currentLicenceEdition.partIndex;
    $scope.currentLicenceEdition.part.includedRatings =
      $scope.currentLicenceEdition.part.includedRatings || [];

    $scope.includedRatings = includedRatings;

    $scope.addRating = function () {
      var modalInstance = scModal.open('newRating', {
        lotId: $stateParams.id,
        appId: $stateParams.appId
      });

      modalInstance.result.then(function (newRating) {
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

            $scope.includedRatings.push(rating);
            $scope.currentLicenceEdition.part.includedRatings.push({
              ind: rating.ratingPartIndex,
              index: rating.editionPartIndex
            });

            $scope.save();
          });
      });

      return modalInstance.opened;
    };

    $scope.addExistingRating = function () {
      var modalInstance = scModal.open('chooseRatings', {
        includedRatings: $scope.currentLicenceEdition.part.includedRatings,
        lotId: $stateParams.id
      });

      modalInstance.result.then(function (selectedRatings) {
        _.forEach(selectedRatings, function(rating) {
          var newlyAddedRating = {
            ind: rating.ratingPartIndex,
            index: rating.editionPartIndex
          };
          $scope.currentLicenceEdition.part.includedRatings.push(newlyAddedRating);

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
    'includedRatings',
    'scMessage',
    'scModal'
  ];

  LicenceEditionsEditRatingsCtrl.$resolve = {
    includedRatings: [
      '$stateParams',
      'PersonRatings',
      'currentLicenceEdition',
      function ($stateParams, PersonRatings, currentLicenceEdition) {
        return  PersonRatings
        .getRatingsWithAllEditions({ id: $stateParams.id })
        .$promise
        .then(function (ratings) {
          return _.map(currentLicenceEdition.part.includedRatings,
            function (rating) {
              return _.find(ratings, { 
                ratingPartIndex: rating.ind,
                editionPartIndex: rating.index
              });
            });
       });
      }
    ]
  };

  angular.module('gva')
    .controller('LicenceEditionsEditRatingsCtrl', LicenceEditionsEditRatingsCtrl);
}(angular, _));
