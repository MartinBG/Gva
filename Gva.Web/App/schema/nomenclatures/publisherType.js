/*global module*/
(function (module) {
  'use strict';

  module.exports = [
      { nomTypeValueId: 10101, code: '1', name: 'Други', nameAlt: 'Other', alias: 'otherDocPublishers' },
      { nomTypeValueId: 10102, code: '2', name: 'Авио-Организация', nameAlt: 'Organization', alias: 'organizations' }
  ];
})(typeof module === 'undefined' ? (this['publisherType'] = {}) : module);
