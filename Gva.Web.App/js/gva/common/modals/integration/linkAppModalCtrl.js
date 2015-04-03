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
    caseApplications,
    scDatatableConfig
  ) {
    $scope.filters = {};
    $scope.wrapper = {};
    $scope.form = {};
    $scope.newLot = {};

    _.forOwn($stateParams, function (value, param) {
      if (value !== null && value !== undefined) {
        $scope.filters[param] = value;
      }
    });

    var queryPersons = function (offset, limit) {
      var params = {
        lin: $scope.filters.lin,
        uin: $scope.filters.uin,
        names: $scope.filters.names,
        licences: $scope.filters.licences,
        ratings: $scope.filters.ratings,
        organization: $scope.filters.organization,
        caseType: $scope.filters.caseType,
        offset: offset,
        limit: limit
      };

      return Persons.query(params).$promise;
    };

    var queryAircrafts = function (offset, limit) {
      var params = {
        manSN: $scope.filters.manSN,
        modelAlt: $scope.filters.modelAlt,
        mark: $scope.filters.mark,
        airCategory: $scope.filters.airCategory,
        aircraftProducer: $scope.filters.aircraftProducer,
        offset: offset,
        limit: limit
      };

      return Aircrafts.query(params).$promise;
    };

    var queryOrganizations = function (offset, limit) {
      var params = {
        cao: $scope.filters.cao,
        uin: $scope.filters.uin,
        dateValidTo: $scope.filters.dateValidTo,
        dateCaoValidTo: $scope.filters.dateCaoValidTo,
        organizationName: $scope.filters.organizationName,
        caseTypeId: $scope.filters.caseTypeId,
        offset: offset,
        limit: limit
      };

      return Organizations.query(params).$promise;
    };

    $scope.currentTab = 'choosePortalApp';

    $scope.selectOptions = {
      allowClear: true,
      placeholder: ' ',
      data: _.map(caseApplications, function (doc) {
        return {
          id: doc.docId,
          text: doc.docRegUri + ' ' + doc.docDocTypeName
        };
      })
    };

    $scope.$watch('wrapper.selectedAppKey', function () {
      if ($scope.wrapper.selectedAppKey) {
        $scope.wrapper.selectedApp = _.find(caseApplications, {
          docId: $scope.wrapper.selectedAppKey.id
        });

        $scope.selectedCaseType = { 
          id: $scope.wrapper.selectedApp.caseType.gvaCaseTypeId,
          text: $scope.wrapper.selectedApp.caseType.name
        };

        $scope.set = $scope.wrapper.selectedApp.set;
      } else {
        $scope.wrapper.selectedApp = null;
      }
    });

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };
    $scope.choosePortalApp = function () {
      return $scope.form.choosePortalAppForm.$validate().then(function () {
        if ($scope.form.choosePortalAppForm.$valid) {
          Nomenclatures.query({
            alias: 'applicationTypes',
            caseTypeAlias: $scope.wrapper.selectedApp.caseType.alias
          }).$promise
          .then(function (appTypes) {
            $scope.appTypes = appTypes;
            $scope.currentTab = 'chooseGvaApp';
            
            $scope.searchParams = {
              code: null,
              alias: 'applicationTypes',
              caseType: $scope.selectedCaseType,
              name: null
            };
          });

        }
      });
    };

    $scope.chooseAppType = function (appType) {
      $scope.wrapper.selectedApp.gvaApplicationType = appType;
      $scope.set = $scope.wrapper.selectedApp.set.toLowerCase();

      $scope.currentTab = 'chooseLot';

      if ($scope.set === 'person') {
        var firstName = $scope.wrapper.selectedApp.personData.firstName ?
          $scope.wrapper.selectedApp.personData.firstName : '';
        var lastName = $scope.wrapper.selectedApp.personData.lastName ?
          $scope.wrapper.selectedApp.personData.lastName : '';
        $scope.filters = {
          lin: $scope.wrapper.selectedApp.personData.lin,
          uin: $scope.wrapper.selectedApp.personData.uin,
          names: firstName + ' ' + lastName,
          caseType: $scope.wrapper.selectedApp.caseType.gvaCaseTypeId
        };

        return $scope.searchPersons();
      } else if ($scope.set === 'aircraft') {
        var producer = $scope.wrapper.selectedApp.aircraftData.aircraftProducer;
        $scope.filters = {
          manSN: null,
          mark: null,
          modelAlt:null,
          aircraftProducer: producer && producer.name ? producer.name  : ''
        };

        return $scope.searchAircrafts();
      } else if ($scope.set === 'organization') {
        var name = $scope.wrapper.selectedApp.organizationData.name?
          $scope.wrapper.selectedApp.organizationData.name : '';

        $scope.filters = {
          caseTypeId: null,
          uin: $scope.wrapper.selectedApp.organizationData.uin,
          organizationName: name
        };

        return $scope.searchOrganizations();
      }
    };

    $scope.searchAppTypes = function () {
     var params = {
       code: $scope.searchParams.code,
        alias: 'applicationTypes',
        caseTypeAlias: $scope.wrapper.selectedApp.caseType.alias,
        name: $scope.searchParams.name
      };
      return Nomenclatures.query(params)
        .$promise.then(function (appTypes) {
          $scope.appTypes = appTypes;
        });
    };

    $scope.searchPersons = function () {
      return queryPersons(0, scDatatableConfig.defaultPageSize)
        .then(function (persons) {
          $scope.items = persons;
        });
    };

    $scope.searchAircrafts = function () {
      return queryAircrafts(0, scDatatableConfig.defaultPageSize)
        .then(function (aircrafts) {
          $scope.items = aircrafts;
        });
    };

    $scope.searchOrganizations = function () {
      return queryOrganizations(0, scDatatableConfig.defaultPageSize)
        .then(function (organizations) {
          $scope.items = organizations;
        });
    };

    $scope.backToGvaApp = function () {
      $scope.currentTab = 'chooseGvaApp';
    };

    $scope.backToPortalApp = function () {
      $scope.currentTab = 'choosePortalApp';
    };

    $scope.backToChooseLot = function () {
      $scope.currentTab = 'chooseLot';
    };

    $scope.newLot = function () {
      $scope.currentTab = 'newLot';
      if ($scope.set === 'person') {
        Persons.newPerson({
          firstName: $scope.wrapper.selectedApp.personData.firstName,
          lastName: $scope.wrapper.selectedApp.personData.lastName,
          uin: $scope.wrapper.selectedApp.personData.uin,
          extendedVersion: false
        }).$promise
        .then(function (newPerson) {
          $scope.newLot = newPerson;
          $scope.newLot.personData = $scope.wrapper.selectedApp.personData;
        });
      } else if ($scope.set === 'aircraft') {
        $scope.showWizzard = true;
        $scope.aircraftWizzardModel = {};
        Aircrafts.newAircraft()
          .$promise
          .then(function (newAircraft) {
            $scope.newLot = newAircraft;
            $scope.newLot.aircraftData = $scope.wrapper.selectedApp.aircraftData;
          });
      } else if ($scope.set === 'organization') {
        $scope.newLot = $scope.wrapper.selectedApp.organizationData;
      }
    };

    $scope.chooseLot = function (lot) {
      return Integration.createApplication({}, {
        docId: $scope.wrapper.selectedApp.docId,
        lotId: lot.id,
        applicationType: $scope.wrapper.selectedApp.gvaApplicationType,
        caseType: $scope.wrapper.selectedApp.caseType,
        correspondentData: $scope.wrapper.selectedApp.correspondentData
      }).$promise.then(function (res) {
        $modalInstance.close(res);
      });
    };

    $scope.setAircraftWizzardData = function () {
      return $scope.form.newLotForm.$validate().then(function () {
        if ($scope.form.newLotForm.$valid) {
          $scope.showWizzard = false;
          if ($scope.aircraftWizzardModel.aircraftModel) {
            $scope.newLot.aircraftData.aircraftProducer =
              $scope.aircraftWizzardModel.aircraftModel.textContent.aircraftProducer;
            $scope.newLot.aircraftData.airCategory =
              $scope.aircraftWizzardModel.aircraftModel.textContent.airCategory;
            $scope.newLot.aircraftData.model =
              $scope.aircraftWizzardModel.aircraftModel.name;
            $scope.newLot.aircraftData.modelAlt = 
              $scope.aircraftWizzardModel.aircraftModel.nameAlt;
          } else {
            $scope.newLot.aircraftData.aircraftProducer =
              $scope.aircraftWizzardModel.aircraftProducer;
            $scope.newLot.aircraftData.airCategory =
              $scope.aircraftWizzardModel.airCategory;
          }
        }
      });
    };

    $scope.saveLot = function () {
      return $scope.form.newLotForm.$validate().then(function () {
        if ($scope.form.newLotForm.$valid) {
          var promise;
          if ($scope.set === 'person') {
            promise = Persons.save($scope.newLot).$promise;
          } else if ($scope.set === 'aircraft') {
            promise = Aircrafts.save($scope.newLot).$promise;
          } else if ($scope.set === 'organization') {
            promise = Organizations.save($scope.newLot).$promise;
          }

          return promise.then(function (result) {
            return $scope.chooseLot(result);
          });
        }
      });
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
    'caseApplications',
    'scDatatableConfig'
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
