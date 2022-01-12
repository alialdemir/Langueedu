((cash) => {
    'use strict';

    cash(document).on('click', '.img__btn', () => {
        cash(document).find('.cont').toggleClass('s--signup');
    });
})(cash);
