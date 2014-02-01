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
    $scope.saveClicked = false;
    $scope.isChild = !!$stateParams.parentDocId;

    if ($scope.isChild) {
      Doc.get({ docId: $stateParams.parentDocId }).$promise.then(function (parentDoc) {
        $scope.parentDoc = parentDoc;
        $scope.parentDocInfo =
          'Към ' + parentDoc.regUri + ' ' + parentDoc.docTypeName + ' ' + parentDoc.docSubject;
      });
    }

    $scope.doc = {
      numberOfDocuments: 1,
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

    $scope.isNumberOfDocsValid = function (value) {
      return (/^\+?(0|[1-9]\d*)$/).test(value);
    };

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

    $scope.save = function () {
      $scope.saveClicked = true;
      if ($scope.docForm.$valid) {

        $scope.doc.docTypeGroupId = $scope.docTypeGroup.nomTypeValueId;
        $scope.doc.docTypeGroupName =  $scope.docTypeGroup.name;

        $scope.doc.docTypeId = $scope.docType.nomTypeValueId;
        $scope.doc.docTypeName =  $scope.docType.name;

        if ($scope.doc.docCorrespondents && $scope.doc.docCorrespondents.length > 0) {
          $scope.doc.correspondentName = $scope.doc.docCorrespondents.map(function (correspondent) {
            return correspondent.name;
          })
          .join('; ');
        }

        Doc.save($scope.doc).$promise.then(function (savedDoc) {
          if ($scope.isChild) {
            $state.go('docs/edit/addressing', { docId: savedDoc.docId });
          }
          else {
            $state.go('docs/search');
          }
        });
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
