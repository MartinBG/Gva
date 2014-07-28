/*global angular,_*/
(function (angular) {
  'use strict';

  function DocumentOthersEditCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentOthers,
    personDocumentOther,
    scMessage
  ) {
    var originalDoc = _.cloneDeep(personDocumentOther);

    $scope.personDocumentOther = personDocumentOther;
    $scope.editMode = null;

    $scope.backFromChild = false;

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

    $scope.deleteOther = function () {
      return scMessage('common.messages.confirmDelete')
      .then(function (result) {
        if (result === 'OK') {
          return PersonDocumentOthers.remove({
            id: $stateParams.id,
            ind: personDocumentOther.partIndex
          }).$promise.then(function () {
            return $state.go('root.persons.view.documentOthers.search');
          });
        }
      });
    };
  }

  DocumentOthersEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentOthers',
    'personDocumentOther',
    'scMessage'
  ];

  DocumentOthersEditCtrl.$resolve = {
    personDocumentOther: [
      '$stateParams',
      'PersonDocumentOthers',
      function ($stateParams, PersonDocumentOthers) {
        return PersonDocumentOthers.get($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocumentOthersEditCtrl', DocumentOthersEditCtrl);
}(angular));
