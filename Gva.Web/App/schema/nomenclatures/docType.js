/*global module*/
(function (module) {
  'use strict';

  module.exports = [
    { nomTypeValueId: 1, code: '', name: 'Резолюция', nameAlt: '', alias: 'resolution', parentId: 1 },
    { nomTypeValueId: 2, code: '', name: 'Задача', nameAlt: '', alias: 'task', parentId: 1 },
    { nomTypeValueId: 3, code: '', name: 'Забележка', nameAlt: '', alias: 'note', parentId: 1 },
    { nomTypeValueId: 4, code: '', name: 'Писмо', nameAlt: '', alias: 'letter', parentId: 1 },

    { nomTypeValueId: 5, code: 'М12.1.5', name: 'Издаване на свидетелство за правоспособност на авиационен персонал – пилоти', nameAlt: '', alias: '', parentId: 2 },
    { nomTypeValueId: 6, code: 'М12.1.6', name: 'Издаване на свидетелство за правоспособност на авиационен персонал – кабинен екипаж, полетни диспечери, бордни инженери, щурмани, бордни съпроводители', nameAlt: '', alias: '', parentId: 2 },
    { nomTypeValueId: 7, code: 'М12.1.8', name: 'Признаване на свидетелство за правоспособност на чужди граждани', nameAlt: '', alias: '', parentId: 2 },
    { nomTypeValueId: 8, code: 'М12.1.14', name: 'Издаване на свидетелство за правоспособност на ръководители на полети', nameAlt: '', alias: '', parentId: 2 },
    { nomTypeValueId: 9, code: 'М12.1.15', name: 'Издаване на свидетелство за правоспособност на инженерно-технически състав по обслужване на средствата за управление на въздушното движение (УВД), на ученик -  ръководители на полети, на асистент координатори на полети и на координатори по УВД', nameAlt: '', alias: '', parentId: 2 },
    { nomTypeValueId: 10, code: 'М12.1.7', name: 'Издаване на свидетелство за правоспособност за техническо обслужване на самолети и хеликоптери', nameAlt: '', alias: '', parentId: 2 },

    { nomTypeValueId: 11, code: '', name: 'Приемно предавателен протокол', nameAlt: '', alias: 'protocol', parentId: 4 }
  ];
})(typeof module === 'undefined' ? (this['docType'] = {}) : module);
