/*global angular*/
(function (angular) {
  'use strict';

  function TemplatesNewCtrl(
    $scope,
    $state,
    WordTemplates,
    dataGenerators,
    template
  ) {
    $scope.template = template;
    $scope.dataGenerators = dataGenerators;
    $scope.save = function () {
      return $scope.newTemplatesForm.$validate()
        .then(function () {
          if ($scope.newTemplatesForm.$valid) {
            return WordTemplates
              .createNew($scope.template)
              .$promise
              .then(function () {
                return $state.go('root.templates.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.templates.search');
    };
  }

  TemplatesNewCtrl.$inject = [
    '$scope',
    '$state',
    'WordTemplates',
    'dataGenerators',
    'template'
  ];

  TemplatesNewCtrl.$resolve = {
    template: [
      'WordTemplates',
      function (WordTemplates) {
        return WordTemplates.newTemplate().$promise;
      }
    ],
    dataGenerators: [
      'Nomenclatures',
      function (Nomenclatures) {
        return Nomenclatures.query({
          alias: 'dataGenerators'
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('TemplatesNewCtrl', TemplatesNewCtrl);
}(angular));
