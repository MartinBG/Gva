/*global angular, _, require*/
(function (angular, _) {
  'use strict';

  function DocsNewCtrl(
    $q,
    $scope,
    $state,
    Doc
  ) {
    var nomenclatures = require('./nomenclatures.sample');

    Doc.create().$promise
      .then(function (result) {
        $scope.doc = result;

        $scope.docFormatTypes = nomenclatures.docFormatTypes;
        $scope.doc.docFormatTypeId =
          _($scope.docFormatTypes).filter({'isActive': true}).first().docFormatTypeId;

        $scope.docCasePartTypes = nomenclatures.docCasePartTypes;
        $scope.doc.docCasePartTypeId =
          _($scope.docCasePartTypes).filter({'isActive': true}).first().docCasePartTypeId;

        $scope.docDirections = nomenclatures.docDirections;
        $scope.doc.docDirectionId =
          _($scope.docDirections).filter({'isActive': true}).first().docDirectionId;
        $scope.doc.docDirectionName =
          _($scope.docDirections).filter({'isActive': true}).first().name;
      });

    $scope.docFormatTypeChange = function ($index) {
      _.forOwn($scope.docFormatTypes, function (item) {
          item.isActive = false;
        });

      $scope.docFormatTypes[$index].isActive = true;
      $scope.doc.docFormatTypeId = $scope.docFormatTypes[$index].docFormatTypeId;
    };

    $scope.docCasePartTypeChange = function ($index) {
      _.forOwn($scope.docCasePartTypes, function (item) {
          item.isActive = false;
        });

      $scope.docCasePartTypes[$index].isActive = true;
      $scope.doc.docCasePartTypeId = $scope.docCasePartTypes[$index].docCasePartTypeId;
    };

    $scope.docDirectionChange = function ($index) {
      _.forOwn($scope.docDirections, function (item) {
          item.isActive = false;
        });

      $scope.docDirections[$index].isActive = true;
      $scope.doc.docDirectionId = $scope.docDirections[$index].docDirectionId;
      $scope.doc.docDirectionName = $scope.docDirections[$index].name;
    };

    $scope.save = function () {
      if ($scope.docForm.$valid) {
        $scope.doc.docTypeGroupId = $scope.doc.docTypeGroupId.nomTypeValueId;
        $scope.doc.docTypeId = $scope.doc.docTypeId.nomTypeValueId;
        $scope.doc.correspondentName = 'TBD';
        $scope.doc.statusId = 2;
        $scope.doc.regDate = new Date();
        $scope.doc.docStatusName = 'Чернова';

        $scope.doc.$saveNew().then(function () {
          $state.go('docs/search');
        });
      }
    };

    $scope.cancel = function () {
      $state.go('docs/search');
    };
  }

  DocsNewCtrl.$inject = [
    '$q',
    '$scope',
    '$state',
    'Doc'
  ];

  angular.module('ems').controller('DocsNewCtrl', DocsNewCtrl);
}(angular, _));
