/*global angular, Select2, _*/
(function (angular, Select2, _) {
  'use strict';

  function AircraftScannedDocCtrl($scope, $state, $stateParams, $compile, AircraftApplication) {
    $scope.lotId = $stateParams.id;

    $scope.$watch('model', function () {
      if (_.isArray($scope.model)) {
        $scope.hideApplications = false;
      }
      else {
        $scope.hideApplications = $scope.model.hideApplications;
        $scope.model = $scope.model.files;
      }

      angular.forEach($scope.model, function (file) {
        file.isDeleted = false;
        file.isAdded = false;
      });
    });

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
        AircraftApplication.query({ id: $stateParams.id, term: query.term }).$promise
            .then(function (result) {
          query.callback({
            results: result
          });
        });
      }
    };

    $scope.addFile = function () {
      $scope.model.unshift({
        file: null,
        bookPageNumber: null,
        pageCount: null,
        isAdded: true,
        isDeleted: false,
        caseType: {
          nomValueId: 3
        },
        applications: []
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
      $state.go('root.aircrafts.view.applications.edit', {
        id: $stateParams.id,
        ind: partIndex
      });
    };
  }

  AircraftScannedDocCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    '$compile',
    'AircraftApplication'
  ];

  angular.module('gva').controller('AircraftScannedDocCtrl', AircraftScannedDocCtrl);
}(angular, Select2, _));
