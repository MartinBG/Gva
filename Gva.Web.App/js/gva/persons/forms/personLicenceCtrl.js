/*global angular*/
(function (angular) {
  'use strict';

  function PersonLicenceCtrl(
    $scope,
    PersonsLastLicenceNumber,
    scFormParams
  ) {
    $scope.isNew = scFormParams.isNew;

    $scope.$watch('model.licenceType', function(){
      if(!$scope.lastLicenceNumber && !!$scope.model.licenceType){
        PersonsLastLicenceNumber.get({
          id: scFormParams.lotId,
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
    'PersonsLastLicenceNumber',
    'scFormParams'
  ];

  angular.module('gva').controller('PersonLicenceCtrl', PersonLicenceCtrl);
}(angular));
