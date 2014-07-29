/*global angular, _*/
(function (angular, _) {
  'use strict';

  function CommonInspectionCtrl(
    $scope,
    $state,
    $stateParams,
    Nomenclatures,
    scFormParams
  ) {
    $scope.watchList = [];

    $scope.model.part.examiners = $scope.model.part.examiners || [];
    $scope.model.part.auditDetails = $scope.model.part.auditDetails || [];
    $scope.model.part.disparities = $scope.model.part.disparities || [];
    $scope.caseTypesOptions = {
      multiple: false,
      allowClear: true,
      placeholder: ' ',
      tags: []
    };

    if($scope.$parent.organization) {
      Nomenclatures.query({
        alias: 'organizationCaseTypes',
        lotId: $stateParams.id
      }).$promise.then(function(result){
        $scope.caseTypesOptions.tags = _.map(result, function (item) {
          return item.name;
        });
        angular.element('.select2input').select2($scope.caseTypesOptions);
      });
    }

    $scope.setPart = scFormParams.setPart;

    var aliases = {
      organization: 'Организация',
      aircraft: 'ВС',
      aiport: 'Летище',
      equipment: 'Съоръжение'
    };

    $scope.model.part.setPart = aliases[$scope.setPart];


    $scope.changedSortOrder = function (newValue, oldValue) {
      if (_.where($scope.model.part.disparities, { sortOrder: newValue })[0]) {
        var subject = _.where($scope.model.part.disparities, { sortOrder: newValue })[0].subject,
          auditDetail = _.where($scope.model.part.auditDetails, { subject: subject })[0],
          sortOrderIndex = auditDetail.disparities.indexOf(oldValue);
        auditDetail.disparities[sortOrderIndex] = newValue;
      }
    };

    for (var index = 0; index < $scope.model.part.disparities.length; index++) {
      var watchString = 'model.part.disparities[' + index + '].sortOrder';
      $scope.watchList.push($scope.$watch(watchString, $scope.changedSortOrder));
    }

    $scope.addDisparity = function (detail) {
      var maxSortNumber = 0;

      _.each($scope.model.part.auditDetails, function (auditDetail) {
        maxSortNumber = Math.max(maxSortNumber, _.max(auditDetail.disparities));
      });

      detail.disparities.push(++maxSortNumber);

      $scope.model.part.disparities.push({
        sortOrder: maxSortNumber,
        subject: detail.subject
      });
      var watchString = 'model.part.disparities[' +
        $scope.model.part.disparities.length +
        '].sortOrder';
      $scope.watchList.push($scope.$watch(watchString, $scope.changedSortOrder));
    };

    $scope.deleteDisparity = function (disparity) {
      var auditDetail = _.where($scope.model.part.auditDetails, { subject: disparity.subject })[0];

      var sortOrderIndex = auditDetail.disparities.indexOf(disparity.sortOrder);
      auditDetail.disparities.splice(sortOrderIndex, 1);

      var index = $scope.model.part.disparities.indexOf(disparity);
      $scope.model.part.disparities.splice(index, 1);
      $scope.watchList[index]();
    };

    $scope.viewRecommendation = function (recommendation) {
      return $state.go('root.organizations.view.recommendations.edit', {
        id: $stateParams.id,
        ind: recommendation.partIndex
      });
    };
  }

  CommonInspectionCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Nomenclatures',
    'scFormParams'
  ];

  angular.module('gva').controller('CommonInspectionCtrl', CommonInspectionCtrl);
}(angular, _));
