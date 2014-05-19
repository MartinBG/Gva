/*global module*/
(function (module) {
  'use strict';

  //Номенклатура Тип EASA на ВС
  module.exports = [
    {
      nomValueId: 8206, code: 'BA', name: 'Balloon', nameAlt: null, parentValueId: null, alias: 'BA'
    },
    {
      nomValueId: 8207, code: 'CO', name: 'Commuter', nameAlt: null, parentValueId: null, alias: 'CO'
    },
    {
      nomValueId: 8208, code: 'EX', name: 'Experimental', nameAlt: null, parentValueId: null, alias: 'EX'
    },
    {
      nomValueId: 8209, code: 'GL', name: 'Glider', nameAlt: null, parentValueId: null, alias: 'GL'
    },
    {
      nomValueId: 8210, code: 'GP', name: 'Gyroplane', nameAlt: null, parentValueId: null, alias: 'GP'
    },
    {
      nomValueId: 8211, code: 'LA', name: 'Large Aeroplane', nameAlt: null, parentValueId: null, alias: 'LA'
    },
    {
      nomValueId: 8212, code: 'MH', name: 'Motor-hanglider', nameAlt: null, parentValueId: null, alias: 'MH'
    },
    {
      nomValueId: 8213, code: 'PT', name: 'Paramotor-Trike', nameAlt: null, parentValueId: null, alias: 'PT'
    },
    {
      nomValueId: 8214, code: 'RC', name: 'Rotorcraft', nameAlt: null, parentValueId: null, alias: 'RC'
    },
    {
      nomValueId: 8215, code: 'SA', name: 'Small Aeroplane', nameAlt: null, parentValueId: null, alias: 'SA'
    },
    {
      nomValueId: 8216, code: 'SR', name: 'Small Rotorcraft', nameAlt: null, parentValueId: null, alias: 'SR'
    },
    {
      nomValueId: 8217, code: 'VLA', name: 'Very Light Aeroplane', nameAlt: null, parentValueId: null, alias: 'VLA'
    },
    {
      nomValueId: 8218, code: 'VLR', name: 'Very Light Rotorcraft', nameAlt: null, parentValueId: null, alias: 'VLR'
    }
  ];
})(typeof module === 'undefined' ? (this['EASAType'] = {}) : module);
