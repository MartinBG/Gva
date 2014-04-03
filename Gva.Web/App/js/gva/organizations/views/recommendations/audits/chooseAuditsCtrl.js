/*global angular, _*/
(function (angular, _) {
  'use strict';

  function ChooseAuditsCtrl(
    $state,
    $stateParams,
    $scope,
    organizationRecommendation,
    availableAudits) {
    $scope.availableAudits = availableAudits;

    $scope.save = function () {
      organizationRecommendation.part.includedAudits = [];

      _.each(_.filter($scope.availableAudits, { 'checked': true }), function (audit) {
        organizationRecommendation.part.includedAudits.push(audit.partIndex);
      });
      return $state.go('^');
    };

    $scope.goBack = function () {
      return $state.go('^');
    };

  }

  ChooseAuditsCtrl.$inject = [
    '$state',
    '$stateParams',
    '$scope',
    'organizationRecommendation',
    'availableAudits'
  ];

  ChooseAuditsCtrl.$resolve = {
    availableAudits: [
      '$stateParams',
      'OrganizationInspection',
      'organizationRecommendation',
      function ($stateParams, OrganizationInspection, organizationRecommendation) {
        return OrganizationInspection.query({ id: $stateParams.id })
            .$promise.then(function (availableAudits) {
              return _.map(availableAudits, function (availableAudit) {
                if (_.contains(organizationRecommendation.part.includedAudits,
                  availableAudit.partIndex)) {
                  availableAudit.checked = true;
                }
                return availableAudit;
              });
            });
      }
    ]
  };

  angular.module('gva').controller('ChooseAuditsCtrl', ChooseAuditsCtrl);
}(angular, _));
