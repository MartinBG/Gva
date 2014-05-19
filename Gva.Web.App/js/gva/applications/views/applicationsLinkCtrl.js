/*global angular, _*/
(function (angular) {
  'use strict';

  function ApplicationsLinkCtrl(
    $q,
    $scope,
    $state,
    $stateParams,
    $sce,
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

    if (selectedDoc.length > 0) {
      appModel.doc = selectedDoc.pop();
      appModel.doc.correspondentNames = $sce.trustAsHtml(appModel.doc.correspondentNames);
    }

    $scope.appModel = appModel;
    $scope.filter = $stateParams.filter;
    $scope.setPartAlias = '';

    $scope.newPerson = function () {
      var personData = {};
      if ($scope.appModel.doc) {
        if ($scope.appModel.doc.docCorrespondents.length > 0) {
          personData.uin = $scope.appModel.doc.docCorrespondents[0].bgCitizenUIN;
          personData.firstName = $scope.appModel.doc.docCorrespondents[0].bgCitizenFirstName;
          personData.lastName = $scope.appModel.doc.docCorrespondents[0].bgCitizenLastName;
        }
      }

      return $state.go('root.applications.link.personNew', null, null, personData);
    };
    $scope.selectPerson = function () {
      var uin, names, firstName, lastName;
      if ($scope.appModel.doc) {
        if ($scope.appModel.doc.docCorrespondents.length > 0) {
          uin = $scope.appModel.doc.docCorrespondents[0].bgCitizenUIN;
          firstName = $scope.appModel.doc.docCorrespondents[0].bgCitizenFirstName;
          lastName = $scope.appModel.doc.docCorrespondents[0].bgCitizenLastName;

          if (!!firstName && !!lastName) {
            names = firstName + ' ' + lastName;
          }
        }
      }

      return $state.go('root.applications.link.personSelect', { uin: uin, names: names });
    };

    $scope.newOrganization = function () {
      var organizationData = {};
      if ($scope.appModel.doc) {
        if ($scope.appModel.doc.docCorrespondents.length > 0) {
          organizationData.name = $scope.appModel.doc.docCorrespondents[0].legalEntityName;
          organizationData.uin = $scope.appModel.doc.docCorrespondents[0].legalEntityBulstat;
        }
      }

      return $state.go('root.applications.link.organizationNew', null, null, organizationData);
    };
    $scope.selectOrganization = function () {
      var uin, name;
      if ($scope.appModel.doc) {
        if ($scope.appModel.doc.docCorrespondents.length > 0) {
          uin = $scope.appModel.doc.docCorrespondents[0].legalEntityBulstat;
          name = $scope.appModel.doc.docCorrespondents[0].legalEntityName;
        }
      }

      return $state.go('root.applications.link.organizationSelect', { uin: uin, name: name });
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
      return $scope.appForm.$validate()
      .then(function () {
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
    '$sce',
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
