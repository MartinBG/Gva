/*global angular*/
(function (angular) {
  'use strict';

  function UnitsCtrl($scope, $state, $stateParams,
    unitsModel,
    scModal,
    scMessage,
    UnitsResource,
    UnitUsersResource,
    l10n,
    $resource) {


  }

  UnitsCtrl.$inject = ['$scope', '$state', '$stateParams',
    'unitsModel', 'scModal', 'scMessage',
    'UnitsResource', 'UnitUsersResource', 'l10n', '$resource'];

  UnitsCtrl.$resolve = {
    unitsModel: ['$stateParams', 'UnitsResource',
      function ($stateParams, UnitsResource) {
        return UnitsResource.query($stateParams).$promise.then(function (unitsModel) {

          return unitsModel;
        });
      }
    ]
  };

  angular.module('common').controller('UnitsCtrl', UnitsCtrl);
}(angular));
