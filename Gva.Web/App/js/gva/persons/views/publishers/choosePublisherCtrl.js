/*global angular, require*/
(function (angular) {
  'use strict';

  function ChoosePublisherCtrl(
    $state,
    $stateParams,
    $scope,
    Publisher,
    publishers,
    selectedPublisher
  ) {
    $scope.filters = {
      text: null,
      publisherType: null
    };

    $scope.publishers = publishers;

    var nomenclatures = require('./nomenclatures.sample');


    if ($stateParams.text)
    {
      $scope.filters.text = $stateParams.text;
    }

    if ($stateParams.publisherTypeAlias)
    {
      $scope.filters.publisherType = nomenclatures.get('publisherTypes', $stateParams.publisherTypeAlias);
    }

    $scope.search = function () {
      $state.go($state.current, {
        text: $scope.filters.text,
        publisherTypeAlias: $scope.filters.publisherType ?
          $scope.filters.publisherType.alias : undefined
      });
    };

    $scope.selectPublisher = function (publisher) {
      selectedPublisher.push(publisher.name);
      $state.go('^');
    };

    $scope.goBack = function () {
      $state.go('^');
    };

  }

  ChoosePublisherCtrl.$inject = [
    '$state',
    '$stateParams',
    '$scope',
    'Publisher',
    'publishers',
    'selectedPublisher'
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
    ]
  };

  angular.module('gva').controller('ChoosePublisherCtrl', ChoosePublisherCtrl);
}(angular));
