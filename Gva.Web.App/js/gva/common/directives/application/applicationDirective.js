// Usage: <gva-applications ng-model="model" state-name="stateName"></gva-applications>

/*global angular, Select2*/
(function (angular, Select2) {
  'use strict';

  function ApplicationsDirective(
    scModal,
    $state,
    $stateParams,
    $compile,
    $parse,
    $filter,
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
            application = result.documentNumber + '-' +
            result.docId + '-' +
            $filter('date')(result.documentDate, 'mediumDate');
          Select2.util.markMatch(application, query.term, markup, escapeMarkup);
          return markup.join('');
        },
        formatSelection: function (app, container) {
          var application = app.documentNumber + '-' +
            app.docId + '-' +
            $filter('date')(app.documentDate, 'mediumDate'),
            text = Select2.util.escapeMarkup(application),
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
          lotId = $parse(iAttrs.lotId)(scope) || $stateParams.id,
          path;

      if (setPart === 'person') {
        path = 'personDocumentApplications';
      }
      else if (setPart === 'organization') {
        path = 'organizationDocumentApplications';
      }
      else if (setPart === 'aircraft') {
        path = 'aircraftDocumentApplications';
      }
      else if (setPart === 'airport') {
        path = 'airportDocumentApplications';
      }
      else if (setPart === 'equipment') {
        path = 'equipmentDocumentApplications';
      }

      scope.viewApplication = function (partIndex) {
        var modalInstance = scModal.open('viewApplication', {
          lotId: lotId,
          path: path,
          partIndex: partIndex,
          setPart: setPart
        });

        modalInstance.result.then(function () {
          var stateName;

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

          $state.go(stateName, { id: lotId, ind: partIndex });
        });

        return modalInstance.opened;
      };
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
    '$filter',
    'ApplicationNoms'
  ];

  angular.module('gva').directive('gvaApplications', ApplicationsDirective);
}(angular, Select2));
