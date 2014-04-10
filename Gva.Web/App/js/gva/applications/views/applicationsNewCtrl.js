/*global angular, _*/
(function (angular) {
  'use strict';

  function ApplicationsNewCtrl(
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
    selectedEquipment
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

    $scope.appModel = appModel;
    $scope.filter = $stateParams.filter;
    $scope.setPartAlias = '';

    $scope.newPerson = function () {
      return $state.go('root.applications.new.personNew');
    };
    $scope.selectPerson = function () {
      return $state.go('root.applications.new.personSelect');
    };

    $scope.newOrganization = function () {
      return $state.go('root.applications.new.organizationNew');
    };
    $scope.selectOrganization = function () {
      return $state.go('root.applications.new.organizationSelect');
    };

    $scope.newAircraft = function () {
      return $state.go('root.applications.new.aircraftNew');
    };
    $scope.selectAircraft = function () {
      return $state.go('root.applications.new.aircraftSelect');
    };

    $scope.newAirport = function () {
      return $state.go('root.applications.new.airportNew');
    };
    $scope.selectAirport = function () {
      return $state.go('root.applications.new.airportSelect');
    };

    $scope.newEquipment = function () {
      return $state.go('root.applications.new.equipmentNew');
    };
    $scope.selectEquipment = function () {
      return $state.go('root.applications.new.equipmentSelect');
    };

    $scope.cancel = function () {
      return $state.go('root.applications.search');
    };

    $scope.save = function () {
      return $scope.appForm.$validate()
      .then(function () {
        if ($scope.appForm.$valid) {
          var newApplication = {
            lotSetAlias: $scope.filter,
            lotId: null,
            doc: {
              docFormatTypeId: $scope.appModel.doc.docFormatTypeId,
              docFormatTypeName: $scope.appModel.doc.docFormatTypeName,
              docCasePartTypeId: $scope.appModel.doc.docCasePartTypeId,
              docCasePartTypeName: $scope.appModel.doc.docCasePartTypeName,
              docDirectionId: $scope.appModel.doc.docDirectionId,
              docDirectionName: $scope.appModel.doc.docDirectionName,
              docTypeGroupId: $scope.appModel.doc.docTypeGroupId,
              docTypeGroupName: $scope.appModel.doc.docTypeGroup.name,
              docTypeId: $scope.appModel.doc.docTypeId,
              docTypeName: $scope.appModel.doc.docType.name,
              docSubject: $scope.appModel.doc.docSubject
            }
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

          return Application.create(newApplication).$promise.then(function (app) {
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

  ApplicationsNewCtrl.$inject = [
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
    'selectedEquipment'
  ];

  ApplicationsNewCtrl.$resolve = {
    appModel: ['$q', 'Nomenclature',
      function ($q, Nomenclature) {
        return $q.all({
          docFormatTypes: Nomenclature.query({ alias: 'docFormatType' }).$promise,
          docCasePartTypes: Nomenclature.query({ alias: 'docCasePartType' }).$promise,
          docDirections: Nomenclature.query({ alias: 'docDirection' }).$promise
        }).then(function (res) {

          var doc = {
            docFormatTypeId: _(res.docFormatTypes).first().nomValueId,
            docFormatTypeName: _(res.docFormatTypes).first().name,
            docCasePartTypeId: _(res.docCasePartTypes).first().nomValueId,
            docCasePartTypeName: _(res.docCasePartTypes).first().name,
            docDirectionId: _(res.docDirections).first().nomValueId,
            docDirectionName: _(res.docDirections).first().name
          };

          return {
            doc: doc,
            docFormatTypes: res.docFormatTypes,
            docCasePartTypes: res.docCasePartTypes,
            docDirections: res.docDirections
          };
        });
      }
    ],
    selectedPerson: function () {
      return [];
    },
    selectedOrganization: function () {
      return [];
    },
    selectedAircraft: function () {
      return [];
    },
    selectedAirport: function () {
      return [];
    },
    selectedEquipment: function () {
      return [];
    }
  };

  angular.module('gva').controller('ApplicationsNewCtrl', ApplicationsNewCtrl);
}(angular, _));
