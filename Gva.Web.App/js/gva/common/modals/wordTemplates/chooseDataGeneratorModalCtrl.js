/*global angular, _*/
(function (angular, _) {
  'use strict';

  function ChooseDataGeneratorModalCtrl(
    $scope,
    $modalInstance,
    scModalParams,
    WordTemplates,
    dataGenerators) {
    $scope.form = {};
    $scope.dataGenerators = dataGenerators;
    $scope.template = scModalParams.template;

    $scope.dataGenerator = _.where(dataGenerators, {
      code: scModalParams.dataGenerator
    })[0];

    $scope.save = function save() {
      return $scope.form.wordTemplatedataGeneratorForm.$validate()
        .then(function () {
          if ($scope.form.wordTemplatedataGeneratorForm.$valid) {
            return WordTemplates.changeTemplateDataDenerator(
              {
                templateId: $scope.template.nomValueId,
                dataGenerator: $scope.dataGenerator.code
              })
              .$promise
              .then(function (result) {
                return $modalInstance.close(result);
              });
          }
        });
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };
  }

  ChooseDataGeneratorModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'scModalParams',
    'WordTemplates',
    'dataGenerators'
  ];

  ChooseDataGeneratorModalCtrl.$resolve = {
    dataGenerators: [
      'Nomenclatures',
      function (Nomenclatures) {
        return Nomenclatures.query({
          alias: 'dataGenerators'
        }).$promise;
      }
    ]
  };
  angular.module('gva').controller('ChooseDataGeneratorModalCtrl', ChooseDataGeneratorModalCtrl);
}(angular, _));
