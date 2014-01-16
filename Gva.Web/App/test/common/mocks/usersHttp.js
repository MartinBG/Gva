/*global angular*/
(function (angular) {
  'use strict';
  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    var roles = [{
        roleId: 1,
        name: 'Role1'
      }, {
        roleId: 2,
        name: 'Role2'
      }],
      users = [{
        userId: 1,
        username: 'admin',
        fullname: 'Administrator',
        roles: roles.slice(),
        isActive: true,
        hasPassword: true
      }, {
        userId: 2,
        username: 'peter',
        fullname: 'Peter Ivanov',
        roles: roles.slice(),
        isActive: true,
        hasPassword: true
      }, {
        userId: 3,
        username: 'georgi',
        fullname: 'Georgi Petrov',
        roles: roles.slice(),
        isActive: true,
        hasPassword: true
      }, {
        userId: 4,
        username: 'test1',
        fullname: 'iztrit',
        roles: roles.slice(),
        isActive: false,
        certificateThumbprint: '1234'
      }],
      nextUserId = 5;

    $httpBackendConfiguratorProvider
      .when('GET', '/api/users?username&fullname&showActive&exact',
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
      .when('GET', '/api/users/:userId',
        function ($params, $filter) {
          var userId = parseInt($params.userId, 10),
            user = $filter('filter')(users, {userId: userId})[0];

          if (!user) {
            return [400];
          }

          return [200, user];
        })
      .when('POST', '/api/users/:userId',
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
      .when('POST', '/api/users',
        function ($params, $jsonData) {
          if (!$jsonData || $jsonData.userId) {
            return [400];
          }

          $jsonData.userId = ++nextUserId;
          $jsonData.hasPassword = $jsonData.password !== undefined && $jsonData.password !== '';
          users.push($jsonData);
          
          return [200];
        })
      .when('GET', '/api/roles',
        function () {
          return [200, roles];
        });
  });
}(angular));
