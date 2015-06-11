/*global angular, _*/
(function (angular, _) {
  'use strict';

  function TemplatesEditCtrl(
    $scope,
    $state,
    $stateParams,
    WordTemplates,
    dataGenerators,
    template,
    scMessage
  ) {
    var originalTemplate = _.cloneDeep(template);

    $scope.template = template;
    $scope.editMode = null;
    $scope.templateId = $stateParams.id;
    $scope.dataGenerators = dataGenerators;
    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.template = _.cloneDeep(originalTemplate);
    };

    $scope.save = function () {
      return $scope.templatesEditForm.$validate()
        .then(function () {
          if ($scope.templatesEditForm.$valid) {
            return WordTemplates
              .save({ templateId: $scope.templateId }, $scope.template)
              .$promise
              .then(function () {
                return $state.go('root.templates.search');
              });
          }
        });
    };

    $scope.deleteTemplate = function () {
      return scMessage('common.messages.confirmDelete')
      .then(function (result) {
        if (result === 'OK') {
          return WordTemplates.remove({
            templateId: $scope.templateId
          }).$promise.then(function () {
            return $state.go('root.templates.search');
          });
        }
      });
    };
  }

  TemplatesEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'WordTemplates',
    'dataGenerators',
    'template',
    'scMessage'
  ];

  TemplatesEditCtrl.$resolve = {
    template: [
      '$stateParams',
      'WordTemplates',
      function ($stateParams, WordTemplates) {
        return WordTemplates.get({
          templateId: $stateParams.id
        }).$promise;
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

  angular.module('gva').controller('TemplatesEditCtrl', TemplatesEditCtrl);
}(angular, _));
