/*global angular*/
(function (angular) {
  'use strict';

  function PersonLicenceCtrl($scope, $stateParams, PersonLastLicenceNumber) {
    $scope.$watch('model.licenceType', function(){
      if(!$scope.lastLicenceNumber && !!$scope.model.licenceType){
        PersonLastLicenceNumber.get({
          id: $stateParams.id,
          licenceType: $scope.model.licenceType.code
        }).$promise
          .then(function(lastLicenceNumber){
            $scope.lastLicenceNumber = lastLicenceNumber.number;
          });
      }
    });
  }

  PersonLicenceCtrl.$inject = ['$scope', '$stateParams', 'PersonLastLicenceNumber'];

  angular.module('gva').controller('PersonLicenceCtrl', PersonLicenceCtrl);
}(angular));
