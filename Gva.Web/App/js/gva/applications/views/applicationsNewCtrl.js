/*global angular, _*/
(function (angular) {
  'use strict';

  function ApplicationsNewCtrl(
    $q,
    $scope,
    $state,
    Application,
    appModel,
    selectedPerson
    ) {

    if (selectedPerson.length > 0) {
      appModel.person = {
        id: selectedPerson.pop()
      };
    }

    $scope.appModel = appModel;

    $scope.newPerson = function () {
      return $state.go('root.applications.new.personNew');
    };

    $scope.selectPerson = function () {
      return $state.go('root.applications.new.personSelect');
    };

    $scope.cancel = function () {
      return $state.go('root.applications.search');
    };

    $scope.save = function () {
      $scope.appForm.$validate()
      .then(function () {
        if ($scope.appForm.$valid) {

          var newApplication = {
            lotId: $scope.appModel.person.id,
            doc: {
              docFormatTypeId: $scope.appModel.doc.docFormatTypeId,
              docFormatTypeName: $scope.appModel.doc.docFormatTypeName,
              docCasePartTypeId: $scope.appModel.doc.docCasePartTypeId,
              docCasePartTypeName: $scope.appModel.doc.docCasePartTypeName,
              docDirectionId: $scope.appModel.doc.docDirectionId,
              docDirectionName: $scope.appModel.doc.docDirectionName,
              docTypeGroupId: $scope.appModel.docTypeGroupId,
              docTypeGroupName: $scope.appModel.docTypeGroup.name,
              docTypeId: $scope.appModel.docTypeId,
              docTypeName: $scope.appModel.docType.name,
              docSubject: $scope.appModel.doc.docSubject
            },
            appPart: $scope.appModel.appPart,
            appFile: $scope.appModel.appFile
          };

          Application.createNew(newApplication).$promise.then(function (result) {
            return $state.go('root.applications.edit.case', { id: result.applicationId });
          });
        }
      });
    };
  }

  ApplicationsNewCtrl.$inject = [
    '$q',
    '$scope',
    '$state',
    'Application',
    'appModel',
    'selectedPerson'
  ];

  ApplicationsNewCtrl.$resolve = {
    appModel: ['$q', 'Nomenclature',
      function ($q, Nomenclature) {
        return $q.all({
          docFormatTypes: Nomenclature.query({ alias: 'docFormatType' }).$promise,
          docCasePartTypes: Nomenclature.query({ alias: 'docCasePartType' }).$promise,
          docDirections: Nomenclature.query({ alias: 'docDirection' }).$promise
        }).then(function (res) {

          var doc = {
            docFormatTypeId: _(res.docFormatTypes).first().nomValueId,
            docFormatTypeName: _(res.docFormatTypes).first().name,
            docCasePartTypeId: _(res.docCasePartTypes).first().nomValueId,
            docCasePartTypeName: _(res.docCasePartTypes).first().name,
            docDirectionId: _(res.docDirections).first().nomValueId,
            docDirectionName: _(res.docDirections).first().name
          };

          return {
            doc: doc,
            docFormatTypes: res.docFormatTypes,
            docCasePartTypes: res.docCasePartTypes,
            docDirections: res.docDirections
          };
        });
      }
    ],
    selectedPerson: function () {
      return [];
    }
  };

  angular.module('gva').controller('ApplicationsNewCtrl', ApplicationsNewCtrl);
}(angular, _));
