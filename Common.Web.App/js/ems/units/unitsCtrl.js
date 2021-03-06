﻿/*global angular*/
(function (angular) {
  'use strict';

  function UnitsCtrl($scope, $state, $stateParams,
    unitsModel,
    scModal,
    scMessage,
    UnitsResource,
    UnitUsersResource, 
    l10n) {
    
    $scope.model = unitsModel;
    $scope.filterValue = '';
    $scope.includeInactive = false;
    $scope.selectedUnit = null;
    $scope.errorMessages = [];

    $scope.refresh = function () {
      return refreshData();
    };

    $scope.canUnitBeDeleted = function (unit) {
      return unit.childUnits.length === 0;
    };

    $scope.deleteUnit = function (unit) {
      return scMessage('Моля, потвърдете изтриването на организационна единица.')
        .then(function (result) {          
          if (result === 'OK') {
            return UnitsResource.remove({ id: unit.unitId })
              .$promise.then(function () {
                return refreshData();
              }, function (response) {
                showResourceError(response);
              });
          }
        });
    };

    $scope.closeAlert = function (index) {
      $scope.errorMessages.splice(index, 1);
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
      return UnitsResource.setActiveStatus({ id: unit.unitId, isActive: isActive }, null)
         .$promise.then(function () {           
           return refreshData();
         }, function (response) {
           showResourceError(response);
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

      return modalInstance.result.then(function (returnedResult) {
        if (returnedResult) {
          //modalInstance.close();
          return refreshData();
        }
      });
    };

    $scope.editUnit = function (unitId) {
      return UnitsResource.get({}, { unitId: unitId })
        .$promise.then(function (unit) {

          var modalInstance = scModal.open('editUnitModal', {
            isEditMode: true,
            unit: unit
          });

          return modalInstance.result.then(function (returnedResult) {
            if (returnedResult) {
              return refreshData();
            }
          });
        });
    };

    $scope.attachUserToUnit = function (unitId) {
      var modalInstance = scModal.open('selectUserModal', {
        unitId: unitId
      });

      return modalInstance.result.then(function (returnedResult) {
        if (returnedResult) {
          return refreshData();
        }
      });
    };

    $scope.detachUserFromUnit = function (unitId, userId) {
      return scMessage('Моля, потвърдете премахването на връзката с потребител.')
        .then(function (result) {
          if (result === 'OK') {
            return UnitUsersResource.remove({ id: unitId, userId: userId }).$promise
              .then(function () {
              return refreshData();
            });
          }
        });
    };

    function showResourceError(response) {
      if (response.status === 422) {
        for (var i = 0; i < response.data.messages.length; i++) {
          $scope.errorMessages.push({
            message: l10n.get('common.units.errors.' + response.data.messages[i].domainErrorCode)
          });
        }
      }
    }

    function refreshData() {
      return UnitsResource.query({ includeInactive: $scope.includeInactive })
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
    'UnitsResource', 'UnitUsersResource', 'l10n'];

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
