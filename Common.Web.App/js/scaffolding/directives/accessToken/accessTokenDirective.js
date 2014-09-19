// Usage: <.. sc-access-token ..>

/*global angular*/
(function (angular) {
  'use strict';

  function AccessTokenDirective(sessionTokenStore) {
    return {
      restrict: 'A',
      priority: 110,
      link: function (scope, element, attrs) {
        element.on('click', function () {
          if (/access_token/.test(attrs.href)) {
            return;
          } else {
            if (/\?/.test(attrs.href)) {
              attrs.$set('href', attrs.href + '&access_token=' + sessionTokenStore.getToken());
            } else {
              attrs.$set('href', attrs.href + '?access_token=' + sessionTokenStore.getToken());
            }
          }
        });
      }
    };
  }

  AccessTokenDirective.$inject = ['sessionTokenStore'];

  angular.module('scaffolding').directive('scAccessToken', AccessTokenDirective);
}(angular));
