/*global angular,_*/
(function (angular,_) {
  'use strict';

  function ApplicationsNewCtrl(
    $q,
    $scope,
    $state,
    $stateParams,
    namedModal,
    Applications,
    Nomenclatures,
    PersonsInfo,
    OrganizationsData,
    application
    ) {
    $scope.$watch('application.lot.id', function (newValue, oldValue) {
      if (newValue !== oldValue) {
        $scope.application.caseType = null;
        $scope.application.docCorrespondents = [];

        if (!!newValue) {
          var caseTypes = [];

          if ($scope.filter !== 'Person') {
            caseTypes = Nomenclatures.query({
              alias: 'caseTypes',
              lotId: $scope.application.lot.id
            }).$promise;
          }

          return $q.all({
            caseTypes: $q.when(caseTypes),
            corrs: Applications.getGvaCorrespodents({ lotId: application.lot.id }).$promise
          }).then(function (results) {
            if (results.caseTypes.length) {
              $scope.application.caseType = results.caseTypes[0];
            }

            $scope.application.docCorrespondents = results.corrs.corrs;
          });
        }
      }
    });

    $scope.$watch('application.caseType', function (newValue, oldValue) {
      if (newValue !== oldValue) {
        $scope.application.applicationType = null;
      }
    });

    $scope.application = application;
    $scope.filter = $stateParams.filter;

    $scope.newCorr = function () {
      var partData = {}, isPersonSelect, isOrgSelect;
      partData.$promise = $q.when(false);

      if ($scope.application.lot && $scope.application.lot.id) {
        if ($scope.filter === 'Person') {
          isPersonSelect = true;
          partData = PersonsInfo.get({ id: $scope.application.lot.id });
        }
        else if ($scope.filter === 'Organization') {
          isOrgSelect = true;
          partData = OrganizationsData.get({ id: $scope.application.lot.id });
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
          var newCorr = $scope.application.docCorrespondents.slice();
          newCorr.push(nomItem.nomValueId);
          $scope.application.docCorrespondents = newCorr;
        });

        return modalInstance.opened;
      });
    };

    $scope.selectCorr = function selectCorr() {
      var partData = {}, isPersonSelect, isOrgSelect, selectedCorrs = [];
      partData.$promise = $q.when(false);

      _.forEach($scope.application.docCorrespondents, function (corr) {
        return selectedCorrs.push({ nomValueId: corr });
      });

      if ($scope.application.lot && $scope.application.lot.id) {
        if ($scope.filter === 'Person') {
          isPersonSelect = true;
          partData = PersonsInfo.get({ id: $scope.application.lot.id });
        }
        else if ($scope.filter === 'Organization') {
          isOrgSelect = true;
          partData = OrganizationsData.get({ id: $scope.application.lot.id });
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
          var newCorr = $scope.application.docCorrespondents.slice();
          newCorr.push(nomItem.nomValueId);
          $scope.application.docCorrespondents = newCorr;
        });

        return modalInstance.opened;
      });
    };

    $scope.requireCorrespondents = function () {
      return $scope.application.docCorrespondents.length > 0;
    };

    $scope.selectAppType = function () {
      var modalInstance = namedModal.open(
        'chooseAppType',
        { },
        {
          appTypes: [
            'Nomenclatures',
            function (Nomenclatures) {
              return Nomenclatures.query({
                alias: 'applicationTypes',
                caseTypeAlias: $scope.application.caseType.alias
              }).$promise;
            }
          ]
        });

      modalInstance.result.then(function (appType) {
        $scope.application.applicationType = appType;
      });

      return modalInstance.opened;
    };

    $scope.cancel = function () {
      return $state.go('root.applications.search');
    };

    $scope.save = function () {
      return $scope.appForm.$validate()
      .then(function () {
        if ($scope.appForm.$valid) {
          var newApplication = {
            setPartPath: null,
            lotId: $scope.application.lot.id,
            correspondents: $scope.application.docCorrespondents,
            applicationType: $scope.application.applicationType,
            caseTypeId: $scope.application.caseType.nomValueId
          };

          //todo make it better
          if ($scope.filter === 'Person') {
            newApplication.setPartPath = 'personDocumentApplications';
          }
          else if ($scope.filter === 'Organization') {
            newApplication.setPartPath = 'organizationDocumentApplications';
          }
          else if ($scope.filter === 'Aircraft') {
            newApplication.setPartPath = 'aircraftDocumentApplications';
          }
          else if ($scope.filter === 'Airport') {
            newApplication.setPartPath = 'airportDocumentApplications';
          }
          else if ($scope.filter === 'Equipment') {
            newApplication.setPartPath = 'equipmentDocumentApplications';
          }

          return Applications.create(newApplication).$promise.then(function (gvaApp) {
            return $state.go('root.applications.new.editPart', {
              setPartPath: newApplication.setPartPath,
              ind: gvaApp.gvaAppLotPart.index,
              lotId: gvaApp.lotId,
              appId: gvaApp.gvaApplicationId,
              filter: $scope.filter
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
    'namedModal',
    'Applications',
    'Nomenclatures',
    'PersonsInfo',
    'OrganizationsData',
    'application'
  ];

  ApplicationsNewCtrl.$resolve = {
    application: function () {
      return {
        lot: {},
        caseType: null,
        applicationType: null,
        docCorrespondents: []
      };
    }
  };

  angular.module('gva').controller('ApplicationsNewCtrl', ApplicationsNewCtrl);
}(angular,_));
