/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('Papers', ['$resource', function ($resource) {
    return $resource('api/papers/:paperId', {}, {
      isValidPaperData: {
        method: 'GET',
        url: 'api/papers/isValidPaperData'
      },
      getPapers: {
        method: 'GET',
        isArray: true,
        url: 'api/papers'
      },
      createNew: {
        method: 'POST',
        url: 'api/papers/createNew'
      },
      newPaper: {
        method: 'GET',
        url: 'api/papers/new'
      }
    });
  }]);
}(angular));
