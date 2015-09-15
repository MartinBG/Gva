/*global angular, _*/
(function (angular, _) {
  'use strict';

  function PersonInfoEditCtrl(
    $scope,
    $state,
    $stateParams,
    PersonsInfo,
    personInfo,
    inspectorExaminerNomValueIds
  ) {
    var originalPersonInfo = _.cloneDeep(personInfo);
    $scope.inspectorCaseTypeId = inspectorExaminerNomValueIds.inspectorCaseTypeId;
    $scope.staffExaminerCaseTypeId = inspectorExaminerNomValueIds.staffExaminerCaseTypeId;
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
        $scope.inspectorCaseTypeId);
    };

    $scope.showExaminerData = function () {
      return _.contains(
        $scope.personInfo.personData.caseTypes,
        $scope.staffExaminerCaseTypeId);
    };
  }

  PersonInfoEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonsInfo',
    'personInfo',
    'inspectorExaminerNomValueIds'
  ];

  PersonInfoEditCtrl.$resolve = {
    personInfo: [
      '$stateParams',
      'PersonsInfo',
      function ($stateParams, PersonsInfo) {
        return PersonsInfo.get({ id: $stateParams.id }).$promise;
      }
    ],
    inspectorExaminerNomValueIds: [
      'Nomenclatures',
      function (Nomenclatures) {
        return Nomenclatures.query({alias: 'personCaseTypes'})
          .$promise
          .then(function(caseTypes){
            var inspectorCaseTypeId =
              _.where(caseTypes, {alias: 'inspector'})[0].nomValueId;
            var staffExaminerCaseTypeId =
              _.where(caseTypes, {alias: 'staffExaminer'})[0].nomValueId;
            return {
              inspectorCaseTypeId: inspectorCaseTypeId,
              staffExaminerCaseTypeId: staffExaminerCaseTypeId
            };
          });
      }
    ]
  };

  angular.module('gva').controller('PersonInfoEditCtrl', PersonInfoEditCtrl);
}(angular, _));
