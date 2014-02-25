/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', '/api/persons/:id/ratingViews',
        function ($params, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var personRatings = _(person.personRatings)
            .forEach(function (rating) {
              rating.partIndex = rating.partIndex;
              rating.rating = rating.part;
              rating.ratingEdition =
                rating.personRatingEditions[rating.personRatingEditions.length - 1];
              rating.firstEditionValidFrom =
                rating.personRatingEditions[0].part.documentDateValidFrom;
              rating.rating.ratingTypeOrRatingLevel = rating.rating.personRatingLevel ?
                rating.rating.personRatingLevel : rating.rating.ratingType;
              var subClasses = '', i;
              if (rating.ratingEdition.part.ratingSubClasses &&
                rating.ratingEdition.part.ratingSubClasses.length > 0) {
                for (i = 0; i < rating.ratingEdition.part.ratingSubClasses.length; i++) {
                  subClasses += rating.ratingEdition.part.ratingSubClasses[i].name + ', ';
                }
                subClasses = ' ( ' + subClasses.substr(0, subClasses.length - 2) + ' )';
              }

              rating.rating.authorizationAndLimitations = rating.rating.authorization ?
                rating.rating.authorization.name : '';

              var limitations = '';
              if (rating.ratingEdition.part.limitations) {
                for (i = 0; i < rating.ratingEdition.part.limitations.length; i++) {
                  limitations += rating.ratingEdition.part.limitations[i].name + ', ';
                }
                limitations = limitations.substring(0, limitations.length - 2);
                rating.rating.authorizationAndLimitations += ' ' + limitations;
              }

              rating.rating.classOrSubclass = rating.rating.aircraftTypeGroup ?
              rating.rating.aircraftTypeGroup.name : rating.rating.ratingClass.name + subClasses;
            }).value();

          return [200, personRatings];
        });
  });
}(angular, _));