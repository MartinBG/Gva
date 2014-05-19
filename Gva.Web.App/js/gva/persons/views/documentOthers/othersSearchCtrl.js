/*global angular*/
(function (angular) {
  'use strict';

  function DocumentOthersSearchCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentOther,
    documentOthers
  ) {
    $scope.documentOthers = documentOthers;

    $scope.editDocumentOther = function (documentOther) {
      return $state.go('root.persons.view.documentOthers.edit',
        {
          id: $stateParams.id,
          ind: documentOther.partIndex
        });
    };

    $scope.newDocumentOther = function () {
      return $state.go('root.persons.view.documentOthers.new');
    };
  }

  DocumentOthersSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentOther',
    'documentOthers'
  ];

  DocumentOthersSearchCtrl.$resolve = {
    documentOthers: [
      '$stateParams',
      'PersonDocumentOther',
      function ($stateParams, PersonDocumentOther) {
        return PersonDocumentOther.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocumentOthersSearchCtrl', DocumentOthersSearchCtrl);
}(angular));
