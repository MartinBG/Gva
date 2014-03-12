/*global module*/
(function (module) {
  'use strict';

  module.exports = [
    {
      "docDirectionId": 1,
      "name": "Входящ",
      "alias": "Incoming",
      "isActive": true,
      "version": "AAAAAAAAHsg=",
      "docTypeClassifications": [],
      "docTypeUnitRoles": [],
      "docs": []
    },
    {
      "docDirectionId": 2,
      "name": "Вътрешен",
      "alias": "Internal",
      "isActive": false,
      "version": "AAAAAAAAHsk=",
      "docTypeClassifications": [],
      "docTypeUnitRoles": [],
      "docs": []
    },
    {
      "docDirectionId": 3,
      "name": "Изходящ",
      "alias": "Outgoing",
      "isActive": false,
      "version": "AAAAAAAAHso=",
      "docTypeClassifications": [],
      "docTypeUnitRoles": [],
      "docs": []
    },
    {
      "docDirectionId": 4,
      "name": "Циркулярен",
      "alias": "InternalOutgoing",
      "isActive": false,
      "version": "AAAAAAAAHss=",
      "docTypeClassifications": [],
      "docTypeUnitRoles": [],
      "docs": []
    }
  ];
})(typeof module === 'undefined' ? (this['docDirection'] = {}) : module);
