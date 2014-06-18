/*global angular, _*/
(function (angular, _) {
  'use strict';

  function validateEmail(email) {
    var re = /^\w+[\w-\.]*\@\w+((-\w+)|(\w*))\.[a-z.]{2,5}$/;

    return re.test(email);
  }

  function SendEmailCtrl(
    $scope,
    $state,
    $stateParams,
    Doc,
    email
  ) {
    $scope.email = email.email;

    $scope.send = function () {
      return $scope.sendEmailForm.$validate().then(function () {
        if ($scope.sendEmailForm.$valid) {
          return Doc.postDocSendEmail({ id: $stateParams.id }, $scope.email).$promise
            .then(function () {
              return $state.go('^');
            });
        }
      });
    };

    $scope.checkEmails = function () {
      var emails = $scope.email.emailBcc.split(';'), valid = true;

      _(emails).forEach(function (email) {
        if (!validateEmail(email.trim()) && email.length !== 0) {
          valid = false;
        }

        return;
      });

      return valid;
    };

    $scope.requireCorrs = function () {
      return $scope.email.emailTo.length > 0;
    };

    $scope.cancel = function () {
      return $state.go('^');
    };
  }

  SendEmailCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Doc',
    'email'
  ];

  SendEmailCtrl.$resolve = {
    email: [
      '$stateParams',
      'Doc',
      function resolveEmail($stateParams, Doc) {
        return Doc.getDocSendEmail({ id: $stateParams.id }).$promise;
      }
    ]
  };

  angular.module('ems').controller('SendEmailCtrl', SendEmailCtrl);
}(angular, _));
