/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('ExaminationSystem', ['$resource', function ($resource) {
    return $resource('api/examinationSystem', {}, {
      loadData: {
        method: 'GET',
        url: 'api/examinationSystem/loadData'
      },
      updateStates: {
        method: 'GET',
        url: 'api/examinationSystem/updateStates'
      },
      getExams: {
        method: 'GET',
        url: 'api/examinationSystem/exams',
        isArray: true
      },
      getCertCampaigns: {
        method: 'GET',
        url: 'api/examinationSystem/certCampaigns',
        isArray: true
      },
      getCertPaths: {
        method: 'GET',
        url: 'api/examinationSystem/certPaths',
        isArray: true
      },
      getQualifications: {
        method: 'GET',
        url: 'api/examinationSystem/qualifications',
        isArray: true
      },
      getExaminees: {
        method: 'GET',
        url: 'api/examinationSystem/examinees',
        isArray: true
      },
      getPersonExams: {
        method: 'GET',
        url: 'api/examinationSystem/personExams',
        isArray: true
      },
      checkConnection: {
        method: 'GET',
        url: 'api/examinationSystem/checkConnection'
      }
    });
  }]);
}(angular));
