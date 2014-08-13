/*global angular*/
(function (angular) {
  'use strict';

  function SendTransferDocModalCtrl(
    $modalInstance,
    $scope,
    scModalParams,
    Docs
  ) {
      //$scope.electronicServiceProviders = _(providers).filter({
      //  isActive: false
    //});

    $scope.form = {};

    $scope.receiverServiceProvider = {
      obj: {},
      id: null
    };

    
    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };

    $scope.send = function () {
      return $scope.form.sendTransferDocForm.$validate().then(function () {
        if ($scope.form.sendTransferDocForm.$valid) {
          return Docs.sendCompetenceTransferDoc({
            id: scModalParams.docId,
            electronicServiceProviderId: $scope.receiverServiceProvider.id
          }, {}).$promise
            .then(function () {
              return $modalInstance.close();
            });
        }
      });
    };
  }

  SendTransferDocModalCtrl.$inject = [
    '$modalInstance',
    '$scope',
    'scModalParams',
    'Docs'
  ];

  //SendTransferDocModalCtrl.$resolve = {
  //  providers: [
  //    'Nomenclatures',
  //    function (Nomenclatures) {
  //      return Nomenclatures.query({ alias: 'electronicServiceProvider' }).$promise;
  //    }
  //  ]
  //};

  angular.module('ems').controller('SendTransferDocModalCtrl', SendTransferDocModalCtrl);
}(angular));
