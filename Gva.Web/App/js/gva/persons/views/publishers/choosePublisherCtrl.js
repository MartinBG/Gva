/*global angular*/
(function (angular) {
  'use strict';

  function ChoosePublisherCtrl(
    $state,
    $stateParams,
    $scope,
    Publisher,
    Nomenclature,
    publishers,
    selectedPublisher,
    publisherType
  ) {
    $scope.filters = {
      text: null,
      publisherType: null
    };

    $scope.publishers = publishers;

    if ($stateParams.text) {
      $scope.filters.text = $stateParams.text;
    }

    if ($stateParams.publisherTypeAlias) {
      $scope.filters.publisherType = publisherType;
    }

    $scope.search = function () {
      return $state.go($state.current, {
        text: $scope.filters.text,
        publisherTypeAlias: $scope.filters.publisherType ?
          $scope.filters.publisherType.alias : undefined
      });
    };

    $scope.selectPublisher = function (publisher) {
      selectedPublisher.push(publisher.name);
      return $state.go('^');
    };

    $scope.goBack = function () {
      return $state.go('^');
    };

  }

  ChoosePublisherCtrl.$inject = [
    '$state',
    '$stateParams',
    '$scope',
    'Publisher',
    'Nomenclature',
    'publishers',
    'selectedPublisher',
    'publisherType'
  ];

  ChoosePublisherCtrl.$resolve = {
    publishers: [
      '$stateParams',
      'Publisher',
      function ($stateParams, Publisher) {
        return Publisher.query({
          text: $stateParams.text,
          publisherTypeAlias: $stateParams.publisherTypeAlias
        }).$promise;
      }
    ],
    publisherType: [
      '$stateParams',
      'Nomenclature',
      function ($stateParams, Nomenclature) {
        if ($stateParams.publisherTypeAlias) {
          return Nomenclature.get({
            alias: 'publisherTypes',
            valueAlias: $stateParams.publisherTypeAlias
          }).$promise;
        } else {
          return null;
        }
      }
    ]
  };

  angular.module('gva').controller('ChoosePublisherCtrl', ChoosePublisherCtrl);
}(angular));
