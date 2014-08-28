/*global angular, _*/
(function (angular, _) {
  'use strict';

  var controllers = {},
      states = {},
      modals = {};

  angular.module('boot', [
    'ng',
    'scaffolding',
    'ui.router'
  ]).config([
    '$controllerProvider',
    '$stateProvider',
    'scModalProvider',
    '$provide',
    function ($controllerProvider, $stateProvider, scModalProvider, $provide) {
      $controllerProvider.register =
        _.wrap($controllerProvider.register, function (original, name, constructor) {
          if (angular.isObject(name)) {
            angular.extend(controllers, name);
          } else {
            controllers[name] = constructor;
          }

          return original.apply($controllerProvider, [name, constructor]);
        });

      scModalProvider.modal = _.wrap(scModalProvider.modal,
        function (original, name, template, controller, size) {
          var modalObj = {
            template: template,
            controller: controller,
            size: size || 'xlg'
          };

          modals[name] = modalObj;

          return original.apply(scModalProvider, [name, modalObj]);
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

          return original.apply($stateProvider, [{
            name: stateArray[0],
            url: stateArray[1],
            views: views,
            'abstract': stateArray.length === 2
          }]);
        } else {
          return original.apply($stateProvider, [name, definition]);
        }
      });

      $stateProvider.decorator('resolve', function (state) {
        states[state.self.name] = state;
        return state.resolve;
      });

      $provide.decorator('$state', function ($delegate) {
        $delegate.getWrapper = function (stateName) {
          return states[stateName];
        };

        var originalGo = $delegate.go;
        $delegate.go = function (to, params, options, payload) {
          if (payload) {
            $delegate.$$payload = _.cloneDeep(payload);
          }

          return originalGo.apply($delegate, [to, params, options]);
        };

        var originalTransitionTo = $delegate.transitionTo;
        $delegate.transitionTo = function (to, toParams, options, payload) {
          if (payload) {
            $delegate.$$payload = _.cloneDeep(payload);
          }

          return originalTransitionTo.apply($delegate, [to, toParams, options]);
        };

        return $delegate;
      });
    }
  ]).run([function () {
    _.forOwn(states, function (state) {
      if ((!state.self.url || state.self.url.indexOf('?') === 0) &&
          state.parent &&
          state.parent['abstract']) {
        state.parent.defaultChild = state;
      }

      if (_.isEmpty(state.resolve)) {
        _.forOwn(state.views, function (view) {
          if (view.controller && controllers[view.controller] &&
            controllers[view.controller].$resolve) {
            _.assign(state.resolve, controllers[view.controller].$resolve);
          }
        });
      }
    });
  }]).run(['$rootScope', '$state', function ($rootScope, $state) {
    $rootScope.$on('$stateChangeSuccess', function (event, toState, toParams, fromState) {
      $state.previous = $state.getWrapper($state.get(fromState).name);
      $state.payload = $state.$$payload;

      $state.$$payload = null;
    });
  }]).run([function () {
    _.forOwn(modals, function (modal) {
      if (modal.controller &&
          controllers[modal.controller] &&
          controllers[modal.controller].$resolve) {
        modal.resolve = controllers[modal.controller].$resolve;
      }
    });
  }]);
}(angular, _));
