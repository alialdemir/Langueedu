const gulp = require("gulp")
const sass = require('gulp-sass')(require('sass'));
const watchSass = require("gulp-watch-sass")
const concat = require('gulp-concat');
var cssnano = require('gulp-cssnano');

gulp.task("sass", () => gulp.src([
    "./**/*.razor.scss",
    "../Langueedu.Web.Components/**/*.razor.scss",
    "../Langueedu.Web.Components/**/**/*.razor.scss"
])
    .pipe(sass())
    .pipe(cssnano())
    .pipe(concat('app.min.css'))
    .pipe(gulp.dest("./wwwroot/css")));

gulp.task("watch", () => {
    gulp.watch([
        "./**/*.razor.scss",
        "../Langueedu.Web.Components/**/*.razor.scss",
        "../Langueedu.Web.Components/**/**/*.razor.scss"
    ], gulp.series('sass'));
});