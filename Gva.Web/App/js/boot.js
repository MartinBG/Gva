/*global angular, _*/
(function (angular, _) {
  'use strict';

  var controllers = {},
      states = {};

  angular.module('boot', [
    'ng',
    'ui.router'
  ]).config([
    '$controllerProvider',
    '$stateProvider',
    '$provide',
    function ($controllerProvider, $stateProvider) {
      $controllerProvider.register =
        _.wrap($controllerProvider.register, function (original, name, constructor) {
          if (angular.isObject(name)) {
            angular.extend(controllers, name);
          } else {
            controllers[name] = constructor;
          }

          return original.call($controllerProvider, name, constructor);
        });

      $stateProvider.state = _.wrap($stateProvider.state, function (original, name, definition) {
        if (angular.isArray(name)) {
          var stateArray = name;
          var views = {};

          if (stateArray.length < 2) {
            throw new Error('State arrays must contain at least 2 items.');
          }

          for (var i = 2, l = stateArray.length; i < l; i++) {
            views[stateArray[i][0]] = {
              templateUrl: stateArray[i][1],
              controller: stateArray[i][2]
            };
          }

          return original.call($stateProvider, {
            name: stateArray[0],
            url: stateArray[1],
            views: views,
            'abstract': stateArray.length === 2
          });
        } else {
          return original.call($stateProvider, name, definition);
        }
      });

      $stateProvider.decorator('resolve', function (state) {
        states[state.self.name] = state;
        return state.resolve;
      });
    }
  ]).run(function () {
    _.forOwn(states, function (state) {
      if (_.isEmpty(state.resolve)) {
        _.forOwn(state.views, function (view) {
          if (view.controller && controllers[view.controller] &&
            controllers[view.controller].$resolve) {
            _.assign(state.resolve, controllers[view.controller].$resolve);
          }
        });
      }
    });
  });
}(angular, _));
