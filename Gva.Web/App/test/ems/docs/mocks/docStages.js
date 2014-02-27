/*global angular*/
(function (angular) {
  'use strict';

  angular.module('app').constant('docStages', [
    {
      docElectronicServiceStageId: 1,
      electronicServiceStageId: 1,
      docId: 1,
      startingDate: '2014-01-08T17:21:43.303',
      electronicServiceStageName: 'Приемане на заявление за  административна услуга',
      electronicServiceStageExecutors: 'Служител ДКХ',
      expectedEndingDate: null,
      endingDate: '2014-01-09T17:38:49.683',
      isCurrentStage: false
    },
    {
      docElectronicServiceStageId: 2,
      electronicServiceStageId: 2,
      docId: 1,
      startingDate: '2014-01-08T17:21:43.303',
      electronicServiceStageName: 'Проверка за редовност и допустимост на заявлението',
      electronicServiceStageExecutors: 'Служител ДКХ',
      expectedEndingDate: null,
      endingDate: '2014-01-09T17:38:49.683',
      isCurrentStage: true
    },
    {
      docElectronicServiceStageId: 3,
      electronicServiceStageId: 1,
      docId: 2,
      startingDate: '2014-01-08T17:21:43.303',
      electronicServiceStageName: 'Приемане на заявление за  административна услуга',
      electronicServiceStageExecutors: 'Служител ДКХ',
      expectedEndingDate: null,
      endingDate: '2014-01-09T17:38:49.683',
      isCurrentStage: false
    },
    {
      docElectronicServiceStageId: 4,
      electronicServiceStageId: 2,
      docId: 2,
      startingDate: '2014-01-08T17:21:43.303',
      electronicServiceStageName: 'Проверка за редовност и допустимост на заявлението',
      electronicServiceStageExecutors: 'Служител ДКХ',
      expectedEndingDate: null,
      endingDate: '2014-01-09T17:38:49.683',
      isCurrentStage: true
    },
    {
      docElectronicServiceStageId: 5,
      electronicServiceStageId: 9,
      docId: 3,
      startingDate: '2014-01-08T17:21:43.303',
      electronicServiceStageName: 'Приемане на заявление за  административна услуга',
      electronicServiceStageExecutors: 'Служител ДКХ',
      expectedEndingDate: null,
      endingDate: '2014-01-09T17:38:49.683',
      isCurrentStage: false
    },
    {
      docElectronicServiceStageId: 6,
      electronicServiceStageId: 10,
      docId: 3,
      startingDate: '2014-01-08T17:21:43.303',
      electronicServiceStageName: 'Проверка за редовност и допустимост на заявлението',
      electronicServiceStageExecutors: 'Служител ДКХ',
      expectedEndingDate: null,
      endingDate: '2014-01-09T17:38:49.683',
      isCurrentStage: true
    }
  ]);

}(angular));

