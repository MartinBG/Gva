/*global angular, _*/

(function (angular, _) {
  'use strict';

  function AppExSystDataCtrl($scope, scModal) {
    $scope.qualifications =  _.map($scope.model.qualifications,
      function (qualification) {
        var state = qualification.state;
        if(state.indexOf(' ') > 0)
        {
          state = state.split(' ');
        }
        return {
          qualificationName: qualification.qualificationName,
          licenceTypeCode: qualification.licenceType.code,
          state: state[0],
          method: state[1]
        };
      });

    $scope.addExams = function () {
      var modalInstance = scModal.open('appExSystChooseExams', {
        qualificationCode: $scope.model.qualificationCode,
        certCampCode: $scope.model.certCampaign.code,
        exams: $scope.model.exams
      });

    modalInstance.result.then(function (exams) {
        $scope.model.exams = exams;
      });

      return modalInstance.opened;
    };
  }

  AppExSystDataCtrl.$inject = ['$scope', 'scModal'];

  angular.module('gva').controller('AppExSystDataCtrl', AppExSystDataCtrl);
}(angular, _));
