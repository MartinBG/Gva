/*global angular, _*/
(function (angular, _) {
  'use strict';

  function ApplicationsNewCtrl(
    $q,
    $scope,
    $state,
    $stateParams,
    Applications,
    PersonsInfo,
    OrganizationsData,
    appModel,
    namedModal
    ) {
    $scope.$watch('appModel.lot.id', function (newValue, oldValue) {
      if (((newValue !== oldValue) || (newValue === oldValue)) && !!newValue) {
        return Applications.getGvaCorrespodents({ lotId: appModel.lot.id }).$promise
          .then(function (data) {
            appModel.doc.docCorrespondents = data.corrs;
          });
      }
    });

    $scope.appModel = appModel;
    $scope.filter = $stateParams.filter;
    $scope.setPartAlias = '';

    $scope.newCorr = function () {
      var partData = {}, isPersonSelect, isOrgSelect;
      partData.$promise = $q.when(false);

      if ($scope.appModel.lot && $scope.appModel.lot.id) {
        if ($scope.filter === 'Person') {
          isPersonSelect = true;
          partData = PersonsInfo.get({ id: $scope.appModel.lot.id });
        }
        else if ($scope.filter === 'Organization') {
          isOrgSelect = true;
          partData = OrganizationsData.get({ id: $scope.appModel.lot.id });
        }
      }

      return partData.$promise.then(function (d) {
        var modalInstance = namedModal.open('newCorr', null, {
          corr: [
            '$stateParams',
            'Nomenclatures',
            'Corrs',
            function ($stateParams, Nomenclatures, Corrs) {
              return $q.all({
                corrTypes: Nomenclatures.query({ alias: 'correspondentType' }).$promise,
                corr: Corrs.getNew().$promise
              }).then(function (res) {
                if (isPersonSelect) {
                  res.corr.correspondentTypeId = _(res.corrTypes).filter({
                    alias: 'BulgarianCitizen'
                  }).first().nomValueId;
                  res.corr.correspondentType = _(res.corrTypes).filter({
                    alias: 'BulgarianCitizen'
                  }).first();

                  res.corr.bgCitizenFirstName = d.personData.part.firstName;
                  res.corr.bgCitizenLastName = d.personData.part.lastName;
                  if (d.personData.part.uin) {
                    res.corr.bgCitizenUIN = d.personData.part.uin;
                  }
                  if (d.personData.part.email) {
                    res.corr.email = d.personData.part.email;
                  }
                }
                if (isOrgSelect) {
                  res.corr.legalEntityName = d.part.name;
                  if (d.part.uin) {
                    res.corr.legalEntityBulstat = d.part.uin;
                  }
                }

                return res.corr;
              });
            }
          ]
        });

        modalInstance.result.then(function (nomItem) {
          var newCorr = $scope.appModel.doc.docCorrespondents.slice();
          newCorr.push(nomItem.nomValueId);
          $scope.appModel.doc.docCorrespondents = newCorr;
        });

        return modalInstance.opened;
      });
    };

    $scope.selectCorr = function selectCorr() {
      var partData = {}, isPersonSelect, isOrgSelect, selectedCorrs = [];
      partData.$promise = $q.when(false);

      _.forEach($scope.appModel.doc.docCorrespondents, function (corr) {
        return selectedCorrs.push({ nomValueId: corr });
      });

      if ($scope.appModel.lot && $scope.appModel.lot.id) {
        if ($scope.filter === 'Person') {
          isPersonSelect = true;
          partData = PersonsInfo.get({ id: $scope.appModel.lot.id });
        }
        else if ($scope.filter === 'Organization') {
          isOrgSelect = true;
          partData = OrganizationsData.get({ id: $scope.appModel.lot.id });
        }
      }

      return partData.$promise.then(function (d) {
        var modalInstance, corr = {};

        if (isPersonSelect) {
          corr.displayName = d.personData.part.firstName + ' ' + d.personData.part.lastName;
          if (d.personData.part.uin) {
            corr.displayName = corr.displayName + ' ' + d.personData.part.uin;
          }
          if (d.personData.part.email) {
            corr.email = d.personData.part.email;
          }
        }
        if (isOrgSelect) {
          corr.displayName = d.part.name;
          if (d.part.uin) {
            corr.displayName = corr.displayName + ' ' + d.part.uin;
          }
        }

        modalInstance = namedModal.open('chooseCorr', {
          selectedCorrs: selectedCorrs,
          corr: corr
        }, {
          corrs: [
            'Corrs',
            function (Corrs) {
              return Corrs.get().$promise;
            }
          ]
        });

        modalInstance.result.then(function (nomItem) {
          var newCorr = $scope.appModel.doc.docCorrespondents.slice();
          newCorr.push(nomItem.nomValueId);
          $scope.appModel.doc.docCorrespondents = newCorr;
        });

        return modalInstance.opened;
      });
    };

    $scope.requireCorrespondents = function () {
      return $scope.appModel.doc.docCorrespondents.length > 0;
    };

    $scope.cancel = function () {
      return $state.go('root.applications.search');
    };

    $scope.save = function () {
      return $scope.appForm.$validate()
      .then(function () {
        if ($scope.appForm.$valid) {
          var newApplication = {
            lotSetAlias: $scope.filter,
            lotId: $scope.appModel.lot.id,
            preDoc: {
              docFormatTypeId: $scope.appModel.doc.docFormatTypeId,
              docCasePartTypeId: $scope.appModel.doc.docCasePartTypeId,
              docDirectionId: $scope.appModel.doc.docDirectionId,
              docTypeGroupId: $scope.appModel.doc.docTypeGroupId,
              docTypeId: $scope.appModel.doc.docTypeId,
              docSubject: $scope.appModel.doc.docSubject,
              correspondents: $scope.appModel.doc.docCorrespondents
            }
          };

          //todo make it better
          if ($scope.filter === 'Person') {
            $scope.setPartAlias = 'personApplication';
          }
          else if ($scope.filter === 'Organization') {
            $scope.setPartAlias = 'organizationApplication';
          }
          else if ($scope.filter === 'Aircraft') {
            $scope.setPartAlias = 'aircraftApplication';
          }
          else if ($scope.filter === 'Airport') {
            $scope.setPartAlias = 'airportApplication';
          }
          else if ($scope.filter === 'Equipment') {
            $scope.setPartAlias = 'equipmentApplication';
          }

          return Applications.create(newApplication).$promise.then(function (app) {
            return $state.go('root.applications.edit.case.addPart', {
              id: app.applicationId,
              docId: app.docId,
              setPartAlias: $scope.setPartAlias
            });
          });
        }
      });
    };
  }

  ApplicationsNewCtrl.$inject = [
    '$q',
    '$scope',
    '$state',
    '$stateParams',
    'Applications',
    'PersonsInfo',
    'OrganizationsData',
    'appModel',
    'namedModal'
  ];

  ApplicationsNewCtrl.$resolve = {
    appModel: ['$q', 'Nomenclatures',
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
            docFormatTypeId: _(res.docFormatTypes).first().nomValueId,
            docFormatTypeName: _(res.docFormatTypes).first().name,
            docCasePartTypeId: _(res.docCasePartTypes).first().nomValueId,
            docCasePartTypeName: _(res.docCasePartTypes).first().name,
            docDirectionId: _(res.docDirections).first().nomValueId,
            docDirectionName: _(res.docDirections).first().name,
            docCorrespondents: []
          };

          return {
            lot: {},
            doc: doc,
            docFormatTypes: res.docFormatTypes,
            docCasePartTypes: res.docCasePartTypes,
            docDirections: res.docDirections
          };
        });
      }
    ]
  };

  angular.module('gva').controller('ApplicationsNewCtrl', ApplicationsNewCtrl);
}(angular, _));
