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
    caseApplications,
    scDatatableConfig
  ) {

    $scope.filters = {
      lin: null,
      uin: null,
      caseType: null,
      names: null
    };

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

    $scope.wrapper = {};
    $scope.form = {};

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
        $scope.wrapper.appType = null;
      }
      return $scope.queryPersons();
    });

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };

    $scope.chooseApp = function () {
      return $scope.form.chooseAppForm.$validate().then(function () {
        if ($scope.form.chooseAppForm.$valid) {
          $scope.set = $scope.wrapper.selectedApp.set.toLowerCase();

          $scope.currentTab = 'chooseLot';
          if($scope.set === 'person') {
            return queryPersons(0, scDatatableConfig.defaultPageSize).then(function (res) {
              $scope.currentTab = 'chooseLot';
              $scope.ngos = res;
            });
          }

        }
      });
    };

    $scope.queryPersons = function (offset, limit) {
      return queryPersons(offset, limit).then(function (res) {
        $scope.items = res;
      });
    };

    $scope.searchPersons = function () {
      return queryPersons(0, scDatatableConfig.defaultPageSize).then(function (res) {
        $scope.items = res;
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
      if($scope.set === 'person') {
        Persons.newPerson({
          firstName: $scope.wrapper.selectedApp.personData.firstName,
          lastName: $scope.wrapper.selectedApp.personData.lastName,
          uin: $scope.wrapper.selectedApp.personData.uin,
          extendedVersion: false
        }).$promise
        .then(function (newPerson) {
          $scope.newLot = newPerson;
        });
      }

      $scope.wrapper.newLot = {
        name: $scope.wrapper.selectedApp.name
      };
    };

    $scope.chooseLot = function (lot) {
      return Integration.createApplication({}, {
        docId: $scope.wrapper.selectedApp.docId,
        lotId: lot.id,
        applicationType: $scope.wrapper.selectedApp.applicationType,
        caseTypes: $scope.wrapper.selectedApp.caseTypes
      }).$promise.then(function (res) {
        $modalInstance.close(res);
        return $state.go('root.applications.edit.data',
          {
            id: res.gvaApplicationId,
            set: res.set,
            lotId: res.lotId,
            ind: res.partIndex
          });
      });
    };

    $scope.saveLot = function () {
      return $scope.form.newLotForm.$validate().then(function () {
        if ($scope.form.newLotForm.$valid) {
          if($scope.set === 'person') {
             return Persons.save($scope.newLot).$promise
               .then(function (result) {
                return $scope.chooseLot(result);
              });
          }
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
