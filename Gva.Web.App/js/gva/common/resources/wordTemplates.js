/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('WordTemplates', ['$resource', function ($resource) {
    return $resource('api/wordTemplates/:templateId', {}, {
      changeTemplateDataDenerator: {
        method: 'POST',
        url: 'api/wordTemplates/:templateId',
        params: {
          templateId: '@templateId',
          dataGenerator: '@dataGenerator'
        }
      },
      isUniqueTemplateName: {
        method: 'GET',
        url: 'api/wordTemplates/isUniqueTemplateName'
      },
      getTemplates: {
        method: 'GET',
        isArray: true,
        url: 'api/wordTemplates'
      },
      newTemplate: {
        method: 'GET',
        url: 'api/wordTemplates/new'
      },
      createNew: {
        method: 'POST',
        url: 'api/wordTemplates/createNew'
      }
    });
  }]);
}(angular));
