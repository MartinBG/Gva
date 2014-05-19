/*global angular, _*/
(function (angular, _) {
  'use strict';

  var adapList = [
    ['map', 'collect'],
    ['reduce', 'inject', 'foldl'],
    ['reduceRight', 'foldr'],
    ['find', 'detect'],
    [/*'filter', */'select'],
    'where',
    'findWhere',
    'reject',
    'invoke',
    'pluck',
    'max',
    'min',
    'sortBy',
    'groupBy',
    'countBy',
    'shuffle',
    'toArray',
    'size',
    ['first', 'head', 'take'],
    'initial',
    'last',
    ['rest', 'tail', 'drop'],
    'compact',
    'flatten',
    'without',
    'union',
    'intersection',
    'difference',
    ['uniq', 'unique'],
    'zip',
    'object',
    'indexOf',
    'lastIndexOf',
    'sortedIndex',
    'keys',
    'values',
    'pairs',
    'invert',
    ['functions', 'methods'],
    'pick',
    'omit',
    'tap',
    'identity',
    'uniqueId',
    'escape',
    'result',
    'template',
    'contains'
  ];

  _.each(adapList, function (filterNames) {
    if (!(_.isArray(filterNames))) {
      filterNames = [filterNames];
    }

    var
      filter = _.bind(_[filterNames[0]], _),
      filterFactory = function () { return filter; };

    _.each(filterNames, function (filterName) {
      angular.module('common').filter(filterName, filterFactory);
    });
  });

}(angular, _));
