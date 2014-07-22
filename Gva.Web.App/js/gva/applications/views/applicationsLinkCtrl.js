/*global angular, _*/
(function (angular) {
  'use strict';

  function ApplicationsLinkCtrl(
    $scope,
    $state,
    $stateParams,
    $sce,
    Applications,
    appModel,
    selectedDoc
    ) {
    if (selectedDoc.length > 0) {
      appModel.doc = selectedDoc.pop();
      appModel.doc.correspondentNames = $sce.trustAsHtml(appModel.doc.correspondentNames);
    }

    $scope.appModel = appModel;
    $scope.filter = $stateParams.filter;
    $scope.setPartAlias = '';

    $scope.selectDoc = function () {
      var regUri = $scope.appModel.doc ? $scope.appModel.doc.regUri : null;

      return $state.go('root.applications.link.docSelect', { hasLot: false, regUri: regUri });
    };

    $scope.cancel = function () {
      return $state.go('root.applications.search');
    };

    $scope.clear = function () {
      $scope.appModel.doc = null;
    };

    $scope.link = function () {
      return $scope.appForm.$validate().then(function () {
        if ($scope.appForm.$valid && $scope.appModel.doc) {
          var newApplication = {
            lotId: $scope.appModel.lot.id,
            docId: $scope.appModel.doc.docId
          };

          //todo make it better
          if ($scope.filter === 'Person') {
            $scope.setPartAlias = 'personApplication';
          }
          else if ($scope.filter === 'Organization') {
            $scope.setPartAlias = 'organizationApplication';
          }
          else if ($scope.filter === 'Aircraft') {
            $scope.setPartAlias = 'aircraftApplication';
          }
          else if ($scope.filter === 'Airport') {
            $scope.setPartAlias = 'airportApplication';
          }
          else if ($scope.filter === 'Equipment') {
            $scope.setPartAlias = 'equipmentApplication';
          }

          return Applications.link(newApplication).$promise.then(function (app) {
            if ($scope.appModel.doc.isElectronic) {
              return $state.go('root.applications.edit.case', {
                id: app.applicationId
              });
            }
            else {
              return $state.go('root.applications.edit.case.addPart', {
                id: app.applicationId,
                docId: app.docId,
                setPartAlias: $scope.setPartAlias
              });
            }
          });
        }
      });
    };
  }

  ApplicationsLinkCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    '$sce',
    'Applications',
    'appModel',
    'selectedDoc'
  ];

  ApplicationsLinkCtrl.$resolve = {
    appModel: function () {
      return {
        lot: {}
      };
    },
    selectedDoc: function () {
      return [];
    }
  };

  angular.module('gva').controller('ApplicationsLinkCtrl', ApplicationsLinkCtrl);
}(angular, _));
