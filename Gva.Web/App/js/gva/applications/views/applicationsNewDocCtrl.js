/*global angular, _, require*/
(function (angular) {
  'use strict';

  function ApplicationsNewDocCtrl(
    $q,
    $scope,
    $state,
    Application,
    Doc
    ) {
    var nomenclatures = require('./nomenclatures.sample');

    Doc.createNew().$promise
      .then(function (result) {
        $scope.$parent.doc = result;

        $scope.$parent.doc.docFormatTypeId =
          _(nomenclatures.docFormatTypes).filter({ 'alias': 'Paper' }).first().docFormatTypeId;

        $scope.$parent.doc.docCasePartTypeId =
          _(nomenclatures.docCasePartTypes).filter({ 'alias': 'Public' }).first().docCasePartTypeId;

        $scope.$parent.doc.docDirectionId =
          _(nomenclatures.docDirections).filter({ 'alias': 'Incomming' }).first().docDirectionId;
        $scope.$parent.doc.docDirectionName =
          _(nomenclatures.docDirections).filter({ 'alias': 'Incomming' }).first().name;
      });

    Application.createNew().$promise
      .then(function (result) {
        $scope.$parent.gvaApplication = result;
      });

    $scope.newPerson = function () {
      $state.go('applications/new/personNew');
    };

    $scope.choosePerson = function () {
      $state.go('applications/new/personChoose');
    };

    $scope.cancel = function () {
      $state.go('docs/search');
    };

    $scope.save = function () {
      $scope.saveClicked = true;

      if ($scope.docForm.$valid) {
        $scope.$parent.doc.docTypeGroupId = $scope.doc.docTypeGroupId.nomTypeValueId;
        $scope.$parent.doc.docTypeId = $scope.doc.docTypeId.nomTypeValueId;
        //$scope.doc.correspondentName = 'TBD';
        $scope.$parent.doc.statusId = 2;
        $scope.$parent.doc.regDate = new Date();
        $scope.$parent.doc.docStatusName = 'Чернова';

        $scope.$parent.doc.$saveNew().then(function (result) {
          $scope.$parent.gvaApplication.docId = result.docId;
          $scope.$parent.gvaApplication.personLotId = $scope.person.nomTypeValueId;

          $scope.$parent.gvaApplication.$saveNew().then(function () {
            $state.go('docs/search');
          });
        });
      }
    };
  }

  ApplicationsNewDocCtrl.$inject = [
    '$q',
    '$scope',
    '$state',
    'Application',
    'Doc'
  ];

  angular.module('gva').controller('ApplicationsNewDocCtrl', ApplicationsNewDocCtrl);
}(angular, _));
