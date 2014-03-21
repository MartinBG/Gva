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

    $scope.isVisibleEdit = true;
    if ($scope.doc.docStatusAlias !== 'Draft') {
      $scope.isVisibleEdit = false;
    }

    $scope.inEditMode = false;

    $scope.markAsRead = function () {
      throw 'not implemented';
      //$scope.doc.isRead = true;
    };

    $scope.markAsUnread = function () {
      throw 'not implemented';
      //$scope.doc.isRead = false;
    };

    $scope.enterEditMode = function () {
      $scope.inEditMode = true;
      $scope.doc.docFiles = $scope.doc.publicDocFiles.concat($scope.doc.privateDocFiles);
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

    $scope.attachDoc = function () {
      return $state.go('root.docs.new', { parentDocId: $scope.doc.docId });
    };

    $scope.attachDocInternal = function (docEntryTypeAlias) {
      return Doc
        .createChild({ docId: doc.docId, docEntryTypeAlias: docEntryTypeAlias })
        .$promise
        .then(function (result) {
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

    $scope.editCasePart = function () {
      return $state.go('root.docs.edit.case.casePart');
    };

    $scope.editDocType = function () {
      return $state.go('root.docs.edit.case.docType');
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
        return Doc.get({ id: $stateParams.id }).$promise;
      }
    ]
  };

  angular.module('ems').controller('DocsEditCtrl', DocsEditCtrl);
}(angular));
