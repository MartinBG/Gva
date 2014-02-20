/*global module*/
(function (module) {
  'use strict';

  // Типове документи за проверка
  module.exports = [
    { nomValueId: 6451, code: 'APP', name: 'Заявление', nameAlt: 'Application', alias: 'Application' },
    { nomValueId: 6452, code: 'PR', name: 'Програма', nameAlt: 'Programme', alias: 'Programme' },
    { nomValueId: 6453, code: 'РЪК', name: 'Ръководство ', nameAlt: 'Manual', alias: 'Manual' },
    { nomValueId: 6454, code: 'REG', name: 'Регистрация ', nameAlt: 'Registration', alias: 'Registration' },
    { nomValueId: 6455, code: '135', name: 'Обява', nameAlt: 'Обява', alias: null },
    { nomValueId: 6456, code: '136', name: 'Licence authentication form (155a)', nameAlt: 'Licence authentication form (155a)', alias: null },
    { nomValueId: 6458, code: 'OPS', name: 'Описание', nameAlt: 'Описание', alias: 'OPS' },
    { nomValueId: 6459, code: 'LUC', name: 'Летателно обучение в УЦ', nameAlt: 'Летателно обучение в УЦ', alias: 'LUC' },
    { nomValueId: 6460, code: '42', name: 'Индивидуална програма за първоначално летателно обучение', nameAlt: 'Индивидуална програма за първоначално летателно обучение', alias: null },
    { nomValueId: 6500, code: '31', name: 'Certificate', nameAlt: 'Certificate', alias: 'Certificate' },
    { nomValueId: 6546, code: '48', name: 'Base training form', nameAlt: 'Base training form', alias: 'BaseTrainingForm' },
    { nomValueId: 6566, code: '72', name: 'Authorisation', nameAlt: 'Authorisation', alias: 'Authorisation' }
  ];
})(typeof module === 'undefined' ? (this['personCheckDocumentType'] = {}) : module);
