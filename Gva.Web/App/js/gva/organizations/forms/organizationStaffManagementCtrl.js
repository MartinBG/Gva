/*global angular, Select2*/
(function (angular, Select2) {
  'use strict';

  function OrganizationStaffManagementCtrl(
    $scope,
    $stateParams,
    $compile,
    OrganizationApplication) {

    $scope.appSelectOpt = {
      multiple: true,
      id: function (app) {
        return app.applicationId;
      },
      formatResult: function (result, container, query, escapeMarkup) {
        var markup = [];
        Select2.util.markMatch(result.applicationName, query.term, markup, escapeMarkup);
        return markup.join('');
      },
      formatSelection: function (app, container) {
        var text = Select2.util.escapeMarkup(app.applicationName),
            elem = '<a ng-click="viewApplication(' + app.partIndex + ')">' + text + '</a>';

        container.append($compile(elem)($scope));
      },
      query: function (query) {
        OrganizationApplication.query({ id: $stateParams.id, term: query.term }).$promise
            .then(function (result) {
          query.callback({
            results: result
          });
        });
      }
    };

    angular.forEach($scope.model, function (file) {
      file.isDeleted = false;
      file.isAdded = false;
    });
  }

  OrganizationStaffManagementCtrl.$inject = [
    '$scope',
    '$stateParams',
    '$compile',
    'OrganizationApplication'
  ];

  angular.module('gva')
    .controller('OrganizationStaffManagementCtrl', OrganizationStaffManagementCtrl);
}(angular, Select2));
