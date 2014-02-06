/*global angular, _*/
(function (angular, _) {
  'use strict';

  function DocsNewCtrl(
    $q,
    $scope,
    $state,
    $stateParams,
    Doc,
    Nomenclature
  ) {
    $scope.isChild = !!$stateParams.parentDocId;

    if ($scope.isChild) {
      Doc.get({ docId: $stateParams.parentDocId }).$promise.then(function (parentDoc) {
        $scope.parentDoc = parentDoc;
        $scope.parentDocInfo =
          'Към ' + parentDoc.regUri + ' ' + parentDoc.docTypeName + ' ' + parentDoc.docSubject;
      });
    }

    $scope.doc = {
      numberOfDocs: 1,
      parentDocId: $stateParams.parentDocId
    };

    Nomenclature.query({ alias: 'docFormatTypes' }).$promise.then(function (result) {
      $scope.docFormatTypes = result;
      $scope.doc.docFormatTypeId = _($scope.docFormatTypes).first().docFormatTypeId;
      $scope.doc.docFormatTypeName = _($scope.docFormatTypes).first().name;
    });

    Nomenclature.query({ alias: 'docCasePartTypes' }).$promise.then(function (result) {
      $scope.docCasePartTypes = result;
      $scope.doc.docCasePartTypeId = _($scope.docCasePartTypes).first().docCasePartTypeId;
      $scope.doc.docCasePartTypeName = _($scope.docCasePartTypes).first().name;
    });

    Nomenclature.query({ alias: 'docDirections' }).$promise.then(function (result) {
      $scope.docDirections = result;
      $scope.doc.docDirectionId = _($scope.docDirections).first().docDirectionId;
      $scope.doc.docDirectionName = _($scope.docDirections).first().name;
    });

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
          $scope.doc.docTypeGroupId = $scope.docTypeGroup.nomTypeValueId;
          $scope.doc.docTypeGroupName =  $scope.docTypeGroup.name;

          $scope.doc.docTypeId = $scope.docType.nomTypeValueId;
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
              $state.go('docs/search', { docIds: result.docIds.join(',')});
            });
          }
          else if (mode === 'create') {
            Doc.createNew($scope.doc).$promise.then(function (result) {
              $state.go('docs/edit/addressing', { docId: result.docId });
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
      if ($scope.isChild) {
        $state.go('docs/edit/addressing', { docId: $stateParams.parentDocId });
      }
      else {
        $state.go('docs/search');
      }
    };
  }

  DocsNewCtrl.$inject = [
    '$q',
    '$scope',
    '$state',
    '$stateParams',
    'Doc',
    'Nomenclature'
  ];

  angular.module('ems').controller('DocsNewCtrl', DocsNewCtrl);
}(angular, _));
