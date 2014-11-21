/*global angular*/
(function (angular) {
  'use strict';

  function NomsCtrl(
    $scope,
    $state,
    $stateParams,
    noms
  ) {
    $scope.noms = noms;

    $scope.editNom = function editNom(nom) {
      var state = 'root.noms.' + nom.alias + '.search';

      return $state.go(state);
    };
  }

  NomsCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'noms'
  ];

  NomsCtrl.$resolve = {
    noms: [
      'l10n',
      function resolveNoms(l10n) {
        return [
          {
            name: l10n.noms.units.name, alias: 'units'
          }
        ];
      }
    ]
  };

  angular.module('ems').controller('NomsCtrl', NomsCtrl);
}(angular));
