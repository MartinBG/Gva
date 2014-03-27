

/*global angular*/
(function (angular) {
  'use strict';

  function AirportDocumentApplicationCtrl($scope) {
    $scope.validateDate = function () {
      if (!$scope.model.requestDate || !$scope.model.documentDate) {
        return true;
      }

      return $scope.model.requestDate <= $scope.model.documentDate;
    };
  }


  angular.module('gva')
  .controller('AirportDocumentApplicationCtrl', AirportDocumentApplicationCtrl);
}(angular));
