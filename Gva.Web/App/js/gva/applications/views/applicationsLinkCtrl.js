/*global angular, _*/
(function (angular) {
  'use strict';

  function ApplicationsLinkCtrl(
    $q,
    $scope,
    $state,
    $stateParams,
    Application,
    appModel,
    selectedPerson,
    selectedOrganization,
    selectedAircraft,
    selectedDoc
    ) {
    if (selectedPerson.length > 0) {
      appModel.person = {
        id: selectedPerson.pop()
      };
    }
    if (selectedOrganization.length > 0) {
      appModel.organization = {
        id: selectedOrganization.pop()
      };
    }
    if (selectedAircraft.length > 0) {
      appModel.aircraft = {
        id: selectedAircraft.pop()
      };
    }

    if (selectedDoc.length > 0) {
      appModel.doc = selectedDoc.pop();
    }

    $scope.appModel = appModel;
    $scope.filter = $stateParams.filter;
    $scope.setPartAlias = '';

    $scope.newPerson = function () {
      return $state.go('root.applications.link.personNew');
    };
    $scope.selectPerson = function () {
      return $state.go('root.applications.link.personSelect');
    };

    $scope.newOrganization = function () {
      return $state.go('root.applications.link.organizationNew');
    };
    $scope.selectOrganization = function () {
      return $state.go('root.applications.link.organizationSelect');
    };

    $scope.newAircraft = function () {
      return $state.go('root.applications.link.aircraftNew');
    };
    $scope.selectAircraft = function () {
      return $state.go('root.applications.link.aircraftSelect');
    };

    $scope.selectDoc = function () {
      return $state.go('root.applications.link.docSelect', { hasLot: false });
    };

    $scope.cancel = function () {
      return $state.go('root.applications.search');
    };

    $scope.clear = function () {
      $scope.appModel.doc = null;
    };

    $scope.link = function () {
      return $scope.appForm.$validate()
      .then(function () {
        if ($scope.appForm.$valid) {

          var newApplication = {
            lotId: null,
            docId: $scope.appModel.doc.docId
          };

          if ($scope.filter === 'Person') {
            newApplication.lotId = $scope.appModel.person.id;
            $scope.setPartAlias = 'personApplication';
          }
          else if ($scope.filter === 'Organization') {
            newApplication.lotId = $scope.appModel.organization.id;
            $scope.setPartAlias = 'organizationApplication';
          }
          else if ($scope.filter === 'Aircraft') {
            newApplication.lotId = $scope.appModel.aircraft.id;
            $scope.setPartAlias = 'aircraftApplication';
          }

          return Application.link(newApplication).$promise.then(function (app) {
            return $state.go('root.applications.edit.case.addPart', {
              id: app.applicationId,
              docId: app.docId,
              setPartAlias: $scope.setPartAlias
            });
          });
        }
      });
    };
  }

  ApplicationsLinkCtrl.$inject = [
    '$q',
    '$scope',
    '$state',
    '$stateParams',
    'Application',
    'appModel',
    'selectedPerson',
    'selectedOrganization',
    'selectedAircraft',
    'selectedDoc'
  ];

  ApplicationsLinkCtrl.$resolve = {
    appModel: function () {
      return {};
    },
    selectedAircraft: function () {
      return [];
    },
    selectedOrganization: function () {
      return [];
    },
    selectedPerson: function () {
      return [];
    },
    selectedDoc: function () {
      return [];
    }
  };

  angular.module('gva').controller('ApplicationsLinkCtrl', ApplicationsLinkCtrl);
}(angular, _));
