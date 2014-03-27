/*global angular, _*/
(function (angular, _) {
  'use strict';

  function EditDocTypeCtrl(
    $scope,
    $state,
    $stateParams,
    Doc,
    doc,
    selectedUnits,
    docTypeModel
  ) {
    $scope.oldDoc = doc;
    $scope.doc = docTypeModel.doc;
    $scope.missingFields = docTypeModel.missingFields;
    $scope.allFields = docTypeModel.allFields;
    $scope.showFields = docTypeModel.showFields;

    $scope.$watch('doc.docTypeId', function (newVal, oldVal) {
      if (newVal !== oldVal) {
        $scope.showFields = false;
        if ($scope.doc.docTypeId && $scope.doc.docDirectionId) {
          calculateFields();
        }
      }
    }, true);

    $scope.$watch('doc.docDirectionId', function (newVal, oldVal) {
      if (newVal !== oldVal) {
        $scope.showFields = false;
        if ($scope.doc.docTypeId && $scope.doc.docDirectionId) {
          calculateFields();
        }
      }
    }, true);

    $scope.cancel = function () {
      return $state.go('^');
    };

    $scope.save = function () {
      $scope.docTypeForm.$validate().then(function () {
        if ($scope.docTypeForm.$valid) {

          return Doc.setDocType({ id: $scope.oldDoc.docId }, $scope.doc).$promise
            .then(function () {
            return $state.transitionTo('root.docs.edit.case', $stateParams, { reload: true });
          });
        }
      });
    };

    $scope.fullFieldName = function (shortFieldName) {
      return 'docUnits' + shortFieldName.charAt(0).toUpperCase() + shortFieldName.slice(1);
    };

    $scope.selectUnit = function selectUnit(mode) {
      selectedUnits.units.splice(0);

      selectedUnits.units = _.assign(selectedUnits.units, $scope.doc[$scope.fullFieldName(mode)]);
      selectedUnits.onUnitSelect = function (unit) {
        $scope.doc[$scope.fullFieldName(mode)].push(unit);
        selectedUnits.onUnitSelect = null;
      };

      docTypeModel.missingFields = $scope.missingFields;
      docTypeModel.allFields = $scope.allFields;
      docTypeModel.showFields= $scope.showFields;

      return $state.go('root.docs.edit.case.docType.selectUnit');
    };

    function createFieldsObj (from, to, cCopy, importedBy, madeBy, inCharge,
      controlling, roleReaders, editors, roleRegistrators) {
      return {
        from: from,
        to: to,
        cCopy: cCopy,
        importedBy: importedBy,
        madeBy: madeBy,
        inCharge: inCharge,
        controlling: controlling,
        roleReaders: roleReaders,
        editors: editors,
        roleRegistrators: roleRegistrators
      };
    }

    function getModeName (docTypeId, docDirectionId) {
      var modeName;
      if (docTypeId === 1) {
        modeName = 'resolution';
      }
      else if (docTypeId === 2) {
        modeName = 'task';
      }
      else if (docTypeId === 3) {
        modeName = 'remark';
      }
      else {
        if (docDirectionId === 1) {
          modeName = 'incoming';
        }
        else if (docDirectionId === 2) {
          modeName = 'internal';
        }
        else if (docDirectionId === 3) {
          modeName = 'outgoing';
        }
        else if (docDirectionId === 4) {
          modeName = 'internalOutgoing';
        }
      }

      return modeName;
    }

    function calculateFields () {
      var missingFields = [[]];
      var allFields = [[]];

      var oldModeName = getModeName(doc.docTypeId, doc.docDirectionId);
      var newModeName = getModeName($scope.doc.docTypeId, $scope.doc.docDirectionId);

      var oldModeFields = _(modeFields).filter( {modeName: oldModeName} ).first().fields;
      var newModeFields = _(modeFields).filter( {modeName: newModeName} ).first().fields;

      _(fieldsList).forEach(function (item) {
        if (newModeFields[item] === true) {
          if (_(allFields).last().length === 3) {
            allFields.push([]);
          }
          _(allFields).last().push(item);
          $scope.doc[$scope.fullFieldName(item)] = $scope.oldDoc[$scope.fullFieldName(item)];
        }
        else {
          $scope.doc[$scope.fullFieldName(item)] = [];
          if (oldModeFields[item] === true && $scope.oldDoc[$scope.fullFieldName(item)].length) {
            if (_(missingFields).last().length === 3) {
              missingFields.push([]);
            }
            _(missingFields).last().push(item);
          }
        }
      });

      _(modeFields).forEach(function (item) {
        $scope.doc[item.docFlagname] = false;
      });

      var newMode = _(modeFields).filter( {modeName: newModeName} ).first();
      $scope.doc[newMode.docFlagname] = true;

      $scope.missingFields = missingFields;
      $scope.allFields = allFields;
      $scope.showFields = true;
    }

    var fieldsList = [
      'from',
      'to',
      'cCopy',
      'importedBy',
      'madeBy',
      'inCharge',
      'controlling',
      'roleReaders',
      'editors',
      'roleRegistrators'
    ];

    var modeFields = [
      {
        modeName: 'incoming',
        docFlagname: 'isDocIncoming',
        fields: createFieldsObj(false, true, true, true, false, true, true, true, true, true)
      },
      {
        modeName: 'internal',
        docFlagname: 'isDocInternal',
        fields: createFieldsObj(true, true, true, true, true, true, true, true, true, true)
      },
      {
        modeName: 'outgoing',
        docFlagname: 'isDocOutgoing',
        fields: createFieldsObj(true, false, false, true, true, true, true, true, true, true)
      },
      {
        modeName: 'internalOutgoing',
        docFlagname: 'isDocInternalOutgoing',
        fields: createFieldsObj(true, true, true, true, true, true, true, true, true, true)
      },
      {
        modeName: 'resolution',
        docFlagname: 'isResolution',
        fields: createFieldsObj(true, false, true, true, false, true, true, true, true, true)
      },
      {
        modeName: 'remark',
        docFlagname: 'isRemark',
        fields: createFieldsObj(false, false, true, true, false, false, false, false, false, false)
      },
      {
        modeName: 'task',
        docFlagname: 'isTask',
        fields: createFieldsObj(true, false, true, true, false, true, true, true, true, true)
      }
    ];
  }

  EditDocTypeCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Doc',
    'doc',
    'selectedUnits',
    'docTypeModel'
  ];

  EditDocTypeCtrl.$resolve = {
    selectedUnits: [
      function resolveSelectedUnits() {
        return {
          units: [],
          onUnitSelect: null
        };
      }
    ],
    docTypeModel: [
      function resolveDocTypeModel() {
        return {
          doc: {},
          missingFields: [],
          allFields: [],
          showFields: false
        };
      }
    ]
  };

  angular.module('ems').controller('EditDocTypeCtrl', EditDocTypeCtrl);
}(angular, _));
