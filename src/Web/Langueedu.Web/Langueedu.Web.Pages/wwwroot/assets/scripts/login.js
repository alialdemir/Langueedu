import cash from "cash-dom";

class Login {
    constructor() {
        cash(document).on('click', '.img__btn', () => {
            cash(document).find('.cont').toggleClass('s--signup');
        });
    }
}

export default new Login();