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

    $scope.save = function () {
      var documentsForStamp = [];
        _.each($scope.docs.documents, function(document) {
        var stageAliases = [];
        if(document.licenceReady) {
          stageAliases.push('licenceReady');
        } 
        if (document.done) {
          stageAliases.push('done');
        }
        if (document.returned) {
          stageAliases.push('returned');
        }

        if (stageAliases.length > 0) {
          if (!document.isOfficiallyReissued) {
            documentsForStamp.push({
              applicationId: document.application.applicationId,
              stageAliases: stageAliases
            });
          } else {
            documentsForStamp.push({
              lotId: document.lotId,
              editionPartIndex: document.editionPartIndex,
              stageAliases: stageAliases
            });
          }
        }
      });

      if (documentsForStamp.length > 0) {
        return Persons
          .saveStampedDocuments(documentsForStamp)
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
      var index = _.findIndex($scope.docs.documents, item);
      if ($(event.target).is(':checked')) {
        $scope.docs.documents[index][action] = true;
      }
      else {
        $scope.docs.documents[index][action] = false;
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
