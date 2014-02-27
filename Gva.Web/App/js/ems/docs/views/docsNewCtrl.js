/*global angular, _*/
(function (angular, _) {
  'use strict';

  function DocsNewCtrl(
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

    $scope.docFormatTypeChange = function ($index) {
      _.forOwn($scope.docModel.docFormatTypes, function (item) {
        item.isActive = false;
      });
      $scope.docModel.docFormatTypes[$index].isActive = true;
      $scope.docModel.doc.docFormatTypeId =
        $scope.docModel.docFormatTypes[$index].docFormatTypeId;
      $scope.docModel.doc.docFormatTypeName =
        $scope.docModel.docFormatTypes[$index].name;
    };

    $scope.docCasePartTypeChange = function ($index) {
      _.forOwn($scope.docModel.docCasePartTypes, function (item) {
        item.isActive = false;
      });

      $scope.docModel.docCasePartTypes[$index].isActive = true;
      $scope.docModel.doc.docCasePartTypeId =
        $scope.docModel.docCasePartTypes[$index].docCasePartTypeId;
      $scope.docModel.doc.docCasePartTypeName =
        $scope.docModel.docCasePartTypes[$index].name;
    };

    $scope.docDirectionChange = function ($index) {
      _.forOwn($scope.docModel.docDirections, function (item) {
        item.isActive = false;
      });
      $scope.docModel.docDirections[$index].isActive = true;
      $scope.docModel.doc.docDirectionId =
        $scope.docModel.docDirections[$index].docDirectionId;
      $scope.docModel.doc.docDirectionName =
        $scope.docModel.docDirections[$index].name;
    };

    $scope.clearCase = function () {
      $scope.docModel.parentDoc = null;
    };

    $scope.selectCase = function () {
      return $state.go('root.docs.new.caseSelect');
    };

    $scope.save = function (mode) {
      $scope.docForm.$validate()
      .then(function () {
        if ($scope.docForm.$valid) {
          if ($scope.docModel.parentDoc) {
            $scope.docModel.doc.parentDocId = $scope.docModel.parentDoc.docId;
          }
          $scope.docModel.doc.docTypeGroupId = $scope.docModel.docTypeGroup.nomValueId;
          $scope.docModel.doc.docTypeGroupName =  $scope.docModel.docTypeGroup.name;

          $scope.docModel.doc.docTypeId = $scope.docModel.docType.nomValueId;
          $scope.docModel.doc.docTypeName =  $scope.docModel.docType.name;

          if ($scope.docModel.doc.docCorrespondents &&
              $scope.docModel.doc.docCorrespondents.length > 0) {
            $scope.docModel.doc.correspondentName =
              $scope.docModel.doc.docCorrespondents.map(function (correspondent) {
                return correspondent.name;
              })
            .join('; ');
          }

          if (mode === 'register') {
            Doc.registerNew($scope.docModel.doc).$promise.then(function (result) {
              $state.go('root.docs.search', { docIds: result.docIds.join(',')});
            });
          }
          else if (mode === 'create') {
            Doc.createNew($scope.doc).$promise.then(function (result) {
              return $state.go('root.docs.edit.view', { docId: result.docId });
            });
          }
        }
      });
    };

    $scope.cancel = function () {
      if (!!$scope.parentDoc) {
        return $state.go('root.docs.edit.view', { docId: $stateParams.parentDocId });
      }
      else {
        $state.go('root.docs.search');
      }
    };
  }

  DocsNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Doc',
    'Nomenclature',
    'docModel',
    'parentDoc'
  ];

  DocsNewCtrl.$resolve = {
    docModel: ['$q', 'Nomenclature',
      function ($q, Nomenclature) {
        return $q.all({
          docFormatTypes: Nomenclature.query({ alias: 'docFormatType' }).$promise,
          docCasePartTypes: Nomenclature.query({ alias: 'docCasePartType' }).$promise,
          docDirections: Nomenclature.query({ alias: 'docDirection' }).$promise
        }).then(function (res) {
          var doc = {
            parentDocId: null,
            docFormatTypeId: _(res.docFormatTypes).first().docFormatTypeId,
            docFormatTypeName: _(res.docFormatTypes).first().name,
            docCasePartTypeId: _(res.docCasePartTypes).first().docCasePartTypeId,
            docCasePartTypeName: _(res.docCasePartTypes).first().name,
            docDirectionId: _(res.docDirections).first().docDirectionId,
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
    parentDoc: ['$stateParams', 'Doc', function ($stateParams, Doc) {
      if (!!$stateParams.parentDocId) {
        return Doc.get({ docId: $stateParams.parentDocId }).$promise.then(function (result) {
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

  angular.module('ems').controller('DocsNewCtrl', DocsNewCtrl);
}(angular, _));
