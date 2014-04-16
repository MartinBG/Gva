/*global angular, moment*/
(function (angular, moment) {
  'use strict';

  function PersonDataCtrl($scope, $stateParams, Person, Nomenclature) {
    Nomenclature.query({alias: 'linTypes'})
      .$promise.then(function(linTypes){
        $scope.linTypes = linTypes;
      });

    $scope.setLin = function (item) {
      $scope.model.linType = item;
      $scope.model.lin = null;

      Person.getNextLin({
        linType: item.code
      })
      .$promise .then(function(result){
        $scope.model.lin = result.nextLin;
      });
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

      var isValidUin = checkDigit === parseInt($scope.model.uin[9], 10);

      if ($scope.model.dateOfBirth) {
        var dateOfBirth = moment($scope.model.dateOfBirth);
        var formattedDate;
        
        if (dateOfBirth.year() >= 2000 && dateOfBirth.year() < 2100) {
          formattedDate = dateOfBirth.format('YY') +
            (dateOfBirth.month() + 41) +
            dateOfBirth.format('DD');
        }
        else {
          formattedDate = dateOfBirth.format('YYMMDD');
        }

        isValidUin = isValidUin && $scope.model.uin.substring(0, 6) === formattedDate;
      }

      if ($scope.model.sex) {
        var isFemale = $scope.model.sex.alias === 'Female';
        var sexDigit = parseInt($scope.model.uin[8], 10);

        isValidUin = isValidUin &&
          (isFemale && sexDigit % 2 === 1 || !isFemale && sexDigit % 2 === 0);
      }

      return isValidUin;
    };
  }

  PersonDataCtrl.$inject = ['$scope', '$stateParams', 'Person', 'Nomenclature'];

  angular.module('gva').controller('PersonDataCtrl', PersonDataCtrl);
}(angular, moment));
