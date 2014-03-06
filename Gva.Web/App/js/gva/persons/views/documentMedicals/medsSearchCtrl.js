/*global angular, _*/
(function (angular, _) {
  'use strict';

  function DocumentMedicalsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentMedical,
    person,
    meds
  ) {
    $scope.medicals = meds;

    $scope.editDocumentMedical = function (medical) {
      return $state.go('root.persons.view.medicals.edit', {
        id: $stateParams.id,
        ind: medical.partIndex
      });
    };

    $scope.deleteDocumentMedical = function (medical) {
      return PersonDocumentMedical.remove({ id: $stateParams.id, ind: medical.partIndex })
        .$promise.then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
        });
    };

    $scope.newDocumentMedical = function () {
      return $state.go('root.persons.view.medicals.new');
    };
  }

  DocumentMedicalsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentMedical',
    'person',
    'meds'
  ];

  DocumentMedicalsSearchCtrl.$resolve = {
    meds: [
      '$stateParams',
      'PersonDocumentMedical',
      'person',
      function ($stateParams, PersonDocumentMedical, person) {
        return PersonDocumentMedical.query($stateParams).$promise
        .then(function (meds) {
          return _(meds)
          .forEach(function (med) {
            var testimonial = med.part.documentNumberPrefix + '-' +
              med.part.documentNumber + '-' +
              person.lin + '-' +
              med.part.documentNumberSuffix;

            med.part.testimonial = testimonial;

            var limitations = '';
            for (var i = 0; i < med.part.limitationsTypes.length; i++) {
              limitations += med.part.limitationsTypes[i].name + ', ';
            }
            limitations = limitations.substring(0, limitations.length - 2);
            med.part.limitations = limitations;
          }).value();
        });
      }
    ]
  };
  angular.module('gva').controller('DocumentMedicalsSearchCtrl', DocumentMedicalsSearchCtrl);
}(angular, _));