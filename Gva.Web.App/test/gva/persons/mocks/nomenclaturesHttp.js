/*global angular, require, _*/
(function (angular, require, _) {
  'use strict';
  var nomenclatures = require('./nomenclatures.sample');

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', 'api/nomenclatures/units?name',
        function ($params, $filter) {
          return [
            200,
            $filter('filter')(nomenclatures.unit, {
              name: $params.name
            })
          ];
        })
      .when('GET', 'api/nomenclatures/persons?id',
        function ($params, $filter, personLots) {

          var res = _(personLots).map(function (item) {
            return {
              nomValueId: item.lotId,
              name: item.personData.part.firstName + ' ' + item.personData.part.lastName
            };
          }).value();

          if ($params.id) {
            res = $filter('filter')(res, { nomValueId: parseInt($params.id, 10) }, true)[0];
          }

          return [200, res];
        })
      .when('GET', 'api/nomenclatures/otherDocumentRoles?term&id',
        function ($params, $filter) {
          var res = nomenclatures.documentRoles;

          if ($params.id) {
            res = $filter('filter')(res, { nomValueId: parseInt($params.id, 10) }, true)[0];
          } else {
            res = $filter('filter')(
              res,
              function (nom) {
                return nom.textContent.categoryAlias === 'other';
              },
              true);

            if ($params.term) {
              res = $filter('filter')(res, { name: $params.term }, false);
            }
          }

          return [200, res];
        })
      .when('GET', 'api/nomenclatures/otherDocumentTypes?term&id',
        function ($params, $filter) {
          var res = nomenclatures.documentTypes;

          if ($params.id) {
            res = $filter('filter')(res, { nomValueId: parseInt($params.id, 10) }, true)[0];
          } else {
            res = $filter('filter')(
              res,
              function (nom) {
                return nom.textContent.isIdDocument === 'false';
              },
              true);

            if ($params.term) {
              res = $filter('filter')(res, { name: $params.term }, false);
            }
          }

          return [200, res];
        })
      .when('GET', 'api/nomenclatures/documentRoles?term&id&categoryAlias&staffCode',
        function ($params, $filter) {
          var res = nomenclatures.documentRoles;

          if ($params.id) {
            res = $filter('filter')(res, { nomValueId: parseInt($params.id, 10) }, true)[0];
          } else {
            if ($params.categoryAlias) {
              res = $filter('filter')(
                res,
                function (nom) {
                  return nom.textContent.categoryAlias === $params.type;
                },
                true);
            }

            if ($params.staffCode) {
              res = $filter('filter')(
                res,
                function (nom) {
                  return nom.textContent.staffAlias === undefined;
                },
                true);
            }

            if ($params.term) {
              res = $filter('filter')(res, { name: $params.term }, false);
            }
          }

          return [200, res];
        })
      .when('GET', 'api/nomenclatures/documentTypes?term&id&isIdDocument&staffCode',
        function ($params, $filter) {
          var res = nomenclatures.documentTypes;

          if ($params.id) {
            res = $filter('filter')(res, { nomValueId: parseInt($params.id, 10) }, true)[0];
          } else {
            if ($params.isIdDocument) {
              res = $filter('filter')(
                res,
                function (nom) {
                  return nom.textContent.isIdDocument === $params.isIdDocument;
                },
                true);
            }

            if ($params.staffCode) {
              res = $filter('filter')(
                res,
                function (nom) {
                  return nom.textContent.staffAlias === undefined ||
                         nom.textContent.staffAlias === $params.staffCode;
                },
                true);
            }

            if ($params.term) {
              res = $filter('filter')(res, { name: $params.term }, false);
            }
          }

          return [200, res];
        })
      .when('GET', 'api/nomenclatures/addressTypes?term&id&type',
        function ($params, $filter) {
          var res = nomenclatures.addressTypes;

          if ($params.id) {
            res = $filter('filter')(res, { nomValueId: parseInt($params.id, 10) }, true)[0];
          } else {
            if ($params.term) {
              res = $filter('filter')(res, { name: $params.term }, false);
            }
          }

          if ($params.type) {
            res = $filter('filter')(
              res,
              function (nom) {
                return nom.textContent.type === $params.type;
              },
              true);
          }

          return [200, res];
        })
      .when('GET', 'api/nomenclatures/electronicServiceStages?docTypeId&id',
        function ($params, $filter) {
          var res = _(nomenclatures.electronicServiceStage).map(function (item) {
            return {
              nomValueId: item.nomValueId,
              name: item.name,
              docTypeId: item.docTypeId,
              item: item
            };
          }).value();

          if ($params.docTypeId) {
            res = $filter('filter')(res, { docTypeId: parseInt($params.docTypeId, 10) }, true);
          }

          if ($params.id) {
            res = $filter('filter')(res, { nomValueId: parseInt($params.id, 10) }, true)[0];
          }

          return [200, res];
        })
      .when('GET', 'api/nomenclatures/docCasePartTypes?id',
        function ($params, $filter) {
          var res = _(nomenclatures.docCasePartType).map(function (item) {
            return {
              nomValueId: item.docCasePartTypeId,
              name: item.name
            };
          }).value();

          if ($params.id) {
            res = $filter('filter')(res, { nomValueId: parseInt($params.id, 10) }, true)[0];
          }

          return [200, res];
        })
      .when('GET', 'api/nomenclatures/docDirections?id',
        function ($params, $filter) {
          var res = _(nomenclatures.docDirection).map(function (item) {
            return {
              nomValueId: item.docDirectionId,
              name: item.name
            };
          }).value();

          if ($params.id) {
            res = $filter('filter')(res, { nomValueId: parseInt($params.id, 10) }, true)[0];
          }

          return [200, res];
        })
      .when('GET',
          'api/nomenclatures/:alias' +
          '?term&id&ids&staffTypeId&parentAlias&parentValueId&va',
        function ($params, $filter) {
          var res = nomenclatures[$params.alias];

          if ($params.id) {
            res = $filter('filter')(res, { nomValueId: parseInt($params.id, 10) }, true)[0];
          } else if ($params.ids) {
            var ids = _.map($params.ids.split(','), function (idStr) {
              return parseInt(idStr, 10);
            });
            res = _.filter(res, function (nom) {
              return _.contains(ids, nom.nomValueId);
            });
          } else if ($params.va) {
            res = $filter('filter')(res, { alias: $params.va }, true)[0];
          } else {
            if ($params.term) {
              res = $filter('filter')(res, { name: $params.term }, false);
            }

            if ($params.parentValueId) {
              res = $filter('filter')(
                res,
                { parentValueId: parseInt($params.parentValueId, 10) },
                true);
            }

            if ($params.staffTypeId) {
              var parentIds = _.pluck(
                $filter('filter')(nomenclatures[$params.parentAlias], {
                  parentValueId: parseInt($params.staffTypeId, 10)
                }, true),
                'nomValueId');

              res = $filter('filter')(
                res,
                function (nom) {
                  return parentIds.indexOf(nom.parentValueId) !== -1;
                }, true);
            }
          }

          return [200, res];
        }
      );
  });
}(angular, require, _));
