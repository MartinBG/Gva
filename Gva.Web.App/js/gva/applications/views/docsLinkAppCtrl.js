/*global angular*/
(function (angular) {
  'use strict';

  function DocsLinkAppCtrl(
    $scope,
    $state,
    $stateParams,
    Applications,
    appModel,
    doc
  ) {
    $scope.$watch('appModel.lotSet', function (newValue, oldValue) {
      if (newValue !== oldValue) {
        appModel.lot = {};
      }
    });

    $scope.doc = doc;
    $scope.appModel = appModel;

    $scope.cancel = function () {
      return $state.go('^');
    };

    $scope.save = function () {
      var setPartAlias;

      return $scope.linkAppForm.$validate().then(function () {
        if ($scope.linkAppForm.$valid) {
          var newApplication = {
            lotId: $scope.appModel.lot.id,
            docId: $scope.doc.docId
          };

          //todo make it better
          if ($scope.appModel.lotSet.alias === 'Person') {
            setPartAlias = 'personApplication';
          }
          else if ($scope.appModel.lotSet.alias === 'Organization') {
            setPartAlias = 'organizationApplication';
          }
          else if ($scope.appModel.lotSet.alias === 'Aircraft') {
            setPartAlias = 'aircraftApplication';
          }
          else if ($scope.appModel.lotSet.alias === 'Airport') {
            setPartAlias = 'airportApplication';
          }
          else if ($scope.appModel.lotSet.alias === 'Equipment') {
            setPartAlias = 'equipmentApplication';
          }

          return Applications.link(newApplication).$promise.then(function (app) {
            if ($scope.doc.isElectronic) {
              return $state.go('root.applications.edit.case', {
                id: app.applicationId
              });
            }
            else {
              return $state.go('root.applications.edit.case.addPart', {
                id: app.applicationId,
                docId: app.docId,
                setPartAlias: setPartAlias
              });
            }
          });
        }
      });
    };
  }

  DocsLinkAppCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Applications',
    'appModel',
    'doc'
  ];

  DocsLinkAppCtrl.$resolve = {
    appModel: function () {
      return {
        lotSet: null
      };
    }
  };

  angular.module('gva').controller('DocsLinkAppCtrl', DocsLinkAppCtrl);
}(angular));
