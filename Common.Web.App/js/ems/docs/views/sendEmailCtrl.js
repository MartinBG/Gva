﻿/*global angular, _*/
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
    $modal,
    Docs,
    email
  ) {
    $scope.email = email.email;

    $scope.viewEditableFile = function viewEditableFile(docFileDO) {
      return Docs
        .createTicket({
          id: docFileDO.docId,
          docFileId: docFileDO.docFileId,
          fileKey: docFileDO.file.key
        }, {})
        .$promise
        .then(function (data) {
          $modal.open({
            templateUrl: 'js/ems/docs/views/portalModal.html',
            controller: 'PortalModalCtrl',
            backdrop: 'static',
            keyboard: false,
            windowClass: 'ems-portal-modal-window',
            resolve: {
              iframeSrc: function () {
                return data.url;
              }
            }
          });
        });
    };

    $scope.send = function () {
      return $scope.sendEmailForm.$validate().then(function () {
        if ($scope.sendEmailForm.$valid) {
          return Docs.sendCorrespondentEmail({ id: $stateParams.id }, $scope.email).$promise
            .then(function () {
              return $state.go('^');
            });
        }
      });
    };

    $scope.removeFile = function (target) {
      $scope.email.publicFiles.splice($scope.email.publicFiles.indexOf(target), 1);
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
    '$modal',
    'Docs',
    'email'
  ];

  SendEmailCtrl.$resolve = {
    email: [
      '$stateParams',
      'Docs',
      function resolveEmail($stateParams, Docs) {
        return Docs.getCorrespondentEmail({ id: $stateParams.id }).$promise;
      }
    ]
  };

  angular.module('ems').controller('SendEmailCtrl', SendEmailCtrl);
}(angular, _));
