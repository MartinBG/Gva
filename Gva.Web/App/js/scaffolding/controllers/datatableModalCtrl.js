(function (angular) {
  'use strict';

  function DatatableModalCtrl ($scope, dialog, columnDefs) {
    $scope.columnDefs = columnDefs;

    $scope.close = function(){
      dialog.close($scope.columnDefs);
    };

    $scope.cancel = function (){
      dialog.close();
    };
  }

  DatatableModalCtrl.$inject = ['$scope','dialog', 'columnDefs'];
  angular.module('scaffolding').controller('scaffolding.DatatableModalCtrl', DatatableModalCtrl);
}(angular));