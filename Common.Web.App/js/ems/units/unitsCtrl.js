/*global angular*/
(function (angular) {
  'use strict';

  function UnitsCtrl($scope, $state, $stateParams,
    unitsModel,
    scModal,
    scMessage,
    UnitsResource,
    UnitUsersResource,
    $timeout) {

    $scope.model = unitsModel;
    $scope.filterValue = '';
    $scope.includeInactive = false;
    $scope.selectedUnit = null;

    $scope.refresh = function () {
      //refreshData();
      return UnitsResource
        .query({ includeInactive: $scope.includeInactive })
       .$promise.then(function (unitsModel) {
         $scope.model = unitsModel;
       });
    };

    $scope.canUnitBeDeleted = function (unit) {
      return unit.childUnits.length === 0;
    };

    $scope.deleteUnit = function (unit) {
      UnitsResource.delete({ id: unit.unitId })
        .$promise.then(function () {
          refreshData();
        });
    };

    $scope.setCollapsedStateOfAll = function (state) {
      for (var i = 0; i < $scope.model.length; i++) {
        setCollapsedState($scope.model[i], state);
      }
    };

    $scope.clearFilter = function () {
      $scope.filterValue = '';
    };

    $scope.setUnitActiveStatus = function (unit, isActive) {
      $scope.isLoading = true;
      UnitsResource.setActiveStatus({ id: unit.unitId, isActive: isActive }, null)
         .$promise.then(function () {
           $scope.isLoading = false;
           unit.isActive = isActive;
         }, function () {
           // error
           $scope.isLoading = false;
         });
    };

    $scope.selectUnit = function (unit) {
      var isThereSelection = $scope.selectedUnit ? true : false;

      // deselect if clicked on currently selected item
      if (isThereSelection &&
        $scope.selectedUnit.unitId === unit.unitId) {
        unit.isSelected = false;
        $scope.selectedUnit = null;
        return;
      }

      unit.isSelected = true;

      if (isThereSelection) {
        $scope.selectedUnit.isSelected = false;
      }

      $scope.selectedUnit = unit;
    };

    $scope.$watch('filterValue', function (filterValue, oldValue) {
      if (filterValue !== oldValue) {
        for (var i = 0; i < $scope.model.length; i++) {
          filterHierarchy(filterValue, $scope.model[i]);
        }
      }
    });

    $scope.addNewUnit = function (parentId, unitType) {
      var modalInstance = scModal.open('editUnitModal', {
        isEditMode: false,
        parentId: parentId,
        unitType: unitType
      });

      modalInstance.result.then(function (returnedResult) {
        if (returnedResult) {
          //modalInstance.close();
          refreshData();
        }
      });
    };

    $scope.editUnit = function (unitId) {
      UnitsResource.get({}, { unitId: unitId })
      .$promise.then(function (unit) {

        var modalInstance = scModal.open('editUnitModal', {
          isEditMode: true,
          unit: unit
          //unitId: unitId
          //parentId: parentId,
          //unitType: unitType
        });

        modalInstance.result.then(function (returnedResult) {
          if (returnedResult) {
            //modalInstance.close();
            refreshData();
          }
        });
      });
    };

    $scope.attachUserToUnit = function (unitId) {
      var modalInstance = scModal.open('selectUserModal', {
        unitId: unitId
      });

      modalInstance.result.then(function (returnedResult) {
        if (returnedResult) {
          $timeout(function () {
            refreshData();
          }, 500);
        }
      });
    };

    $scope.detachUserFromUnit = function (unitId, userId) {
      scMessage('Моля, потвърдете премахването на връзката с потребител.')
        .then(function (result) {
          if (result === 'OK') {
            // need this because popup window is rendered after resource loading finishes
            $timeout(function () {
              UnitUsersResource.remove({ id: unitId, userId: userId }, function () {
                refreshData();
              });
            }, 500);
          }
        });
    };

    function refreshData() {
      UnitsResource.query({ includeInactive: $scope.includeInactive })
        .$promise.then(function (unitsModel) {
          $scope.model = unitsModel;
        });
    }

    function setCollapsedState(unit, state) {
      unit.isCollapsed = state;
      if (unit.childUnits) {
        for (var i = 0; i < unit.childUnits.length; i++) {
          setCollapsedState(unit.childUnits[i], state);
        }
      }
    }

    function filterHierarchy(filterValue, item) {
      var collection = item.childUnits;
      var isChildVisible = false;

      for (var i = 0; i < collection.length; i++) {
        // We need to check if ANY of the returned result is true, 
        // so we don't hide this (parent) node.
        var recursiveResult = filterHierarchy(filterValue, collection[i]);
        isChildVisible = isChildVisible || recursiveResult;
      }

      if (isChildVisible === true ||
        doesStringContainsCharacters(item.name, filterValue)) {
        item.isVisible = true;
        return true;
      }
      else {
        item.isVisible = false;
        return false;
      }
    }

    function doesStringContainsCharacters(string, charSequesnce) {
      var doesContain = string.toLowerCase()
        .indexOf(charSequesnce.toLowerCase());

      return doesContain > -1;
    }
  }

  UnitsCtrl.$inject = ['$scope', '$state', '$stateParams',
    'unitsModel', 'scModal', 'scMessage',
    'UnitsResource', 'UnitUsersResource', '$timeout'];

  UnitsCtrl.$resolve = {
    unitsModel: ['$stateParams', 'UnitsResource',
      function ($stateParams, UnitsResource) {
        return UnitsResource.query($stateParams).$promise.then(function (unitsModel) {

          return unitsModel;
        });
      }
    ]
  };

  angular.module('common').controller('UnitsCtrl', UnitsCtrl);
}(angular));
