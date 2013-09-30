﻿(function (angular) {
  'use strict';

  function StatesProvider($stateProvider) {
    this.states = {
      'root': {
        name: 'navbar',
        views: {
          'rootView': {
            templateUrl: 'navigation/templates/navbar.html'
          },
          'breadcrumbBarView': {
            templateUrl: 'navigation/templates/navbar.html'
          }
        }
      }
    };

    $stateProvider.state(this.states.root);
  }

  StatesProvider.$inject = [ '$stateProvider' ];

  StatesProvider.prototype.$get = function () {
    return this.states;
  };

  angular.module('navigation').provider('navigation.States', StatesProvider);
}(angular));