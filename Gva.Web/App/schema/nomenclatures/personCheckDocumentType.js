/*global module*/
(function (module) {
  'use strict';

  // Типове документи за проверка
  module.exports = [
    { nomTypeValueId: 6451, code: 'APP', name: 'Заявление', nameAlt: 'Application', alias: 'Application' },
    { nomTypeValueId: 6452, code: 'PR', name: 'Програма', nameAlt: 'Programme', alias: 'Programme' },
    { nomTypeValueId: 6453, code: 'РЪК', name: 'Ръководство ', nameAlt: 'Manual', alias: 'Manual' },
    { nomTypeValueId: 6454, code: 'REG', name: 'Регистрация ', nameAlt: 'Registration', alias: 'Registration' },
    { nomTypeValueId: 6455, code: '135', name: 'Обява', nameAlt: 'Обява', alias: null },
    { nomTypeValueId: 6456, code: '136', name: 'Licence authentication form (155a)', nameAlt: 'Licence authentication form (155a)', alias: null },
    { nomTypeValueId: 6458, code: 'OPS', name: 'Описание', nameAlt: 'Описание', alias: 'OPS' },
    { nomTypeValueId: 6459, code: 'LUC', name: 'Летателно обучение в УЦ', nameAlt: 'Летателно обучение в УЦ', alias: 'LUC' },
    { nomTypeValueId: 6460, code: '42', name: 'Индивидуална програма за първоначално летателно обучение', nameAlt: 'Индивидуална програма за първоначално летателно обучение', alias: null },
    { nomTypeValueId: 6500, code: '31', name: 'Certificate', nameAlt: 'Certificate', alias: 'Certificate' },
    { nomTypeValueId: 6546, code: '48', name: 'Base training form', nameAlt: 'Base training form', alias: 'BaseTrainingForm' },
    { nomTypeValueId: 6566, code: '72', name: 'Authorisation', nameAlt: 'Authorisation', alias: 'Authorisation' }
  ];
})(typeof module === 'undefined' ? (this['personCheckDocumentType'] = {}) : module);
