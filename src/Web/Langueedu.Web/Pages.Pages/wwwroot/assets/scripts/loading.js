import cash from "cash-dom";

class Loading {
    constructor() {
        let loadingElement = cash(document).find('.loader-wrapper')[0]
        loadingElement.style.height = 0 + "px";

        setTimeout(() => {
            loadingElement.remove();
        }, 1300);
    }
}

export default new Loading();