/*global angular, _, $*/
(function (angular, _, $) {
  'use strict';

  function PrintLicenceModalCtrl(
    $scope,
    $modalInstance,
    PersonLicenceEditions,
    scModalParams,
    licenceEdition
  ) {

    $scope.model = {
      stampNumber: licenceEdition.part.stampNumber,
      stampNumberReadonly: !!licenceEdition.part.stampNumber
    };

    $scope.save = function () {
      licenceEdition.part.stampNumber = $scope.model.stampNumber;

      return PersonLicenceEditions.save({
        id: scModalParams.lotId,
        ind: scModalParams.licencePartIndex,
        index: scModalParams.editionPartIndex
      }, licenceEdition).$promise.then(function (savedEdition) {
        return $modalInstance.close(savedEdition);
      });
    };

    $scope.print = function () {
      var href = 'api/print?lotId=' + scModalParams.lotId +
                  '&licenceInd=' + scModalParams.licencePartIndex,
          hiddenElement = $('<a href="' + href + '" target="_self"></a>')[0];

      hiddenElement.click();
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };
  }

  PrintLicenceModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'PersonLicenceEditions',
    'scModalParams',
    'licenceEdition'
  ];

  PrintLicenceModalCtrl.$resolve = {
    licenceEdition: [
      'scModalParams',
      'PersonLicenceEditions',
      function resolveDocs(scModalParams, PersonLicenceEditions) {
        return PersonLicenceEditions.get({
          id: scModalParams.lotId,
          ind: scModalParams.licencePartIndex,
          index: scModalParams.editionPartIndex
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('PrintLicenceModalCtrl', PrintLicenceModalCtrl);
}(angular, _, $));
