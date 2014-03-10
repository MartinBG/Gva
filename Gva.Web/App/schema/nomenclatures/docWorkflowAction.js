/*global module*/
(function (module) {
  'use strict';

  module.exports = [
    { nomValueId: 1, name: 'За подпис', alias: 'SignRequest', isActive: true },
    { nomValueId: 2, name: 'За съгласуване', alias: 'DiscussRequest', isActive: true },
    { nomValueId: 3, name: 'За одобрение', alias: 'ApprovalRequest', isActive: true },
    { nomValueId: 4, name: 'За регистрация', alias: 'RegistrationRequest', isActive: true },
    { nomValueId: 5, name: 'Подпис', alias: 'Sign', isActive: true },
    { nomValueId: 6, name: 'Съгласуване', alias: 'Discuss', isActive: true },
    { nomValueId: 7, name: 'Одобрение', alias: 'Approval', isActive: true },
    { nomValueId: 8, name: 'Регистрация (авт.)', alias: 'Registration', isActive: true },
    { nomValueId: 9, name: 'Електронен подпис (авт.)', alias: 'ElectronicSign', isActive: true }
  ];
})(typeof module === 'undefined' ? (this['docWorkflowAction'] = {}) : module);
