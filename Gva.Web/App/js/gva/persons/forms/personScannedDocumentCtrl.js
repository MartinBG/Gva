/*global angular, Select2, _*/
(function (angular, Select2, _) {
  'use strict';

  function PersonScannedDocCtrl($scope, $state, $stateParams, $compile, PersonApplication) {
    $scope.lotId = $stateParams.id;

    if (_.isArray($scope.model)) {
      $scope.hideApplications = false;
    }
    else {
      $scope.hideApplications = $scope.model.hideApplications;
      $scope.model = $scope.model.files;
    }

    $scope.appSelectOpt = {
      multiple: true,
      id: function (app) {
        return app.applicationId;
      },
      formatResult: function (result, container, query, escapeMarkup) {
        var markup = [];
        Select2.util.markMatch(result.applicationName, query.term, markup, escapeMarkup);
        return markup.join('');
      },
      formatSelection: function (app, container) {
        var text = Select2.util.escapeMarkup(app.applicationName),
            elem = '<a ng-click="viewApplication(' + app.partIndex + ')">' + text + '</a>';

        container.append($compile(elem)($scope));
      },
      query: function (query) {
        PersonApplication.query({ id: $stateParams.id, term: query.term }).$promise
            .then(function (result) {
          query.callback({
            results: result
          });
        });
      }
    };

    angular.forEach($scope.model, function (file) {
      file.isDeleted = false;
      file.isAdded = false;
    });

    $scope.addFile = function () {
      $scope.model.unshift({
        file: null,
        bookPageNumber: null,
        pageCount: null,
        isAdded: true,
        isDeleted: false
      });
    };

    $scope.removeFile = function (index) {
      if ($scope.model[index].isAdded === true) {
        $scope.model.splice(index, 1);
      }
      else {
        $scope.model[index].isDeleted = true;
      }
    };

    $scope.showFile = function () {
      return function (file) {
        return !file.isDeleted;
      };
    };

    $scope.viewApplication = function (partIndex) {
      $state.go('root.persons.view.documentApplications.edit', {
        id: $stateParams.id,
        ind: partIndex
      });
    };
  }

  PersonScannedDocCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    '$compile',
    'PersonApplication'
  ];

  angular.module('gva').controller('PersonScannedDocCtrl', PersonScannedDocCtrl);
}(angular, Select2, _));
