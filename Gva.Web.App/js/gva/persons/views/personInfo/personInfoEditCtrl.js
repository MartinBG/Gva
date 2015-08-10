/*global angular, _*/
(function (angular, _) {
  'use strict';

  function PersonInfoEditCtrl(
    $scope,
    $state,
    $stateParams,
    PersonsInfo,
    personInfo,
    personCaseTypes
  ) {
    var originalPersonInfo = _.cloneDeep(personInfo);
    $scope.inspectorCaseType = _.where(personCaseTypes, { alias: 'inspector' })[0];
    $scope.examinerCaseType = _.where(personCaseTypes, { alias: 'staffExaminer' })[0];
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
      return _.contains(
        $scope.personInfo.personData.caseTypes,
        $scope.inspectorCaseType.nomValueId);
    };

    $scope.showExaminerData = function () {
      return _.contains(
        $scope.personInfo.personData.caseTypes,
        $scope.examinerCaseType.nomValueId);
    };
  }

  PersonInfoEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonsInfo',
    'personInfo',
    'personCaseTypes'
  ];

  PersonInfoEditCtrl.$resolve = {
    personInfo: [
      '$stateParams',
      'PersonsInfo',
      function ($stateParams, PersonsInfo) {
        return PersonsInfo.get({ id: $stateParams.id }).$promise;
      }
    ],
    personCaseTypes: [
      'Nomenclatures',
      function (Nomenclatures) {
        return Nomenclatures.query({ alias: 'personCaseTypes' }).$promise;
      }
    ]
  };

  angular.module('gva').controller('PersonInfoEditCtrl', PersonInfoEditCtrl);
}(angular, _));
