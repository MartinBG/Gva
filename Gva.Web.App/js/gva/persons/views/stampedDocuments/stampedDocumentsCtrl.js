/*global angular, $, _*/
(function (angular, $, _) {
  'use strict';

  function StampedDocumentsCtrl(
    $scope,
    $state,
    $stateParams,
    Persons,
    docs
    ) {
    $scope.docs = docs;
    $scope.documentsCount = docs.documentsCount;

    $scope.filters = {
      licenceNumber: null,
      lin: null,
      uin: null,
      names: null
    };

    _.forOwn($stateParams, function (value, param) {
      if (value !== null && value !== undefined) {
        $scope.filters[param] = value;
      }
    });

    $scope.documentsForStamp = [];
    $scope.save = function () {
      var changedDocuments = _.filter($scope.documentsForStamp, function (doc) {
        return doc.stageAliases.length > 0;
      });

      if (changedDocuments.length > 0) {
        return Persons
          .saveStampedDocuments(changedDocuments)
          .$promise
          .then(function () {
            return $state.transitionTo($state.current, $stateParams, { reload: true });
          });
      } else {
        return $state.transitionTo($state.current, $stateParams);
      }

    };

    $scope.search = function () {
      return $state.go('root.stampedDocuments', {
        licenceNumber: $scope.filters.licenceNumber,
        lin: $scope.filters.lin,
        uin: $scope.filters.uin,
        names: $scope.filters.names,
        stampNumber: $scope.filters.stampNumber,
        isOfficiallyReissuedId: $scope.filters.isOfficiallyReissuedId
      });
    };

    $scope.selectCheck = function (event, item, action) {
      var data = {
        lotId: item.lotId,
        applicationId: item.application ? item.application.applicationId : null,
        editionPartIndex: item.editionPartIndex,
        ratingEditionPartIndex: item.ratingEditionPartIndex,
        ratingPartIndex: item.ratingPartIndex
      };
      var existingEntryIndex = _.findIndex($scope.documentsForStamp, data);
      if (existingEntryIndex === -1) {
        _.assign(data, { stageAliases: [action]});
        $scope.documentsForStamp.push(data);

      } else {
        if (!$scope.documentsForStamp[existingEntryIndex].stageAliases) {
          $scope.documentsForStamp[existingEntryIndex].stageAliases = [action];
        } else {
          if ($(event.target).is(':checked')) {
            $scope.documentsForStamp[existingEntryIndex].stageAliases.push(action);
          }
          else {
            $scope.documentsForStamp[existingEntryIndex].stageAliases = 
              _.without($scope.documentsForStamp[existingEntryIndex].stageAliases, action);
          }
        }
      }
    };

    $scope.getDocuments = function (page, pageSize) {
      var params = {};

      _.assign(params, $scope.filters);
      _.assign(params, {
        offset: (page - 1) * pageSize,
        limit: pageSize
      });

      return Persons.getStampedDocuments(params).$promise;
    };

    $scope.isChecked = function (item, action) {
      var currentItem = {
        lotId: item.lotId,
        applicationId: item.application ? item.application.applicationId : null,
        editionPartIndex: item.editionPartIndex,
        ratingEditionPartIndex: item.ratingEditionPartIndex,
        ratingPartIndex: item.ratingPartIndex
      };
      var existingEntryIndex = _.findIndex($scope.documentsForStamp, currentItem);
      if (existingEntryIndex === -1) {
        return false;
      } else {
        return _.contains($scope.documentsForStamp[existingEntryIndex].stageAliases, action);
      }
    };
  }

  StampedDocumentsCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Persons',
    'docs'
  ];

  StampedDocumentsCtrl.$resolve = {
    docs: [
      '$stateParams',
      'Persons',
      function ($stateParams, Persons) {
        return Persons.getStampedDocuments($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('StampedDocumentsCtrl', StampedDocumentsCtrl);
}(angular, $, _));
