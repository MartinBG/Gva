/*global angular, moment*/
(function (angular, moment) {
  'use strict';

  function DocumentLangCertsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    langCerts
  ) {
    $scope.documentLangCerts = langCerts;

    $scope.isInvalidDocument = function(item){
      return item.valid && item.valid.code === 'N';
    };

    $scope.isExpiringDocument = function(item) {
      var today = moment(new Date()),
          difference = moment(item.documentDateValidTo).diff(today, 'days');

      return 0 <= difference && difference <= 30;
    };

    $scope.isExpiredDocument = function(item) {
      return moment(new Date()).isAfter(item.documentDateValidTo);
    };
  }

  DocumentLangCertsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'langCerts'
  ];

  DocumentLangCertsSearchCtrl.$resolve = {
    langCerts: [
      '$stateParams',
      'PersonDocumentLangCerts',
      function ($stateParams, PersonDocumentLangCerts) {
        return PersonDocumentLangCerts.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocumentLangCertsSearchCtrl', DocumentLangCertsSearchCtrl);
}(angular, moment));
