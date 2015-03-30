/*global angular, _*/
(function (angular, _) {
  'use strict';

  function LinkAppModalCtrl(
    $scope,
    $state,
    $stateParams,
    $modalInstance,
    scModalParams,
    Integration,
    Persons,
    Aircrafts,
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

    $scope.currentTab = 'chooseApp';
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
        $scope.set = $scope.wrapper.selectedApp.set;
      } else {
        $scope.wrapper.selectedApp = null;
      }

      if ($scope.set === 'person') {
        return $scope.searchPersons();
      } else if ($scope.set === 'aircraft') {
        return $scope.searchAircrafts();
      }
    });

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };

    $scope.chooseApp = function () {
      return $scope.form.chooseAppForm.$validate().then(function () {
        if ($scope.form.chooseAppForm.$valid) {
          $scope.set = $scope.wrapper.selectedApp.set.toLowerCase();

          $scope.currentTab = 'chooseLot';
          if ($scope.set === 'person') {
            $scope.filters = {
              lin: $scope.wrapper.selectedApp.personData.lin,
              uin: $scope.wrapper.selectedApp.personData.uin,
              caseType: null,
              names: $scope.wrapper.selectedApp.personData.firstName + ' ' + 
                $scope.wrapper.selectedApp.personData.lastName
            };

            return $scope.searchPersons();
          } else if ($scope.set === 'aircraft') {
            var producer = $scope.wrapper.selectedApp.aircraftData.aircraftProducer;
            $scope.filters = {
              manSN: null,
              modelAlt: null,
              mark: null,
              aircraftProducer: producer ? producer.name : ''
            };
            return $scope.searchAircrafts();
          }

        }
      });
    };

    $scope.searchPersons = function () {
      return queryPersons(0, scDatatableConfig.defaultPageSize).then(function (persons) {
        $scope.items = persons;
      });
    };

    $scope.searchAircrafts = function () {
      return queryAircrafts(0, scDatatableConfig.defaultPageSize).then(function (aircrafts) {
        $scope.items = aircrafts;
      });
    };

    $scope.backToApp = function () {
      $scope.currentTab = 'chooseApp';
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
      }
    };

    $scope.chooseLot = function (lot) {
      return Integration.createApplication({}, {
        docId: $scope.wrapper.selectedApp.docId,
        lotId: lot.id,
        applicationType: $scope.wrapper.selectedApp.applicationType,
        caseTypes: $scope.wrapper.selectedApp.caseTypes,
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
    'Integration',
    'Persons',
    'Aircrafts',
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
