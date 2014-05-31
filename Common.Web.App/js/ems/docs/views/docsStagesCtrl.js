/*global angular, _*/
(function (angular, _) {
  'use strict';

  function DocsStagesCtrl(
    $scope,
    $sce,
    doc
  ) {
    $scope.doc = doc;
    $scope.docId = doc.docId;


    $scope.docElectronicServiceStages = _.map(_.cloneDeep(doc.docElectronicServiceStages),
      function (docElectronicServiceStage) {
        docElectronicServiceStage.electronicServiceStageExecutors =
          $sce.trustAsHtml(docElectronicServiceStage.electronicServiceStageExecutors);

        return docElectronicServiceStage;
      });

    $scope.removeDocStage = function () {
      throw 'not implemented';
    };
  }

  DocsStagesCtrl.$inject = [
    '$scope',
    '$sce',
    'doc'
  ];

  angular.module('ems').controller('DocsStagesCtrl', DocsStagesCtrl);
}(angular, _));
