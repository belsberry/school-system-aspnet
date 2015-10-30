var gulp = require('gulp');
var config = require("./gulp.config");
var $ = require("gulp-load-plugins")({ lazy: true });
var del = require("del");
var merge = require("merge2");

var http = require("http");

gulp.task("default", $.taskListing);

gulp.task("clean-build", function(){
  var files = [config.buildRoot + "**/*.*"];
  del(files);
});

gulp.task("build", ["clean-build", "inject"], function(){
  var jsFilter = $.filter(["**/*.js"], {restore: true});
  var cssFilter = $.filter(["**/*.css"], {restore: true});
  var assets = $.useref.assets();

  var srcFiles = [config.index, config.login];

  return gulp
    .src(srcFiles)
    .pipe($.plumber())
    .pipe($.print())
    .pipe(assets)
    .pipe(cssFilter)
    .pipe($.csso())
    .pipe(cssFilter.restore)
    .pipe(jsFilter)
    .pipe($.uglify())
    .pipe(jsFilter.restore)
    .pipe($.rev())
    .pipe(assets.restore())
    .pipe($.useref())
    .pipe($.revReplace({replaceInExtensions: [".js", ".css", ".html", ".ejs"]}))
    .pipe(gulp.dest(config.root))
    .pipe($.rev.manifest())
    .pipe(gulp.dest("./.tmp"));
});

gulp.task("inject", ["move-bower", "templateCache", "less", "assets"], function () {
    var options = config.getDefaultWiredepOptions();
    var wiredep = require("wiredep").stream;
    var srcFiles = [config.indexSrc, config.loginSrc];
    return gulp
        .src(srcFiles)
        .pipe($.plumber())
        .pipe($.print())
        .pipe(wiredep(options))
        .pipe($.inject(gulp.src(config.js)))
        .pipe($.inject(gulp.src(config.temp + config.templateCache.outFile), { name: "inject:templates" }))
        .pipe($.inject(gulp.src(config.bootstrapCss), {name: "inject:bootstrap"}))
        .pipe($.inject(gulp.src(config.appCss), { name: "inject:app" }))
        .pipe(gulp.dest(config.root));
});

gulp.task("move-bower", ["clean-temp-bower"], function(){
   return gulp
        .src(config.depRoot + "**/*.*")
        .pipe(gulp.dest(config.bowerTempRoot)); 
});

gulp.task("clean-temp-bower", function(done){
   var bowerTemp = [].concat(config.bowerTempRoot + "**");
   del(bowerTemp, done); 
});

gulp.task("clean-assets", function(){
    var assets = [].concat(config.fontFolder + "**/*.*");
    del(assets);
});

gulp.task("assets", ["clean-assets"], function(){

  return gulp.src(config.fontFiles)
    .pipe($.plumber())
    .pipe($.print())
    .pipe($.rename({ dirname: "" }))
    .pipe(gulp.dest(config.fontFolder));
});

gulp.task("inject-tests", function () {
    var wiredepOptions = config.getDefaultWiredepOptions();
    wiredepOptions.devDependencies = true;
    wiredepOptions.ignorePath = "..";

    var wiredep = require("wiredep").stream;
    return gulp
        .src(config.jasmine.specRunner)
        .pipe($.print())
        .pipe($.inject(gulp.src(config.jasmine.libJs), { name: "inject:jasmine" }))
        .pipe(wiredep(wiredepOptions))
        .pipe($.inject(gulp.src(config.jasmine.libCss), { name: "inject:jasmine" }))
        .pipe($.inject(gulp.src(config.jasmine.specs), { name: "inject:specs" }))
        .pipe($.inject(gulp.src(config.js), { name: "inject:app" }))
        .pipe(gulp.dest(config.jasmine.testingRoot));
});

gulp.task("templateCache", ["clean-templates"], function () {
    return gulp
        .src(config.htmlTemplates)
        .pipe($.plumber())
        .pipe($.print())
        .pipe($.minifyHtml())
        .pipe($.angularTemplatecache(config.templateCache.outFile, config.templateCache.options))
        .pipe(gulp.dest(config.temp));
});

gulp.task("clean-css", function () {
    var cssFiles = [].concat(config.cssOutputRoot + "**/*.css");
    del(cssFiles);
});

gulp.task("clean-templates", function () {
    var templateFiles = [].concat(config.temp + config.templateCache.outFile);
    del(templateFiles);
});

gulp.task("less", ["clean-css"], function () {
    var bootstrap = gulp
        .src(config.bootstrapLess)
        .pipe($.print())
        .pipe($.less({
            paths: [config.bootstrapIncludeRoot, config.lessRoot]
        }));

    var fontAwesome = gulp
        .src(config.fontAwesomeLess)
        .pipe($.print())
        .pipe($.plumber())
        .pipe($.less({
          paths: [config.fontAwesomeIncludeRoot, config.lessRoot]
        }));

    var appStyles = gulp
        .src(config.appLess)
        .pipe($.print())
        .pipe($.less({
            paths: [config.bootstrapIncludeRoot, config.lessRoot]
        }));
    return merge(bootstrap, appStyles, fontAwesome).pipe(gulp.dest(config.cssOutputRoot));
});



gulp.task("watch-dev", ["inject"], function () {
  var browserSync = require("browser-sync");
  var server = $.express.run(["server.js"]);
  browserSync.init(config.browserSyncOptions);
  gulp.watch(config.allSourceFiles, ["inject"]);
  gulp.watch(config.allSourceFiles, browserSync.reload);
  
});

gulp.task("karma", function () {
    var KarmaServer = require("karma").Server;
    new KarmaServer({
        configFile: config.karma.configFile,
        singleRun: true
    }).start();
});



gulp.task("watch-unittests", ["inject-tests"], function () {
    var browserSync = require("browser-sync");
    //startup a static server using express and serve the raw files
    var express = require("express");
    var app = express();
    //Jasmine spec runner
    app.get("/", function (req, res) {
        res.sendFile(__dirname + "/testing/SpecRunner.html");
    });
    app.use("/bower_components", express.static("bower_components"));
    app.use("/node_modules", express.static("node_modules"));
    app.use("/app", express.static("app"));
    app.listen(config.jasmine.serverConfig.port);

    //starts browser sync
    browserSync.init(config.jasmine.browserSync);
    //check changes and reload if necessary
    gulp.watch(config.js, browserSync.reload);
});
