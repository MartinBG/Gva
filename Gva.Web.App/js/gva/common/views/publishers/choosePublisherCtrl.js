/*global angular, _*/
(function (angular, _) {
  'use strict';

  function ChoosePublisherCtrl(
    $state,
    $stateParams,
    $scope,
    l10n,
    Publisher,
    Nomenclature,
    publishers,
    selectedPublisher
  ) {
    $scope.publisherTypesDictionary = {
      'inspector': l10n.get('common.publishers.inspector'),
      'examiner': l10n.get('common.publishers.examiner'),
      'school': l10n.get('common.publishers.school'),
      'organization': l10n.get('common.publishers.organization'),
      'caa': l10n.get('common.publishers.caa'),
      'other':  l10n.get('common.publishers.other')
    };

    $scope.publisherTypes = [];
    _.forEach($scope.publisherTypesDictionary, function(value,key){
     $scope.publisherTypes.push({id: key, text: value});
    });

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
    'l10n',
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
