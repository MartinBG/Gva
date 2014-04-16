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
    selectedAirport,
    selectedEquipment,
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
    if (selectedAirport.length > 0) {
      appModel.airport = {
        id: selectedAirport.pop()
      };
    }
    if (selectedEquipment.length > 0) {
      appModel.equipment = {
        id: selectedEquipment.pop()
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

    $scope.newAirport = function () {
      return $state.go('root.applications.link.airportNew');
    };
    $scope.selectAirport = function () {
      return $state.go('root.applications.link.airportSelect');
    };

    $scope.newEquipment = function () {
      return $state.go('root.applications.link.equipmentNew');
    };
    $scope.selectEquipment = function () {
      return $state.go('root.applications.link.equipmentSelect');
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
          else if ($scope.filter === 'Airport') {
            newApplication.lotId = $scope.appModel.airport.id;
            $scope.setPartAlias = 'airportApplication';
          }
          else if ($scope.filter === 'Equipment') {
            newApplication.lotId = $scope.appModel.equipment.id;
            $scope.setPartAlias = 'equipmentApplication';
          }

          return Application.link(newApplication).$promise.then(function (app) {
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
    '$q',
    '$scope',
    '$state',
    '$stateParams',
    'Application',
    'appModel',
    'selectedPerson',
    'selectedOrganization',
    'selectedAircraft',
    'selectedAirport',
    'selectedEquipment',
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
    selectedAirport: function () {
      return [];
    },
    selectedEquipment: function () {
      return [];
    },
    selectedDoc: function () {
      return [];
    }
  };

  angular.module('gva').controller('ApplicationsLinkCtrl', ApplicationsLinkCtrl);
}(angular, _));
