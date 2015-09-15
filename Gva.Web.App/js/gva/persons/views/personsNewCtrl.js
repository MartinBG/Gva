/*global angular, _*/
(function (angular, _) {
  'use strict';

  function PersonsNewCtrl(
    $scope, 
    $state, 
    Persons,
    person,
    inspectorExaminerNomValueIds) {
    $scope.newPerson = person;
    $scope.inspectorCaseTypeId = inspectorExaminerNomValueIds.inspectorCaseTypeId;
    $scope.staffExaminerCaseTypeId = inspectorExaminerNomValueIds.staffExaminerCaseTypeId;

    $scope.save = function () {
      return $scope.newPersonForm.$validate()
      .then(function () {
        if ($scope.newPersonForm.$valid) {
          return Persons.save($scope.newPerson).$promise
            .then(function (result) {
              return $state.go('root.persons.view.licences.search', { id: result.id });
            });
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('root.persons.search');
    };

    $scope.showInspData = function () {
      return _.contains(
        $scope.newPerson.personData.caseTypes,
        $scope.inspectorCaseTypeId);
    };

    $scope.showExaminerData = function () {
      return _.contains(
        $scope.newPerson.personData.caseTypes,
        $scope.staffExaminerCaseTypeId);
    };
  }

  PersonsNewCtrl.$inject = [
    '$scope',
    '$state',
    'Persons',
    'person',
    'inspectorExaminerNomValueIds'
  ];

  PersonsNewCtrl.$resolve = {
    person: [
      'Persons',
      function (Persons) {
        return Persons.newPerson({extendedVersion : false}).$promise;
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

  angular.module('gva').controller('PersonsNewCtrl', PersonsNewCtrl);
}(angular, _));
