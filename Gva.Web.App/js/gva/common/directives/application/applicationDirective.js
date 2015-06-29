// Usage: <gva-applications ng-model="model" lot-id="lotId"></gva-applications>

/*global angular, Select2*/
(function (angular, Select2) {
  'use strict';

  function ApplicationsDirective(
    scModal,
    $state,
    $stateParams,
    $compile,
    $parse,
    ApplicationNoms)
  {
    function preLink(scope, element, attrs) {
      var lotId = $parse(attrs.lotId)(scope) || $stateParams.id;

      scope.appSelectOpt = {
        multiple: true,
        id: function (app) {
          return app.applicationId;
        },
        formatResult: function (result, container, query, escapeMarkup) {
          var markup = [],
            application = result.applicationName;

          Select2.util.markMatch(application, query.term, markup, escapeMarkup);
          return markup.join('');
        },
        formatSelection: function (app, container) {
          var application = app.applicationName;

          var text = Select2.util.escapeMarkup(application),
              elem = '<sc-button btn-sref=\'' +
              '{ state: "root.applications.edit.case", params: { id: ' +
              app.applicationId + ' } }\' text=\'' + text + '\'></sc-button>';

          container.append($compile(elem)(scope));
        },
        query: function (query) {
          ApplicationNoms.query({ lotId: lotId, term: query.term }).$promise
              .then(function (result) {
                query.callback({
                  results: result
                });
              });
        }
      };
    }

    function postLink() {
    }

    return {
      restrict: 'E',
      replace: true,
      template: '<input type="hidden" class="input-sm form-control"' +
                    'id="appSelect" ui-select2="appSelectOpt" />',
      link: { pre: preLink, post: postLink }
    };
  }

  ApplicationsDirective.$inject = [
    'scModal',
    '$state',
    '$stateParams',
    '$compile',
    '$parse',
    'ApplicationNoms'
  ];

  angular.module('gva').directive('gvaApplications', ApplicationsDirective);
}(angular, Select2));
