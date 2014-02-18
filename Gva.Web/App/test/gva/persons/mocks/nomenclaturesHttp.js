/*global angular, require, _*/
(function (angular, require, _) {
  'use strict';
  var nomenclatures = require('./nomenclatures.sample');

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', '/api/nomenclatures/units?name',
        function ($params, $filter) {
          return [
            200,
            $filter('filter')(nomenclatures.unit, {
              name: $params.name
            })
          ];
        })
      .when('GET', '/api/nomenclatures/persons?id',
        function ($params, $filter, personLots) {

          var res = _(personLots).map(function (item) {
            return {
              nomTypeValueId: item.lotId,
              name: item.personData.part.firstName + ' ' + item.personData.part.lastName
            };
          }).value();

          if ($params.id) {
            res = $filter('filter')(res, { nomTypeValueId: parseInt($params.id, 10) }, true)[0];
          }

          return [200, res];
        })
      .when('GET', '/api/nomenclatures/personCheckDocumentRoles?term&id',
        function ($params, $filter) {
          var res = nomenclatures.personCheckDocumentRoles;

          if ($params.id) {
            res = $filter('filter')(res, { nomTypeValueId: parseInt($params.id, 10) }, true)[0];
          } else {
            if ($params.term) {
              res = $filter('filter')(res, { name: $params.term }, false);
            }
          }

          return [200, res];
        })
      .when('GET', '/api/nomenclatures/personCheckDocumentTypes?term&id',
        function ($params, $filter) {
          var res = nomenclatures.personCheckDocumentTypes;

          if ($params.id) {
            res = $filter('filter')(res, { nomTypeValueId: parseInt($params.id, 10) }, true)[0];
          } else {
            if ($params.term) {
              res = $filter('filter')(res, { name: $params.term }, false);
            }
          }

          return [200, res];
        })
      .when('GET', '/api/nomenclatures/documentTrainingRoles?term&id',
        function ($params, $filter) {
          var res = $filter('filter')(
             nomenclatures.documentRoles,
            function (nom) {
              return nom.content.categoryCode === 'O';
            },
            true);

          if ($params.id) {
            res = $filter('filter')(res, { nomTypeValueId: parseInt($params.id, 10) }, true)[0];
          } else {
            if ($params.term) {
              res = $filter('filter')(res, { name: $params.term }, false);
            }
          }

          return [200, res];
        })
      .when('GET', '/api/nomenclatures/documentTrainingTypes?term&id',
        function ($params, $filter) {
          var res = nomenclatures.personOtherDocumentTypes;

          if ($params.id) {
            res = $filter('filter')(res, { nomTypeValueId: parseInt($params.id, 10) }, true)[0];
          } else {
            if ($params.term) {
              res = $filter('filter')(res, { name: $params.term }, false);
            }
          }

          return [200, res];
        })
      .when('GET', '/api/nomenclatures/personIdDocumentTypes?term&id',
        function ($params, $filter) {
          var res = nomenclatures.personIdDocumentTypes;

          if ($params.id) {
            res = $filter('filter')(res, { nomTypeValueId: parseInt($params.id, 10) }, true)[0];
          } else {
            if ($params.term) {
              res = $filter('filter')(res, { name: $params.term }, false);
            }
          }

          return [200, res];
        })
      .when('GET', '/api/nomenclatures/addressTypes?term&id&type',
        function ($params, $filter) {
          var res = nomenclatures.addressTypes;

          if ($params.id) {
            res = $filter('filter')(res, { nomTypeValueId: parseInt($params.id, 10) }, true)[0];
          } else {
            if ($params.term) {
              res = $filter('filter')(res, { name: $params.term }, false);
            }
          }

          if ($params.type) {
            res = $filter('filter')(
              res,
              function (nom) {
                return nom.content.type === $params.type;
              },
              true);
          }

          return [200, res];
        })
      .when('GET',
          '/api/nomenclatures/:alias' +
          '?term&parentId&id&staffTypeId&parentAlias&nomTypeParentValueId',
        function ($params, $filter) {
          var res = nomenclatures[$params.alias];

          if ($params.id) {
            res = $filter('filter')(res, { nomTypeValueId: parseInt($params.id, 10) }, true)[0];
          } else {
            if ($params.term) {
              res = $filter('filter')(res, { name: $params.term }, false);
            }

            if ($params.parentId) {
              res = $filter('filter')(res, { parentId: parseInt($params.parentId, 10) }, true);
            }

            if ($params.nomTypeParentValueId) {
              res = $filter('filter')(
                res,
                { nomTypeParentValueId: parseInt($params.nomTypeParentValueId, 10) },
                true);
            }

            if ($params.staffTypeId) {
              var parentIds = _.pluck(
                $filter('filter')(nomenclatures[$params.parentAlias], {
                  nomTypeParentValueId: parseInt($params.staffTypeId, 10)
                }, true),
                'nomTypeValueId');

              res = $filter('filter')(
                res,
                function (nom) {
                  return parentIds.indexOf(nom.nomTypeParentValueId) !== -1;
                }, true);
            }
          }

          return [200, res];
        }
      );
  });
}(angular, require, _));
