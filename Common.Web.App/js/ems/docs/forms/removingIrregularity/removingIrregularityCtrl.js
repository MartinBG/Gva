/*global angular, moment, _*/
(function (angular, moment, _) {
  'use strict';

  function RemovingIrregularityCtrl(
    $scope,
    Docs,
    Nomenclatures
  ) {

    Docs.getRioEditableFile({
      id: $scope.model.docId
    }).$promise.then(function (result) {

      $scope.model.jObject = result.content;

      if (!!$scope.model.jObject.DeadlineCorrectionIrregularities) {
        $scope.deadlinePeriod = {
          days:
            moment.duration($scope.model.jObject.DeadlineCorrectionIrregularities).asDays()
        };
      }
      else {
        $scope.deadlinePeriod = {
          days: null
        };
      }

      Nomenclatures.query({ alias: 'electronicServiceProvider' }).$promise
        .then(function (result) {
          var nomId = null;

          if (!!$scope.model.jObject
            .ElectronicServiceProviderBasicData.ElectronicServiceProviderType) {
            nomId = _(result).filter({
              code: $scope.model.jObject
                .ElectronicServiceProviderBasicData.ElectronicServiceProviderType
            }).first().nomValueId;
          }

          $scope.serviceProvider = {
            obj: {},
            id: nomId
          };

          Nomenclatures.query({ alias: 'irregularityType' }).$promise
          .then(function (noms) {
            _($scope.model.jObject.IrregularitiesCollection).forEach(function (item) {
              item.irregularityTypeId = _(noms).filter({
                name: item.IrregularityType
              }).first().nomValueId;
            });

            $scope.isLoaded = true;
          });

        });
      });

    $scope.serviceProviderChange = function () {
      $scope.model.jObject.ElectronicServiceProviderBasicData.ElectronicServiceProviderType =
        $scope.serviceProvider.obj.code;
      $scope.model.jObject.ElectronicServiceProviderBasicData.EntityBasicData.Identifier =
        $scope.serviceProvider.obj.bulstat;
      $scope.model.jObject.ElectronicServiceProviderBasicData.EntityBasicData.Name =
        $scope.serviceProvider.obj.name;
    };

    $scope.deadlinePeriodChange = function () {
      if (!!$scope.deadlinePeriod.days) {
        $scope.model.jObject.DeadlineCorrectionIrregularities =
          'P' + $scope.deadlinePeriod.days + 'D';
      }
    };

    $scope.addIrregularity = function () {
      $scope.model.jObject.IrregularitiesCollection.push({
        IrregularityType: '',
        AdditionalInformationSpecifyingIrregularity: ''
      });
    };

    $scope.removeIrregularity = function (index) {
      $scope.model.jObject.IrregularitiesCollection.splice(index, 1);
    };

    $scope.irregularityChange = function (index) {
      $scope.model.jObject.IrregularitiesCollection[index].IrregularityType =
        $scope.model.jObject.IrregularitiesCollection[index].obj.name;
      $scope.model.jObject.IrregularitiesCollection[index].
        AdditionalInformationSpecifyingIrregularity = $scope.model.jObject
        .IrregularitiesCollection[index].obj.description;
    }; 
  }

  RemovingIrregularityCtrl.$inject = [
    '$scope',
    'Docs',
    'Nomenclatures'
  ];

  angular.module('ems').controller('RemovingIrregularityCtrl', RemovingIrregularityCtrl);
}(angular, moment, _));
