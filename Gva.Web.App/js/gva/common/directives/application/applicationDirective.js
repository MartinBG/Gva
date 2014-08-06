/*
Usage: <gva-applications ng-model="model" state-name="stateName"></gva-applications>
*/

/*global angular, Select2*/
(function (angular, Select2) {
  'use strict';

  function ApplicationsDirective($state, $stateParams, $compile, $parse, ApplicationNoms) {
    function preLink(scope, element, attrs) {
      var lotId = $parse(attrs.lotId)(scope) || attrs.lotId || $stateParams.id;

      scope.appSelectOpt = {
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

          container.append($compile(elem)(scope));
        },
        query: function (query) {
          ApplicationNoms.query({ id: lotId, term: query.term }).$promise
              .then(function (result) {
                query.callback({
                  results: result
                });
              });
        }
      };
    }

    function postLink(scope, iElement, iAttrs) {
      var setPart = $parse(iAttrs.setPart)(scope) || iAttrs.setPart,
          lotId = $parse(iAttrs.lotId)(scope) || iAttrs.lotId || $stateParams.id,
          stateName;
      if (setPart === 'person') {
        stateName = 'root.persons.view.documentApplications.edit';
      }
      else if (setPart === 'organization') {
        stateName = 'root.organizations.view.documentApplications.edit';
      }
      else if (setPart === 'aircraft') {
        stateName = 'root.aircrafts.view.applications.edit';
      }
      else if (setPart === 'airport') {
        stateName = 'root.airports.view.applications.edit';
      }
      else if (setPart === 'equipment') {
        stateName = 'root.equipments.view.applications.edit';
      }

      scope.viewApplication = function (partIndex) {
        $state.go(stateName, {
          id: lotId,
          ind: partIndex
        });
      };
    }

    return {
      restrict: 'E',
      replace: true,
      template: '<input type="hidden" class="input-sm form-control" ui-select2="appSelectOpt" />',
      link: { pre: preLink, post: postLink }
    };
  }

  ApplicationsDirective.$inject = [
    '$state',
    '$stateParams',
    '$compile',
    '$parse',
    'ApplicationNoms'
  ];

  angular.module('gva').directive('gvaApplications', ApplicationsDirective);
}(angular, Select2));
