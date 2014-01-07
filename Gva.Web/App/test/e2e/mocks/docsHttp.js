/*global angular*/
(function (angular) {
  'use strict';
  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    var docs = [{
        docId: 1
      }, {
        docId: 2
      }];

    $httpBackendConfiguratorProvider
      .when('GET', '/api/docs/:docId',
        function ($params, $filter) {
          var docId = parseInt($params.docId, 10),
            doc = $filter('filter')(docs, { docId: docId })[0];

          if (!doc) {
            return [400];
          }

          return [200, doc];
        });
  });
}(angular));
