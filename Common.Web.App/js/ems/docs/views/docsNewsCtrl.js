﻿/*global angular, _*/
(function (angular, _) {
  'use strict';

  function DocsNewsCtrl(
    $scope,
    $state,
    $stateParams,
    Doc,
    Nomenclature,
    docModel
  ) {
    $scope.docModel = docModel;

    $scope.register = function () {
      $scope.docModel.doc.register = true;

      return $scope.docForm.$validate().then(function () {
        if ($scope.docForm.$valid) {
          return Doc
            .save($scope.docModel.doc)
            .$promise
            .then(function (result) {
              return $state.go('root.docs.search', { filter: 'all', ds: result.ids });
            });
        }
      });
    };

    $scope.save = function () {
      return $scope.docForm.$validate().then(function () {
        if ($scope.docForm.$valid) {
          return Doc
            .save($scope.docModel.doc)
            .$promise
            .then(function (result) {
              return $state.go('root.docs.edit.view', { id: result.docId });
            });
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('root.docs.search');
    };
  }

  DocsNewsCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Doc',
    'Nomenclature',
    'docModel'
  ];

  DocsNewsCtrl.$resolve = {
    docModel: ['$q', 'Nomenclature',
      function ($q, Nomenclature) {
        return $q.all({
          docFormatTypes: Nomenclature.query({ alias: 'docFormatType' }).$promise,
          docCasePartTypes: Nomenclature.query({ alias: 'docCasePartType' }).$promise,
          docDirections: Nomenclature.query({ alias: 'docDirection' }).$promise
        }).then(function (res) {
          res.docFormatTypes = _.filter(res.docFormatTypes, function (dft) {
            return dft.alias === 'Paper';
          });
          res.docCasePartTypes = _.filter(res.docCasePartTypes, function (dcpt) {
            return dcpt.alias === 'Public';
          });

          var doc = {
            docFormatTypeId: _(res.docFormatTypes).filter({ alias: 'Paper' }).first().nomValueId,
            docFormatTypeName: _(res.docFormatTypes).filter({ alias: 'Paper' }).first().name,
            docCasePartTypeId: _(res.docCasePartTypes).filter({alias: 'Public'}).first().nomValueId,
            docCasePartTypeName: _(res.docCasePartTypes).filter({ alias: 'Public' }).first().name,
            docDirectionId: _(res.docDirections).first().nomValueId,
            docDirectionName: _(res.docDirections).first().name,
            docTypeGroupId: undefined,
            docTypeId: undefined,
            correspondents: undefined,
            register: false,
            docNumbers: 1
          };

          return {
            doc: doc,
            docFormatTypes: res.docFormatTypes,
            docCasePartTypes: res.docCasePartTypes,
            docDirections: res.docDirections
          };
        });
      }
    ]
  };

  angular.module('ems').controller('DocsNewsCtrl', DocsNewsCtrl);
}(angular, _));