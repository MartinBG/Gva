/*global angular, _*/
(function (angular, _) {
  'use strict';

  function OrganizationsViewCtrl(
    $scope,
    $state,
    $stateParams,
    Organization,
    organization,
    application
  ) {
    $scope.organization = organization;
    $scope.application = application;

    $scope.edit = function () {
      return $state.go('root.organizations.view.edit');
    };

    $scope.viewApplication = function (appId) {
      return $state.go('root.applications.edit.case', { id: appId });
    };

    $scope.tablist = {};
    if(_.contains(organization.caseTypes, 'ЛГ')) {
      $scope.tablist =  _.extend($scope.tablist, {
        'organizations.tabs.approvals': 'root.organizations.view.approvals'
      });
    }

    if(_.contains(organization.caseTypes, 'ЛО')) {
      $scope.tablist =  _.extend($scope.tablist, {
        'organizations.tabs.airportOperator': {
          'organizations.tabs.certAirportOperators': 'root.organizations.view.certAirportOperators'
        }
      });
    }

    if(_.contains(organization.caseTypes, 'ОНО')) {
      $scope.tablist =  _.extend($scope.tablist, {
        'organizations.tabs.groundServiceOperators': {
          'organizations.tabs.certGroundServiceOperators':
            'root.organizations.view.certGroundServiceOperators',
          'organizations.tabs.groundServiceOperatorsSnoOperational':
            'root.organizations.view.groundServiceOperatorsSnoOperational'
        }
      });
    }

    if(_.contains(organization.caseTypes, 'ВП')) {
      $scope.tablist =  _.extend($scope.tablist, {
        'organizations.tabs.airCarrier': {
          'organizations.tabs.certAirCarriers': 'root.organizations.view.certAirCarriers'
        }
      });
    }

    if(_.contains(organization.caseTypes, 'АО')) {
      $scope.tablist =  _.extend($scope.tablist, {
        'organizations.tabs.airOperator': {
          'organizations.tabs.certAirOperators': 'root.organizations.view.certAirOperators'
        }
      });
    }

    if(_.contains(organization.caseTypes, 'ДАО')) {
      $scope.tablist =  _.extend($scope.tablist, {
        'organizations.tabs.airNavigationServiceDeliverer': {
          'organizations.tabs.certAirNavigationServiceDeliverer':
            'root.organizations.view.certAirNavigationServiceDeliverers'
        }
      });
    }

    $scope.tablist = _.extend($scope.tablist, {
      'organizations.tabs.inspections': {
        'organizations.tabs.inspection': 'root.organizations.view.inspections',
        'organizations.tabs.recommendations': 'root.organizations.view.recommendations',
        'organizations.tabs.auditplans': 'root.organizations.view.auditplans'
      },
      'organizations.tabs.staff': {
        'organizations.tabs.staffManagement': 'root.organizations.view.staffManagement',
        'organizations.tabs.staffExaminers': 'root.organizations.view.staffExaminers'
      },
      'organizations.tabs.addresses': 'root.organizations.view.addresses',
      'organizations.tabs.registers': {
        'organizations.tabs.regAirportOperators': 'root.organizations.view.regAirportOperators',
        'organizations.tabs.regGroundServiceOperators':
          'root.organizations.view.regGroundServiceOperators'
      },
      'organizations.tabs.docs': {
        'organizations.tabs.others': 'root.organizations.view.documentOthers'
      },
      'organizations.tabs.inventory': 'root.organizations.view.inventory',
      'organizations.tabs.applications': 'root.organizations.view.documentApplications'
    });
  }

  OrganizationsViewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Organization',
    'organization',
    'application'
  ];

  OrganizationsViewCtrl.$resolve = {
    organization: [
      '$stateParams',
      'Organization',
      function ($stateParams, Organization) {
        return Organization.get({ id: $stateParams.id }).$promise;
      }
    ],
    application: [
      '$stateParams',
      'OrganizationApplication',
      function ResolveApplication($stateParams, OrganizationApplication) {
        if (!!$stateParams.appId) {
          return OrganizationApplication.get($stateParams).$promise;
        }

        return null;
      }
    ]
  };

  angular.module('gva').controller('OrganizationsViewCtrl', OrganizationsViewCtrl);
}(angular, _));
