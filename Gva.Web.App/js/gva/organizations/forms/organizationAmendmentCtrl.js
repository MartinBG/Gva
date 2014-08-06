/*global angular, _*/
(function (angular, _) {
  'use strict';
  function OrganizationAmendmentCtrl(
    $scope,
    $state,
    scModal,
    scFormParams) {
    $scope.lotId = scFormParams.lotId;

    $scope.select2Options = {
      multiple: false,
      allowClear: true,
      placeholder: ' '
    };

    var updateSelect2Options = function (limitations) {
      var select2Elem = angular.element('.select2input'),
          currSelection = select2Elem.select2('val'),
          tags = [],
          newSelection;

      angular.forEach(limitations, function(lim){
        if(lim.lim147limitation) {
          tags.push(lim.lim147limitation);
        } else if(lim.lim145limitation) {
          tags.push(lim.lim145limitation);
        } else if (lim.aircraftTypeGroup) {
          tags.push(lim.aircraftTypeGroup);
        }
      });

      newSelection = _.filter(currSelection, function (value) {
        return _.contains(tags, value);
      });

      $scope.select2Options.tags = tags;
      select2Elem.select2($scope.select2Options);
      select2Elem.select2('val', newSelection);
    };

    $scope.deleteDocument = function (document) {
      var index = $scope.model.includedDocuments.indexOf(document);
      $scope.model.includedDocuments.splice(index, 1);
    };

    $scope.chooseDocuments = function () {
      var modalInstance = scModal.open('chooseOrganizationDocs', {
        includedDocs: _.pluck($scope.model.includedDocuments, 'partIndex'),
        lotId: scFormParams.lotId
      });

      modalInstance.result.then(function (selectedDocs) {
        $scope.model.includedDocuments = $scope.model.includedDocuments.concat(selectedDocs);
      });
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

    $scope.chooseLimitation = function (section) {
      var modalInstance = scModal.open('chooseLimitation', { section: section });

      modalInstance.result.then(function (limitationName) {
        if (section.alias === 'lim147limitations') {
          $scope.model.lims147[section.index].lim147limitation = limitationName;
        } else if (section.alias === 'lim145limitations') {
          $scope.model.lims145[section.index].lim145limitation = limitationName;
        } else if (section.alias === 'aircraftTypeGroups') {
          $scope.model.limsMG[section.index].aircraftTypeGroup = limitationName;
        }
      });
    };

    $scope.deleteLimitation147 = function (index) {
      $scope.model.lims147.splice(index, 1);
    };

    $scope.addLimitation147 = function () {
      var sortOder = Math.max(0, _.max(_.pluck($scope.model.lims147, 'sortOrder'))) + 1;

      $scope.model.lims147.push({
        sortOrder: sortOder
      });
    };

    $scope.deleteLimitation145 = function (index) {
      $scope.model.lims145.splice(index, 1);
    };

    $scope.addLimitation145 = function () {
      $scope.model.lims145.push({});
    };

    $scope.deleteLimitationMG = function (index) {
      $scope.model.limsMG.splice(index, 1);
    };

    $scope.addLimitationMG = function () {
      $scope.model.limsMG.push({});
    };
  }

  OrganizationAmendmentCtrl.$inject = [
    '$scope',
    '$state',
    'scModal',
    'scFormParams'
  ];

  angular.module('gva').controller('OrganizationAmendmentCtrl', OrganizationAmendmentCtrl);
}(angular, _));
