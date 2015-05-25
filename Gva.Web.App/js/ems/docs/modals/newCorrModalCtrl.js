/*global angular, _*/
(function (angular, _) {
  'use strict';

  function NewCorrModalCtrl(
    $modalInstance,
    $scope,
    Corrs,
    corr
  ) {
    $scope.form = {};
    $scope.corr = corr;

    $scope.save = function save() {
      return $scope.form.corrForm.$validate().then(function () {
        if ($scope.form.corrForm.$valid) {
          return Corrs.save($scope.corr).$promise.then(function (corr) {
            var nomItem = {
              name: corr.obj.displayName,
              nomValueId: corr.correspondentId
            };

            return $modalInstance.close(nomItem);
          });
        }
      });
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };
  }

  NewCorrModalCtrl.$inject = [
    '$modalInstance',
    '$scope',
    'Corrs',
    'corr'
  ];

  NewCorrModalCtrl.$resolve = {
    corr: ['$q', 'Nomenclatures', 'Corrs', 'Integration','scModalParams',
      function ($q, Nomenclatures, Corrs, Integration, scModalParams) {
        if (scModalParams.lotId) {
          return Integration.convertLotToCorrespondent({
            lotId: scModalParams.lotId
          }).$promise
            .then(function (corr) {
              return corr;
            });
        } else {
          return $q.all({
            corrTypes: Nomenclatures.query({ alias: 'correspondentType' }).$promise,
            corr: Corrs.getNew().$promise
          }).then(function (res) {
            var corrType = _(res.corrTypes).filter({
                alias: 'BulgarianCitizen'
              }).first();
              res.corr.correspondentTypeId = corrType.nomValueId;
              res.corr.correspondentType = corrType;
             
            return res.corr;
          });
        }
      }
    ]
  };

  angular.module('ems').controller('NewCorrModalCtrl', NewCorrModalCtrl);
}(angular, _));
