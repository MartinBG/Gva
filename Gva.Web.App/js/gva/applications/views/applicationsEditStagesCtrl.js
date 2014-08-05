/*global angular, moment, _*/
(function (angular, moment, _) {
  'use strict';

  function ApplicationsEditStagesCtrl(
    $scope,
    $state,
    $stateParams,
    $sce,
    scModal,
    DocStages,
    appStages,
    doc
  ) {
    $scope.doc = doc;
    $scope.appStages = appStages;
    $scope.docElectronicServiceStages = _.map(_.cloneDeep(doc.docElectronicServiceStages),
      function (docElectronicServiceStage) {
        docElectronicServiceStage.electronicServiceStageExecutors =
          $sce.trustAsHtml(docElectronicServiceStage.electronicServiceStageExecutors);

        return docElectronicServiceStage;
      });

    $scope.newAppStage = function () {
      var nextOrdinal = Math.max(0, _.max(_.pluck($scope.appStages, 'ordinal'))) + 1;

      var modalInstance = scModal.open('newAppStage', {
        ordinal: nextOrdinal,
        appId: $stateParams.id
      });

      modalInstance.result.then(function () {
        return $state.transitionTo($state.$current, $stateParams, { reload: true });
      });

      return modalInstance.opened;
    };

    $scope.viewAppStage = function (stage) {
      var modalInstance = scModal.open('editAppStage', {
        appId: $stateParams.id,
        stageId: stage.id
      });

      modalInstance.result.then(function () {
        return $state.transitionTo($state.$current, $stateParams, { reload: true });
      });

      return modalInstance.opened;
    };

    $scope.endDocStage = function () {
      var modalInstance = scModal.open('endDocStage', { doc: doc });

      modalInstance.result.then(function () {
        return $state.transitionTo($state.$current, $stateParams, { reload: true });
      });

      return modalInstance.opened;
    };

    $scope.nextDocStage = function () {
      var caseDoc = _.find(doc.docRelations, function (item) {
        return item.docId === item.rootDocId;
      });

      var modalInstance = scModal.open('nextDocStage', {
        caseDoc: caseDoc,
        doc: doc
      });

      modalInstance.result.then(function () {
        return $state.transitionTo($state.$current, $stateParams, { reload: true });
      });

      return modalInstance.opened;
    };

    $scope.editDocStage = function () {
      var modalInstance = scModal.open('editDocStage', { doc: doc });

      modalInstance.result.then(function () {
        return $state.transitionTo($state.$current, $stateParams, { reload: true });
      });

      return modalInstance.opened;
    };

    $scope.reverseDocStage = function () {
      return $state.go('root.applications.edit.stages').then(function () {
        return DocStages.reverse({
          id: doc.docId,
          docVersion: doc.version
        }).$promise.then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
        });
      });
    };
  }

  ApplicationsEditStagesCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    '$sce',
    'scModal',
    'DocStages',
    'appStages',
    'doc'
  ];

  ApplicationsEditStagesCtrl.$resolve = {
    doc: [
      '$stateParams',
      'Docs',
      'application',
      function resolveDoc($stateParams, Docs, application) {
        return Docs.get({ id: application.docId }).$promise.then(function (doc) {
          doc.openAccordion = false;
          doc.flags = {};

          //? depends on caseDoc on current doc
          doc.flags.isVisibleNextStageCmd = true;
          doc.flags.isVisibleEndStageCmd = doc.docElectronicServiceStages.length > 0 &&
            !doc.docElectronicServiceStages[doc.docElectronicServiceStages.length - 1].endingDate;
          doc.flags.isVisibleEditStageCmd =
            doc.canEditTechElectronicServiceStage;
          doc.flags.isVisibleReverseStageCmd = doc.docElectronicServiceStages.length > 1 &&
            doc.canEditTechElectronicServiceStage;

          return doc;
        });
      }
    ],
    appStages: [
      '$stateParams',
      'AppStages',
      function resolveAppStages($stateParams, AppStages) {
        return AppStages.query({ id: $stateParams.id }).$promise;
      }
    ]
  };
  angular.module('gva').controller('ApplicationsEditStagesCtrl', ApplicationsEditStagesCtrl);
}(angular, moment, _));
