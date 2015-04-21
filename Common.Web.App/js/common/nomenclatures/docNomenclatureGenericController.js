/*global angular, _*/
(function (angular, _) {
  'use strict';

  function DocNomenclatureGenericController($scope, $state, $stateParams, nomenclaturesModel, l10n) {
    $scope.model = nomenclaturesModel;
    var name = $stateParams.category;

    $scope.save = function (data, id) {
      //$scope.user not updated yet
      //angular.extend(data, { id: id });
      //return $http.post('/saveUser', data);
      //return true;
    };


    //$scope.cancel = function (id) {
    //  if (!id) {
    //    var lastIndex = $scope.users.length - 1;
    //    $scope.users.splice(lastIndex, 1);
    //  }
    //};

    $scope.add = function () {
      $scope.inserted = {
        id: null,
        name: '',        
        isActive: true
      };

      $scope.users.push($scope.inserted);
    };

    $scope.cancel = function (nomenclature) {
      if (!nomenclature.id) {
        _.pull($scope.model, nomenclature);
      }
    };
  }

  DocNomenclatureGenericController.$inject = ['$scope', '$state', '$stateParams', 'nomenclaturesModel', 'l10n'];

  DocNomenclatureGenericController.$resolve = {
    nomenclaturesModel: ['$stateParams', '$resource',
      function ($stateParams, $resource) {

        return $resource('api/nomenclatures/' + $stateParams.category)
          .query().$promise;
      }
    ]
  };

  angular.module('common').controller('DocNomenclatureGenericController', DocNomenclatureGenericController);
}(angular, _));
