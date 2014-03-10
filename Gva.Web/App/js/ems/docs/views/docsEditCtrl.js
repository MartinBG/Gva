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
    DocStage,
    doc
  ) {
    $scope.$state = $state;
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
      return $state.transitionTo($state.current, $stateParams, { reload: true });
    };

    $scope.save = function () {
      $scope.editDocForm.$validate().then(function () {
        if ($scope.editDocForm.$valid) {
          return Doc
            .save($stateParams, $scope.doc).$promise
            .then(function () {
              return $state.transitionTo($state.current, $stateParams, { reload: true });
            });
        }
      });
    };

    $scope.attachNewDoc = function () {
      return $state.go('root.docs.new', { parentDocId: $scope.doc.docId });
    };

    $scope.endStage = function () {
      return $state.go('root.docs.edit.stages.end');
    };

    $scope.nextStage = function () {
      return $state.go('root.docs.edit.stages.next');
    };

    $scope.editStage = function () {
      return $state.go('root.docs.edit.stages.edit');
    };

    $scope.reverseStage = function () {
      return DocStage.reverse({ docId: doc.docId }).$promise.then(function (result) {
        doc.docElectronicServiceStages = result.stages;
      });
    };

    $scope.nextStatus = function () {
      return Doc.nextStatus({ docId: doc.docId }).$promise.then(function () {
        return $state.transitionTo($state.current, $stateParams, { reload: true });
      });
    };

    $scope.reverseStatus = function () {
      return Doc.reverseStatus({ docId: doc.docId }).$promise.then(function () {
        return $state.transitionTo($state.current, $stateParams, { reload: true });
      });
    };

    $scope.cancelStatus = function () {
      return Doc.cancelStatus({ docId: doc.docId }).$promise.then(function () {
        return $state.transitionTo($state.current, $stateParams, { reload: true });
      });
    };

    $scope.setRegUri = function () {
      return Doc.setRegUri({ docId: doc.docId }).$promise.then(function () {
        return $state.transitionTo($state.current, $stateParams, { reload: true });
      });
    };

    $scope.signRequest = function () {
      return $state.go('root.docs.edit.workflows.signRequest');
    };

    $scope.discussRequest = function () {
      return $state.go('root.docs.edit.workflows.discussRequest');
    };

    $scope.approvalRequest = function () {
      return $state.go('root.docs.edit.workflows.approvalRequest');
    };

    $scope.registrationRequest = function () {
      return $state.go('root.docs.edit.workflows.registrationRequest');
    };

    $scope.signConfirm = function () {
      return $state.go('root.docs.edit.workflows.signConfirm');
    };

    $scope.discussConfirm = function () {
      return $state.go('root.docs.edit.workflows.discussConfirm');
    };

    $scope.approvalConfirm = function () {
      return $state.go('root.docs.edit.workflows.approvalConfirm');
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

      Doc.registerNew(newDoc).$promise.then(function (savedDoc) {
        return $state.go('root.docs.edit.view', { docId: savedDoc.docId });
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
    'DocStage',
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
