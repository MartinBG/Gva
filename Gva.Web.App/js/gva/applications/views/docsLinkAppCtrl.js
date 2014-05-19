/*global angular*/
(function (angular) {
  'use strict';

  function DocsLinkAppCtrl(
    $scope,
    $state,
    $stateParams,
    Application,
    selectedPerson,
    selectedOrganization,
    selectedAircraft,
    selectedAirport,
    selectedEquipment,
    appModel,
    doc
  ) {
    if (selectedPerson.length > 0) {
      appModel.lot = {
        id: selectedPerson.pop()
      };
    }
    if (selectedOrganization.length > 0) {
      appModel.lot = {
        id: selectedOrganization.pop()
      };
    }
    if (selectedAircraft.length > 0) {
      appModel.lot = {
        id: selectedAircraft.pop()
      };
    }
    if (selectedAirport.length > 0) {
      appModel.lot = {
        id: selectedAirport.pop()
      };
    }
    if (selectedEquipment.length > 0) {
      appModel.lot = {
        id: selectedEquipment.pop()
      };
    }

    $scope.$watch('appModel.lotSet', function (newValue, oldValue) {
      if (newValue !== oldValue) {
        appModel.lot = null;
      }
    });

    $scope.doc = doc;
    $scope.appModel = appModel;

    $scope.newPerson = function () {
      return $state.go('root.docs.edit.case.linkApp.personNew');
    };
    $scope.selectPerson = function () {
      return $state.go('root.docs.edit.case.linkApp.personSelect');
    };

    $scope.newOrganization = function () {
      return $state.go('root.docs.edit.case.linkApp.organizationNew');
    };
    $scope.selectOrganization = function () {
      return $state.go('root.docs.edit.case.linkApp.organizationSelect');
    };

    $scope.newAircraft = function () {
      return $state.go('root.docs.edit.case.linkApp.aircraftNew');
    };
    $scope.selectAircraft = function () {
      return $state.go('root.docs.edit.case.linkApp.aircraftSelect');
    };

    $scope.newAirport = function () {
      return $state.go('root.docs.edit.case.linkApp.airportNew');
    };
    $scope.selectAirport = function () {
      return $state.go('root.docs.edit.case.linkApp.airportSelect');
    };

    $scope.newEquipment = function () {
      return $state.go('root.docs.edit.case.linkApp.equipmentNew');
    };
    $scope.selectEquipment = function () {
      return $state.go('root.docs.edit.case.linkApp.equipmentSelect');
    };

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

          return Application.link(newApplication).$promise.then(function (app) {
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
    'Application',
    'selectedPerson',
    'selectedOrganization',
    'selectedAircraft',
    'selectedAirport',
    'selectedEquipment',
    'appModel',
    'doc'
  ];

  DocsLinkAppCtrl.$resolve = {
    appModel: function () {
      return {
        lotSet: null
      };
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
    }
  };

  angular.module('gva').controller('DocsLinkAppCtrl', DocsLinkAppCtrl);
}(angular));
