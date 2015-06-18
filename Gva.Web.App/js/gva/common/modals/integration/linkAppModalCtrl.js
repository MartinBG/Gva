/*global angular, _*/
(function (angular, _) {
  'use strict';

  function LinkAppModalCtrl(
    $scope,
    $state,
    $stateParams,
    $modalInstance,
    scModalParams,
    Nomenclatures,
    Integration,
    Persons,
    Aircrafts,
    Organizations,
    caseApplications
  ) {

    // common
    $scope.currentTab = scModalParams.isFromPortal ? 'choosePortalApp' : 'chooseSet';
    $scope.selection = {};
    $scope.isFromPortal = scModalParams.isFromPortal;

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };

    $scope.chooseLot = function (lotId) {
      return Integration.createApplication({}, {
        lotId: lotId,
        docId: $scope.selection.docId,
        applicationType: $scope.selection.gvaApplicationType
      }).$promise.then(function (res) {
        $modalInstance.close(res);
      });
    };

    // choosePortalApp
    $scope.choosePortalApp = {};

    $scope.choosePortalApp.appSelectOptions = {
      allowClear: true,
      placeholder: ' ',
      data: _.map(caseApplications, function (app) {
        app.id = app.docId;
        app.text = app.docRegUri + ' ' + app.docDocTypeName;

        return app;
      })
    };

    $scope.choosePortalApp.selectedAppChanged = function () {
      if ($scope.choosePortalApp.selectedApp) {
        $scope.selection.set = $scope.choosePortalApp.selectedApp.set;
      }
      else {
        $scope.selection.set = null;
      }
    };

    $scope.choosePortalApp.forward = function () {
      return $scope.choosePortalApp.form.$validate().then(function () {
        if ($scope.choosePortalApp.form.$valid) {

          $scope.selection = {
            docId: $scope.choosePortalApp.selectedApp.docId,
            set: $scope.choosePortalApp.selectedApp.set,
            caseTypeId: $scope.choosePortalApp.selectedApp.caseTypeId
          };

          switch ($scope.choosePortalApp.selectedApp.set) {
            case 'Person':
              $scope.selection.personData = $scope.choosePortalApp.selectedApp.personData;
              break;

            case 'Aircraft':
              $scope.selection.aircraftData = $scope.choosePortalApp.selectedApp.aircraftData;
              break;

            case 'Organization':
              $scope.selection.organizationData =
                $scope.choosePortalApp.selectedApp.organizationData;
              break;

            default:
              throw new Error('Unsupported set.');
          }

          return $scope.chooseGvaAppType.init();
        }
      });
    };

    // chooseSet
    $scope.chooseSet = {};

    $scope.chooseSet.setSelectOptions = {
      allowClear: true,
      placeholder: ' ',
      data: [
        { id: 'Person', text: 'Физическо лице' },
        { id: 'Aircraft', text: 'ВС' },
        { id: 'Organization', text: 'Организация' }
      ]
    };

    $scope.chooseSet.selectedSetChanged = function () {
      if ($scope.chooseSet.selectedSet) {
        $scope.selection.set = $scope.chooseSet.selectedSet.id;
      }
      else {
        $scope.selection.set = null;
      }
    };

    $scope.chooseSet.hasNoAppChanged = function () {
      $scope.selection.hasNoApp = $scope.chooseSet.hasNoApp;
    };

    $scope.chooseSet.forward = function () {
      return $scope.chooseSet.form.$validate().then(function () {
        if ($scope.chooseSet.form.$valid) {

          $scope.selection = {
            docId: scModalParams.docId,
            set: $scope.chooseSet.selectedSet.id,
            hasNoApp: $scope.chooseSet.hasNoApp
          };

          if ($scope.selection.hasNoApp) {
            if ($scope.selection.set === 'Person') {
              return $scope.chooseLotPerson.init();
            }
            else if ($scope.selection.set === 'Aircraft') {
              return $scope.chooseLotAircraft.init();
            }
            else if ($scope.selection.set === 'Organization') {
              return $scope.chooseLotOrganization.init();
            }
          }
          else {
            return $scope.chooseGvaAppType.init();
          }
        }
      });
    };

    // chooseGvaAppType
    $scope.chooseGvaAppType = {};

    $scope.chooseGvaAppType.init = function () {
      if (scModalParams.isFromPortal) {
        // caseType is fixed, so no choice of caseType
        $scope.chooseGvaAppType.searchParams = {
          code: null,
          name: null
        };
      }
      else {
        $scope.chooseGvaAppType.searchParams = {
          caseTypeId: null,
          code: null,
          name: null
        };
      }

      return $scope.chooseGvaAppType.search().then(function () {
        $scope.currentTab = 'chooseGvaAppType';
      });
    };

    $scope.chooseGvaAppType.back = function () {
      $scope.currentTab = scModalParams.isFromPortal ? 'choosePortalApp' : 'chooseSet';
    };

    $scope.chooseGvaAppType.search = function () {
      var params = {
        alias: 'applicationTypes',
        set: $scope.selection.set,
        caseTypeId: scModalParams.isFromPortal ?
          $scope.selection.caseTypeId :
          $scope.chooseGvaAppType.searchParams.caseTypeId,
        code: $scope.chooseGvaAppType.searchParams.code,
        name: $scope.chooseGvaAppType.searchParams.name
      };

      return Nomenclatures.query(params).$promise.then(function (appTypes) {
        $scope.chooseGvaAppType.appTypes = appTypes;
      });
    };

    $scope.chooseGvaAppType.choose = function (appType) {
      $scope.selection.gvaApplicationType = appType;

      if (!$scope.selection.caseTypeId) {
        $scope.selection.caseTypeId = $scope.chooseGvaAppType.searchParams.caseTypeId;
      }

      if ($scope.selection.set === 'Person') {
        return $scope.chooseLotPerson.init();
      }
      else if ($scope.selection.set === 'Aircraft') {
        return $scope.chooseLotAircraft.init();
      }
      else if ($scope.selection.set === 'Organization') {
        return $scope.chooseLotOrganization.init();
      }
    };

    // chooseLotPerson
    $scope.chooseLotPerson = {};

    $scope.chooseLotPerson.init = function () {
      $scope.chooseLotPerson.filters = {
        lin: null,
        uin: null,
        names: null,
        caseType: null
      };

      if ($scope.selection.personData) {
        _.assign($scope.chooseLotPerson.filters, {
          lin: $scope.selection.personData.lin,
          uin: $scope.selection.personData.uin,
          names: ($scope.selection.personData.firstName || '') +
                 ' ' +
                 ($scope.selection.personData.lastName || '')
        });
      }

      if ($scope.selection.caseTypeId) {
        _.assign($scope.chooseLotPerson.filters, {
          caseType: $scope.selection.caseTypeId
        });
      }

      return $scope.chooseLotPerson.search().then(function () {
        $scope.currentTab = 'chooseLotPerson';
      });
    };

    $scope.chooseLotPerson.search = function () {
      return Persons.query($scope.chooseLotPerson.filters).$promise.then(function (persons) {
        $scope.chooseLotPerson.items = persons;
      });
    };

    $scope.chooseLotPerson.back = function () {
      $scope.currentTab = $scope.selection.hasNoApp ? 'chooseSet' : 'chooseGvaAppType';
    };

    $scope.chooseLotPerson.create = function () {
      return $scope.createLotPerson.init();
    };

    // chooseLotAircraft
    $scope.chooseLotAircraft = {};

    $scope.chooseLotAircraft.init = function () {
      $scope.chooseLotAircraft.filters = {
        mark: null,
        manSN: null,
        modelAlt: null,
        aircraftProducer: null
      };

      if ($scope.selection.aircraftData) {
        var producer = $scope.selection.aircraftData.aircraftProducer;

        _.assign($scope.chooseLotAircraft.filters, {
          mark: null,
          manSN: null,
          modelAlt: null,
          aircraftProducer: producer && producer.name || ''
        });
      }

      return $scope.chooseLotAircraft.search().then(function () {
        $scope.currentTab = 'chooseLotAircraft';
      });
    };

    $scope.chooseLotAircraft.search = function () {
      return Aircrafts.query($scope.chooseLotAircraft.filters).$promise.then(function (aircrafts) {
        $scope.chooseLotAircraft.items = aircrafts;
      });
    };

    $scope.chooseLotAircraft.back = function () {
      $scope.currentTab = $scope.selection.hasNoApp ? 'chooseSet' : 'chooseGvaAppType';
    };

    $scope.chooseLotAircraft.create = function () {
      return $scope.createLotAircraft.init();
    };

    // chooseLotOrganization
    $scope.chooseLotOrganization = {};

    $scope.chooseLotOrganization.init = function () {
      $scope.chooseLotOrganization.filters = {
        organizationName: null,
        caseTypeId: null,
        uin: null
      };

      if ($scope.selection.organizationData) {
        _.assign($scope.chooseLotOrganization.filters, {
          organizationName: $scope.selection.organizationData.name || '',
          caseTypeId: null,
          uin: $scope.selection.organizationData.uin
        });
      }

      return $scope.chooseLotOrganization.search().then(function () {
        $scope.currentTab = 'chooseLotOrganization';
      });
    };

    $scope.chooseLotOrganization.search = function () {
      return Organizations.query($scope.chooseLotAircraft.filters).$promise
        .then(function (organizations) {
          $scope.chooseLotOrganization.items = organizations;
        });
    };

    $scope.chooseLotOrganization.back = function () {
      $scope.currentTab = $scope.selection.hasNoApp ? 'chooseSet' : 'chooseGvaAppType';
    };

    $scope.chooseLotOrganization.create = function () {
      return $scope.createLotOrganization.init();
    };

    // createLotPerson
    $scope.createLotPerson = {};

    $scope.createLotPerson.init = function () {
      return Persons.newPerson({ extendedVersion: false }).$promise.then(function (newPerson) {
        $scope.currentTab = 'createLotPerson';
        $scope.createLotPerson.newLot = newPerson;
        if ($scope.selection.personData) {
          _.assign($scope.createLotPerson.newLot.personData, $scope.selection.personData);
        }
      });
    };

    $scope.createLotPerson.save = function () {
      return $scope.createLotPerson.form.$validate().then(function () {
        if ($scope.createLotPerson.form.$valid) {
          return Persons.save($scope.createLotPerson.newLot).$promise.then(function (result) {
            return $scope.chooseLot(result.lotId);
          });
        }
      });
    };

    $scope.createLotPerson.back = function () {
      $scope.currentTab = 'chooseLotPerson';
    };

    // createLotAircraft
    $scope.createLotAircraft = {};

    $scope.createLotAircraft.init = function () {
      return Aircrafts.newAircraft().$promise
        .then(function (newAircraft) {
          $scope.currentTab = 'createLotAircraft';
          $scope.createLotAircraft.newLot = newAircraft;
          if ($scope.selection.aircraftData) {
            _.assign($scope.createLotAircraft.newLot.personData, $scope.selection.aircraftData);
          }
        });
    };

    $scope.createLotAircraft.save = function () {
      return $scope.createLotAircraft.form.$validate().then(function () {
        if ($scope.createLotAircraft.form.$valid) {
          return Aircrafts.save($scope.createLotAircraft.newLot).$promise.then(function (result) {
            return $scope.chooseLot(result.lotId);
          });
        }
      });
    };

    $scope.createLotAircraft.back = function () {
      $scope.currentTab = 'chooseLotAircraft';
    };

    // createLotOrganization
    $scope.createLotOrganization = {};

    $scope.createLotOrganization.init = function () {
      return Organizations.newOrganization().$promise
        .then(function (newOrganization) {
          $scope.currentTab = 'createLotOrganization';
          $scope.createLotOrganization.newLot = newOrganization;
          if ($scope.selection.aircraftData) {
            _.assign(
              $scope.createLotOrganization.newLot.organizationData,
              $scope.selection.organizationData);
          }
        });
    };

    $scope.createLotOrganization.save = function () {
      return $scope.createLotOrganization.form.$validate().then(function () {
        if ($scope.createLotOrganization.form.$valid) {
          return Organizations.save($scope.createLotOrganization.newLot).$promise
            .then(function (result) {
              return $scope.chooseLot(result.lotId);
            });
        }
      });
    };

    $scope.createLotOrganization.back = function () {
      $scope.currentTab = 'chooseLotOrganization';
    };
  }

  LinkAppModalCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    '$modalInstance',
    'scModalParams',
    'Nomenclatures',
    'Integration',
    'Persons',
    'Aircrafts',
    'Organizations',
    'caseApplications'
  ];

  LinkAppModalCtrl.$resolve = {
    caseApplications: [
      'Integration',
      'scModalParams',
      function (Integration, scModalParams) {
        return Integration.getCaseApplications({ docId: scModalParams.docId }).$promise;
      }
    ]
  };

  angular.module('gva').controller('LinkAppModalCtrl', LinkAppModalCtrl);
}(angular, _));
