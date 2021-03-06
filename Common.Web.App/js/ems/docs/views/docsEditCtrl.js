﻿/*global angular, _*/
(function (angular, _) {
  'use strict';

  function DocsEditCtrl(
    $q,
    $scope,
    $filter,
    $state,
    $stateParams,
    l10n,
    scMessage,
    Docs,
    DocStages,
    DocStatuses,
    doc
  ) {
    $scope.$state = $state;
    $scope.l10n = l10n;
    $scope.doc = doc;

    $scope.inEditMode = false;

    $scope.markAsRead = function () {
      return Docs
        .markAsRead({
          id: $scope.doc.docId,
          docVersion: $scope.doc.version
        }, {}).$promise.then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
        });
    };

    $scope.markAsUnread = function () {
      return Docs
        .markAsUnread({
          id: $scope.doc.docId,
          docVersion: $scope.doc.version
        }, {}).$promise.then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
        });
    };

    $scope.eSign = function () {
      // TODO SIGNING throw 'not implemented';
    };

    $scope.undoESign = function () {
      // TODO SIGNING throw 'not implemented';
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

          return Docs
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

    $scope.attachDocInternal = function (docEntryTypeAlias, docTypeAlias) {
      return Docs
        .createChild({
          id: doc.docId,
          docEntryTypeAlias: docEntryTypeAlias,
          docTypeAlias: docTypeAlias
        }, {}).$promise.then(function (result) {
          return $state.go('root.docs.edit.view', { id: result.docId });
        });
    };

    $scope.attachRemovingIrregularities = function () {
      return Docs.createPublicChild({
          id: doc.docId
      }, {
        docTypeAlias: 'RemovingIrregularitiesInstructions',
        correspondents: _.pluck(doc.docCorrespondents, 'nomValueId')
      }).$promise.then(function (result) {
          return $state.go('root.docs.edit.view', { id: result.docId });
        });
    };

    $scope.attachIndividualActRefusal = function () {
      return Docs.createPublicChild({
        id: doc.docId
      }, {
        docTypeAlias: 'IndividualAdministrativeActRefusal',
        correspondents: _.pluck(doc.docCorrespondents, 'nomValueId')
      }).$promise.then(function (result) {
        return $state.go('root.docs.edit.view', { id: result.docId });
      });
    };

    $scope.attachConsiderationRefusal = function () {
      return Docs.createPublicChild({
        id: doc.docId
      }, {
        docTypeAlias: 'CorrespondenceConsiderationRefusal',
        correspondents: _.pluck(doc.docCorrespondents, 'nomValueId')
      }).$promise.then(function (result) {
        return $state.go('root.docs.edit.view', { id: result.docId });
      });
    };

    $scope.attachResolution = function () {
      return $scope.attachDocInternal('Resolution', 'Resolution');
    };

    $scope.attachResolutionParentOnly = function () {
      return $scope.attachDocInternal('Resolution', 'ResolutionParentOnly');
    };

    $scope.attachTask = function () {
      return $scope.attachDocInternal('Task', 'Task');
    };

    $scope.attachTaskParentOnly = function () {
      return $scope.attachDocInternal('Task', 'TaskParentOnly');
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
        return DocStages.reverse({
          id: doc.docId,
          docVersion: doc.version
        }).$promise.then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
        });
      });
    };

    $scope.nextStatus = function (params) {
      return DocStatuses.next({
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
      return DocStatuses.reverse({
        id: doc.docId,
        alias: params.alias,
        docVersion: doc.version
      }, {}).$promise.then(function () {
        return $state.transitionTo($state.current, $stateParams, { reload: true });
      });
    };

    $scope.cancelStatus = function () {
      return DocStatuses.cancel({
        id: doc.docId,
        docVersion: doc.version
      }, {}).$promise.then(function () {
        return $state.transitionTo($state.current, $stateParams, { reload: true });
      });
    };

    $scope.register = function () {
      return Docs.register({
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

    $scope.editDocClassification = function () {
      return $state.go('root.docs.edit.case.docClassification');
    };

    $scope.changeDocParent = function () {
      return $state.go('root.docs.edit.case.changeDocParent');
    };

    $scope.sendEmail = function () {
      return $state.go('root.docs.edit.case.sendEmail');
    };

    $scope.deleteDoc = function () {
      return scMessage('common.messages.confirmDelete')
      .then(function (result) {
        if (result === 'OK') {
          return Docs.deleteDoc({ id: doc.docId }).$promise.then(function (data) {
            if (data.caseId) {
              return $state.go('root.docs.edit.case', { id: data.caseId });
            } else {
              return $state.go('root.docs.search');
            }
          });
        }
      });
    };

    $scope.createNewCase = function () {
      return Docs.createNewCase({ id: doc.docId }, {}).$promise.then(function () {
        return $state.go('root.docs.edit.case', { id: doc.docId }, { reload: true });
      });
    };

    $scope.goToDoc = function (message) {
      return $state.go('root.docs.edit.case', { id: message.docId });
    };
  }

  DocsEditCtrl.$inject = [
    '$q',
    '$scope',
    '$filter',
    '$state',
    '$stateParams',
    'l10n',
    'scMessage',
    'Docs',
    'DocStages',
    'DocStatuses',
    'doc'
  ];

  DocsEditCtrl.$resolve = {
    doc: [
      '$stateParams',
      'Docs',
      function resolveDoc($stateParams, Docs) {
        return Docs.get({ id: $stateParams.id }).$promise.then(function (doc) {
          doc.openAccordion = false;

          doc.resolutionUnitsInCharge =
            _(doc.docRelations)
            .where(function (dr) {
              return dr.parentDocId === doc.docId &&
                (dr.docDocStatusAlias === 'Processed' || dr.docDocStatusAlias === 'Finished') &&
                (dr.docEntryTypeAlias === 'Resolution' || dr.docEntryTypeAlias === 'Taks') &&
                dr.docUnitsInCharge &&
                dr.docUnitsInCharge.length;
            })
            .map(function (dr) {
              return _(dr.docUnitsInCharge)
                .pluck('name')
                .reduce(function (a, b) { return a + '; ' + b; });
            })
            .value();

          doc.flags = {};

          doc.flags.getDocStatusName = function (docStatusAlias) {
            switch (docStatusAlias) {
              case 'Draft':
                return doc.isElectronic ? 'от портал' : 'чернова';
              case 'Processed':
                return (doc.isResolution || doc.isTask) ? 'възложен' :
                  doc.isDocOutgoing ? 'изготвен' : 'обработен';
              default:
                throw new Error('Unexpected doc status.');
            }
          };

          doc.flags.isVisibleCreateChildDoc = true;
          doc.flags.isVisibleCreateChildResolution = doc.canManagement;
          doc.flags.isVisibleCreateChildTask = doc.canManagement;
          doc.flags.isVisibleCreateChildRemark = true;

          doc.flags.isVisibleEditCmd = doc.docStatusAlias === 'Draft' && doc.canEdit;

          doc.flags.isVisibleProcessedStatusCmd =
            doc.docStatusAlias === 'Draft' && (doc.canEdit || doc.canManagement);
          doc.flags.isVisibleFinishedStatusCmd =
            doc.docStatusAlias === 'Processed' && doc.canFinish;
          doc.flags.isVisibleCanceledStatusCmd =
            doc.docStatusAlias === 'Processed' && doc.canFinish;

          doc.flags.isVisibleDraftStatusReverseCmd =
            doc.docStatusAlias === 'Processed' && doc.canReverse;
          doc.flags.isVisibleProcessedStatusReverseCmd =
            (doc.docStatusAlias === 'Finished' || doc.docStatusAlias === 'Canceled') &&
            doc.canReverse;

          doc.flags.isVisibleRegisterCmd =
            doc.docStatusAlias !== 'Finished' && doc.docStatusAlias !== 'Canceled' &&
            !doc.isRegistered && doc.canRegister &&
            !doc.isResolution && !doc.isTask && !doc.isRemark;
          doc.flags.isVisibleEsignCmd =
            doc.docStatusAlias === 'Processed' && doc.canESign &&
            !doc.isResolution && !doc.isTask && !doc.isRemark &&
            !doc.isDocIncoming;
          doc.flags.isVisibleUndoEsignCmd =
            doc.docStatusAlias === 'Processed' && doc.canESign &&
            !doc.isResolution && !doc.isTask && !doc.isRemark &&
            !doc.isDocIncoming;

          doc.flags.isVisibleSignRequestCmd =
            doc.docStatusAlias === 'Processed' &&
            (doc.canEdit || doc.canManagement) &&
            !doc.isResolution && !doc.isTask && !doc.isRemark &&
            !doc.isDocIncoming;
          doc.flags.isVisibleDiscussRequestCmd =
            doc.docStatusAlias === 'Processed' &&
            (doc.canEdit || doc.canManagement) &&
            !doc.isResolution && !doc.isTask && !doc.isRemark &&
            !doc.isDocIncoming;
          doc.flags.isVisibleApprovalRequestCmd =
            doc.docStatusAlias === 'Processed' &&
            (doc.canEdit || doc.canManagement) &&
            !doc.isResolution && !doc.isTask && !doc.isRemark &&
            !doc.isDocIncoming;
          doc.flags.isVisibleRegistrationRequestCmd =
            doc.docStatusAlias === 'Processed' &&
            (doc.canEdit || doc.canManagement) &&
            !doc.isResolution && !doc.isTask && !doc.isRemark &&
            !doc.isDocIncoming;

          doc.flags.isVisibleSignCmd =
            doc.docStatusAlias === 'Processed' && doc.canDocWorkflowSign &&
            !doc.isResolution && !doc.isTask && !doc.isRemark &&
            !doc.isDocIncoming;
          doc.flags.isVisibleDiscussCmd =
            doc.docStatusAlias === 'Processed' && doc.canDocWorkflowDiscuss &&
            !doc.isResolution && !doc.isTask && !doc.isRemark &&
            !doc.isDocIncoming;
          doc.flags.isVisibleApprovalCmd =
            doc.docStatusAlias === 'Processed' && doc.canDocWorkflowDiscuss &&
            !doc.isResolution && !doc.isTask && !doc.isRemark &&
            !doc.isDocIncoming;
          doc.flags.canSubstituteWorkflow = doc.canSubstituteManagement;
          doc.flags.canDocWorkflowManagement = doc.canDocWorkflowManagement;

          //? depends on caseDoc on current doc
          doc.flags.isVisibleNextStageCmd = true;
          doc.flags.isVisibleEndStageCmd = doc.docElectronicServiceStages.length > 0 &&
            !doc.docElectronicServiceStages[doc.docElectronicServiceStages.length - 1].endingDate;
          doc.flags.isVisibleEditStageCmd =
            doc.canEditTechElectronicServiceStage;
          doc.flags.isVisibleReverseStageCmd = doc.docElectronicServiceStages.length > 1 &&
            doc.canEditTechElectronicServiceStage;

          doc.flags.isVisibleSendMailCmd =
            doc.canSendMail &&
            (doc.isDocOutgoing || doc.isDocInternalOutgoing) &&
            (doc.docStatusAlias === 'Processed' || doc.docStatusAlias === 'Finished');

          doc.flags.isVisibleDeleteDoc =
            doc.canEdit &&
            !doc.isRegistered &&
            doc.docStatusAlias === 'Draft' &&
            doc.docWorkflows.length === 0 &&
            !doc.hasChildDocs;

          doc.flags.isVisibleEditCasePartCmd =
            !doc.isCase &&
            (doc.canDocCasePartManagement ||
            (doc.docStatusAlias === 'Draft' && doc.canEdit));
          doc.flags.isVisibleEditTechCmd = !doc.isResolution && !doc.isTask &&
            !doc.isRemark && doc.canEditTech;
          doc.flags.isVisibleDocMoveCmd = doc.canDocMovement;
          doc.flags.isVisibleDocMoveToNewCmd = !doc.isCase && doc.canDocMovement;

          doc.flags.isVisbleDivider1 = doc.flags.isVisibleRegisterCmd;
          doc.flags.isVisbleDivider2 = doc.flags.isVisibleEsignCmd ||
            doc.flags.isVisibleUndoEsignCmd;
          doc.flags.isVisbleDivider3 = doc.flags.isVisibleSignCmd ||
            doc.flags.isVisibleDiscussCmd || doc.flags.isVisibleApprovalCmd;
          doc.flags.isVisbleDivider4 = doc.flags.isVisibleSignRequestCmd ||
            doc.flags.isVisibleDiscussRequestCmd || doc.flags.isVisibleApprovalRequestCmd ||
            doc.flags.isVisibleRegistrationRequestCmd;
          doc.flags.isVisbleDivider5 = doc.flags.isVisibleDraftStatusReverseCmd;

          return doc;
        });
      }
    ]
  };

  angular.module('ems').controller('DocsEditCtrl', DocsEditCtrl);
}(angular, _));
