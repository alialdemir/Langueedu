((cash) => {
    "use strict";

    let loadingElement = cash(document).find('.loader-wrapper')[0]//document.getElementsByClassName('loader-wrapper')[0];
    loadingElement.style.height = 0 + "px";

    setTimeout(() => {
        loadingElement.remove();
    }, 1300);
})(cash);
