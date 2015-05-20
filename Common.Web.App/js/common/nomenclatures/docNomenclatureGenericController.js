/*global angular, _*/
(function (angular, _) {
  'use strict';

  function DocNomenclatureGenericController($scope, $state,
    $stateParams, nomenclaturesModel, l10n, $resource) {

    $scope.model = nomenclaturesModel;

    $scope.save = function (data, nomenclature) {

      data.id = nomenclature.id;
      $resource('api/nomenclaturesManagement/' + $stateParams.category + '/:id', { id: '@id' })
        .save(data)
        .$promise.then(function () {
          return $state.reload();
        });
    };

    $scope.add = function () {
      $scope.model.push({
        id: null,
        name: '',
        isActive: true
      });
    };

    $scope.cancel = function (nomenclature) {
      if (!nomenclature.id) {
        _.pull($scope.model, nomenclature);
      }
    };
  }

  DocNomenclatureGenericController.$inject = ['$scope',
    '$state', '$stateParams', 'nomenclaturesModel', 'l10n', '$resource'];

  DocNomenclatureGenericController.$resolve = {
    nomenclaturesModel: ['$stateParams', '$resource',
      function ($stateParams, $resource) {

        return $resource('api/nomenclaturesManagement/' + $stateParams.category)
          .query().$promise.then(function (result) {

            return result;
          });
      }
    ]
  };

  angular.module('common').controller('DocNomenclatureGenericController',
    DocNomenclatureGenericController);
}(angular, _));
