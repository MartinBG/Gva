/*global angular,_*/
(function (angular) {
  'use strict';

  function DocumentOthersEditCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentOthers,
    personDocumentOther,
    selectedPublisher
  ) {
    var originalDoc = _.cloneDeep(personDocumentOther);

    $scope.personDocumentOther = personDocumentOther;
    $scope.personDocumentOther.part.documentPublisher = selectedPublisher.pop() ||
      personDocumentOther.part.documentPublisher;
    $scope.editMode = null;

    $scope.backFromChild = false;

    if ($state.previous && $state.previous.includes[$state.current.name]) {
      $scope.backFromChild = true;
    }

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.personDocumentOther = _.cloneDeep(originalDoc);
    };

    $scope.save = function () {
      return $scope.editDocumentOtherForm.$validate()
        .then(function () {
          if ($scope.editDocumentOtherForm.$valid) {
            return PersonDocumentOthers
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
      return PersonDocumentOthers.remove({
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
    'PersonDocumentOthers',
    'personDocumentOther',
    'selectedPublisher'
  ];

  DocumentOthersEditCtrl.$resolve = {
    personDocumentOther: [
      '$stateParams',
      'PersonDocumentOthers',
      function ($stateParams, PersonDocumentOthers) {
        return PersonDocumentOthers.get($stateParams).$promise;
      }
    ],
    selectedPublisher: function () {
      return [];
    }
  };

  angular.module('gva').controller('DocumentOthersEditCtrl', DocumentOthersEditCtrl);
}(angular));