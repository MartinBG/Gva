/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('gva').factory('Organization', ['$resource', function($resource) {
    var Organization = $resource('/api/organizations/:id', {}, {
      'getCaseTypes': {
        method: 'GET',
        url: '/api/organizations/caseTypes'
      }
    });

    var p = _.partial(Organization, {
      organizationData: {
        name: null,
        nameAlt: null,
        code: null,
        uin: null,
        CAO: null,
        ICAO: null,
        IATA: null,
        SITA: null,
        organizationType: null,
        organizationKind: null,
        dateCAOFirstIssue: null,
        dateCAOLastIssue: null,
        dateCAOValidTo: null,
        dateValidTo: null,
        phones: null,
        webSite: null,
        notes: null,
        docRoom: null,
        valid: null,
        caseTypes: []
      }
    });

    _.extend(p, Organization);
    p.prototype = Organization.prototype;

    return p;
  }]);
}(angular, _));
