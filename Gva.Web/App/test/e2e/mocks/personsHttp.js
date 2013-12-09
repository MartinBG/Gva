/*global angular, getData*/
(function (angular) {
  'use strict';

  var persons = getData('person.sample');

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', '/api/persons?lin&exact',
        function ($params, $filter, $delay) {
          return [
            200,
            $delay(4000, $filter('filter')(persons, function (person) {
              if ($params.exact) {
                return person.personData.lin === $params.lin;
              } else {
                return person.personData.lin.indexOf($params.lin) >= 0;
              }
            }))
          ];
        })
      .when('POST', '/api/persons',
        function ($params, $jsonData, $delay) {
          persons.push($jsonData);

          return [200, $delay(500)];
        });
  });
}(angular));
