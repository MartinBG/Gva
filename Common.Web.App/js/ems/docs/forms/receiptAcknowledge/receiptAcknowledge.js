/*global angular, _*/
(function (angular, _) {
  'use strict';

  function ReceiptAcknowledgeCtrl(
    $scope,
    Docs,
    Nomenclatures
  ) {

    Docs.getRioEditableFile({
      id: $scope.model.docId
    }).$promise.then(function (result) {
      $scope.model.jObject = result.content;

      Nomenclatures.query({ alias: 'electronicServiceProvider' }).$promise.then(function (result) {
        var nomId = null;

        if ($scope.model.jObject.electronicServiceProvider.electronicServiceProviderType) {
          nomId = _(result).filter({
            code: $scope.model.jObject.electronicServiceProvider.electronicServiceProviderType
          }).first().nomValueId;
        }

        $scope.serviceProvider = {
          obj: {},
          id: nomId
        };

        if ($scope.model.jObject.caseAccessIdentifier) {
          var regex = new RegExp('<b>(.*?)<\/b>', 'g');

          var uri = regex.exec($scope.model.jObject.caseAccessIdentifier)[1];
          var accessCode = regex.exec($scope.model.jObject.caseAccessIdentifier)[1];

          $scope.caseInfo = {
            uri: uri,
            accessCode: accessCode
          };
        }
        else {
          $scope.caseInfo = {
            uri: null,
            accessCode: null
          };
        }

        $scope.isLoaded = true;
      });
    });

    $scope.serviceProviderChange = function () {
      $scope.model.jObject.electronicServiceProvider.electronicServiceProviderType =
        $scope.serviceProvider.obj.code;
      $scope.model.jObject.electronicServiceProvider.entityBasicData.identifier =
        $scope.serviceProvider.obj.bulstat;
      $scope.model.jObject.electronicServiceProvider.entityBasicData.name =
        $scope.serviceProvider.obj.name;
    };

    $scope.caseInfoChange = function () {
      $scope.model.jObject.caseAccessIdentifier =
        '<p>Номер на преписка: <b>' + $scope.caseInfo.uri + '</b><br/>' +
        'Код за достъп: <b>' + $scope.caseInfo.accessCode + '</b><br/></p>';
    };

  }

  ReceiptAcknowledgeCtrl.$inject = [
    '$scope',
    'Docs',
    'Nomenclatures'
  ];

  angular.module('ems').controller('ReceiptAcknowledgeCtrl', ReceiptAcknowledgeCtrl);
}(angular, _));
