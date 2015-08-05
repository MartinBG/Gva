/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonDocumentLangCerts', ['$resource', function ($resource) {
    return $resource('api/persons/:id/personDocumentLangCertificates/:ind', {}, {
      newLangCert: {
        method: 'GET',
        url: 'api/persons/:id/personDocumentLangCertificates/new'
      },
      newLangLevel: {
        method: 'GET',
        url: 'api/persons/:id/personDocumentLangCertificates/newLangLevel'
      },
      getLangCertsByValidity: {
        method: 'GET',
        url: 'api/persons/:id/personDocumentLangCertificates/byValidity',
        isArray: true
      },
      getLangLevelHistory: {
        method: 'GET',
        url: 'api/persons/:id/personDocumentLangCertificates/:ind/langLevelHistory',
        isArray: true
      }
    });
  }]);
}(angular));