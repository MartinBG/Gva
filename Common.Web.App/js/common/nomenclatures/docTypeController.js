/*global angular*/
(function (angular) {
  'use strict';

  function DocTypeController($scope, $state, $stateParams, l10n) {


  }

  DocTypeController.$inject = ['$scope', '$state', '$stateParams', 'l10n'];

  DocTypeController.$resolve = {
    unitsModel: ['$stateParams', 'UnitsResource',
      function ($stateParams, UnitsResource) {
        return UnitsResource.query($stateParams).$promise.then(function (unitsModel) {

          return unitsModel;
        });
      }
    ]
  };

  angular.module('common').controller('DocTypeController', DocTypeController);
}(angular));
