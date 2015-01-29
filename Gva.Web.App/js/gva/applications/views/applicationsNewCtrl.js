/*global angular, _*/
(function (angular, _) {
  'use strict';

  function ApplicationsNewCtrl(
    $q,
    $scope,
    $state,
    $stateParams,
    scModal,
    Applications,
    Nomenclatures,
    PersonsInfo,
    OrganizationsData,
    application
    ) {

    $scope.application = application;
    $scope.set = $stateParams.set;
    $scope.application.lot.id = $stateParams.lotId;

    $scope.$watch('application.lot.id', function (newValue, oldValue) {
      if (newValue !== oldValue) {
        $scope.application.caseType = null;
        $scope.application.docCorrespondents = [];

        if (!!newValue) {
          var caseTypes = [];

          if ($scope.set !== 'person') {
            caseTypes = Nomenclatures.query({
              alias: 'caseTypes',
              lotId: $scope.application.lot.id
            }).$promise;
          }

          return $q.all({
            caseTypes: $q.when(caseTypes),
            corrs: Applications.getGvaCorrespodents({ lotId: application.lot.id }).$promise
          }).then(function (results) {
            if (results.caseTypes.length) {
              $scope.application.caseType = results.caseTypes[0];
            }

            $scope.application.docCorrespondents = results.corrs.corrs;
          });
        }
      }
    });

    $scope.$watch('application.caseType', function (newValue, oldValue) {
      if (newValue !== oldValue) {
        $scope.application.applicationType = null;
      }
    });

    $scope.addNewCaseType = function () {
      var params = {
        lotId: $scope.application.lot.id
      };

      var modalInstance = scModal.open('addCaseTypes', params);
      return modalInstance.opened;
    };

    $scope.newCorr = function () {
      var partData = {}, isPersonSelect, isOrgSelect;
      partData.$promise = $q.when(false);

      if ($scope.application.lot && $scope.application.lot.id) {
        if ($scope.set === 'person') {
          isPersonSelect = true;
          partData = PersonsInfo.get({ id: $scope.application.lot.id });
        }
        else if ($scope.set === 'organization') {
          isOrgSelect = true;
          partData = OrganizationsData.get({ id: $scope.application.lot.id });
        }
      }

      return partData.$promise.then(function (d) {
        var params = {};

        if (isPersonSelect) {
          params.person = {
            firstName: d.personData.firstName,
            lastName: d.personData.lastName,
            uin: d.personData.uin,
            email: d.personData.email
          };
        }
        else if (isOrgSelect) {
          params.org = {
            name: d.name,
            uin: d.uin
          };
        }

        var modalInstance = scModal.open('newCorr', params);

        modalInstance.result.then(function (nomItem) {
          var newCorr = $scope.application.docCorrespondents.slice();
          newCorr.push(nomItem.nomValueId);
          $scope.application.docCorrespondents = newCorr;
        });

        return modalInstance.opened;
      });
    };

    $scope.selectCorr = function selectCorr() {
      var partData = {}, isPersonSelect, isOrgSelect, selectedCorrs = [];
      partData.$promise = $q.when(false);

      _.forEach($scope.application.docCorrespondents, function (corr) {
        return selectedCorrs.push({ nomValueId: corr });
      });

      if ($scope.application.lot && $scope.application.lot.id) {
        if ($scope.set === 'person') {
          isPersonSelect = true;
          partData = PersonsInfo.get({ id: $scope.application.lot.id });
        }
        else if ($scope.set === 'organization') {
          isOrgSelect = true;
          partData = OrganizationsData.get({ id: $scope.application.lot.id });
        }
      }

      return partData.$promise.then(function (d) {
        var modalInstance, corr = {};

        if (isPersonSelect) {
          corr.displayName = d.personData.firstName + ' ' + d.personData.lastName;
          if (d.personData.uin) {
            corr.displayName += ' ' + d.personData.uin;
          }
          if (d.personData.email) {
            corr.email = d.personData.email;
          }
        }
        if (isOrgSelect) {
          corr.displayName = d.name;
          if (d.uin) {
            corr.displayName += ' ' + d.uin;
          }
        }

        modalInstance = scModal.open('chooseCorr', {
          selectedCorrs: selectedCorrs,
          corr: corr
        });

        modalInstance.result.then(function (nomItem) {
          var newCorr = $scope.application.docCorrespondents.slice();
          newCorr.push(nomItem.nomValueId);
          $scope.application.docCorrespondents = newCorr;
        });

        return modalInstance.opened;
      });
    };

    $scope.requireCorrespondents = function (docCorrespondents) {
      return docCorrespondents.length > 0;
    };

    $scope.selectAppType = function () {
      var modalInstance = scModal.open('chooseAppType', { caseType: $scope.application.caseType });

      modalInstance.result.then(function (appType) {
        $scope.application.applicationType = appType;
      });

      return modalInstance.opened;
    };

    $scope.cancel = function () {
      return $state.go('root.applications.search');
    };

    $scope.save = function () {
      return $scope.appForm.$validate()
      .then(function () {
        if ($scope.appForm.$valid) {
          var newApplication = {
            lotId: $scope.application.lot.id,
            correspondents: $scope.application.docCorrespondents,
            applicationType: $scope.application.applicationType,
            caseTypeId: $scope.application.caseType.nomValueId
          };
          newApplication.setPartPath = $scope.set + 'DocumentApplications';

          return Applications.create(newApplication).$promise.then(function (gvaApp) {
            return $state.go('root.applications.edit.data', {
              set: $scope.set,
              ind: gvaApp.partIndex,
              lotId: gvaApp.lotId,
              id: gvaApp.gvaApplicationId
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
    'scModal',
    'Applications',
    'Nomenclatures',
    'PersonsInfo',
    'OrganizationsData',
    'application'
  ];

  ApplicationsNewCtrl.$resolve = {
    application: function () {
      return {
        lot: {},
        caseType: null,
        applicationType: null,
        docCorrespondents: []
      };
    }
  };

  angular.module('gva').controller('ApplicationsNewCtrl', ApplicationsNewCtrl);
}(angular, _));
