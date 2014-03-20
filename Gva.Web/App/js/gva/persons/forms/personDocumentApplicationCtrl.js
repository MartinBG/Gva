

/*global angular*/
(function (angular) {
  'use strict';

  function PersonDocumentApplicationCtrl($scope) {
    $scope.validateDate = function () {
      if (!$scope.model.requestDate || !$scope.model.documentDate) {
        return true;
      }

      return $scope.model.requestDate <= $scope.model.documentDate;
    };
  }


  angular.module('gva').controller('PersonDocumentApplicationCtrl', PersonDocumentApplicationCtrl);
}(angular));
