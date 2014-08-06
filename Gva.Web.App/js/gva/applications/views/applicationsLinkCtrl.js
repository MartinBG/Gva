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
    scModal
    ) {
    $scope.appModel = appModel;
    $scope.filter = $stateParams.filter;
    $scope.setPartAlias = '';

    $scope.cancel = function () {
      return $state.go('root.applications.search');
    };

    $scope.clear = function () {
      $scope.appModel.doc = null;
    };

    $scope.selectDoc = function () {
      var modalInstance = scModal.open('chooseDoc', { filter: $scope.filter });

      modalInstance.result.then(function (doc) {
        $scope.appModel.doc = doc;
        $scope.appModel.doc.correspondentNames = $sce.trustAsHtml(doc.correspondentNames);
      });

      return modalInstance.opened;
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
    'scModal'
  ];

  ApplicationsLinkCtrl.$resolve = {
    appModel: function () {
      return {
        lot: {}
      };
    }
  };

  angular.module('gva').controller('ApplicationsLinkCtrl', ApplicationsLinkCtrl);
}(angular, _));
