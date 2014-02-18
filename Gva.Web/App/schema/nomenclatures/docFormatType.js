/*global module*/
(function (module) {
  'use strict';

  module.exports = [
    {
      "docFormatTypeId": 1,
      "name": "Електронен",
      "alias": "Electronic",
      "isActive": true,
      "version": "AAAAAAAAIBI=",
      "docs": []
    },
    {
      "docFormatTypeId": 2,
      "name": "Електронен с хартия",
      "alias": "ElectronicWithPaper",
      "isActive": false,
      "version": "AAAAAAAAIBM=",
      "docs": []
    },
    {
      "docFormatTypeId": 3,
      "name": "Хартиен",
      "alias": "Paper",
      "isActive": false,
      "version": "AAAAAAAAIBQ=",
      "docs": []
    }
  ];
})(typeof module === 'undefined' ? (this['docFormatType'] = {}) : module);
