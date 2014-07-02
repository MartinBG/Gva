/*global angular*/
(function (angular) {
  'use strict';

  function DocumentOthersSearchCtrl(
    $scope,
    $state,
    $stateParams,
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
    'documentOthers'
  ];

  DocumentOthersSearchCtrl.$resolve = {
    documentOthers: [
      '$stateParams',
      'PersonDocumentOthers',
      function ($stateParams, PersonDocumentOthers) {
        return PersonDocumentOthers.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocumentOthersSearchCtrl', DocumentOthersSearchCtrl);
}(angular));
