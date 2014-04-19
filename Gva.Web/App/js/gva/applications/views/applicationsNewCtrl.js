/*global angular, _*/
(function (angular, _) {
  'use strict';

  function ApplicationsNewCtrl(
    $q,
    $scope,
    $state,
    $stateParams,
    Nomenclature,
    Application,
    PersonData,
    OrganizationData,
    appModel,
    selectedPerson,
    selectedOrganization,
    selectedAircraft,
    selectedAirport,
    selectedEquipment,
    selectedCorrs
    ) {
    if (selectedPerson.length > 0) {
      appModel.lot = {
        id: selectedPerson.pop()
      };
    }
    if (selectedOrganization.length > 0) {
      appModel.lot = {
        id: selectedOrganization.pop()
      };
    }
    if (selectedAircraft.length > 0) {
      appModel.lot = {
        id: selectedAircraft.pop()
      };
    }
    if (selectedAirport.length > 0) {
      appModel.lot = {
        id: selectedAirport.pop()
      };
    }
    if (selectedEquipment.length > 0) {
      appModel.lot = {
        id: selectedEquipment.pop()
      };
    }

    $scope.appModel = appModel;
    $scope.filter = $stateParams.filter;
    $scope.setPartAlias = '';

    $scope.newPerson = function () {
      return $state.go('root.applications.new.personNew');
    };
    $scope.selectPerson = function () {
      return $state.go('root.applications.new.personSelect');
    };

    $scope.newOrganization = function () {
      return $state.go('root.applications.new.organizationNew');
    };
    $scope.selectOrganization = function () {
      return $state.go('root.applications.new.organizationSelect');
    };

    $scope.newAircraft = function () {
      return $state.go('root.applications.new.aircraftNew');
    };
    $scope.selectAircraft = function () {
      return $state.go('root.applications.new.aircraftSelect');
    };

    $scope.newAirport = function () {
      return $state.go('root.applications.new.airportNew');
    };
    $scope.selectAirport = function () {
      return $state.go('root.applications.new.airportSelect');
    };

    $scope.newEquipment = function () {
      return $state.go('root.applications.new.equipmentNew');
    };
    $scope.selectEquipment = function () {
      return $state.go('root.applications.new.equipmentSelect');
    };

    $scope.newCorr = function () {
      var doc = $scope.appModel.doc, partData, isPersonSelect, isOrgSelect;

      selectedCorrs.corrs.splice(0);
      selectedCorrs.onCorrSelect = function (corr) {
        doc.docCorrespondents.push(corr);
        selectedCorrs.onCorrSelect = null;
      };

      if ($scope.appModel.lot && $scope.appModel.lot.id) {
        if ($scope.filter === 'Person') {
          isPersonSelect = true;
          partData = PersonData.get({ id: $scope.appModel.lot.id });
        }
        else if ($scope.filter === 'Organization') {
          isOrgSelect = true;
          partData = OrganizationData.get({ id: $scope.appModel.lot.id });
        }
        else {
          partData = $q.when(false);
        }
      }
      else {
        partData = $q.when(false);
      }

      return $q.all({
        partData: partData.$promise,
        corrGroups: Nomenclature.query({ alias: 'correspondentGroup' }).$promise,
        corrTypes: Nomenclature.query({ alias: 'correspondentType' }).$promise
      }).then(function (res) {
        var corr = {};
        var corrGroup = _(res.corrGroups).filter({ alias: 'Applicants' }).first();
        var corrType = _(res.corrTypes).filter({ alias: 'LegalEntity' }).first();
        var corrTypePerson = _(res.corrTypes).filter({ alias: 'BulgarianCitizen' }).first();

        corr.correspondentGroupId = corrGroup.nomValueId;
        corr.correspondentTypeId = corrType.nomValueId;
        corr.correspondentType = corrType;
        if ($scope.filter === 'Person') {
          corr.correspondentTypeId = corrTypePerson.nomValueId;
          corr.correspondentType = corrTypePerson;
        }
        if (isPersonSelect) {
          corr.bgCitizenFirstName = res.partData.part.firstName;
          corr.bgCitizenLastName = res.partData.part.lastName;
          if (res.partData.part.uin) {
            corr.bgCitizenUIN = res.partData.part.uin;
          }
          if (res.partData.part.email) {
            corr.email = res.partData.part.email;
          }
        }
        if (isOrgSelect) {
          corr.legalEntityName = res.partData.part.name;
          if (res.partData.part.uin) {
            corr.legalEntityBulstat = res.partData.part.uin;
          }
        }

        return $state.go('root.applications.new.corrNew', null, null, corr);
      });
    };
    $scope.selectCorr = function selectCorr() {
      var doc = $scope.appModel.doc;
      selectedCorrs.corrs.splice(0);
      selectedCorrs.corrs = _.assign(selectedCorrs.corrs, doc.docCorrespondents);
      selectedCorrs.onCorrSelect = function (corr) {
        doc.docCorrespondents.push(corr);
        selectedCorrs.onCorrSelect = null;
      };

      if ($scope.appModel.lot && $scope.appModel.lot.id) {
        if ($scope.filter === 'Person') {
          return PersonData.get({ id: $scope.appModel.lot.id }).$promise.then(function (data) {
            var displayName = data.part.firstName + ' ' + data.part.lastName;
            if (data.part.uin) {
              displayName = displayName + ' ' + data.part.uin;
            }

            return $state.go('root.applications.new.corrSelect', {
              displayName: displayName,
              email: data.part.email
            });
          });
        }
        else if ($scope.filter === 'Organization') {
          return OrganizationData.get({ id: $scope.appModel.lot.id }).$promise
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

          return Application.create(newApplication).$promise.then(function (app) {
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
    'Nomenclature',
    'Application',
    'PersonData',
    'OrganizationData',
    'appModel',
    'selectedPerson',
    'selectedOrganization',
    'selectedAircraft',
    'selectedAirport',
    'selectedEquipment',
    'selectedCorrs'
  ];

  ApplicationsNewCtrl.$resolve = {
    appModel: ['$q', 'Nomenclature',
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
            docFormatTypeId: _(res.docFormatTypes).first().nomValueId,
            docFormatTypeName: _(res.docFormatTypes).first().name,
            docCasePartTypeId: _(res.docCasePartTypes).first().nomValueId,
            docCasePartTypeName: _(res.docCasePartTypes).first().name,
            docDirectionId: _(res.docDirections).first().nomValueId,
            docDirectionName: _(res.docDirections).first().name,
            docCorrespondents: []
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
    selectedPerson: function () {
      return [];
    },
    selectedOrganization: function () {
      return [];
    },
    selectedAircraft: function () {
      return [];
    },
    selectedAirport: function () {
      return [];
    },
    selectedEquipment: function () {
      return [];
    },
    selectedCorrs: [
      function resolveSelectedCorrs() {
        return {
          corrs: [],
          onCorrSelect: null
        };
      }
    ]
  };

  angular.module('gva').controller('ApplicationsNewCtrl', ApplicationsNewCtrl);
}(angular, _));
