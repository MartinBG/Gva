/*global angular*/
(function (angular) {
  'use strict';

  function PersonDocumentEducationCtrl($scope, $stateParams, PersonDocumentEducation) {
    $scope.isPositive = function (value) {
      return (value >= 0 ? true: false);
    };

    var checkValue = function(param, value){
      if (!value || !$stateParams.id) {
        return true;
      }

      return PersonDocumentEducation.query({
        id: $stateParams.id,
        number: param === 'number'? value : undefined
      }).$promise
        .then(function (documentEducations) {
          return documentEducations.length === 0 ||
            (documentEducations.length === 1  &&
            documentEducations[0].partIndex === parseInt($stateParams.ind, 10));
        });
    };

    $scope.isUniqueDocNumber = function (value) {
      return checkValue('number', value);
    };
  }

  PersonDocumentEducationCtrl.$inject = ['$scope', '$stateParams', 'PersonDocumentEducation'];

  angular.module('gva').controller('PersonDocumentEducationCtrl', PersonDocumentEducationCtrl);
}(angular));
