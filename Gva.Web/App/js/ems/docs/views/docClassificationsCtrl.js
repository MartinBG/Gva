﻿/*global angular*/
(function (angular) {
  'use strict';

  function DocClassificationsCtrl(
    //$q,
    //$scope,
    //$filter,
    //$state,
    //$stateParams,
    //Doc
  ) {

    //if ($stateParams.docId) {
    //  $scope.isEdit = true;
    //  $scope.doc = Doc.get({ docId: $stateParams.docId });
    //} else {
    //  $scope.isEdit = false;

    //  //Corr.create().$promise
    //  //  .then(function (result) {
    //  //    $scope.corr = result;
    //  //  });

    //}

    //$scope.inEditMode = false;

    //$scope.enterEditMode = function () {
    //  $scope.inEditMode = true;
    //};

    //$scope.exitEditMode = function () {
    //  $scope.inEditMode = false;
    //};

  }

  DocClassificationsCtrl.$inject = [
    //'$q',
    //'$scope',
    //'$filter',
    //'$state',
    //'$stateParams',
    //'Doc'
  ];

  angular.module('ems').controller('DocClassificationsCtrl', DocClassificationsCtrl);
}(angular));
