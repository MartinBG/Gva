/*global angular, _, require, jQuery*/
(function (angular, _) {
  'use strict';
  angular.module('app').config(function ($httpBackendConfiguratorProvider) {

    $httpBackendConfiguratorProvider
      .when('GET', '/api/docs/:docId/stages/current',
        function($params, docStages) {
          var currentStage =
            _(docStages)
            .filter({ docId: parseInt($params.docId, 10), isCurrentStage: true })
            .first();

          return [200, currentStage];
        })
      .when('POST', '/api/docs/:docId/stages/next',
        function ($params, $jsonData, docStages) {
          var docId = parseInt($params.docId, 10);
          var currentStage = _(docStages).filter({ docId: docId, isCurrentStage: true }).first();

          if (!currentStage) {
            return [400];
          }

          var nextDocEStageId = _(docStages).pluck('docElectronicServiceStageId').max().value() + 1;

          var electronicServiceStages = require('./electronicServiceStage');
          var stage = _(electronicServiceStages)
            .filter({ nomValueId: $jsonData.electronicServiceStageId })
            .first();

          var newStage = {
            docElectronicServiceStageId: nextDocEStageId,
            electronicServiceStageId: $jsonData.electronicServiceStageId,
            docId: docId,
            startingDate: $jsonData.startingDate,
            electronicServiceStageName: stage.name,
            electronicServiceStageExecutors: 'Служител ДКХ',
            expectedEndingDate: $jsonData.expectedEndingDate,
            endingDate: $jsonData.endingDate,
            isCurrentStage: true
          };

          docStages.push(newStage);

          currentStage.isCurrentStage = false;
          currentStage.endingDate = newStage.startingDate;

          var returnValue = _.filter(docStages, { docId: docId });

          return [200, { stages: returnValue }];
        })
      .when('POST', '/api/docs/:docId/stages/edit',
        function ($params, $jsonData, docStages) {
          var docId = parseInt($params.docId, 10);
          var currentStage = _(docStages).filter({ docId: docId, isCurrentStage: true }).first();

          if (!currentStage) {
            return [400];
          }

          var electronicServiceStages = require('./electronicServiceStage');
          var stage = _(electronicServiceStages)
            .filter({ nomValueId: $jsonData.electronicServiceStageId })
            .first();

          currentStage.electronicServiceStageId = stage.nomValueId;
          currentStage.electronicServiceStageName = stage.name;
          currentStage.startingDate = $jsonData.startingDate;
          currentStage.electronicServiceStageExecutors = 'Служител ДКХ';
          currentStage.expectedEndingDate = $jsonData.expectedEndingDate;
          currentStage.endingDate = $jsonData.endingDate;

          var returnValue = _.filter(docStages, { docId: docId });

          return [200, { stages: returnValue }];
        })
      .when('POST', '/api/docs/:docId/stages/end',
        function ($params, $jsonData, docStages) {
          var docId = parseInt($params.docId, 10);
          var currentStage = _(docStages).filter({ docId: docId, isCurrentStage: true }).first();

          if (!currentStage) {
            return [400];
          }

          currentStage.endingDate = $jsonData.endingDate;

          var returnValue = _.filter(docStages, { docId: docId });

          return [200, { stages: returnValue }];
        })
      .when('POST', '/api/docs/:docId/stages/reverse',
        function ($params, docStages) {
          var docId = parseInt($params.docId, 10);
          _(docStages).remove({ docId: docId, isCurrentStage: true });

          var lastStage = _(docStages).findLast({ docId: docId });

          if (!!lastStage) {
            lastStage.isCurrentStage = true;
          }

          var returnValue = _.filter(docStages, { docId: docId });

          return [200, { stages: returnValue }];
        });
  });
}(angular, _, require, jQuery));
