/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('WordTemplates', ['$resource', function ($resource) {
    return $resource('api/wordTemplates/', {}, {
      changeTemplateDataDenerator: {
        method: 'POST',
        url: 'api/wordTemplates/:templateId',
        params: {
          templateId: '@templateId',
          dataGenerator: '@dataGenerator'
        }
      }
    });
  }]);
}(angular));
