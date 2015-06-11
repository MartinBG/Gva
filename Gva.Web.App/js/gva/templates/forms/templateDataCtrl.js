/*global angular*/

(function (angular) {
  'use strict';

  function TemplateDataCtrl($scope, scFormParams, WordTemplates) {
    $scope.dataGenerators = scFormParams.dataGenerators;
    $scope.isNew = scFormParams.isNew;
    $scope.isValidName = function () {
      if (!$scope.model.name) {
        return true;
      }
      if (!/^[A-Za-z0-9_-]*$/.test($scope.model.name)) {
        return false;
      }

      return true;
    };

    $scope.isUniqueName = function () {
      if($scope.isNew && $scope.model.name) {
        return WordTemplates.isUniqueTemplateName({
          templateName: $scope.model.name
        })
          .$promise
          .then(function (result) {
            return result.isUnique;
          });
      } else {
        return true;
      }
    };
  }

  TemplateDataCtrl.$inject = ['$scope', 'scFormParams', 'WordTemplates'];

  angular.module('gva').controller('TemplateDataCtrl', TemplateDataCtrl);
}(angular));
