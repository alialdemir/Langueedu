const mix = require('laravel-mix');
const tailwindcss = require('tailwindcss');
const TerserPlugin = require('terser-webpack-plugin');
const PurgecssPlugin = require('purgecss-webpack-plugin')
const glob = require('glob')
const path = require('path')

const CKERegex = {
    svg: /ckeditor5-[^/\\]+[/\\]theme[/\\]icons[/\\][^/\\]+\.svg$/,
    css: /ckeditor5-[^/\\]+[/\\]theme[/\\].+\.css/,
};

const PATHS = {
    src: path.join(__dirname, 'Assets')
}

Mix.listen('configReady', (webpackConfig) => {
    const rules = webpackConfig.module.rules;
    const targetSVG = /(\.(png|jpe?g|gif|webp)$|^((?!font).)*\.svg$)/;
    const targetFont = /(\.(woff2?|ttf|eot|otf)$|font.*\.svg$)/;
    const targetCSS = /\.p?css$/;

    for (let rule of rules) {
        if (rule.test.toString() === targetSVG.toString()) {
            rule.exclude = CKERegex.svg;
        } else if (rule.test.toString() === targetFont.toString()) {
            rule.exclude = CKERegex.svg;
        } else if (rule.test.toString() === targetCSS.toString()) {
            rule.exclude = CKERegex.css;
        }
    }
});

mix.js('wwwroot/assets/scripts/app.js', 'wwwroot/scripts/app.min.js')
    .sass('wwwroot/assets/scss/app.scss', 'wwwroot/css/app.min.css', {
        sassOptions: {
            quietDeps: true,
        }
    })
    .options({
        processCssUrls: false,
        postCss: [tailwindcss('./tailwind.config.js')],
    })
    .autoload({
        'cash-dom': ['cash'],
        'jquery': ['jquery'],
    })
    .copyDirectory('wwwroot/assets/scripts/bootstrap/bootstrap.min.js', 'wwwroot/scripts/bootstrap/bootstrap.min.js')
    .copyDirectory('wwwroot/assets/scripts/jquery/jquery.min.js', 'wwwroot/scripts/jquery/jquery.min.js')
    .copyDirectory('wwwroot/assets/scss/bootstrap/bootstrap.css', 'wwwroot/css/bootstrap.min.css')
    .copyDirectory('wwwroot/assets/fonts/', 'wwwroot/fonts/')
    .copyDirectory('wwwroot/assets/images/', 'wwwroot/images/')
    .webpackConfig({
        module: {
            rules: [
                {
                    test: CKERegex.svg,
                    use: ['raw-loader'],
                },
                {
                    test: CKERegex.css,
                    use: [
                        {
                            loader: 'style-loader',
                            options: {
                                injectType: 'singletonStyleTag',
                                attributes: {
                                    'data-cke': true,
                                },
                            },
                        },
                        {
                            loader: 'sass-loader',
                            options: {
                                indentedSyntax: true
                            }
                        },

                        'css-loader',
                        {
                            loader: 'postcss-loader'
                        },
                    ],
                },
            ],
        },
        optimization: {
            removeAvailableModules: false,
            minimizer: [
                new TerserPlugin({
                    terserOptions: {
                        compress: {
                            ecma: 5,
                            warnings: false,
                            comparisons: false,
                            inline: 2
                        },
                        mangle: {
                            safari10: true
                        },
                        output: {
                            ecma: 5,
                            comments: false,
                            ascii_only: true
                        }
                    },
                }),
            ],
        },
        // // // // plugins: [
        // // // //     new PurgecssPlugin({
        // // // //         paths: glob.sync(`${PATHS.src}/**/*`, { nodir: true }),
        // // // //     }),
        // // // // ]
    })
    .browserSync({
        proxy: 'icewall-html.test',
        files: ['wwwroot/**/*.*'],
    });
