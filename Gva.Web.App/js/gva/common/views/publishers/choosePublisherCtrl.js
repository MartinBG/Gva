/*global angular, _*/
(function (angular, _) {
  'use strict';

  function ChoosePublisherCtrl(
    $state,
    $stateParams,
    $scope,
    Publisher,
    Nomenclature,
    publishers,
    selectedPublisher
  ) {
    $scope.publisherTypes = [
      {id: 'inspector', text: 'Инспектор'},
      {id: 'examiner', text: 'Проверяващ'},
      {id: 'school', text: 'Учебен център'},
      {id: 'organization', text: 'Авио-организация'},
      {id: 'caa', text: 'Бъздушна администрация'},
      {id: 'other', text: 'Други'}
    ];

    $scope.publisherTypesDictionary = {
      'inspector': 'Инспектор',
      'examiner': 'Проверяващ',
      'school': 'Учебен център',
      'organization': 'Авио-организация',
      'caa':'Бъздушна администрация',
      'other':  'Други'
    };

    $scope.publishers = publishers;

    if ($stateParams.name) {
      $scope.publisherName = $stateParams.name;
    }

    if ($stateParams.publisherType) {
      $scope.publisherType = _.where($scope.publisherTypes,
        {id: $stateParams.publisherType})[0];
    }

    $scope.search = function () {
      return $state.go($state.current, {
        name: $scope.publisherName,
        publisherType: $scope.publisherType ?
          $scope.publisherType.id : undefined
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
    'selectedPublisher'
  ];

  ChoosePublisherCtrl.$resolve = {
    publishers: [
      '$stateParams',
      'Publisher',
      function ($stateParams, Publisher) {
        return Publisher.query({
          name: $stateParams.name,
          publisherType: $stateParams.publisherType
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('ChoosePublisherCtrl', ChoosePublisherCtrl);
}(angular, _));
