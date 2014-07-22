/*global angular, _*/
(function (angular, _) {
  'use strict';

  function DocsNewCtrl(
    $scope,
    $state,
    $stateParams,
    Docs,
    docModel,
    parentDoc,
    namedModal
  ) {
    if (parentDoc.length > 0) {
      docModel.parentDoc = parentDoc.pop();
    }

    $scope.docModel = docModel;

    $scope.clearCase = function () {
      $scope.docModel.parentDoc = null;
    };

    $scope.selectCase = function () {
      return $state.go('root.docs.new.caseSelect');
    };

    $scope.setParentDocId = function () {
      if ($scope.docModel.parentDoc) {
        $scope.docModel.doc.parentDocId = $scope.docModel.parentDoc.docId;
      }
    };

    $scope.register = function () {
      $scope.setParentDocId();
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
      $scope.setParentDocId();

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
      if (!!$scope.docModel.parentDoc) {
        return $state.go('root.docs.edit.view', { id: $stateParams.parentDocId });
      }
      else {
        return $state.go('root.docs.search');
      }
    };

    $scope.newCorr = function () {
      var modalInstance = namedModal.open('newCorr', null, {
        corr: [
          '$stateParams',
          'Corrs',
          function resolveCorr($stateParams, Corrs) {
            return Corrs.getNew().$promise;
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

    $scope.selectCorr = function () {
      var modalInstance, selectedCorrs = [];
      _.forEach($scope.docModel.doc.correspondents, function (corr) {
        return selectedCorrs.push({ nomValueId: corr });
      });
      
      modalInstance = namedModal.open('chooseCorr', {
        selectedCorrs: selectedCorrs,
        corr: {}
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

  DocsNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Docs',
    'docModel',
    'parentDoc',
    'namedModal'
  ];

  DocsNewCtrl.$resolve = {
    docModel: ['$q', '$stateParams', 'Nomenclatures',
      function ($q, $stateParams, Nomenclatures) {
        return $q.all({
          docFormatTypes: Nomenclatures.query({ alias: 'docFormatType' }).$promise,
          docCasePartTypes: Nomenclatures.query({ alias: 'docCasePartType' }).$promise,
          docDirections: Nomenclatures.query({ alias: 'docDirection' }).$promise
        }).then(function (res) {

          if (!!$stateParams.eDoc) {
            res.docFormatTypes = _.filter(res.docFormatTypes, function (dft) {
              return dft.alias === 'Electronic';
            });
          }
          else {
            res.docFormatTypes = _.filter(res.docFormatTypes, function (dft) {
              return dft.alias === 'Paper';
            });
          }

          if (!!$stateParams.parentDocId) {
            res.docCasePartTypes = _.filter(res.docCasePartTypes, function (dcpt) {
              return dcpt.alias === 'Public' || dcpt.alias === 'Internal';
            });
          }
          else {
            res.docCasePartTypes = _.filter(res.docCasePartTypes, function (dcpt) {
              return dcpt.alias === 'Public';
            });
          }

          var doc = {
            parentDocId: null,
            docFormatTypeId: (!!$stateParams.eDoc) ?
              _(res.docFormatTypes).filter({ alias: 'Electronic' }).first().nomValueId :
              _(res.docFormatTypes).filter({ alias: 'Paper' }).first().nomValueId,
            docFormatTypeName: (!!$stateParams.eDoc) ?
              _(res.docFormatTypes).filter({ alias: 'Electronic' }).first().name :
              _(res.docFormatTypes).filter({ alias: 'Paper' }).first().name,
            docCasePartTypeId: _(res.docCasePartTypes).filter({alias: 'Public'}).first().nomValueId,
            docCasePartTypeName: _(res.docCasePartTypes).filter({ alias: 'Public' }).first().name,
            docDirectionId: _(res.docDirections).first().nomValueId,
            docDirectionName: _(res.docDirections).first().name,
            docTypeGroupId: undefined,
            docTypeId: undefined,
            correspondents: [],
            register: false
          };

          return {
            doc: doc,
            docFormatTypes: res.docFormatTypes,
            docCasePartTypes: res.docCasePartTypes,
            docDirections: res.docDirections
          };
        });
      }
    ],
    parentDoc: ['$stateParams', 'Docs', function ($stateParams, Docs) {
      if (!!$stateParams.parentDocId) {
        return Docs.get({ id: $stateParams.parentDocId })
          .$promise
          .then(function (result) {
            return [{
              docId: result.docId,
              regUri: result.regUri,
              docTypeName: result.docTypeName,
              docSubject: result.docSubject
            }];
          });
      }
      else {
        return [];
      }
    }]
  };

  angular.module('ems').controller('DocsNewCtrl', DocsNewCtrl);
}(angular, _));
