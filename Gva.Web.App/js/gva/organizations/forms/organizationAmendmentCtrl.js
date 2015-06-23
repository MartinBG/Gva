/*global angular, _*/
(function (angular, _) {
  'use strict';
  function OrganizationAmendmentCtrl(
    $scope,
    $state,
    scModal,
    scFormParams) {
    $scope.lotId = scFormParams.lotId;
    $scope.isNew = scFormParams.isNew;

    var data = [];
    $scope.select2Options = {
      multiple: false,
      allowClear: true,
      placeholder: ' ',
      width: 200,
      data: function () {
        return { results: data };
      }
    };

    var updateSelect2Options = function (limitations) {
      data = [];
      angular.forEach(limitations, function (lim, index) {
        if (lim.lim147limitation) {
          data.push({ id: index, text: lim.lim147limitation });
        } else if(lim.lim145limitation) {
          data.push({ id: index, text: lim.lim145limitation });
        } else if (lim.aircraftTypeGroup) {
          data.push({ id: index, text: lim.aircraftTypeGroup });
        }
      });
    };

    $scope.deleteDocument = function (document) {
      var index = $scope.model.part.includedDocuments.indexOf(document);
      $scope.model.part.includedDocuments.splice(index, 1);
    };

    $scope.chooseDocuments = function () {
      var modalInstance = scModal.open('chooseOrganizationDocs', {
        includedDocs: _.pluck($scope.model.part.includedDocuments, 'partIndex'),
        lotId: scFormParams.lotId
      });

      modalInstance.result.then(function (selectedDocs) {
        $scope.model.part.includedDocuments =
          $scope.model.part.includedDocuments.concat(selectedDocs);
      });
    };

    $scope.viewDocument = function (document) {
      var state;
      var params = {};
      if (document.setPartAlias === 'organizationOther') {
        state = 'root.organizations.view.documentOthers.edit';
        params = { ind: document.partIndex };
      }
      else if (document.setPartAlias === 'organizationApplication') {
        state = 'root.applications.edit.data';
        params = { 
          ind: document.partIndex,
          id: document.applicationId,
          set: 'organization',
          lotId: scFormParams.lotId
        };
      }

      return $state.go(state, params);
    };

    $scope.$watch('model.part.lims147.length', function () {
      if($scope.model) {
        updateSelect2Options($scope.model.part.lims147);
        angular.forEach($scope.model.part.lims147, function(lim, key){
          $scope.$watch('model.part.lims147[' + key + '].lim147limitation',
            function(){
              updateSelect2Options($scope.model.part.lims147);
            });
        });
      }
    });

    $scope.$watch('model.part.lims145.length', function () {
      if($scope.model) {
        updateSelect2Options($scope.model.part.lims145);
        angular.forEach($scope.model.part.lims145, function(lim, key){
          $scope.$watch('model.part.lims145[' + key + '].lim145limitation',
            function(){
              updateSelect2Options($scope.model.part.lims145);
            });
        });
      }
    });

    $scope.$watch('model.part.limsMG.length', function () {
      if($scope.model) {
        updateSelect2Options($scope.model.part.limsMG);
        angular.forEach($scope.model.part.limsMG, function(lim, key){
          $scope.$watch('model.part.limsMG[' + key + '].aircraftTypeGroup',
            function(){
              updateSelect2Options($scope.model.part.limsMG);
            });
        });
      }
    });

    $scope.chooseLimitation = function (section) {
      var modalInstance = scModal.open('chooseLimitation', { section: section });

      modalInstance.result.then(function (limitationName) {
        if (section.alias === 'lim147limitations') {
          $scope.model.part.lims147[section.index].lim147limitation = limitationName;
        } else if (section.alias === 'lim145limitations') {
          $scope.model.part.lims145[section.index].lim145limitation = limitationName;
        } else if (section.alias === 'aircraftTypeGroups') {
          $scope.model.part.limsMG[section.index].aircraftTypeGroup = limitationName;
        }
      });
    };

    $scope.deleteLimitation147 = function (index) {
      $scope.model.part.lims147.splice(index, 1);
    };

    $scope.addLimitation147 = function () {
      var sortOder = Math.max(0, _.max(_.pluck($scope.model.part.lims147, 'sortOrder'))) + 1;

      $scope.model.part.lims147.push({
        sortOrder: sortOder
      });
    };

    $scope.deleteLimitation145 = function (index) {
      $scope.model.part.lims145.splice(index, 1);
    };

    $scope.addLimitation145 = function () {
      $scope.model.part.lims145.push({});
    };

    $scope.deleteLimitationMG = function (index) {
      $scope.model.part.limsMG.splice(index, 1);
    };

    $scope.addLimitationMG = function () {
      $scope.model.part.limsMG.push({});
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
