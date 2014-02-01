﻿/*global angular*/
(function (angular) {
  'use strict';

  function DocumentMedicalsSearchCtrl($scope, $state, $stateParams, PersonDocumentMedical) {
    PersonDocumentMedical.query($stateParams).$promise.then(function (medicals) {
      $scope.medicals = medicals.map(function (medical) {
        var testimonial = medical.part.documentNumberPrefix + ' ' +
          medical.part.documentNumber + ' ' +
          medical.part.documentNumberSuffix;

        medical.part.testimonial = testimonial;

        var limitations = '';
        for (var i = 0; i < medical.part.limitationsTypes.length; i++) {
          limitations += medical.part.limitationsTypes[i].name + ', ';
        }
        limitations = limitations.substring(0, limitations.length - 2);
        medical.part.limitations = limitations;

        return medical;
      });
    });

    $scope.editDocumentMedical = function (medical) {
      return $state.go('persons.medicals.edit', { id: $stateParams.id, ind: medical.partIndex });
    };

    $scope.deleteDocumentMedical = function (medical) {
      return PersonDocumentMedical.remove({ id: $stateParams.id, ind: medical.partIndex })
        .$promise.then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
        });
    };

    $scope.newDocumentMedical = function () {
      return $state.go('persons.medicals.new');
    };

  }

  DocumentMedicalsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentMedical'
  ];

  angular.module('gva').controller('DocumentMedicalsSearchCtrl', DocumentMedicalsSearchCtrl);
}(angular));