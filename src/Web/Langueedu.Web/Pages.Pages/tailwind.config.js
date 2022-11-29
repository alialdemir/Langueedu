module.exports = {
  mode: "jit",
  content: [
    "./**/*.razor",
    "./wwwroot/assets/scss/theme/*",
    "../Langueedu.Components/**/*.razor",
    "../Langueedu.Components/**/**/*.razor",
    "../Langueedu.Components/**/**/**/*.razor",

    "./**/*.razor",
    "./wwwroot/assets/scss/theme/*.scss",
    "../Langueedu.Components/**/*.razor.scss",
    "../Langueedu.Components/**/**/*.razor.scss",
    "../Langueedu.Components/**/**/**/*.razor.scss",
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
