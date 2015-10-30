module.exports = (function () {
    var root = "./wwwroot/";
    var buildRoot = "./build/";
    var sourceRoot = "./app/";
    var depRoot = "./bower_components/";
    var lessRoot = "./less/";
    var temp = root + ".tmp/";
    var bowerTempRoot = temp + "bower_components/";
    var cssOutputRoot = temp + "css/";
    var npmRoot = "./node_modules/";
    var assetFolder = root + "assets/";


    var bowerConfig = {
        "json": require("./bower.json"),
        "directory": __dirname + "/wwwroot/.tmp/bower_components/",
        ignorePath: ".."
    };

    return {

        /**
         *  Directories/Files
         */
        allSourceFiles:[
            lessRoot + "**/*.less",
            sourceRoot + "**/*.html",
            sourceRoot + "**/*.js",
            sourceRoot + "**/*.ejs",
            "!" + sourceRoot + "**/*.specs.js"
        ],
        appLess: [
            lessRoot + "**/*.less",
            "!" + lessRoot + "bootstrap-*.less",
            "!" + lessRoot + "font-awesome-*.less"
        ],
        appCss: [
            cssOutputRoot + "**/*.css",
            "!" + cssOutputRoot + "bootstrap*.css"
        ],
        buildRoot: buildRoot,
        bootstrapLess: lessRoot + "bootstrap-custom.less",
        bootstrapIncludeRoot: depRoot + "bootstrap-less/less/",
        bootstrapCss: cssOutputRoot + "bootstrap-*.css",
        bowerTempRoot: bowerTempRoot,
        depRoot: depRoot,
        fontAwesomeLess: lessRoot + "font-awesome-custom.less",
        fontAwesomeIncludeRoot: depRoot + "font-awesome/less/",

        angularFiles: depRoot + "**/angular*.js",
        lessRoot: lessRoot,
        serverFiles: [
          root + "server.js",
          root + "server/**/*.js"
        ],
        cssOutputRoot: cssOutputRoot,
        index: root + "index.html",
        indexSrc: sourceRoot + "index.html",
        login: root + "login.ejs",
        loginSrc: sourceRoot + "login.ejs",
        js: [
                depRoot + "socket.io-client/socket.io.js",
                sourceRoot + "**/*.js",
                "!" + sourceRoot + "**/*.specs.js"
            ],
        htmlTemplates: sourceRoot + "**/*.template.html",
        temp: temp,
        templateCache: {
            outFile: "templates.js",
            options: {
                root: "app",
                standalone: false,
                module: "app"
            }
        },
        root: root,
        fontFiles: [
          depRoot + "**/*.eot",
          depRoot + "**/*.svg",
          depRoot + "**/*.ttf",
          depRoot + "**/*.woff",
          depRoot + "**/*.woff2",

        ],
        fontFolder: assetFolder + "fonts/",
        assetFolder: assetFolder,

        /**
         * bower config
         */

        bowerConfig: bowerConfig,

        /**
         * Karma config
         */

        karma:{
            configFile: __dirname + "/karma.conf.js",
            files: [
              depRoot + "angular/angular.js",
              depRoot + "angular-mocks/angular-mocks.js",
              sourceRoot + '**/*.js',
              sourceRoot + '**/*.specs.js'
            ]
        },

        /**
         *  Development Browsersync Options
         */
         browserSyncOptions:{
           index: "",
           proxy: "localhost:8080",
           notify: true,
           reloadDelay: 2000
         },

        /**
         * Jasmine Settings
         */
        jasmine: {
            specs: sourceRoot + "**/*.specs.js",
            libJs: [

                npmRoot + "jasmine-core/lib/jasmine-core/jasmine.js",
                npmRoot + "jasmine-core/lib/jasmine-core/jasmine-html.js",
                npmRoot + "jasmine-core/lib/jasmine-core/boot.js"
            ],
            libCss: npmRoot + "jasmine-core/lib/jasmine-core/jasmine.css",
            specRunner: "./testing/SpecRunner.html",
            testingRoot: "./testing/",
            browserSync: {
                index: "testing/SpecRunner.html",
                proxy: "localhost:3005",
                notify: true
            },
            serverConfig: { ecstatic: { root: __dirname + "/testing" }, port: "3005" }
        },


        /**
         * wiredep
         */

        getDefaultWiredepOptions: function () {
            return {
                "bowerJson": bowerConfig.json,
                "directory": bowerConfig.directory,
                "ignorePath": bowerConfig.ignorePath
            };
        }
    };

})();
