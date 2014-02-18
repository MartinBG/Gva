/*global angular*/
(function (angular) {
  'use strict';

  function DocsEditCtrl(
    $q,
    $scope,
    $filter,
    $state,
    $stateParams,
    Doc,
    doc
  ) {
    $scope.doc = doc;

    $scope.inEditMode = false;

    $scope.markAsRead = function () {
      $scope.doc.isRead = true;
    };

    $scope.markAsUnread = function () {
      //todo call to backend and set DocUser.HasRead flags
      $scope.doc.isRead = false;
    };

    $scope.enterEditMode = function () {
      $scope.inEditMode = true;
    };

    $scope.exitEditMode = function () {
      $scope.inEditMode = false;
    };

    $scope.save = function () {
      if ($scope.editDocForm.$valid) {
        return Doc
          .save($stateParams, $scope.doc).$promise
          .then(function () {
            return $state.transitionTo($state.current, $stateParams, { reload: true });
          });
      }
    };

    $scope.attachNewDoc = function () {
      $state.go('root.docs.new', { parentDocId: $scope.doc.docId });
    };

    $scope.attachDoc = function (docTypeId) {
      var newDoc = {
        parentDocId:  $scope.doc.docId,
        docFormatTypeId: 3,
        docFormatTypeName: 'Хартиен',
        docCasePartTypeId: 1,
        docCasePartTypeName: 'Публичен',
        docDirectionId: 1,
        docDirectionName: 'Входящ',
        docTypeId: docTypeId,
        docTypeName:
          docTypeId === 1 ? 'Резолюция' : (docTypeId === 2 ? 'Задача' : 'Забележка'),
        docSubject:
          docTypeId === 1 ? 'Резолюция' : (docTypeId === 2 ? 'Задача' : 'Забележка'),
        correspondents: $scope.doc.correspondents,
        correspondentName: $scope.doc.correspondentName
      };

      Doc.save(newDoc).$promise.then(function (savedDoc) {
        $state.go('root.docs.edit.addressing', { docId: savedDoc.docId });
      });

    };
  }

  DocsEditCtrl.$inject = [
    '$q',
    '$scope',
    '$filter',
    '$state',
    '$stateParams',
    'Doc',
    'doc'
  ];

  DocsEditCtrl.$resolve = {
    doc: [
      '$stateParams',
      'Doc',
      function resolveDoc($stateParams, Doc) {
        return Doc.get({ docId: $stateParams.docId }).$promise;
      }
    ]
  };

  angular.module('ems').controller('DocsEditCtrl', DocsEditCtrl);
}(angular));
