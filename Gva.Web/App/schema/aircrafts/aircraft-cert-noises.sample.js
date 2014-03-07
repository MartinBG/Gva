/*global module, require*/
(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');

  module.exports = {
    aircraft1Noise1: {
      certId: '5',
      issueDate: '2013-09-04T00:00',
      issueNumber: '333',
      tcdsn: '',
      chapter: 'ИКАО Анекс 16, том І, Глава: 6',
      flyover: '',
      approach:  '',
      lateral:  '',
      overflight: '79.20',
      takeoff:  '',
      modifications: '',
      modificationsAlt: '',
      notes: 'TCDSN EASA.IM.A.277'
    }
  };
})(typeof module === 'undefined' ? (this['aircraft-cert-noises.sample'] = {}) : module);
