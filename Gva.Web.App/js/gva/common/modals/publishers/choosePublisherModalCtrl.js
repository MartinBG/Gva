﻿/*global angular,_*/
(function (angular,_) {
  'use strict';

  function ChoosePublisherModalCtrl(
    $scope,
    $modalInstance,
    l10n,
    Publishers,
    publishers
  ) {
    $scope.publisherTypesDictionary = {
      'inspector': l10n.get('common.choosePublisherModal.inspector'),
      'examiner': l10n.get('common.choosePublisherModal.examiner'),
      'school': l10n.get('common.choosePublisherModal.school'),
      'organization': l10n.get('common.choosePublisherModal.organization'),
      'caa': l10n.get('common.choosePublisherModal.caa'),
      'other': l10n.get('common.choosePublisherModal.other')
    };

    $scope.publisherTypes = [];
    _.forEach($scope.publisherTypesDictionary, function (value, key) {
      $scope.publisherTypes.push({ id: key, text: value });
    });

    $scope.publishers = publishers;

    $scope.filters = {
      name: null,
      publisherType: null,
      code: null,
      lin: null
    };

    $scope.search = function () {
      return Publishers.query({
        publisherName: $scope.filters.name,
        publisherCode: $scope.filters.code,
        publisherLin: $scope.filters.lin,
        publisherType: $scope.filters.publisherType ?
          $scope.filters.publisherType.id : undefined
      }).$promise.then(function (publishers) {
        $scope.publishers = publishers;
      });
    };

    $scope.selectPublisher = function (publisher) {
      return $modalInstance.close(publisher.name);
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };
  }

  ChoosePublisherModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'l10n',
    'Publishers',
    'publishers'
  ];

  ChoosePublisherModalCtrl.$resolve = {
    publishers: [
      'Publishers',
      function (Publishers) {
        return Publishers.query({
          publisherName: undefined,
          publisherType: undefined,
          publisherCode: undefined
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('ChoosePublisherModalCtrl', ChoosePublisherModalCtrl);
}(angular,_));
