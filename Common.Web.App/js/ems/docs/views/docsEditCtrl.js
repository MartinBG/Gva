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
    DocStatus,
    doc
  ) {
    $scope.$state = $state;
    $scope.doc = doc;

    $scope.doc.openAccordion = false;
    $scope.doc.flags = {};

    $scope.doc.flags.isVisibleCreateChildDoc = true;
    $scope.doc.flags.isVisibleCreateChildResolution = true;
    $scope.doc.flags.isVisibleCreateChildTask = true;
    $scope.doc.flags.isVisibleCreateChildRemark = true;

    $scope.doc.flags.isVisibleEditCmd = $scope.doc.docStatusAlias === 'Draft' && $scope.doc.canEdit;

    if ($scope.doc.isResolution || $scope.doc.isTask || $scope.doc.isRemark) {
      $scope.doc.flags.isVisibleDraftStatusCmd = false;
      $scope.doc.flags.isVisiblePreparedStatusCmd = false;
      $scope.doc.flags.isVisibleProcessedStatusCmd = false;
      $scope.doc.flags.isVisibleFinishedStatusCmd =
        $scope.doc.docStatusAlias === 'Draft' && $scope.doc.canFinish;
      $scope.doc.flags.isVisibleCanceledStatusCmd =
        $scope.doc.docStatusAlias === 'Draft' && $scope.doc.canFinish;

      $scope.doc.flags.isVisibleDraftStatusReverseCmd =
        ($scope.doc.docStatusAlias === 'Finished' || $scope.doc.docStatusAlias === 'Canceled') &&
        $scope.doc.canReverse;
      $scope.doc.flags.isVisiblePreparedStatusReverseCmd = false;
      $scope.doc.flags.isVisibleProcessedStatusReverseCmd = false;
      $scope.doc.flags.isVisibleFinishedStatusReverseCmd = false;
      $scope.doc.flags.isVisibleCanceledStatusReverseCmd = false;
    } else {
      if ($scope.doc.isDocIncoming) {
        $scope.doc.flags.isVisibleDraftStatusCmd = false;
        $scope.doc.flags.isVisiblePreparedStatusCmd = false;
        $scope.doc.flags.isVisibleProcessedStatusCmd =
          $scope.doc.docStatusAlias === 'Draft' &&
          ($scope.doc.canEdit || $scope.doc.canManagement);
        $scope.doc.flags.isVisibleFinishedStatusCmd =
          $scope.doc.docStatusAlias === 'Processed' && $scope.doc.canFinish;
        $scope.doc.flags.isVisibleCanceledStatusCmd =
          $scope.doc.docStatusAlias === 'Processed' && $scope.doc.canFinish;

        $scope.doc.flags.isVisibleDraftStatusReverseCmd =
          $scope.doc.docStatusAlias === 'Processed' && $scope.doc.canReverse;
        $scope.doc.flags.isVisiblePreparedStatusReverseCmd = false;
        $scope.doc.flags.isVisibleProcessedStatusReverseCmd =
          ($scope.doc.docStatusAlias === 'Finished' || $scope.doc.docStatusAlias === 'Canceled') &&
          $scope.doc.canReverse;
        $scope.doc.flags.isVisibleFinishedStatusReverseCmd = false;
        $scope.doc.flags.isVisibleCanceledStatusReverseCmd = false;
      } else {
        $scope.doc.flags.isVisibleDraftStatusCmd = false;
        $scope.doc.flags.isVisiblePreparedStatusCmd =
          $scope.doc.docStatusAlias === 'Draft' && $scope.doc.canEdit;
        $scope.doc.flags.isVisibleProcessedStatusCmd =
          $scope.doc.docStatusAlias === 'Prepared' &&
          ($scope.doc.canEdit || $scope.doc.canManagement);
        $scope.doc.flags.isVisibleFinishedStatusCmd =
          $scope.doc.docStatusAlias === 'Processed' && $scope.doc.canFinish;
        $scope.doc.flags.isVisibleCanceledStatusCmd =
          $scope.doc.docStatusAlias === 'Processed' && $scope.doc.canFinish;

        $scope.doc.flags.isVisibleDraftStatusReverseCmd =
          $scope.doc.docStatusAlias === 'Prepared' && $scope.doc.canReverse;
        $scope.doc.flags.isVisiblePreparedStatusReverseCmd =
          $scope.doc.docStatusAlias === 'Processed' && $scope.doc.canReverse;
        $scope.doc.flags.isVisibleProcessedStatusReverseCmd =
          ($scope.doc.docStatusAlias === 'Finished' || $scope.doc.docStatusAlias === 'Canceled') &&
          $scope.doc.canReverse;
        $scope.doc.flags.isVisibleFinishedStatusReverseCmd = false;
        $scope.doc.flags.isVisibleCanceledStatusReverseCmd = false;
      }
    }

    $scope.doc.flags.isVisibleRegisterCmd = !$scope.doc.isRegistered && $scope.doc.canRegister &&
      !$scope.doc.isResolution && !$scope.doc.isTask && !$scope.doc.isRemark;
    $scope.doc.flags.isVisibleEsignCmd =
      $scope.doc.docStatusAlias === 'Prepared' && $scope.doc.canESign &&
      !$scope.doc.isResolution && !$scope.doc.isTask && !$scope.doc.isRemark &&
      !$scope.doc.isDocIncoming;
    $scope.doc.flags.isVisibleUndoEsignCmd =
      $scope.doc.docStatusAlias === 'Prepared' && $scope.doc.canESign &&
      !$scope.doc.isResolution && !$scope.doc.isTask && !$scope.doc.isRemark &&
      !$scope.doc.isDocIncoming;

    $scope.doc.flags.isVisibleSignRequestCmd =
      $scope.doc.docStatusAlias === 'Prepared' &&
      ($scope.doc.canEdit || $scope.doc.canManagement) &&
      !$scope.doc.isResolution && !$scope.doc.isTask && !$scope.doc.isRemark &&
      !$scope.doc.isDocIncoming;
    $scope.doc.flags.isVisibleDiscussRequestCmd =
      $scope.doc.docStatusAlias === 'Prepared' &&
      ($scope.doc.canEdit || $scope.doc.canManagement) &&
      !$scope.doc.isResolution && !$scope.doc.isTask && !$scope.doc.isRemark &&
      !$scope.doc.isDocIncoming;
    $scope.doc.flags.isVisibleApprovalRequestCmd =
      $scope.doc.docStatusAlias === 'Prepared' &&
      ($scope.doc.canEdit || $scope.doc.canManagement) &&
      !$scope.doc.isResolution && !$scope.doc.isTask && !$scope.doc.isRemark &&
      !$scope.doc.isDocIncoming;
    $scope.doc.flags.isVisibleRegistrationRequestCmd =
      $scope.doc.docStatusAlias === 'Prepared' &&
      ($scope.doc.canEdit || $scope.doc.canManagement) &&
      !$scope.doc.isResolution && !$scope.doc.isTask && !$scope.doc.isRemark &&
      !$scope.doc.isDocIncoming;

    $scope.doc.flags.isVisibleSignCmd =
      $scope.doc.docStatusAlias === 'Prepared' && $scope.doc.canManagement &&
      !$scope.doc.isResolution && !$scope.doc.isTask && !$scope.doc.isRemark &&
      !$scope.doc.isDocIncoming;
    $scope.doc.flags.isVisibleDiscussCmd =
      $scope.doc.docStatusAlias === 'Prepared' && $scope.doc.canManagement &&
      !$scope.doc.isResolution && !$scope.doc.isTask && !$scope.doc.isRemark &&
      !$scope.doc.isDocIncoming;
    $scope.doc.flags.isVisibleApprovalCmd =
      $scope.doc.docStatusAlias === 'Prepared' && $scope.doc.canManagement &&
      !$scope.doc.isResolution && !$scope.doc.isTask && !$scope.doc.isRemark &&
      !$scope.doc.isDocIncoming;
    $scope.doc.flags.canSubstituteWorkflow = $scope.doc.canSubstituteManagement;
    $scope.doc.flags.canDeleteWorkflow = $scope.doc.canDeleteManagement;

    //? depends on caseDoc on current doc
    $scope.doc.flags.isVisibleNextStageCmd = true;
    $scope.doc.flags.isVisibleEndStageCmd = $scope.doc.docElectronicServiceStages.length > 0 &&
      !$scope.doc.docElectronicServiceStages[doc.docElectronicServiceStages.length - 1].endingDate;
    $scope.doc.flags.isVisibleEditStageCmd =
      $scope.doc.canEditTechElectronicServiceStage;
    $scope.doc.flags.isVisibleReverseStageCmd = $scope.doc.docElectronicServiceStages.length > 1 &&
      $scope.doc.canEditTechElectronicServiceStage;

    $scope.doc.flags.isVisibleEditCasePartCmd =
      !$scope.doc.isCase &&
      ($scope.doc.canChangeDocCasePart ||
      ($scope.doc.docStatusAlias === 'Draft' && $scope.doc.canEdit));
    $scope.doc.flags.isVisibleEditTechCmd = !$scope.doc.isResolution && !$scope.doc.isTask &&
      !$scope.doc.isRemark && $scope.doc.canEditTech;

    $scope.doc.flags.isVisbleDivider1 = doc.flags.isVisibleRegisterCmd;
    $scope.doc.flags.isVisbleDivider2 = doc.flags.isVisibleEsignCmd ||
      doc.flags.isVisibleUndoEsignCmd;
    $scope.doc.flags.isVisbleDivider3 = doc.flags.isVisibleSignCmd ||
      doc.flags.isVisibleDiscussCmd || doc.flags.isVisibleApprovalCmd;
    $scope.doc.flags.isVisbleDivider4 = doc.flags.isVisibleSignRequestCmd ||
      doc.flags.isVisibleDiscussRequestCmd || doc.flags.isVisibleApprovalRequestCmd ||
      doc.flags.isVisibleRegistrationRequestCmd;
    $scope.doc.flags.isVisbleDivider5 = doc.flags.isVisibleDraftStatusReverseCmd ||
      doc.flags.isVisiblePreparedStatusReverseCmd || doc.flags.isVisibleFinishedStatusReverseCmd ||
      doc.flags.isVisibleCanceledStatusReverseCmd;

    $scope.inEditMode = false;

    $scope.markAsRead = function () {
      return Doc
        .markAsRead({
          id: $scope.doc.docId,
          docVersion: $scope.doc.version
        }, {}).$promise.then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
        });
    };

    $scope.markAsUnread = function () {
      return Doc
        .markAsUnread({
          id: $scope.doc.docId,
          docVersion: $scope.doc.version
        }, {}).$promise.then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
        });
    };

    $scope.eSign = function () {
      throw 'not implemented';
    };

    $scope.undoESign = function () {
      throw 'not implemented';
    };

    $scope.enterEditMode = function () {
      $scope.inEditMode = true;
      $scope.doc.docFiles = $scope.doc.publicDocFiles.concat($scope.doc.privateDocFiles);
    };

    $scope.exitEditMode = function () {
      return $state.transitionTo($state.current, $stateParams, { reload: true });
    };

    $scope.save = function () {
      return $scope.editDocForm.$validate().then(function () {
        if ($scope.editDocForm.$valid) {

          return Doc
            .save($stateParams, $scope.doc)
            .$promise
            .then(function () {
              return $state.transitionTo($state.current, $stateParams, { reload: true });
            });
        }
        else {
          $scope.doc.openAccordion = true;
        }
      });
    };

    $scope.attachDoc = function () {
      return $state.go('root.docs.new', { parentDocId: $scope.doc.docId });
    };

    $scope.attachElectronicDoc = function () {
      return $state.go('root.docs.new', { parentDocId: $scope.doc.docId, eDoc: true });
    };

    $scope.attachDocInternal = function (docEntryTypeAlias) {
      return Doc
        .createChild({
          id: doc.docId,
          docEntryTypeAlias: docEntryTypeAlias
        }, {}).$promise.then(function (result) {
          return $state.go('root.docs.edit.view', { id: result.docId });
        });
    };

    $scope.attachResolution = function () {
      return $scope.attachDocInternal('Resolution');
    };

    $scope.attachTask = function () {
      return $scope.attachDocInternal('Task');
    };

    $scope.attachRemark = function () {
      return $scope.attachDocInternal('Remark');
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
      return $state.go('root.docs.edit.stages').then(function () {
        return DocStage.reverse({
          id: doc.docId,
          docVersion: doc.version
        }).$promise.then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
        });
      });
    };

    $scope.nextStatus = function (params) {
      return DocStatus.next({
        id: doc.docId,
        alias: params.alias,
        docVersion: doc.version
      }, {}).$promise.then(function (data) {
        if (data && data.docRelations) {
          return $state.go('root.docs.edit.case.caseFinish', $stateParams, {}, {
            docRelations: data.docRelations
          });
        } else {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
        }
      });
    };

    $scope.reverseStatus = function (params) {
      return DocStatus.reverse({
        id: doc.docId,
        alias: params.alias,
        docVersion: doc.version
      }, {}).$promise.then(function () {
        return $state.transitionTo($state.current, $stateParams, { reload: true });
      });
    };

    $scope.cancelStatus = function () {
      return DocStatus.cancel({
        id: doc.docId,
        docVersion: doc.version
      }, {}).$promise.then(function () {
        return $state.transitionTo($state.current, $stateParams, { reload: true });
      });
    };

    $scope.register = function () {
      return Doc.register({
        id: doc.docId,
        docVersion: doc.version
      }, {}).$promise.then(function (data) {
        if (data.result && data.result === 'Manual') {
          return $state.go('root.docs.edit.case.manualRegister');
        } else {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
        }
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

    $scope.editCasePart = function () {
      return $state.go('root.docs.edit.case.casePart');
    };

    $scope.editDocType = function () {
      return $state.go('root.docs.edit.case.docType');
    };

    $scope.changeDocParent = function () {
      return $state.go('root.docs.edit.case.changeDocParent');
    };

    $scope.sendEmail = function () {
      return $state.go('root.docs.edit.case.sendEmail');
    };

    $scope.createNewCase = function () {
      return Doc.createNewCase({ id: doc.docId }, {}).$promise.then(function () {
        return $state.go('root.docs.edit.case', { id: doc.docId }, { reload: true });
      });
    };

    $scope.goToDoc = function (message) {
      return $state.go('root.docs.edit.view', { id: message.docId });
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
    'DocStatus',
    'doc'
  ];

  DocsEditCtrl.$resolve = {
    doc: [
      '$stateParams',
      'Doc',
      function resolveDoc($stateParams, Doc) {
        return Doc.get({ id: $stateParams.id }).$promise;
      }
    ]
  };

  angular.module('ems').controller('DocsEditCtrl', DocsEditCtrl);
}(angular));
