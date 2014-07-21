/*global angular, _*/
(function (angular, _) {
  'use strict';

  function DocsNewsCtrl(
    $scope,
    $state,
    $stateParams,
    Docs,
    docModel,
    namedModal
  ) {
    $scope.docModel = docModel;

    $scope.register = function () {
      $scope.docModel.doc.register = true;

      return $scope.docForm.$validate().then(function () {
        if ($scope.docForm.$valid) {
          return Docs
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
          return Docs
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

    $scope.newCorr = function () {
      var modalInstance = namedModal.open('newCorr');

      modalInstance.result.then(function (nomItem) {
        var newCorr = $scope.docModel.doc.correspondents.slice();
        newCorr.push(nomItem.nomValueId);
        $scope.docModel.doc.correspondents = newCorr;
      });

      return modalInstance.opened;
    };

    $scope.selectCorr = function () {
      var modalInstance, selectedCorrs = [];
      _.forEach($scope.docModel.doc.correspondents, function (corr) {
        return selectedCorrs.push({ nomValueId: corr });
      });

      modalInstance = namedModal.open('chooseCorr', {
        selectedCorrs: selectedCorrs
      }, {
        corrs: [
          'Corrs',
          function (Corrs) {
            return Corrs.get().$promise;
          }
        ]
      });

      modalInstance.result.then(function (nomItem) {
        var newCorr = $scope.docModel.doc.correspondents.slice();
        newCorr.push(nomItem.nomValueId);
        $scope.docModel.doc.correspondents = newCorr;
      });

      return modalInstance.opened;
    };
  }

  DocsNewsCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Docs',
    'docModel',
    'namedModal'
  ];

  DocsNewsCtrl.$resolve = {
    docModel: ['$q', 'Nomenclatures',
      function ($q, Nomenclatures) {
        return $q.all({
          docFormatTypes: Nomenclatures.query({ alias: 'docFormatType' }).$promise,
          docCasePartTypes: Nomenclatures.query({ alias: 'docCasePartType' }).$promise,
          docDirections: Nomenclatures.query({ alias: 'docDirection' }).$promise
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
            correspondents: [],
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
