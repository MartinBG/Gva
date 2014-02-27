/*global module, by*/
(function (module, by) {
  'use strict';

  function ViewApplicationCasePO(context) {
    this.breadcrumb = context.findElement(by.xpath('//ul[@class="breadcrumb"]/li[last()]'));
  }

  ViewApplicationCasePO.prototype.newPart = function (context) {
    var newPartLink = context
      .findElement(by.css('tbody tr:nth-child(3) td:nth-child(4) div:nth-child(1) a'));

    newPartLink.click();
  };

  module.exports = ViewApplicationCasePO;
}(module, by));
