/*global angular, _*/
(function (angular, _) {
  'use strict';

  function PersonReportCtrl($scope, Persons, scModal, scMessage, scFormParams) {
    $scope.includedChecks = [];
    if ($scope.model.part) {
      $scope.model.part.includedChecks = $scope.model.part.includedChecks || [];
      $scope.model.part.includedPersons = $scope.model.part.includedPersons || [];
    }

    if (!scFormParams.isNew && $scope.model.part.includedChecks.length) {
      Persons
        .getChecksForReport({
          checks: $scope.model.part.includedChecks
        })
        .$promise
        .then(function (checks) {
          $scope.includedChecks = checks;
        });
    }

    $scope.addPersons = function () {
      var modalInstance = scModal.open('choosePersons', {
        includedPersons:  $scope.model.part.includedPersons
      });

      modalInstance.result.then(function (selectedPersons) {
        _.forEach(selectedPersons, function (person) {
          $scope.model.part.includedPersons.push({
            id: person.id,
            lin: person.lin
          });
        });
      });

      return modalInstance.opened;
    };

    $scope.addCheck = function (person) {
      var modalInstance = scModal.open('newCheck', {
        lotId: person.id
      });

      modalInstance.result.then(function (newCheck) {
        var check = newCheck.part;
        check.personLin = person.lin;
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

    $scope.removePerson = function (person) {
      return scMessage('common.messages.confirmDelete')
        .then(function (result) {
          if (result === 'OK') {
            $scope.model.part.includedPersons = 
              _.without($scope.model.part.includedPersons, person);
          }
        });
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
