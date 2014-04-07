/*global angular,_*/
(function (angular) {
  'use strict';

  function DocumentOthersEditCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentOther,
    personDocumentOther,
    selectedPublisher
  ) {
    var originalDoc = _.cloneDeep(personDocumentOther);

    $scope.personDocumentOther = personDocumentOther;
    $scope.personDocumentOther.part.documentPublisher = selectedPublisher.pop() ||
      personDocumentOther.part.documentPublisher;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.personDocumentOther.part = _.cloneDeep(originalDoc.part);
      $scope.$broadcast('cancel', originalDoc);
    };

    $scope.save = function () {
      return $scope.editDocumentOtherForm.$validate()
        .then(function () {
          if ($scope.editDocumentOtherForm.$valid) {
            return PersonDocumentOther
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.personDocumentOther)
              .$promise
              .then(function () {
                return $state.go('root.persons.view.documentOthers.search');
              });
          }
        });
    };

    $scope.choosePublisher = function () {
      return $state.go('root.persons.view.documentOthers.edit.choosePublisher');
    };

    $scope.deleteOther = function () {
      return PersonDocumentOther.remove({
        id: $stateParams.id,
        ind: personDocumentOther.partIndex
      }).$promise.then(function () {
        return $state.go('root.persons.view.documentOthers.search');
      });
    };
  }

  DocumentOthersEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentOther',
    'personDocumentOther',
    'selectedPublisher'
  ];

  DocumentOthersEditCtrl.$resolve = {
    personDocumentOther: [
      '$stateParams',
      'PersonDocumentOther',
      function ($stateParams, PersonDocumentOther) {
        return PersonDocumentOther.get($stateParams).$promise;
      }
    ],
    selectedPublisher: function () {
      return [];
    }
  };

  angular.module('gva').controller('DocumentOthersEditCtrl', DocumentOthersEditCtrl);
}(angular));