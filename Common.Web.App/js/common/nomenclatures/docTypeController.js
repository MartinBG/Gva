/*global angular, _*/
(function (angular, _) {
  'use strict';

  function DocTypeController($scope, $state,
    $stateParams, nomenclaturesModel, l10n, $resource) {

    nomenclaturesModel.registers.splice(0, 0, { id: null, name: 'не е зададена стойност' });

    $scope.model = nomenclaturesModel.docTypes;
    $scope.docTypeGroups = nomenclaturesModel.docTypeGroups;
    $scope.registers = nomenclaturesModel.registers;

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
        isActive: true,
        primaryRegisterIndexId: nomenclaturesModel.registers[0].id,
        docTypeGroupId: nomenclaturesModel.docTypeGroups[0].id
      });
    };

    $scope.cancel = function (nomenclature) {
      if (!nomenclature.id) {
        _.pull($scope.model, nomenclature);
      }
    };

    $scope.displayDocTypeGroupName = function (id) {
      if (!id) {
        return '';
      }

      return _.find(nomenclaturesModel.docTypeGroups, function (item) {
        return item.id === id;
      }).name;
    };

    $scope.displayRegisterName = function (id) {
      if (!id) {
        return '';
      }

      return _.find(nomenclaturesModel.registers, function (item) {
        return item.id === id;
      }).name;
    };
  }

  DocTypeController.$inject = ['$scope',
    '$state', '$stateParams', 'nomenclaturesModel', 'l10n', '$resource'];

  DocTypeController.$resolve = {
    nomenclaturesModel: ['$q', '$stateParams', '$resource',
      function ($q, $stateParams, $resource) {

        return $q.all({
          docTypes: $resource('api/nomenclaturesManagement/docTypes').query().$promise,
          docTypeGroups: $resource('api/nomenclaturesManagement/docTypeGroups').query()
            .$promise,
          registers: $resource('api/register').query().$promise
        })
        .then(function (result) {

          return {
            docTypes: result.docTypes,
            docTypeGroups: result.docTypeGroups,
            registers: result.registers
          };
        });
      }
    ]
  };

  angular.module('common').controller('DocTypeController', DocTypeController);
}(angular, _));
