/*global angular, _*/
(function (angular, _) {
  'use strict';

  function ApplicationsEditCaseCtrl(
    $scope,
    $state,
    $stateParams,
    application,
    DocStatus
    ) {
    $scope.application = application;

    _.map(application.appDocCase, function (adc) {
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

      if (value.setPartAlias === 'personDocumentId') {
        state = 'root.persons.view.documentIds.edit';
      }
      else if (value.setPartAlias === 'personEducation') {
        state = 'root.persons.view.documentEducations.edit';
      }
      else if (value.setPartAlias === 'personEmployment') {
        state = 'root.persons.view.employments.edit';
      }
      else if (value.setPartAlias === 'personMedical') {
        state = 'root.persons.view.medicals.edit';
      }
      else if (value.setPartAlias === 'personCheck') {
        state = 'root.persons.view.checks.edit';
      }
      else if (value.setPartAlias === 'personTraining') {
        state = 'root.persons.view.documentTrainings.edit';
      }
      else if (value.setPartAlias === 'personOther') {
        state = 'root.persons.view.documentOthers.edit';
      }
      else if (value.setPartAlias === 'personApplication') {
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
    'application',
    'DocStatus'
  ];

  angular.module('gva').controller('ApplicationsEditCaseCtrl', ApplicationsEditCaseCtrl);
}(angular, _));
