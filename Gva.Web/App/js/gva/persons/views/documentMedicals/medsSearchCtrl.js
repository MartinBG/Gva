/*global angular, _*/
(function (angular, _) {
  'use strict';

  function DocumentMedicalsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentMedical,
    meds,
    person
  ) {
    $scope.medicals = _(meds).forEach(function (m) {
      var testimonial = m.part.documentNumberPrefix + '-' +
        m.part.documentNumber + '-' +
        person.lin + '-' +
        (m.part.documentNumberSuffix !== undefined ? m.part.documentNumberSuffix : '');
      m.part.testimonial = testimonial;

      var limitations = '';
      _(m.part.limitationsTypes).forEach(function (limitationType) {
        limitations += limitationType.name + ', ';
      });
      limitations = limitations.substring(0, limitations.length - 2);
      m.part.limitations = limitations;
    }).value();

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
    'meds',
    'person'
  ];

  DocumentMedicalsSearchCtrl.$resolve = {
    meds: [
      '$stateParams',
      'PersonDocumentMedical',
      function ($stateParams, PersonDocumentMedical) {
        return PersonDocumentMedical.query($stateParams).$promise;
      }
    ]
  };
  angular.module('gva').controller('DocumentMedicalsSearchCtrl', DocumentMedicalsSearchCtrl);
}(angular, _));