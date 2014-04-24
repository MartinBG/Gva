/*global angular, _*/
(function (angular, _) {
  'use strict';
  function OrganizationAmendmentCtrl($scope, $state) {

    $scope.deleteDocument = function (document) {
      var index = $scope.model.includedDocuments.indexOf(document);
      $scope.model.includedDocuments.splice(index, 1);
    };
    $scope.select2Options = {
      multiple: false,
      allowClear: true,
      placeholder: ' '
    };
    
    var updateSelect2Options = function (limitations) {
      $scope.select2Options.tags = [];
      angular.forEach(limitations, function(lim){
        if(lim.lim147limitation) {
          $scope.select2Options.tags.push(lim.lim147limitation);
        } else if(lim.lim145limitation) {
          $scope.select2Options.tags.push(lim.lim145limitation);
        } else if (lim.aircraftTypeGroup) {
          $scope.select2Options.tags.push(lim.aircraftTypeGroup);
        }
      });

      angular.element('.select2input').select2($scope.select2Options);
    };

    $scope.$watch('model.lims147.length', function () {
      if($scope.model) {
        updateSelect2Options($scope.model.lims147);
        angular.forEach($scope.model.lims147, function(lim, key){
          $scope.$watch('model.lims147[' + key + '].lim147limitation',
            function(){
              updateSelect2Options($scope.model.lims147);
            });
        });
      }
    });

    $scope.$watch('model.lims145.length', function () {
      if($scope.model) {
        updateSelect2Options($scope.model.lims145);
        angular.forEach($scope.model.lims145, function(lim, key){
          $scope.$watch('model.lims145[' + key + '].lim145limitation',
            function(){
              updateSelect2Options($scope.model.lims145);
            });
        });
      }
    });

    $scope.$watch('model.limsMG.length', function () {
      if($scope.model) {
        updateSelect2Options($scope.model.limsMG);
        angular.forEach($scope.model.limsMG, function(lim, key){
          $scope.$watch('model.limsMG[' + key + '].aircraftTypeGroup',
            function(){
              updateSelect2Options($scope.model.limsMG);
            });
        });
      }
    });

    $scope.chooseDocuments = function () {
      $state.go('.chooseDocuments', {}, {}, {
        selectedDocuments: $scope.model.includedDocuments
      });
    };

    $scope.$watch('model', function(){
      if($scope.model){
        if ($state.previous && $state.previous.includes[$state.current.name] && $state.payload) {
          if ($state.payload.selectedDocuments) {
            [].push.apply($scope.model.includedDocuments, $state.payload.selectedDocuments);
          }
        }
      }
    });
    // coming from a child state and carrying payload
    

    $scope.chooseLimitation = function (section) {
      return $state.go('.chooseLimitation',
        {limitationAlias: section.alias, index: section.index});
    };

    $scope.deleteLimitation147 = function (limitation) {
      var index = $scope.model.lims147.indexOf(limitation);
      $scope.model.lims147.splice(index, 1);
    };

    $scope.addLimitation147 = function () {
      var sortOder = Math.max(0, _.max(_.pluck($scope.model.lims147, 'sortOrder'))) + 1;

      $scope.model.lims147.push({
        sortOrder: sortOder
      });
    };

    $scope.deleteLimitation145= function (limitation) {
      var index = $scope.model.lims145.indexOf(limitation);
      $scope.model.lims145.splice(index, 1);
    };

    $scope.addLimitation145 = function () {
      $scope.model.lims145.push({});
    };

    $scope.deleteLimitationMG = function (limitation) {
      var index = $scope.model.limsMG.indexOf(limitation);
      $scope.model.limsMG.splice(index, 1);
    };

    $scope.addLimitationMG = function () {
      $scope.model.limsMG.push({});
    };

    $scope.viewDocument = function (document) {
      var state;

      if (document.setPartAlias === 'organizationOther') {
        state = 'root.organizations.view.documentOthers.edit';
      }
      else if (document.setPartAlias === 'organizationApplication') {
        state = 'root.organizations.view.documentApplications.edit';
      }

      return $state.go(state, { ind: document.partIndex });
    };
  }

  OrganizationAmendmentCtrl.$inject = ['$scope', '$state'];

  angular.module('gva').controller('OrganizationAmendmentCtrl', OrganizationAmendmentCtrl);
}(angular, _));
