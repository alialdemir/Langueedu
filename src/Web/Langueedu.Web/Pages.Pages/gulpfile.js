const gulp = require("gulp");

// Load Gulp...of course

const { src, dest, parallel } = require("gulp");

const sass = require("gulp-sass")(require("sass"));
const watchSass = require("gulp-watch-sass");
const concat = require("gulp-concat");
const cssnano = require("gulp-cssnano");

// tailwindcss
const tailwindcss = require("tailwindcss");
const postcss = require("gulp-postcss");

// JS related plugins
const uglify = require("gulp-uglify");
const babelify = require("babelify");
const browserify = require("browserify");
const source = require("vinyl-source-stream");
const buffer = require("vinyl-buffer");

// Utility plugins
const rename = require("gulp-rename");

// Project related constiables

const jsSRC = "./wwwroot/assets/scripts/";
const jsFront = "app.js";
const jsFiles = [jsFront];
const jsURL = "./wwwroot/scripts/";

const paths = [
  "./**/*.razor.scss",
  "./wwwroot/assets/scss/theme/*.scss",
  "../Langueedu.Components/**/*.razor.scss",
  "../Langueedu.Components/**/**/*.razor.scss",
  "../Langueedu.Components/**/**/**/*.razor.scss",
  "./wwwroot/assets/scss/app.scss",
];

gulp.task("sass", () =>
  gulp
    .src(paths)
    .pipe(sass())
    .pipe(
      postcss([tailwindcss("./tailwind.config.js"), require("autoprefixer")])
    )
    .pipe(cssnano())
    .pipe(concat("app.min.css"))
    .pipe(gulp.dest("./wwwroot/css"))
);

gulp.task("script", (done) => {
  jsFiles.map((entry) => {
    return browserify({
      entries: [jsSRC + entry],
    })
      .transform(babelify, { presets: ["@babel/preset-env"] })
      .bundle()
      .pipe(source(entry))
      .pipe(
        rename({
          extname: ".min.js",
        })
      )
      .pipe(buffer())
      .pipe(uglify())
      .pipe(dest(jsURL));
  });
  done();
});

gulp.task("watch", () => {
  gulp.watch(paths, gulp.series("sass"));
  gulp.watch(jsSRC + "*.js", gulp.series("script"));
});
