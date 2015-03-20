/*global angular*/
(function (angular) {
  'use strict';

  function UnitsCtrl($scope, $state, $stateParams, unitsModel, scModal, UnitsResource) {
    $scope.model = unitsModel;
    $scope.filterValue = '';
    $scope.includeInactive = false;
    $scope.selectedUnit = null;

    $scope.refresh = function () {
      refreshData();
    };

    // This function is currently not used.
    function getHierarchyItemById(hierarchyItem, id) {
      if (hierarchyItem.unitId === id) {
        return hierarchyItem;
      }

      for (var i = 0; i < hierarchyItem.childUnits.length; i++) {
        var result = getHierarchyItemById(hierarchyItem.childUnits[i], id);
        if (result !== null) {
          return result;
        }
      }

      return null;
    }

    function refreshData() {
      UnitsResource.query({ includeInactive: $scope.includeInactive })
        .$promise.then(function (unitsModel) {
          $scope.model = unitsModel;
        });
    }

    $scope.canUnitBeDeleted = function (unit) {
      return unit.childUnits.length === 0;
    };



    $scope.deleteUnit = function (unit) {
      UnitsResource.delete({ id: unitId })
        .$promise.then(function () {
          refreshData();
        });
      // test code 
      //var item = getHierarchyItemById($scope.model[0], unit.parentUnitId);
      //if (item) {
      //  var index = null;
      //  // get index of childunit in array so we can remove it
      //  for (var i = 0; i < item.childUnits.length; i++) {
      //    if (item.childUnits[i].unitId = unit.unitId) {            
      //      item.childUnits.splice(i, 1);
      //      break;
      //    }
      //  }
      //}
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

      UnitsResource.setActiveStatus({ id: unit.unitId, isActive: isActive }, null)
         .$promise.then(function () {
           unit.isActive = isActive;
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

    $scope.addNewUnit = function (parentId, type) {
      var modalInstance = scModal.open('editUnitModal', {
        parentId: parentId,
        type: type
      });

      modalInstance.result.then(function (returnedResult) {
        if (returnedResult) {
          refreshData();
        }
      });
    };

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
        // We need to check if ANY of the returned result is true, so we don't hide this (parent) node.
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

  UnitsCtrl.$inject = ['$scope', '$state', '$stateParams', 'unitsModel', 'scModal', 'UnitsResource'];

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
