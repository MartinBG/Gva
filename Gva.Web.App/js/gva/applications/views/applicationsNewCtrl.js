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
    selectedCorrs
    ) {
    if (selectedCorrs.current.length > 0) {
      appModel.doc.docCorrespondents.push(selectedCorrs.current.pop());
    }

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

      selectedCorrs.current.splice(0);
      selectedCorrs.total = $scope.appModel.doc.docCorrespondents;

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
        var corr = {},
            data = d.personData;

        if (isPersonSelect) {
          corr.bgCitizenFirstName = data.part.firstName;
          corr.bgCitizenLastName = data.part.lastName;
          if (data.part.uin) {
            corr.bgCitizenUIN = data.part.uin;
          }
          if (data.part.email) {
            corr.email = data.part.email;
          }
        }
        if (isOrgSelect) {
          corr.legalEntityName = data.part.name;
          if (data.part.uin) {
            corr.legalEntityBulstat = data.part.uin;
          }
        }

        return $state.go('root.applications.new.corrNew', null, null, corr);
      });

    };
    $scope.selectCorr = function selectCorr() {
      selectedCorrs.current.splice(0);
      selectedCorrs.total = $scope.appModel.doc.docCorrespondents;

      if ($scope.appModel.lot && $scope.appModel.lot.id) {
        if ($scope.filter === 'Person') {
          return PersonsInfo.get({ id: $scope.appModel.lot.id })
            .$promise.then(function (d) {
              var data = d.personData,
                  displayName = data.part.firstName + ' ' + data.part.lastName;
              if (data.part.uin) {
                displayName = displayName + ' ' + data.part.uin;
              }
              return $state.go('root.applications.new.corrSelect', {
                displayName: displayName
              });
          });
        }
        else if ($scope.filter === 'Organization') {
          return OrganizationsData.get({ id: $scope.appModel.lot.id }).$promise
            .then(function (data) {
              return $state.go('root.applications.new.corrSelect', {
                displayName: data.part.name
              });
            });
        }
      }

      return $state.go('root.applications.new.corrSelect');
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
    'selectedCorrs'
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
    ],
    selectedCorrs: function selectedCorrs() {
      return {
        total: [],
        current: []
      };
    }
  };

  angular.module('gva').controller('ApplicationsNewCtrl', ApplicationsNewCtrl);
}(angular, _));
