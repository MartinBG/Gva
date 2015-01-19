/*global angular, _*/
(function (angular, _) {
  'use strict';

  function PersonReportCtrl($scope, Persons, scModal, scMessage, scFormParams) {
    $scope.hideCaseType = scFormParams.hideCaseType;
    $scope.isNew = scFormParams.isNew; 
    $scope.includedChecks = [];
    if ($scope.model.part) {
      $scope.model.part.includedChecks = $scope.model.part.includedChecks || [];
      $scope.model.part.includedPersons = $scope.model.part.includedPersons || [];
    }

    if (!$scope.isNew && $scope.model.part.includedChecks.length) {
      Persons
        .getChecksForReport({
          checks: $scope.model.part.includedChecks
        })
        .$promise
        .then(function (checks) {
          $scope.includedChecks = checks;
        });
    }

    $scope.addCheck = function () {
      var modalInstance = scModal.open('choosePerson', {showPersonTitle: true});

      modalInstance.result.then(function (selectedPerson) {
         $scope.createCheck(selectedPerson);
      });

      return modalInstance.opened;
    };

    $scope.createCheck = function (person) {
      var modalInstance = scModal.open('newCheck', {
        lotId: person.id,
        publisher: scFormParams.publisher
      });

      modalInstance.result.then(function (newCheck) {
        var check = newCheck.part;
        check.personLin = person.lin;
        check.lotId = person.id;
        check.partIndex = newCheck.partIndex;
        check.ratingTypes = _.pluck(newCheck.part.ratingTypes, 'code').join(',');
        $scope.includedChecks.push(check);

        $scope.model.part.includedChecks.push(newCheck.partId);
      });

      return modalInstance.opened;
    };

    $scope.removeCheck = function (check) {
      return scMessage('common.messages.confirmDelete')
        .then(function (result) {
          if (result === 'OK') {
            $scope.includedChecks = _.without($scope.includedChecks, check);

            _.remove($scope.model.part.includedChecks,
              function(includedCheck) {
                return check.partId === includedCheck;
              });
          }
        });
    };

    $scope.isUniqueDocNumber = function () {
      if($scope.model.part.documentNumber) {
        return Persons
          .isUniqueDocNumber({
            documentNumber: $scope.model.part.documentNumber,
            documentPersonNumber: $scope.model.part.documentPersonNumber,
            partIndex: $scope.model.partIndex
          })
        .$promise
        .then(function (result) {
          return result.isUnique;
        });
      } else {
        return true;
      }
    };
  }

  PersonReportCtrl.$inject = [
    '$scope',
    'Persons',
    'scModal',
    'scMessage',
    'scFormParams'
  ];

  angular.module('gva').controller('PersonReportCtrl', PersonReportCtrl);
}(angular, _));
