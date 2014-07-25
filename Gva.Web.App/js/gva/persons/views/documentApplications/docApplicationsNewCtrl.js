﻿/*global angular*/
(function (angular) {
  'use strict';

  function DocApplicationsNewCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentApplications,
    personDocumentApplication
  ) {
    $scope.personDocumentApplication = personDocumentApplication;
    $scope.lotId = $stateParams.id;

    $scope.save = function () {
      return $scope.newDocumentApplicationForm.$validate()
        .then(function () {
          if ($scope.newDocumentApplicationForm.$valid) {
            return PersonDocumentApplications
              .save({ id: $stateParams.id }, $scope.personDocumentApplication).$promise
              .then(function () {
                return $state.go('root.persons.view.documentApplications.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.persons.view.documentApplications.search');
    };
  }

  DocApplicationsNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentApplications',
    'personDocumentApplication'
  ];
  DocApplicationsNewCtrl.$resolve = {
    personDocumentApplication: function () {
      return {
        part: {},
        files: {
          hideApplications: true,
          files: []
        }
      };
    }
  };

  angular.module('gva').controller('DocApplicationsNewCtrl', DocApplicationsNewCtrl);
}(angular));
