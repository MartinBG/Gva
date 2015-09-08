/*global angular*/

(function (angular) {
  'use strict';

  function PaperDataCtrl($scope, scFormParams, Papers) {
    $scope.isNew = scFormParams.isNew;

    $scope.isValidData = function () {
      if($scope.model.name && $scope.model.firstNumber) {
        return Papers.isValidPaperData({
          paperName: $scope.model.name,
          paperId: $scope.model.paperId
        })
          .$promise
          .then(function (result) {
            return result.isValid;
          });
      } else {
        return true;
      }
    };
  }

  PaperDataCtrl.$inject = ['$scope', 'scFormParams', 'Papers'];

  angular.module('gva').controller('PaperDataCtrl', PaperDataCtrl);
}(angular));
