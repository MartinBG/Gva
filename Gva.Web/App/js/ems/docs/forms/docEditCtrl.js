/*global angular*/
(function (angular) {
  'use strict';

  function DocsEditCtrl(
    $q,
    $scope,
    $filter,
    $state,
    $stateParams,
    Doc
  ) {
    if ($stateParams.docId) {
      $scope.isEdit = true;
      $scope.doc = Doc.get({ docId: $stateParams.docId });
    } else {
      $scope.isEdit = false;

      //Corr.create().$promise
      //  .then(function (result) {
      //    $scope.corr = result;
      //  });

    }

  }

  DocsEditCtrl.$inject = [
    '$q',
    '$scope',
    '$filter',
    '$state',
    '$stateParams',
    'Doc'
  ];

  angular.module('ems').controller('DocsEditCtrl', DocsEditCtrl);
}(angular));
