const gulp = require("gulp")
const sass = require('gulp-sass')(require('sass'));
const watchSass = require("gulp-watch-sass")
const concat = require('gulp-concat');
const cssnano = require('gulp-cssnano');

const paths = [
    "../**/*.razor.scss",
    "../Langueedu.Web.Components/**/*.razor.scss",
    "../Langueedu.Web.Components/**/**/*.razor.scss",
    "./wwwroot/assets/scss/app.scss",
];

gulp.task("sass", () => gulp.src(paths)
    .pipe(sass())
    .pipe(cssnano())
    .pipe(concat('app.min.css'))
    .pipe(gulp.dest("./wwwroot/css")));


gulp.task("watch", () => {
    gulp.watch(paths, gulp.series('sass'));
});