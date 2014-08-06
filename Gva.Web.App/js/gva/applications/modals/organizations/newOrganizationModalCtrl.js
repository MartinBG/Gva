/*global angular*/
(function (angular) {
  'use strict';

  function NewOrganizationModalCtrl(
    $scope,
    $modalInstance,
    Organizations,
    organization
  ) {
    $scope.form = {};
    $scope.organization = organization;

    $scope.save = function () {
      return $scope.form.newOrganizationForm.$validate().then(function () {
        if ($scope.form.newOrganizationForm.$valid) {
          return Organizations.save($scope.organization).$promise.then(function (savedOrg) {
            return $modalInstance.close(savedOrg.id);
          });
        }
      });
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };
  }

  NewOrganizationModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'Organizations',
    'organization'
  ];

  NewOrganizationModalCtrl.$resolve = {
    organization: [
      'scModalParams',
      function (scModalParams) {
        return {
          organizationData: {
            uin: scModalParams.uin,
            name: scModalParams.name
          }
        };
      }
    ]
  };

  angular.module('gva').controller('NewOrganizationModalCtrl', NewOrganizationModalCtrl);
}(angular));
