/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    function sortByDate(a, b) {
      if (a.part.certDate > b.part.certDate) {
        return -1;
      } else if (a.part.certDate < b.part.certDate) {
        return 1;
      } else {
        return 0;
      }
    }

    $httpBackendConfiguratorProvider
      .when('GET', '/api/aircrafts/:id/aircraftCertRegistrationsFM',
        function ($params, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();
          aircraft.aircraftCertRegistrationsFM =
            aircraft.aircraftCertRegistrationsFM.sort(sortByDate);
          return [200, aircraft.aircraftCertRegistrationsFM];
        })
      .when('GET', '/api/aircrafts/:id/aircraftCertRegistrationsFM/:ind',
        function ($params, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();
          var regs = aircraft.aircraftCertRegistrationsFM.sort(sortByDate),
            reg;
          if ($params.ind && $params.ind !== 'current') {
            reg = _(regs)
              .filter({ partIndex: parseInt($params.ind, 10) }).first();
          } else {
            reg = regs.length > 0 ? regs[0] : null;
          }

          if (reg || $params.ind === 'current') {
            if (reg !== null) {
              var ind = _.indexOf(regs, reg);
              reg.firstIndex = regs[regs.length - 1] ? regs[regs.length - 1].partIndex : undefined;
              reg.prevIndex = regs[ind + 1] ? regs[ind + 1].partIndex : undefined;
              reg.nextIndex = regs[ind - 1] ? regs[ind - 1].partIndex : undefined;
              reg.lastIndex = regs[0] ? regs[0].partIndex : undefined;

              reg.lastReg = {
                certNumber: regs[0].part.certNumber,
                register: regs[0].part.register,
                certDate: regs[0].part.certDate,
                regMark: regs[0].part.regMark
              };

              reg.firstReg = {
                certNumber: regs[regs.length - 1].part.certNumber,
                register: regs[regs.length - 1].part.register,
                certDate: regs[regs.length - 1].part.certDate,
                regMark: regs[regs.length - 1].part.regMark
              };
            }
            return [200, reg];
          }
          else {
            return [404];
          }
        })
      .when('POST', '/api/aircrafts/:id/aircraftCertRegistrationsFM',
        function ($params, $jsonData, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var reg = $jsonData;

          reg.partIndex = aircraft.nextIndex++;

          aircraft.aircraftCertRegistrationsFM.push(reg);

          return [200];
        })
      .when('POST', '/api/aircrafts/:id/aircraftCertRegistrationsFM/:ind',
        function ($params, $jsonData, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var reg = _(aircraft.aircraftCertRegistrationsFM)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          _.assign(reg, $jsonData);

          return [200];
        })
      .when('DELETE', '/api/aircrafts/:id/aircraftCertRegistrationsFM/:ind',
        function ($params, $jsonData, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var regInd = _(aircraft.aircraftCertRegistrationsFM)
            .findIndex({ partIndex: parseInt($params.ind, 10) });

          aircraft.aircraftCertRegistrationsFM.splice(regInd, 1);

          return [200];
        });
  });
}(angular, _));