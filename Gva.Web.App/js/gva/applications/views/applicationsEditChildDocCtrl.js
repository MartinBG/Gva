/*global angular, _*/
(function (angular, _) {
  'use strict';

  function ApplicationsEditChildDocCtrl(
    $scope,
    $state,
    $stateParams,
    Docs,
    scModal,
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
          return Docs.save($scope.docModel.doc)
            .$promise
            .then(function () {
              return $state.transitionTo('root.applications.edit.case', {
                id: $stateParams.id
              }, { reload: true });
            });
        }
      });
    };

    $scope.newCorr = function () {
      var modalInstance = scModal.open('newCorr');

      modalInstance.result.then(function (nomItem) {
        var newCorr = $scope.docModel.doc.correspondents.slice();
        newCorr.push(nomItem.nomValueId);
        $scope.docModel.doc.correspondents = newCorr;
      });

      return modalInstance.opened;
    };

    $scope.selectCorr = function () {
      var modalInstance, selectedCorrs = [];
      _.forEach($scope.docModel.doc.correspondents, function (corr) {
        return selectedCorrs.push({ nomValueId: corr });
      });

      modalInstance = scModal.open('chooseCorr', {
        selectedCorrs: selectedCorrs,
        corr: {}
      });

      modalInstance.result.then(function (nomItem) {
        var newCorr = $scope.docModel.doc.correspondents.slice();
        newCorr.push(nomItem.nomValueId);
        $scope.docModel.doc.correspondents = newCorr;
      });

      return modalInstance.opened;
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
    'Docs',
    'scModal',
    'docModel',
    'parentDoc'
  ];

  ApplicationsEditChildDocCtrl.$resolve = {
    docModel: ['$q', 'Nomenclatures',
      function ($q, Nomenclatures) {
        return $q.all({
          docFormatTypes: Nomenclatures.query({ alias: 'docFormatType' }).$promise,
          docCasePartTypes: Nomenclatures.query({ alias: 'docCasePartType' }).$promise,
          docDirections: Nomenclatures.query({ alias: 'docDirection' }).$promise
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
            correspondents: [],
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
    parentDoc: ['$stateParams', 'Docs', function ($stateParams, Docs) {
      if (!!$stateParams.parentDocId) {
        return Docs.get({ id: $stateParams.parentDocId })
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
