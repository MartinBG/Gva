/*global angular, _*/
(function (angular, _) {
  'use strict';

  function PersonInfoEditCtrl(
    $scope,
    $state,
    $stateParams,
    PersonsInfo,
    personInfo
  ) {
    var originalPersonInfo = _.cloneDeep(personInfo);

    $scope.personInfo = personInfo;
    $scope.editMode = null;
    $scope.personId = $stateParams.id;

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
            return PersonsInfo
              .save({ id: $stateParams.id }, $scope.personInfo)
              .$promise
              .then(function () {
                return $state.transitionTo('root.persons.view', $stateParams, { reload: true });
              });
          }
        });
    };

    $scope.showInspData = function () {
      return _.some($scope.personInfo.personData.caseTypes, function (caseType) {
        return caseType.alias === 'inspector';
      });
    };
  }

  PersonInfoEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonsInfo',
    'personInfo'
  ];

  PersonInfoEditCtrl.$resolve = {
    personInfo: [
      '$stateParams',
      'PersonsInfo',
      function ($stateParams, PersonsInfo) {
        return PersonsInfo.get({ id: $stateParams.id }).$promise;
      }
    ]
  };

  angular.module('gva').controller('PersonInfoEditCtrl', PersonInfoEditCtrl);
}(angular, _));
