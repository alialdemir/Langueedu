module.exports = {
    mode: 'jit',
    content: [
        "./**/*.razor",
        "./wwwroot/assets/scss/theme/*",
        "../Langueedu.Web.Components/**/*.razor",
        "../Langueedu.Web.Components/**/**/*.razor",
        "../Langueedu.Web.Components/**/**/**/*.razor",
    ],
    theme: {
        extend: {},
    },
    plugins: [],
}