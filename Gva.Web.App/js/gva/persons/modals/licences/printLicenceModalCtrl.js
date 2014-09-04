/*global angular, _, $*/
(function (angular, _, $) {
  'use strict';

  function PrintLicenceModalCtrl(
    $scope,
    $modalInstance,
    PersonLicences,
    scModalParams,
    licence
  ) {
    var lastEdition = _.last(licence.part.editions);

    $scope.model = {
      stampNumber: lastEdition.stampNumber,
      stampNumberReadonly: !!lastEdition.stampNumber
    };

    $scope.save = function () {
      lastEdition.stampNumber = $scope.model.stampNumber;

      return PersonLicences.save({
        id: scModalParams.lotId,
        ind: scModalParams.index
      }, licence).$promise.then(function (savedLicence) {
        return $modalInstance.close(savedLicence);
      });
    };

    $scope.print = function () {
      var href = 'api/print?lotId=' + scModalParams.lotId +
                  '&licenceInd=' + scModalParams.index,
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
    'PersonLicences',
    'scModalParams',
    'licence'
  ];

  PrintLicenceModalCtrl.$resolve = {
    licence: [
      'scModalParams',
      'PersonLicences',
      function resolveDocs(scModalParams, PersonLicences) {
        return PersonLicences.get({
          id: scModalParams.lotId,
          ind: scModalParams.index
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('PrintLicenceModalCtrl', PrintLicenceModalCtrl);
}(angular, _, $));
