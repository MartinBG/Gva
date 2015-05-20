/*global angular*/
/*jshint maxlen:false*/
(function (angular) {
  'use strict';

  angular.module('app')

  .config(['l10nProvider', function (l10n) {
    l10n.add('bg-bg', {
      app: {
        unknownErrorMessage: 'Възникна непредвидена грешка! Моля презаредете страницата с F5 и опитайте отново.'
      }
    });
  }]);
}(angular));
