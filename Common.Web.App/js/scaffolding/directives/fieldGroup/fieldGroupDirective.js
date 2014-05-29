// Usage: <sc-field-group text="" validations="" field-name=""> #content </sc-field-group>

/*global angular,_*/
(function (angular,_) {
  'use strict';

  function FieldGroupDirective($parse, $compile, l10n) {

    function FieldGroupCompile(tElement, tAttrs) {
      var validationElem = tElement.find('sc-validation-error');

      if (tAttrs.validations) {
        validationElem.attr('validations', tAttrs.validations);
        validationElem.attr('field-name', tAttrs.fieldName);
        tAttrs.$set('scHasError', tAttrs.fieldName);
      }
      else {
        validationElem.remove();
        tElement.removeAttr('sc-has-error')
      }

      var label = tElement.find('label');
      label.text(l10n.get(tAttrs.text));

      return function FieldGroupLink($scope, element, attrs, scSearch, transcludeFn) {
        var hasPreffix = false,
            hasSuffix = false;

        transcludeFn($scope, function (clone) {
          var modelFound = false;
          var addons = _.map(element.find('span'), function (addon) {
            return angular.element(addon);
          });

          angular.forEach(clone, function (item) {
            var elem = angular.element(item),
                tagName = elem.prop('tagName');

            if (!tagName) {
              return;
            }

            if (elem.attr('ng-model')) {
              addons[0].after(elem);
              modelFound = true;
            }
            else if (modelFound) {
              if (tagName === 'BUTTON') {
                addons[1].prop('class', 'input-group-btn');
              }

              hasSuffix = true;
              addons[1].prepend(elem);
            }
            else {
              if (tagName === 'BUTTON') {
                addons[0].prop('class', 'input-group-btn');
              }

              hasPreffix = true;
              addons[0].prepend(elem);
            }
          });

          if (!hasPreffix) {
            addons[0].remove();
          }

          if (!hasSuffix) {
            addons[1].remove();
          }

          $scope.$on('$destroy', function () {
            clone.remove();
          });
        });
      }
    }

    return {
      restrict: 'E',
      transclude: true,
      terminal: true,
      replace: true,
      templateUrl: 'js/scaffolding/directives/fieldGroup/fieldGroupDirective.html',
      compile: FieldGroupCompile
    };
  }

  FieldGroupDirective.$inject = ['$parse', '$compile', 'l10n'];

  angular.module('scaffolding').directive('scFieldGroup', FieldGroupDirective);
}(angular,_));