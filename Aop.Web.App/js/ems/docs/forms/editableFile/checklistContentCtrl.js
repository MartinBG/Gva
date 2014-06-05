/*global angular, _, $*/
(function (angular, _, $) {
  'use strict';

  function ChecklistContentCtrl(
    $scope,
    $parse
  ) {
    $scope.$watch('model.compareToVersion', function () {
      $('input[type=checkbox]').removeClass('checklist-current-value-checkbox');
      $('input[type=checkbox]').removeClass('checklist-compared-value-checkbox');

      _.each($('input[type=checkbox]'), function (checkbox) {
        var ngModelStr = checkbox.attributes['ng-model'].value,
          currentCheckbox = $parse(ngModelStr)($scope),
          compareCheckbox =
            $parse(ngModelStr.replace('currentVersion', 'compareToVersion'))($scope);

        if($scope.model.isComparing && currentCheckbox !== compareCheckbox) {
          if (currentCheckbox === true) {
            checkbox.classList.add('checklist-current-value-checkbox');
          } else if (compareCheckbox === true) {
            checkbox.classList.add('checklist-compared-value-checkbox');
          }
        }
      });
    });
  }

  ChecklistContentCtrl.$inject = [
    '$scope',
    '$parse'
  ];

  angular.module('ems').controller('ChecklistContentCtrl', ChecklistContentCtrl);
}(angular, _, $));
