/*global angular, require*/
(function (angular) {
  'use strict';

  var personDocumentIds = require('./person-document-id.sample');

  angular.module('app').factory('applicationLotFiles', [
    function () {

      return [
        {
          applicationLotFileId: 1,
          docFileKey: '04BCC096-AB2F-4C77-AB82-6FC3E9CE1671',
          lotId: 1, //bypassed TODO add gvaLoTFiles
          partIndex: 11, //bypassed TODO add gvaLoTFiles
          part: personDocumentIds.person1Id, //bypassed TODO add gvaLoTFiles
          setPartName: 'Документ за самоличност', //bypassed TODO add gvaLoTFiles
          setPartAlias: 'DocumentId' //bypassed TODO add gvaLoTFiles

        }
        //,
        //{
        //  applicationLotFileId: 2,
        //  docFileKey: '04BCC096-AB2F-4C77-AB82-6FC3E9CE1672',
        //  lotId: 1, //bypassed TODO add gvaLoTFiles
        //  partIndex: 12, //bypassed TODO add gvaLoTFiles
        //  part: personDocumentIds.person1Id, //bypassed TODO add gvaLoTFiles 
        //  setPartName: 'Документ за самоличност', //bypassed TODO add gvaLoTFiles
        //  setPartAlias: 'DocumentId' //bypassed TODO add gvaLoTFiles
        //}
      ];
    }
  ]);
}(angular));
