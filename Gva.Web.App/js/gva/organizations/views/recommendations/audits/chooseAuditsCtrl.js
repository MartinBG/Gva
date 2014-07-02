/*global angular, _*/
(function (angular, _) {
  'use strict';

  function ChooseAuditsCtrl(
    $state,
    $stateParams,
    $scope,
    audits
  ) {

    $scope.audits = _.map(audits, function (audit) {
      if (_.contains($state.payload.selectedAudits,
        audit.partIndex)) {
        audit.checked = true;
      }

      if(audit.part.auditDetails.length === 0){
        audit.disabled = true;
      }

      return audit;
    });

    $scope.save = function () {
      var includedAudits = [];
      _.each(_.filter($scope.audits, { 'checked': true }), function (audit) {
        includedAudits.push(audit.partIndex);
      });

      return $state.go('^', {}, {}, { selectedAudits: includedAudits });
    };

    $scope.goBack = function () {
      return $state.go('^');
    };

  }

  ChooseAuditsCtrl.$inject = [
    '$state',
    '$stateParams',
    '$scope',
    'audits'
  ];

  ChooseAuditsCtrl.$resolve = {
    audits: [
      '$stateParams',
      'OrganizationInspections',
      function ($stateParams, OrganizationInspections) {
        return OrganizationInspections.query({ id: $stateParams.id }).$promise;
      }
    ]
  };

  angular.module('gva').controller('ChooseAuditsCtrl', ChooseAuditsCtrl);
}(angular, _));
