/*global angular*/
(function (angular) {
  'use strict';

  function PersonLicenceCtrl(
    $scope,
    $stateParams,
    PersonsLastLicenceNumber,
    scFormParams
  ) {
    $scope.isNew = scFormParams.isNew;
    $scope.$watch('model.licenceType', function(){
      if(!$scope.lastLicenceNumber && !!$scope.model.licenceType){
        PersonsLastLicenceNumber.get({
          id: $stateParams.id,
          licenceType: $scope.model.licenceType.code
        }).$promise
          .then(function(lastLicenceNumber){
            $scope.lastLicenceNumber = lastLicenceNumber.number;
          });
      }
    });
  }

  PersonLicenceCtrl.$inject = [
    '$scope',
    '$stateParams',
    'PersonsLastLicenceNumber',
    'scFormParams'
  ];

  angular.module('gva').controller('PersonLicenceCtrl', PersonLicenceCtrl);
}(angular));
