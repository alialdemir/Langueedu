module.exports = {
  mode: "jit",
  content: [
    "./**/*.razor",
    "./wwwroot/assets/scss/theme/*",
    "../Langueedu.Web.Components/**/*.razor",
    "../Langueedu.Web.Components/**/**/*.razor",
    "../Langueedu.Web.Components/**/**/**/*.razor",

    "./**/*.razor",
    "./wwwroot/assets/scss/theme/*.scss",
    "../Langueedu.Web.Components/**/*.razor.scss",
    "../Langueedu.Web.Components/**/**/*.razor.scss",
    "../Langueedu.Web.Components/**/**/**/*.razor.scss",
  ],
  theme: {
    extend: {
      zIndex: {
        102: "102",
      },

      colors: {
        primary: "#ffbf00",
      },
    },
  },
  plugins: [],
};
