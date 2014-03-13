/*global angular, _, require, moment*/
(function (angular, _, require, moment) {
  'use strict';
  angular.module('app').config(function ($httpBackendConfiguratorProvider) {

    var nomenclatures = require('./nomenclatures.sample');

    $httpBackendConfiguratorProvider
      .when('POST', '/api/docs/:docId/workflow/add',
      function ($params, $jsonData, docWorkflows) {
        var docId = parseInt($params.docId, 10);

        var todayDate = moment(),
            workflowAction = _(nomenclatures.docWorkflowAction)
            .filter({ nomValueId: $jsonData.docWorkflowActionId })
            .first();

        var newDocWorkflow = {
          docId: docId,
          docWorkFlowActionId: $jsonData.docWorkflowActionId,
          docWorkflowActionName: workflowAction.name,
          eventDate: todayDate.format('YYYY-MM-DDTHH:mm:ss'),
          yesNo: $jsonData.confirm,
          userId: $jsonData.userId,
          userName: $jsonData.userName,
          toUnitId: null,
          toUnitName: null,
          principalUnitId: null,
          principalUnitName: null,
          note: $jsonData.note
        };

        var nextDocWorkflow;

        if (!$jsonData.toUnits) {
          nextDocWorkflow = _(docWorkflows).pluck('docWorkflowId').max().value() + 1;
          newDocWorkflow.docWorkflowId = nextDocWorkflow;

          docWorkflows.push(newDocWorkflow);
        }
        else {
          _.forEach($jsonData.toUnits, function (unit) {
            nextDocWorkflow = _(docWorkflows).pluck('docWorkflowId').max().value() + 1;

            var docWorkflow = _.cloneDeep(newDocWorkflow);
            docWorkflow.docWorkflowId = nextDocWorkflow;
            docWorkflow.toUnitId = unit.nomValueId;
            docWorkflow.toUnitName = unit.name;

            docWorkflows.push(docWorkflow);
          });
        }

        var returnValue = _.filter(docWorkflows, { docId: docId });

        return [200, { docWorkflows: returnValue }];
      })
      .when('POST', '/api/docs/:docId/workflow/remove',
      function ($params, $jsonData, docWorkflows) {
        var docId = parseInt($params.docId, 10);

        _(docWorkflows).remove({ docId: docId, docWorkflowId: $jsonData.docWorkflowId });

        var returnValue = _.filter(docWorkflows, { docId: docId });

        return [200, { docWorkflows: returnValue }];
      });
  });
}(angular, _, require, moment));
