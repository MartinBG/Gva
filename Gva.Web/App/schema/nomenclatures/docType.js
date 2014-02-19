/*global module*/
(function (module) {
  'use strict';

  module.exports = [
    { nomValueId: 1, code: '', name: 'Резолюция', nameAlt: '', alias: 'resolution', parentValueId: 1 },
    { nomValueId: 2, code: '', name: 'Задача', nameAlt: '', alias: 'task', parentValueId: 1 },
    { nomValueId: 3, code: '', name: 'Забележка', nameAlt: '', alias: 'note', parentValueId: 1 },
    { nomValueId: 4, code: '', name: 'Писмо', nameAlt: '', alias: 'letter', parentValueId: 1 },

    { nomValueId: 5, code: 'М12.1.5', name: 'Издаване на свидетелство за правоспособност на авиационен персонал – пилоти', nameAlt: '', alias: '', parentValueId: 2 },
    { nomValueId: 6, code: 'М12.1.6', name: 'Издаване на свидетелство за правоспособност на авиационен персонал – кабинен екипаж, полетни диспечери, бордни инженери, щурмани, бордни съпроводители', nameAlt: '', alias: '', parentValueId: 2 },
    { nomValueId: 7, code: 'М12.1.8', name: 'Признаване на свидетелство за правоспособност на чужди граждани', nameAlt: '', alias: '', parentValueId: 2 },
    { nomValueId: 8, code: 'М12.1.14', name: 'Издаване на свидетелство за правоспособност на ръководители на полети', nameAlt: '', alias: '', parentValueId: 2 },
    { nomValueId: 9, code: 'М12.1.15', name: 'Издаване на свидетелство за правоспособност на инженерно-технически състав по обслужване на средствата за управление на въздушното движение (УВД), на ученик -  ръководители на полети, на асистент координатори на полети и на координатори по УВД', nameAlt: '', alias: '', parentValueId: 2 },
    { nomValueId: 10, code: 'М12.1.7', name: 'Издаване на свидетелство за правоспособност за техническо обслужване на самолети и хеликоптери', nameAlt: '', alias: '', parentValueId: 2 },

    { nomValueId: 11, code: '', name: 'Приемно предавателен протокол', nameAlt: '', alias: 'protocol', parentValueId: 4 }
  ];
})(typeof module === 'undefined' ? (this['docType'] = {}) : module);
