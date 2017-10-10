// Include Gulp
var gulp = require('gulp');
var concat = require('gulp-concat');
var uglify = require('gulp-uglify');
var filter = require('gulp-filter');
var sourcemaps = require('gulp-sourcemaps');
var mainBowerFiles = require('main-bower-files');
var cleanCSS = require('gulp-clean-css');
var order = require('gulp-order');
var exclude = require('arr-exclude');
var print = require('gulp-print');

var config = {
    //Include all js files but exclude any min.js files
    js: ['wwwroot/app/**/*.js'],
    css: ['wwwroot/css/**/*.css', '!wwwroot/css/**/*.min.css', '!wwwroot/css/skins/**/*.css', '!wwwroot/css/vendor*.css']
}

gulp.task('watch',
    function () {
        gulp.watch('wwwroot/app/**/*.js', ['build-app:js']);
        gulp.watch('wwwroot/css/*.css', ['build-app:css']);

        gulp.watch('lib/**/*.js', ['build-vendor']);
        gulp.watch('lib/**/*.css', ['build-vendor']);
    });

gulp.task('default', function () {
    // place code for your default task here
});

gulp.task('vendor', ['build-vendor:js', 'build-vendor:css']);

gulp.task('build-app:js', function () {
    console.log('------------------------------------');
    console.log('Building app js');
    console.log('------------------------------------');
    return gulp.src(config.js)
        .pipe(filter(['**/*.js']))
        .pipe(order([
            '**/app.module.js',
            '**/core/core.module.js',
            '**/core/config.js',
            '**/*.module.js'
        ]))
        .pipe(print())
        .pipe(sourcemaps.init())
        //.pipe(uglify())
        .pipe(concat('app.min.js'))
        .pipe(sourcemaps.write())
        .pipe(gulp.dest('wwwroot/js/'));
});

gulp.task('build-app:css', function () {
    return gulp.src(config.css)
        .pipe(filter(['**/*.css']))
        .pipe(print())
        .pipe(sourcemaps.init())
        .pipe(cleanCSS())
        .pipe(concat('site.min.css'))
        .pipe(sourcemaps.write())
        .pipe(gulp.dest('css/'));
});

gulp.task('fonts', function () {
    return gulp.src(mainBowerFiles({
        // Only return the font files
        filter: /.*\.(eot|svg|ttf|woff|woff2)$/i
    }))
        .pipe(print())
        .pipe(gulp.dest('wwwroot/fonts'));
});

gulp.task('build-vendor:js', function () {
    console.log('------------------------------------');
    console.log('Building vendor js file');
    console.log('------------------------------------');
    return gulp.src(mainBowerFiles(['**/*.js']))
        .pipe(filter(['**/*.js']))
        .pipe(print())
        .pipe(sourcemaps.init())
        .pipe(uglify())
        .pipe(concat('vendor.min.js'))
        .pipe(sourcemaps.write())
        .pipe(gulp.dest('wwwroot/js/'));
});

gulp.task('build-vendor:css', function () {
    console.log('------------------------------------');
    console.log('Building vendor css file');
    console.log('------------------------------------');
    return gulp.src(mainBowerFiles())
        .pipe(filter(['**/*.css']))
        .pipe(print())
        .pipe(sourcemaps.init())
        // .pipe(cleanCSS())
        .pipe(concat('vendor.min.css'))
        .pipe(sourcemaps.write())
        .pipe(gulp.dest('wwwroot/css/'));
});