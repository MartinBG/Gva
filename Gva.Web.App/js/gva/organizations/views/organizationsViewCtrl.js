/*global angular, _*/
(function (angular, _) {
  'use strict';

  function OrganizationsViewCtrl(
    $scope,
    $state,
    $stateParams,
    organization,
    application,
    caseType
  ) {
    $scope.organization = organization;
    $scope.application = application;
    $scope.caseType = parseInt($stateParams.caseTypeId, 10);

    $scope.edit = function () {
      return $state.go('root.organizations.view.edit');
    };

    $scope.viewApplication = function (appId) {
      return $state.go('root.applications.edit.case', {
        id: appId,
        filter: $stateParams.filter
      });
    };

    $scope.changeCaseType = function () {
      $stateParams.caseTypeId = $scope.caseType;
      $state.go($state.current, $stateParams, { reload: true });
    };

    var initTabList = function (caseTypes) {
      $scope.tablist = {};
      if (_.contains(caseTypes, 'approvedOrg')) {
        $scope.tablist = _.extend($scope.tablist, {
          'organizations.tabs.approvals': 'root.organizations.view.approvals'
        });
      }

      if (_.contains(caseTypes, 'airportOperator')) {
        $scope.tablist = _.extend($scope.tablist, {
          'organizations.tabs.airportOperator': {
            'organizations.tabs.certAirportOperators':
              'root.organizations.view.certAirportOperators'
          }
        });
      }

      if (_.contains(caseTypes, 'groundSvcOperator')) {
        $scope.tablist = _.extend($scope.tablist, {
          'organizations.tabs.groundServiceOperators': {
            'organizations.tabs.certGroundServiceOperators':
              'root.organizations.view.certGroundServiceOperators',
            'organizations.tabs.groundServiceOperatorsSnoOperational':
              'root.organizations.view.groundServiceOperatorsSnoOperational'
          }
        });
      }

      if (_.contains(caseTypes, 'airCarrier')) {
        $scope.tablist = _.extend($scope.tablist, {
          'organizations.tabs.airCarrier': {
            'organizations.tabs.certAirCarriers': 'root.organizations.view.certAirCarriers'
          }
        });
      }

      if (_.contains(caseTypes, 'airOperator')) {
        $scope.tablist = _.extend($scope.tablist, {
          'organizations.tabs.airOperator': {
            'organizations.tabs.certAirOperators': 'root.organizations.view.certAirOperators'
          }
        });
      }

      if (_.contains(caseTypes, 'airNavSvcProvider')) {
        $scope.tablist = _.extend($scope.tablist, {
          'organizations.tabs.airNavigationServiceDeliverer': {
            'organizations.tabs.certAirNavigationServiceDeliverer':
              'root.organizations.view.certAirNavigationServiceDeliverers'
          }
        });
      }

      $scope.tablist = _.extend($scope.tablist, {
        'organizations.tabs.inspections': {
          'organizations.tabs.inspection': 'root.organizations.view.inspections',
          'organizations.tabs.recommendations': 'root.organizations.view.recommendations'
        },
        'organizations.tabs.staff': {
          'organizations.tabs.staffManagement': 'root.organizations.view.staffManagement',
          'organizations.tabs.awExaminers': 'root.organizations.view.awExaminers'
        },
        'organizations.tabs.addresses': 'root.organizations.view.addresses',
        'organizations.tabs.docs': {
          'organizations.tabs.others': 'root.organizations.view.documentOthers'
        },
        'organizations.tabs.inventory': 'root.organizations.view.inventory',
        'organizations.tabs.applications': 'root.organizations.view.documentApplications'
      });
    };

    if (caseType) {
      initTabList([caseType.alias]);
    }
    else {
      initTabList(organization.caseTypes);
    }
  }

  OrganizationsViewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'organization',
    'application',
    'caseType'
  ];

  OrganizationsViewCtrl.$resolve = {
    organization: [
      '$stateParams',
      'Organizations',
      function ($stateParams, Organizations) {
        return Organizations.get($stateParams).$promise;
      }
    ],
    application: [
      '$stateParams',
      'OrganizationApplications',
      function ResolveApplication($stateParams, OrganizationApplications) {
        if (!!$stateParams.appId) {
          return OrganizationApplications.get($stateParams).$promise
            .then(function (result) {
              if (result.applicationId) {
                return result;
              }

              return null;
            });
        }

        return null;
      }
    ],
    caseType: [
      '$stateParams',
      'Nomenclatures',
      function ($stateParams, Nomenclatures) {
        if ($stateParams.caseTypeId) {
          return Nomenclatures.get({
            alias: 'caseTypes',
            id: $stateParams.caseTypeId
          }).$promise.then(function (caseType) {
            return caseType;
          });
        }

        return null;
      }
    ]
  };

  angular.module('gva').controller('OrganizationsViewCtrl', OrganizationsViewCtrl);
}(angular, _));
