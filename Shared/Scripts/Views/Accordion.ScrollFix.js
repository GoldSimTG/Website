﻿(function (goldSimWeb, $, undefined) {

  var _previousAccordion = null;
  var _previousAccordionItem = null;
  var _previousHeight = 0;
  var _previousTop = window.innerHeight;

  $(".accordion").on("down.zf.accordion", function (event) {
    _previousAccordion = $(this);
    _previousAccordionItem = $(this).find(".is-active .accordion-content");
    _previousHeight = _previousAccordionItem.innerHeight();
    _previousTop = _previousAccordionItem.offset().top;
  });

  $("li.accordion-item").on("click", function (event) {
    var accordionItem = $(this);
    var top = $(accordionItem).offset().top;
    var offset = 0;
    if (!accordionItem.hasClass("is-active")) {
      _previousTop = window.innerHeight;
      return;
    }
    if (_previousAccordion && _previousTop < top && _previousAccordion.has(accordionItem).length) {
      offset = _previousHeight;
    }
    $('html,body').animate({ scrollTop: top - offset }, 'fast');
    setTimeout(function () {
      $('html,body').animate({ scrollTop: top - offset }, 'fast');
      },
      200
    );

  });
} (window.goldSimWeb = window.goldSimWeb || {}, jQuery));