﻿/*global angular, _*/
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
      //applicationCommonData.docFileId = docFileId;
      //applicationCommonData.currentDocId = docId;

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

    $scope.viewPart = function (docCase) {
      var state;

      if (docCase.setPartAlias === 'documentId') {
        state = 'root.persons.view.documentIds.edit';
      }
      else if (docCase.setPartAlias === 'education') {
        state = 'root.persons.view.documentEducations.edit';
      }
      else if (docCase.setPartAlias === 'employment') {
        state = 'root.persons.view.employments.edit';
      }
      else if (docCase.setPartAlias === 'medical') {
        state = 'root.persons.view.medicals.edit';
      }
      else if (docCase.setPartAlias === 'check') {
        state = 'root.persons.view.checks.edit';
      }
      else if (docCase.setPartAlias === 'training') {
        state = 'root.persons.view.documentTrainings.edit';
      }
      else if (docCase.setPartAlias === 'other') {
        state = 'root.persons.view.documentOthers.edit';
      }
      else if (docCase.setPartAlias === 'application') {
        state = 'root.persons.view.documentApplications.edit';
      }

      return $state.go(state, {
        id: $scope.application.lotId,
        ind: docCase.partIndex
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
