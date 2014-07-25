/*global angular, moment, _*/
(function (angular, moment, _) {
  'use strict';

  function ApplicationsEditStagesCtrl(
    $scope,
    $state,
    $stateParams,
    $sce,
    namedModal,
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

      var modalInstance = namedModal.open('newAppStage', { ordinal: nextOrdinal });

      modalInstance.result.then(function () {
        return $state.transitionTo($state.$current, $stateParams, { reload: true });
      });

      return modalInstance.opened;
    };

    $scope.viewAppStage = function (stage) {
      var modalInstance = namedModal.open('editAppStage', {}, {
        stageModel: [
          'AppStages',
          function (AppStages) {
            return AppStages.get({ id: $stateParams.id, ind: stage.id }).$promise;
          }
        ]
      });

      modalInstance.result.then(function () {
        return $state.transitionTo($state.$current, $stateParams, { reload: true });
      });

      return modalInstance.opened;
    };

    $scope.endDocStage = function () {
      var stageModel = DocStages.get({
        id: doc.docId,
        docVersion: doc.version
      })
        .$promise
        .then(function (result) {
          result.docVersion = doc.version;
          result.docTypeId = doc.docTypeId;
          return result;
        });

      var modalInstance = namedModal.open('endDocStage', { stageModel: stageModel });

      modalInstance.result.then(function () {
        return $state.transitionTo($state.$current, $stateParams, { reload: true });
      });

      return modalInstance.opened;
    };

    $scope.nextDocStage = function () {
      var caseDoc = _.first(doc.docRelations, function (item) {
        return item.docId === item.rootDocId;
      });

      var stageModel = {
        docId: doc.docId,
        docVersion: caseDoc.length > 0 ? caseDoc[0].docVersion : doc.version,
        docTypeId: caseDoc.length > 0 ? caseDoc[0].docDocTypeId : doc.docTypeId,
        startingDate: moment().startOf('minute').format('YYYY-MM-DDTHH:mm:ss')
      };

      var modalInstance = namedModal.open('nextDocStage', { stageModel: stageModel });

      modalInstance.result.then(function () {
        return $state.transitionTo($state.$current, $stateParams, { reload: true });
      });

      return modalInstance.opened;
    };

    $scope.editDocStage = function () {
      var stageModel = DocStages.get({
        id: doc.docId,
        docVersion: doc.version
      })
        .$promise
        .then(function (result) {
          result.docVersion = doc.version;
          result.docTypeId = doc.docTypeId;
          return result;
        });

      var modalInstance = namedModal.open('editDocStage', { stageModel: stageModel });

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
    //

  }

  ApplicationsEditStagesCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    '$sce',
    'namedModal',
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
