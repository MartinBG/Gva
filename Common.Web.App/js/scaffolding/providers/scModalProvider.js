/*global angular,_*/
(function (angular,_) {
  'use strict';

  function ScModalProvider() {
    var modals = {};

    this.modal = function modal(name, modalObj) {
      modals[name] = modalObj;

      return this;
    };

    this.$get = ['$modal', '$injector', function modalProviderFactory($modal, $injector) {
      var self = this;

      self.modal = { };

      self.modal.open = function (modalName, params) {
        var modalObj = modals[modalName];
        params = params || {};

        if (!modalObj) {
          return new Error('Invalid modal ' + modalName);
        }

        var promisesObj = {
          scModalParams: function () {
            return params;
          }
        };

        _.forOwn(modalObj.resolve, function (value, key) {
          promisesObj[key] = function () {
            return $injector.invoke(value, null, { scModalParams: params });
          };
        });

        return $modal.open({
          templateUrl: modalObj.template,
          controller: modalObj.controller,
          windowClass: 'xlg-modal',
          resolve: promisesObj,
          backdrop: 'static'
        });
      };

      return self.modal;
    }];
  }

  angular.module('scaffolding').provider('scModal', ScModalProvider);
}(angular,_));
