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
    corr: [
      '$q',
      'Nomenclatures',
      'Corrs',
      'scModalParams',
      function ($q, Nomenclatures, Corrs, scModalParams) {
        return $q.all({
          corrTypes: Nomenclatures.query({ alias: 'correspondentType' }).$promise,
          corr: Corrs.getNew().$promise
        }).then(function (res) {
          if (scModalParams.person) {
            res.corr.correspondentTypeId = _(res.corrTypes).filter({
              alias: 'BulgarianCitizen'
            }).first().nomValueId;
            res.corr.correspondentType = _(res.corrTypes).filter({
              alias: 'BulgarianCitizen'
            }).first();

            res.corr.bgCitizenFirstName = scModalParams.person.firstName;
            res.corr.bgCitizenLastName = scModalParams.person.lastName;
            res.corr.bgCitizenUIN = scModalParams.person.uin;
            res.corr.email = scModalParams.person.email;
          }
          else if (scModalParams.org) {
            res.corr.legalEntityName = scModalParams.org.name;
            res.corr.legalEntityBulstat = scModalParams.org.uin;
          }

          return res.corr;
        });
      }
    ]
  };

  angular.module('ems').controller('NewCorrModalCtrl', NewCorrModalCtrl);
}(angular, _));
