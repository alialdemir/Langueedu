import cash from "cash-dom";

class MobileMenu {
    constructor() {
        cash(document).on('click', '.mobile-toggle', () => {
            cash('.nav-menus').toggleClass('open');
        });
    }
}

export default new MobileMenu();