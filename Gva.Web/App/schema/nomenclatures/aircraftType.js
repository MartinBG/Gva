/*global module*/
(function (module) {
  'use strict';

  //Номенклатура Типове ВС
  module.exports = [
    {
      nomValueId: 7445, code: 'LA', name: 'Големи ВС', nameAlt: 'Large aircraft', alias: 'LA',
      textContent: {
        description: 'Aeroplanes with a maximum take-off mass of more than 5700 kg, requiring type training and individual type rating.'
      }
    },
    {
      nomValueId: 7446, code: 'A-tr', name: 'ВС до 5700кг и по малко', nameAlt: 'Aeroplanes of 5700kg and below', alias: 'A-tr',
      textContent: {
        description: 'Requiring type training and individual type rating.'
      }
    },
    {
      nomValueId: 7447, code: 'AMTE', name: 'Самолети multiple turbine engines of 5700kg and below', nameAlt: 'Aeroplanes multiple turbine engines of 5700kg and below', alias: 'AMTE',
      textContent: {
        description: 'Eligible for type examinations and manufacturer group ratings.'
      }
    },
    {
      nomValueId: 7448, code: 'ASTE', name: 'Самолети single turbine engine of 5700kg and below', nameAlt: 'Aeroplanes single turbine engine of 5700kg and below', alias: 'ASTE',
      textContent: {
        description: 'Eligible for type examinations and group ratings.'
      }
    },
    {
      nomValueId: 7449, code: 'AMPE-MS', name: 'Самолети multiple piston engines – metal structure (AMPE-MS)', nameAlt: 'Aeroplane multiple piston engines – metal structure (AMPE-MS)', alias: 'AMPE-MS',
      textContent: {
        description: 'Eligible for type examinations and group ratings.'
      }
    },
    {
      nomValueId: 7450, code: 'ASPE-MS', name: 'Самолети single piston engine – metal structure (ASPE-MS)', nameAlt: 'Aeroplane single piston engine – metal structure (ASPE-MS)', alias: 'ASPE-MS',
      textContent: {
        description: 'Eligible for type examinations and group ratings.'
      }
    },
    {
      nomValueId: 7451, code: 'AMPE-WS', name: 'Самолети multiple piston engines – wooden structure', nameAlt: 'Aeroplane multiple piston engines – wooden structure', alias: 'AMPE-WS',
      textContent: {
        description: 'Eligible for type examinations and group ratings.'
      }
    },
    {
      nomValueId: 7452, code: 'ASPE-WS', name: 'Самолети single piston engine – wooden structure.', nameAlt: 'Aeroplane single piston engine – wooden structure.', alias: 'ASPE-WS',
      textContent: {
        description: 'Eligible for type examinations and group ratings'
      }
    },
    {
      nomValueId: 7453, code: 'AMPE-CS', name: 'Самолети multiple piston engines – composite structure ', nameAlt: 'Aeroplane multiple piston engines – composite structure ', alias: 'AMPE-CS',
      textContent: {
        description: 'Eeligible for type examinations and group ratings.'
      }
    },
    {
      nomValueId: 7454, code: 'ASPE-CS', name: 'Самолети single piston engine – composite structure', nameAlt: 'Aeroplane single piston engine – composite structure', alias: 'ASPE-CS',
      textContent: {
        description: 'Eligible for type examinations and group ratings.'
      }
    },
    {
      nomValueId: 7455, code: 'MEH', name: 'Multi-engine хеликоптери ', nameAlt: 'Multi-engine helicopters ', alias: 'MEH',
      textContent: {
        description: 'Requiring type training and individual type rating.'
      }
    }
  ];
})(typeof module === 'undefined' ? (this['aircraftType'] = {}) : module);
