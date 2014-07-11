/*global angular,_*/
(function (angular,_) {
  'use strict';

  function NamedModalProvider() {
    var modals = {};

    this.modal = function modal(name, modalObj) {
      modals[name] = modalObj;

      return this;
    };

    this.$get = ['$modal', function modalProviderFactory($modal) {
      var self = this;

      self.modal = { };

      self.modal.open = function (modalName, params) {
        var modalObj = modals[modalName];

        if (!modalObj) {
          return new Error('Invalid modal ' + modalName);
        }

        var resolve = modalObj.resolve || {};

        _.forOwn(params, function (param, paramName) {
          resolve[paramName] = function () {
            return param;
          };
        });

        return $modal.open({
          templateUrl: modalObj.template,
          controller: modalObj.controller,
          windowClass: 'xlg-modal',
          resolve: resolve,
          backdrop: 'static'
        });
      };

      return self.modal;
    }];
  }

  angular.module('scaffolding').provider('namedModal', NamedModalProvider);
}(angular,_));
