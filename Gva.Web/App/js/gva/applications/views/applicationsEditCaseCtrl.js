/*global angular, _*/
(function (angular, _) {
  'use strict';

  function ApplicationsEditCaseCtrl(
    $scope,
    $state,
    $sce,
    application,
    applicationCommonData
    ) {
    $scope.application = application;

    _.map(application.applicationDocCase, function (adc) {
      if (!adc.docDataHtml.$$unwrapTrustedValue) {
        adc.docDataHtml = $sce.trustAsHtml(adc.docDataHtml);
      }
      if (!adc.docDescriptionHtml.$$unwrapTrustedValue) {
        adc.docDescriptionHtml = $sce.trustAsHtml(adc.docDescriptionHtml);
      }

      return adc;
    });


    $scope.linkNew = function (docId, appLotFile) {
      applicationCommonData.isLinkNew = true;
      applicationCommonData.currentDocId = appLotFile.docId;
      applicationCommonData.docFileKey = appLotFile.file.key;
      applicationCommonData.docFileName = appLotFile.file.name;
      applicationCommonData.name = appLotFile.name;
      applicationCommonData.docFileId = appLotFile.docFileId;
      applicationCommonData.docFileKindId = appLotFile.docFileKindId;
      applicationCommonData.docFileTypeId = appLotFile.docFileTypeId;

      return $state.go('root.applications.edit.case.newFile');
    };

    $scope.linkPart = function (docId, docFileId) {
      applicationCommonData.docFileId = docFileId;
      applicationCommonData.currentDocId = docId;

      return $state.go('root.applications.edit.case.linkPart');
    };

    $scope.newFile = function (docId) {
      applicationCommonData.isLinkNew = false;
      applicationCommonData.currentDocId = docId;

      return $state.go('root.applications.edit.case.newFile');
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
    '$sce',
    'application',
    'applicationCommonData'
  ];

  ApplicationsEditCaseCtrl.$resolve = {
    applicationCommonData: function () {
      return {};
    }
  };

  angular.module('gva').controller('ApplicationsEditCaseCtrl', ApplicationsEditCaseCtrl);
}(angular, _));
