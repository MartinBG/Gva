/*global angular, _*/
(function (angular, _) {
  'use strict';

  function ApplicationsEditCaseCtrl(
    $scope,
    $state,
    $stateParams,
    $sce,
    application,
    DocStatus
    ) {
    $scope.application = application;

    _.map(application.applicationDocCase, function (adc) {
      switch (adc.docDocStatusAlias) {
      case 'Draft':
        adc.nextStatusText = 'Отбелязване като изготвен';
        break;
      case 'Prepared':
        adc.nextStatusText = 'Отбелязване като обработен';
        break;
      case 'Processed':
        adc.nextStatusText = 'Отбелязване като приключен';
        break;
      default:
        adc.nextStatusText = null;
        break;
      }

      if (!adc.docDataHtml.$$unwrapTrustedValue) {
        adc.docDataHtml = $sce.trustAsHtml(adc.docDataHtml);
      }
      if (!adc.docDescriptionHtml.$$unwrapTrustedValue) {
        adc.docDescriptionHtml = $sce.trustAsHtml(adc.docDescriptionHtml);
      }

      return adc;
    });

    $scope.nextStatus = function (docId, docVersion) {
      return DocStatus.next({
        docId: docId,
        docVersion: docVersion
      })
        .$promise
        .then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
        });
    };

    $scope.linkNew = function (docId, docFileId) {
      return $state.go('root.applications.edit.case.newFile', {
        docId: docId,
        docFileId: docFileId
      });
    };

    $scope.linkPart = function (docFileId) {
      return $state.go('root.applications.edit.case.linkPart', {
        docFileId: docFileId
      });
    };

    $scope.newFile = function (docId) {
      return $state.go('root.applications.edit.case.newFile', {
        docId: docId
      });
    };

    $scope.newDocFile = function (docId) {
      return $state.go('root.applications.edit.case.newDocFile', {
        docId: docId
      });
    };

    $scope.childDoc = function (docId) {
      return $state.go('root.applications.edit.case.childDoc', {
        parentDocId: docId
      });
    };

    $scope.viewPart = function (value) {
      var state;

      if (value.setPartAlias === 'documentId') {
        state = 'root.persons.view.documentIds.edit';
      }
      else if (value.setPartAlias === 'education') {
        state = 'root.persons.view.documentEducations.edit';
      }
      else if (value.setPartAlias === 'employment') {
        state = 'root.persons.view.employments.edit';
      }
      else if (value.setPartAlias === 'medical') {
        state = 'root.persons.view.medicals.edit';
      }
      else if (value.setPartAlias === 'check') {
        state = 'root.persons.view.checks.edit';
      }
      else if (value.setPartAlias === 'training') {
        state = 'root.persons.view.documentTrainings.edit';
      }
      else if (value.setPartAlias === 'other') {
        state = 'root.persons.view.documentOthers.edit';
      }
      else if (value.setPartAlias === 'application') {
        state = 'root.persons.view.documentApplications.edit';
      }

      return $state.go(state, {
        id: $scope.application.lotId,
        ind: value.partIndex
      });
    };

    $scope.viewDoc = function (docId) {
      return $state.go('root.docs.edit.view', { id: docId });
    };
  }

  ApplicationsEditCaseCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    '$sce',
    'application',
    'DocStatus'
  ];

  angular.module('gva').controller('ApplicationsEditCaseCtrl', ApplicationsEditCaseCtrl);
}(angular, _));
