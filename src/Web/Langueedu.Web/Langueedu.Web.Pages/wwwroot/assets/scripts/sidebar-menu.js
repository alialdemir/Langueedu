
import cash from "cash-dom";

class SidebarMenu {
    constructor() {
        cash(document).on('click', '.iconMenu-bar li', () => {
            cash('.iconMenu-bar li').removeClass('open');

            if (cash('.iconsidebar-menu').hasClass('iconbar-mainmenu-close')) {
                cash('.iconsidebar-menu').removeClass('iconbar-mainmenu-close');
            }

            cash(this).addClass('open');
        });

        // this will get the full URL at the address bar
        var url = window.location.href;
        cash('.iconMenu-bar li').removeClass('open');
        cash('.iconbar-mainmenu li a').each(() => {

            newurl = url.trim();
            var strVal = '';
            var lastChar = newurl.slice(-1);
            if (lastChar == '/') {
                strVal = newurl.slice(0, -1);
                var lastindex = strVal.split('/');
                var module = lastindex[lastindex.length - 1];
                if (module == 'ltr') {
                    url = url + 'index.html';
                }
            }

            // checks if its the same on the address bar
            if (url === (this.href)) {
                // console.log(this.closest('li'));
                cash(this).closest('li').addClass('active');
                cash(this).addClass('active');
                //for making parent of submenu active
                cash(this).closest('li').parent().parent().addClass('open');
            }
        });

        cash(document).on('click', '.mobile-sidebar #sidebar-toggle', () => {
            var that = cash('.iconsidebar-menu');

            if (that.hasClass('iconbar-second-close')) {
                //that.removeClass();
                that.removeClass('iconbar-second-close').addClass('iconsidebar-menu');
            } else if (that.hasClass('iconbar-mainmenu-close')) {
                that.removeClass('iconbar-mainmenu-close').addClass('iconbar-second-close');
            } else {
                that.addClass('iconbar-mainmenu-close');
            }
        });

        const resize = (width) => {
            if (width <= 991) {
                cash('.iconsidebar-menu').addClass('iconbar-mainmenu-close');
                cash('.iconMenu-bar').removeClass('active');
                cash('.iconsidebar-menu').addClass('iconbar-second-close');
                cash('.iconsidebar-menu').removeClass('iconbar-mainmenu-close');
            } else {
                cash('.iconsidebar-menu').removeClass('iconbar-mainmenu-close');
                cash('.iconMenu-bar').addClass('active');
                cash('.iconsidebar-menu').removeClass('iconbar-second-close');
                cash('.iconsidebar-menu').addClass('iconbar-mainmenu-close');
            }
        }

        window.addEventListener('resize', function (event) {
            resize(event.target.innerWidth)
        }, true);

        var sidebarInterval = setInterval(() => {
            if (cash('.iconsidebar-menu').length > 0) {
                clearInterval(sidebarInterval)
                resize(window.innerWidth);
            }
        }, 500)
    }
}

export default new SidebarMenu();