/*global angular, $, _*/
(function (angular, $, _) {
  'use strict';

  function StampedDocumentsCtrl(
    $scope,
    $state,
    $stateParams,
    Persons,
    documents,
    scModal
    ) {
      $scope.documents = documents;
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
      var documentsForStamp = _.map($scope.documents, function(document){
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

        if (stageAliases !== []) {
          return {
            applicationId: document.application.applicationId,
            stageAliases: stageAliases
          };
        }
      });

      return Persons
        .saveStampedDocuments(documentsForStamp)
        .$promise
        .then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
        });
    };

    
    $scope.search = function () {
      return $state.go('root.persons.stampedDocuments', {
        licenceNumber: $scope.filters.licenceNumber,
        lin: $scope.filters.lin,
        uin: $scope.filters.uin,
        names: $scope.filters.names,
        stampNumber: $scope.filters.stampNumber
      });
    };

    $scope.selectCheck = function (event, item, action) {
      if ($(event.target).is(':checked')) {
        item[action] = true;
      }
      else {
        item[action] = false;
      }
    };

    $scope.viewApplication = function (lotId, partIndex) {
      var modalInstance = scModal.open('viewApplication', {
        lotId: lotId,
        path: 'personDocumentApplications',
        partIndex: partIndex,
        setPart: 'person'
      });

      modalInstance.result.then(function () {
        return $state.go('root.persons.view.documentApplications.edit', {
          id: lotId,
          ind: partIndex
        });
      });

      return modalInstance.opened;
    };

  }

  StampedDocumentsCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Persons',
    'documents',
    'scModal'
  ];

  StampedDocumentsCtrl.$resolve = {
    documents: [
      '$stateParams',
      'Persons',
      function ($stateParams, Persons) {
        return Persons.getStampedDocuments($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('StampedDocumentsCtrl', StampedDocumentsCtrl);
}(angular, $, _));
