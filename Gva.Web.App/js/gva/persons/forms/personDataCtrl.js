/*global angular, moment*/
(function (angular, moment) {
  'use strict';

  function PersonDataCtrl($scope, Persons, Nomenclatures) {
    Nomenclatures.query({alias: 'linTypes'})
      .$promise.then(function(linTypes){
        $scope.linTypes = linTypes;
      });

    $scope.setLin = function (item) {
      $scope.model.linType = item;
      $scope.model.lin = null;

      if ($scope.model.linType.code !== 'none') {
        Persons.getNextLin({
          linTypeId: item.nomValueId
        }).$promise.then(function (result) {
          $scope.model.lin = result.nextLin;
        });
      }
    };

    $scope.requireCaseTypes = function () {
      return $scope.model.caseTypes.length > 0;
    };

    $scope.uinValid = function () {
      if (!$scope.model.uin) {
        return true;
      }

      if (!/\d{10}/.test($scope.model.uin)) {
        return false;
      }

      var checkDigit = ($scope.model.uin[0] * 2 +
                       $scope.model.uin[1] * 4 +
                       $scope.model.uin[2] * 8 +
                       $scope.model.uin[3] * 5 +
                       $scope.model.uin[4] * 10 +
                       $scope.model.uin[5] * 9 +
                       $scope.model.uin[6] * 7 +
                       $scope.model.uin[7] * 3 +
                       $scope.model.uin[8] * 6) % 11;
      checkDigit = checkDigit === 10 ? 0 : checkDigit;

      return checkDigit === parseInt($scope.model.uin[9], 10);
    };

    $scope.setAgeSex = function () {
      if (!/\d{10}/.test($scope.model.uin)) {
        return;
      }

      if (!$scope.model.sex) {
        var sexDigit = parseInt($scope.model.uin[8], 10);
        if (sexDigit % 2 === 1) {
          Nomenclatures.get({ alias: 'gender', valueAlias: 'Female' })
            .$promise.then(function (sex) {
              $scope.model.sex = sex;
            });
        }
        else {
          Nomenclatures.get({ alias: 'gender', valueAlias: 'Male' })
            .$promise.then(function (sex) {
              $scope.model.sex = sex;
            });
        }
      }

      if (!$scope.model.dateOfBirth) {
        var dateOfBirth = $scope.model.uin.substring(0, 6);
        var monthDigits = parseInt(dateOfBirth.substring(2, 4), 10);
        if (monthDigits > 13) {
          monthDigits = monthDigits - 40;
          monthDigits = monthDigits < 10 ? '0' + monthDigits : monthDigits;
          dateOfBirth = dateOfBirth.substring(0, 2) + monthDigits + dateOfBirth.substring(4, 6);
          $scope.model.dateOfBirth = moment('20' + dateOfBirth, 'YYYYMMDD').format();
        }
        else {
          $scope.model.dateOfBirth = moment('19' + dateOfBirth, 'YYYYMMDD').format();
        }
      }
    };
  }

  PersonDataCtrl.$inject = ['$scope', 'Persons', 'Nomenclatures'];

  angular.module('gva').controller('PersonDataCtrl', PersonDataCtrl);
}(angular, moment));
