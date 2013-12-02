/*global angular*/
(function (angular) {
  'use strict';
  angular.module('persons', [
    'ng',
    'ngResource',
    'ui.router',
    'ui.bootstrap',
    'persons.templates',
    'navigation',
    'l10n',
    'l10n-tools'
  ]).config(['scaffoldingProvider', function (scaffoldingProvider) {
    scaffoldingProvider.form({
      name: 'gvaPersonData',
      templateUrl: 'persons/forms/personData/personData.html',
      controller: 'persons.PersonDataCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaPersonAddress',
      templateUrl: 'persons/forms/personAddress/personAddress.html'
    });
    scaffoldingProvider.form({
      name: 'gvaPersonDocumentId',
      templateUrl: 'persons/forms/personDocumentId/personDocumentId.html'
    });
  }]);
}(angular));
