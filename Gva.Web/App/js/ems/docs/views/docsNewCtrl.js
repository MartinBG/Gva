/*global angular, _*/
(function (angular, _) {
  'use strict';

  function DocsNewCtrl(
    $scope,
    $state,
    $stateParams,
    Doc,
    Nomenclature,
    docData
  ) {

    $scope.parentDoc = docData.parentDoc;
    $scope.doc = docData.doc;
    $scope.docFormatTypes = docData.docFormatTypes;
    $scope.docCasePartTypes = docData.docCasePartTypes;
    $scope.docDirections = docData.docDirections;

    $scope.docFormatTypeChange = function ($index) {
      _.forOwn($scope.docFormatTypes, function (item) {
        item.isActive = false;
      });
      $scope.docFormatTypes[$index].isActive = true;
      $scope.doc.docFormatTypeId = $scope.docFormatTypes[$index].docFormatTypeId;
      $scope.doc.docFormatTypeName = $scope.docFormatTypes[$index].name;
    };

    $scope.docCasePartTypeChange = function ($index) {
      _.forOwn($scope.docCasePartTypes, function (item) {
        item.isActive = false;
      });

      $scope.docCasePartTypes[$index].isActive = true;
      $scope.doc.docCasePartTypeId = $scope.docCasePartTypes[$index].docCasePartTypeId;
      $scope.doc.docCasePartTypeName = $scope.docCasePartTypes[$index].name;
    };

    $scope.docDirectionChange = function ($index) {
      _.forOwn($scope.docDirections, function (item) {
        item.isActive = false;
      });
      $scope.docDirections[$index].isActive = true;
      $scope.doc.docDirectionId = $scope.docDirections[$index].docDirectionId;
      $scope.doc.docDirectionName = $scope.docDirections[$index].name;
    };

    $scope.save = function (mode) {
      $scope.validationMode = mode;
      $scope.docForm.$validate()
      .then(function () {
        if ($scope.docForm.$valid) {
          $scope.doc.docTypeGroupId = $scope.docTypeGroup.nomValueId;
          $scope.doc.docTypeGroupName =  $scope.docTypeGroup.name;

          $scope.doc.docTypeId = $scope.docType.nomValueId;
          $scope.doc.docTypeName =  $scope.docType.name;

          if ($scope.doc.docCorrespondents && $scope.doc.docCorrespondents.length > 0) {
            $scope.doc.correspondentName =
              $scope.doc.docCorrespondents.map(function (correspondent) {
                return correspondent.name;
              })
            .join('; ');
          }

          if (mode === 'register') {
            Doc.registerNew($scope.doc).$promise.then(function (result) {
              $state.go('root.docs.search', { docIds: result.docIds.join(',')});
            });
          }
          else if (mode === 'create') {
            Doc.createNew($scope.doc).$promise.then(function (result) {
              $state.go('root.docs.edit.addressing', { docId: result.docId });
            });
          }
        }
      });
    };

    $scope.isNumberOfDocsValid = function () {
      if ($scope.validationMode === 'create' && $scope.doc.numberOfDocs) {
        return $scope.doc.numberOfDocs === 1;
      }
      else {
        return true;
      }
    };

    $scope.cancel = function () {
      if (!!$scope.parentDoc) {
        $state.go('root.docs.edit.addressing', { docId: $stateParams.parentDocId });
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
    'docData'
  ];

  DocsNewCtrl.$resolve = {
    docData: ['$q', '$stateParams', 'Nomenclature', 'Doc',
      function ($q, $stateParams, Nomenclature, Doc) {
        return $q.all({
          parentDoc: !!$stateParams.parentDocId ?
            Doc.get({ docId: $stateParams.parentDocId }).$promise : null,
          docFormatTypes: Nomenclature.query({ alias: 'docFormatType' }).$promise,
          docCasePartTypes: Nomenclature.query({ alias: 'docCasePartType' }).$promise,
          docDirections: Nomenclature.query({ alias: 'docDirection' }).$promise
        }).then(function (res) {
          var doc = {
            numberOfDocs: 1,
            parentDocId: $stateParams.parentDocId,
            docFormatTypeId: _(res.docFormatTypes).first().docFormatTypeId,
            docFormatTypeName: _(res.docFormatTypes).first().name,
            docCasePartTypeId: _(res.docCasePartTypes).first().docCasePartTypeId,
            docCasePartTypeName: _(res.docCasePartTypes).first().name,
            docDirectionId: _(res.docDirections).first().docDirectionId,
            docDirectionName: _(res.docDirections).first().name
          };

          return {
            parentDoc: res.parentDoc,
            doc: doc,
            docFormatTypes: res.docFormatTypes,
            docCasePartTypes: res.docCasePartTypes,
            docDirections: res.docDirections
          };
        });
      }
    ]
  };

  angular.module('ems').controller('DocsNewCtrl', DocsNewCtrl);
}(angular, _));
