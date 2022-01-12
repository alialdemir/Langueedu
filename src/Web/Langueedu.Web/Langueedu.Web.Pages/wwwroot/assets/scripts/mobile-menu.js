(function (cash) {
    'use strict';

    cash(document).on('click', '.mobile-toggle', () => {
        cash('.nav-menus').toggleClass('open');
    });
})(cash);
