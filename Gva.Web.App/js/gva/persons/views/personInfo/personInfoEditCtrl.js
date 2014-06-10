/*global angular, _*/
(function (angular, _) {
  'use strict';

  function PersonInfoEditCtrl(
    $scope,
    $state,
    $stateParams,
    PersonInfo,
    personInfo
  ) {
    var originalPersonInfo = _.cloneDeep(personInfo);

    $scope.personInfo = personInfo;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.personInfo = _.cloneDeep(originalPersonInfo);
    };

    $scope.save = function () {
      return $scope.editPersonForm.$validate()
        .then(function () {
          if ($scope.editPersonForm.$valid) {
            return PersonInfo
              .save({ id: $stateParams.id }, $scope.personInfo)
              .$promise
              .then(function () {
                return $state.transitionTo('root.persons.view', $stateParams, { reload: true });
              });
          }
        });
    };

    $scope.showInspData = function () {
      return _.some($scope.personInfo.personData.part.caseTypes, function (caseType) {
        return caseType.alias === 'inspector';
      });
    };
  }

  PersonInfoEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonInfo',
    'personInfo'
  ];

  PersonInfoEditCtrl.$resolve = {
    personInfo: [
      '$stateParams',
      'PersonInfo',
      function ($stateParams, PersonInfo) {
        return PersonInfo.get({ id: $stateParams.id }).$promise;
      }
    ]
  };

  angular.module('gva').controller('PersonInfoEditCtrl', PersonInfoEditCtrl);
}(angular, _));
