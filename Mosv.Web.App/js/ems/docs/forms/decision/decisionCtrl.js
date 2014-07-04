/*global angular*/
(function (angular) {
  'use strict';

  function DecisionCtrl(
    $scope,
    Docs
  ) {
      $scope.isLoaded = false;

      Docs.getRioEditableFile({
        id: $scope.model.docId
      }).$promise.then(function (result) {
        $scope.model.jObject = result.content;

        if (!!$scope.model.jObject.OfficialCollection[0]) {
          if (!!$scope.model.jObject.OfficialCollection[0].PersonNames) {
            $scope.model.jObject.PersonType = {
              id: 'bgCitizen',
              text: 'Българсрски гражданин'
            };
          }
          else if (!!$scope.model.jObject.OfficialCollection[0].ForeignCitizenNames) {
            $scope.model.jObject.PersonType = {
              id: 'foreignCitizen',
              text: 'Чужд гражданин'
            };
          }
        }
        else {
          $scope.model.jObject.OfficialCollection.push({
            ElectronicDocumentAuthorQuality: null,
            PersonNames: null,
            ForeignCitizenNames: null
          });
        }

        $scope.isLoaded = true;
      });

      $scope.degreeAccessValueChanged = function () {
        $scope.model.jObject.DegreeAccessRequestedPublicInformation =
          $scope.model.jObject.DegreeAccessRequestedPublicInformation.id;
      };

      $scope.personTypeValueChanged = function () {
        if (!!$scope.model.jObject.PersonType) {
          if ($scope.model.jObject.PersonType.id === 'bgCitizen') {
            $scope.model.jObject.OfficialCollection[0].PersonNames = {
              First: null,
              Middle: null,
              Last: null
            };

            $scope.model.jObject.OfficialCollection[0].ForeignCitizenNames = null;
          }
          else if ($scope.model.jObject.PersonType.id === 'foreignCitizen') {
            $scope.model.jObject.OfficialCollection[0].ForeignCitizenNames = {
              FirstCyrillic: null,
              LastCyrillic: null,
              OtherCyrillic: null
            };

            $scope.model.jObject.OfficialCollection[0].PersonNames = null;
          }
        }
      }; 
  }

  DecisionCtrl.$inject = [
    '$scope',
    'Docs'
  ];

  angular.module('ems').controller('DecisionCtrl', DecisionCtrl);
}(angular));
