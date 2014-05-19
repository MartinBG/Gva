/*global angular*/
(function (angular) {
  'use strict';

  function ApprovalsNewCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationApproval,
    organizationApproval,
    selectedLimitation
  ) {

    $scope.approval = organizationApproval;

    var limitation = selectedLimitation.pop();

    if(limitation) {
      var index = parseInt(limitation.index, 10);
      if(limitation.limitationAlias === 'lim147limitations') {
        $scope.approval.part.amendments[0].lims147[index].lim147limitation = limitation.name;
      } else if(limitation.limitationAlias === 'lim145limitations') {
        $scope.approval.part.amendments[0].lims145[index].lim145limitation = limitation.name;
      } else if (limitation.limitationAlias === 'aircraftTypeGroups') {
        $scope.approval.part.amendments[0].limsMG[index].aircraftTypeGroup = limitation.name;
      }
    }

    $scope.save = function () {
      return $scope.newApprovalForm.$validate()
        .then(function () {
          if ($scope.newApprovalForm.$valid) {
            return OrganizationApproval
              .save({ id: $stateParams.id }, $scope.approval).$promise
              .then(function () {
                return $state.go('root.organizations.view.approvals.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.organizations.view.approvals.search');
    };
  }

  ApprovalsNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationApproval',
    'organizationApproval',
    'selectedLimitation'
  ];

  ApprovalsNewCtrl.$resolve = {
    organizationApproval: function () {
      return {
        part: {
          amendments: [{
            includedDocuments: [],
            lims145: [],
            lims147: [],
            limsMG: []
          }]
        }
      };
    },
    selectedLimitation: function () {
      return [];
    }
  };

  angular.module('gva').controller('ApprovalsNewCtrl', ApprovalsNewCtrl);
}(angular));
