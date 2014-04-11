/*global angular, _*/
(function (angular, _) {
  'use strict';

  function JoinFilter() {
    return function (collection, separator) {
      return _.toArray(collection).join(separator);
    };
  }

  JoinFilter.$inject = [];

  angular.module('common').filter('join', JoinFilter);

}(angular, _));
