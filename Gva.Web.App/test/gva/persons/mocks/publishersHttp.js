/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
        .when('GET', '/api/publishers?text&publisherTypeAlias',
          function ($params, $filter, publishers) {
            var pubs = [];

            if ($params.publisherTypeAlias) {
              pubs = publishers[$params.publisherTypeAlias];
            } else {
              var keys = _.keys(publishers);
              _.each(keys, function (key) {
                pubs = pubs.concat(publishers[key]);
              });
            }
            if ($params.text) {
              return [
                200,
                $filter('filter')(pubs, {
                  name: $params.text
                })
              ];
            } else {
              return [200, pubs];
            }
          });

  });
}(angular, _));
