/*global angular, _*/
(function (angular, _) {
  'use strict';

  function ApplicationsEditChildDocCtrl(
    $scope,
    $state,
    $stateParams,
    Doc,
    Nomenclature,
    docModel,
    parentDoc
  ) {
    if (parentDoc.length > 0) {
      docModel.parentDoc = parentDoc.pop();
    }

    $scope.docModel = docModel;

    $scope.setParentDocId = function () {
      if ($scope.docModel.parentDoc) {
        $scope.docModel.doc.parentDocId = $scope.docModel.parentDoc.docId;
      }
    };

    $scope.register = function () {
      $scope.setParentDocId();
      $scope.docModel.doc.register = true;

      return $scope.docForm.$validate().then(function () {
        if ($scope.docForm.$valid) {
          return Doc.save($scope.docModel.doc)
            .$promise
            .then(function () {
              return $state.transitionTo('root.applications.edit.case', {
                id: $stateParams.id
              }, { reload: true });
            });
        }
      });
    };

    $scope.cancel = function () {
      if (!!$scope.docModel.parentDoc) {
        return $state.go('^');
      }
    };
  }

  ApplicationsEditChildDocCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Doc',
    'Nomenclature',
    'docModel',
    'parentDoc'
  ];

  ApplicationsEditChildDocCtrl.$resolve = {
    docModel: ['$q', 'Nomenclature',
      function ($q, Nomenclature) {
        return $q.all({
          docFormatTypes: Nomenclature.query({ alias: 'docFormatType' }).$promise,
          docCasePartTypes: Nomenclature.query({ alias: 'docCasePartType' }).$promise,
          docDirections: Nomenclature.query({ alias: 'docDirection' }).$promise
        }).then(function (res) {
          var doc = {
            parentDocId: null,
            docFormatTypeId: _(res.docFormatTypes).first().nomValueId,
            docFormatTypeName: _(res.docFormatTypes).first().name,
            docCasePartTypeId: _(res.docCasePartTypes).first().nomValueId,
            docCasePartTypeName: _(res.docCasePartTypes).first().name,
            docDirectionId: _(res.docDirections).first().nomValueId,
            docDirectionName: _(res.docDirections).first().name,
            docTypeGroupId: undefined,
            docTypeId: undefined,
            correspondents: undefined,
            register: false
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
    parentDoc: ['$stateParams', 'Doc', function ($stateParams, Doc) {
      if (!!$stateParams.parentDocId) {
        return Doc.get({ id: $stateParams.parentDocId })
          .$promise
          .then(function (result) {
            return [{
              docId: result.docId,
              regUri: result.regUri,
              docTypeName: result.docTypeName,
              docSubject: result.docSubject
            }];
          });
      }
      else {
        return [];
      }
    }]
  };

  angular.module('gva').controller('ApplicationsEditChildDocCtrl', ApplicationsEditChildDocCtrl);
}(angular, _));
