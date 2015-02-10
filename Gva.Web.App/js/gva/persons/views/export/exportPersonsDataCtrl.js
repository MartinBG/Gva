/*global angular, _*/
(function (angular, _) {
  'use strict';

  function ExportPersonsDataCtrl(
    $scope,
    scModal
  ) {
    $scope.personsForExport = [];
    $scope.personIds  = [];
    $scope.addPersons = function () {
      var modalInstance = scModal.open('choosePersons',
        { includedPersons: $scope.personsForExport });

      modalInstance.result.then(function (persons) {
        $scope.personsForExport = $scope.personsForExport.concat(persons);
        $scope.personIds = _.pluck($scope.personsForExport, 'id');
      });

      return modalInstance.opened;
    };

    $scope.removePerson = function (person) {
      $scope.personsForExport = _.without($scope.personsForExport, person);
      $scope.personIds = _.without($scope.personIds, person.id);
    };
  }

  ExportPersonsDataCtrl.$inject = ['$scope', 'scModal'];

  angular.module('gva').controller('ExportPersonsDataCtrl', ExportPersonsDataCtrl);
}(angular, _));
