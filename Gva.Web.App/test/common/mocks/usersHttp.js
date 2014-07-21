/*global angular*/
(function (angular) {
  'use strict';
  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    var roles = [{
        roleId: 1,
        name: 'ЛАП четене'
      }, {
        roleId: 2,
        name: 'ЛАП редакция'
      }, {
        roleId: 3,
        name: 'ЛАП админ'
      }, {
        roleId: 4,
        name: 'ВС четене'
      }, {
        roleId: 5,
        name: 'ВС редакция'
      }, {
        roleId: 6,
        name: 'ВС админ'
      }, {
        roleId: 7,
        name: 'ОО четене'
      }, {
        roleId: 8,
        name: 'ОО редакция'
      }, {
        roleId: 9,
        name: 'ОО админ'
      }, {
        roleId: 10,
        name: 'ЛЛП четене'
      }, {
        roleId: 11,
        name: 'ЛЛП редакция'
      }, {
        roleId: 12,
        name: 'ЛЛП админ'
      }, {
        roleId: 13,
        name: 'СУВД четене'
      }, {
        roleId: 14,
        name: 'СУВД редакция'
      }, {
        roleId: 15,
        name: 'СУВД админ'
      }, {
        roleId: 16,
        name: 'АУЦ четене'
      }, {
        roleId: 17,
        name: 'АУЦ редакция'
      }, {
        roleId: 18,
        name: 'АУЦ админ'
      }, {
        roleId: 19,
        name: 'ВП четене'
      }, {
        roleId: 20,
        name: 'ВП редакция'
      }, {
        roleId: 21,
        name: 'ВП админ'
      }, {
        roleId: 22,
        name: 'САО четене'
      }, {
        roleId: 23,
        name: 'САО редакция'
      }, {
        roleId: 24,
        name: 'САО админ'
      }, {
        roleId: 25,
        name: 'АНО четене'
      }, {
        roleId: 26,
        name: 'АНО редакция'
      }, {
        roleId: 27,
        name: 'АНО админ'
      }, {
        roleId: 28,
        name: 'АС четене'
      }, {
        roleId: 29,
        name: 'АС редакция'
      }, {
        roleId: 30,
        name: 'АС админ'
      }, {
        roleId: 1,
        name: 'АИС достъп'
      }],
      users = [],
      nextUserId = 5;

    $httpBackendConfiguratorProvider
      .when('GET', 'api/users?username&fullname&showActive&exact',
        function ($params, $filter) {
          return [
            200,
            $filter('filter')(users, {
              username: $params.username,
              fullname: $params.fullname,
              isActive: $params.showActive
            })
          ];
        })
      .when('GET', 'api/users/:userId',
        function ($params, $filter) {
          var userId = parseInt($params.userId, 10),
            user = $filter('filter')(users, {userId: userId})[0];

          if (!user) {
            return [400];
          }

          return [200, user];
        })
      .when('POST', 'api/users/:userId',
        function ($params, $jsonData, $filter) {
          var userId = parseInt($params.userId, 10),
            userIndex = users.indexOf($filter('filter')(users, {userId: userId})[0]);

          if (userIndex === -1) {
            return [400];
          }

          $jsonData.hasPassword = $jsonData.password !== undefined && $jsonData.password !== '';
          users[userIndex] = $jsonData;

          return [200];
        })
      .when('POST', 'api/users',
        function ($params, $jsonData) {
          if (!$jsonData || $jsonData.userId) {
            return [400];
          }

          $jsonData.userId = ++nextUserId;
          $jsonData.hasPassword = $jsonData.password !== undefined && $jsonData.password !== '';
          users.push($jsonData);

          return [200];
        })
      .when('GET', 'api/roles',
        function () {
          return [200, roles];
        });
  });
}(angular));
