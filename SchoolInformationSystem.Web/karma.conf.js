var gulpConfig = require("./gulp.config");

module.exports = function(config) {
  var bowerFiles = [
    //bower:js
    "wwwroot/.tmp/bower_components/angular/angular.js",
    "wwwroot/.tmp/bower_components/angular-route/angular-route.js",
    "wwwroot/.tmp/bower_components/Chart.js/Chart.js",
    "wwwroot/.tmp/bower_components/angular-chart.js/dist/angular-chart.js",
    "wwwroot/.tmp/bower_components/underscore/underscore.js",
    "wwwroot/.tmp/bower_components/angular-toastr/dist/angular-toastr.tpls.js",
    "wwwroot/.tmp/bower_components/angular-bootstrap/ui-bootstrap-tpls.js",
    "wwwroot/.tmp/bower_components/angular-mocks/angular-mocks.js",
    //endbower
  ]
  var coverageFileList = [
      gulpConfig.tempAppFolder.slice(2) + "**/*.js",
      gulpConfig.root.slice(2) + gulpConfig.templateCache.outFile,
      gulpConfig.jasmine.specs.slice(2)
  ];
  var fullFileList = [].concat(bowerFiles, coverageFileList);
  function getCoverageList(){
    var coverageObj = {};
    for(var i = 0; i < coverageFileList.length; i++){
      coverageObj[coverageFileList[i]] = "coverage";
    }
    return coverageObj;
  }
  config.set({

    // base path that will be used to resolve all patterns (eg. files, exclude)
    basePath: '',


    // frameworks to use
    // available frameworks: https://npmjs.org/browse/keyword/karma-adapter
    frameworks: ['jasmine'],


    // list of files / patterns to load in the browser
    files: fullFileList,


    // list of files to exclude
    exclude: [
    ],


    // preprocess matching files before serving them to the browser
    // available preprocessors: https://npmjs.org/browse/keyword/karma-preprocessor
    preprocessors: getCoverageList(),


    // test results reporter to use
    // possible values: 'dots', 'progress'
    // available reporters: https://npmjs.org/browse/keyword/karma-reporter
    reporters: ['progress', 'coverage'],

    // web server port
    port: 9876,


    // enable / disable colors in the output (reporters and logs)
    colors: true,


    // level of logging
    // possible values: config.LOG_DISABLE || config.LOG_ERROR || config.LOG_WARN || config.LOG_INFO || config.LOG_DEBUG
    logLevel: config.LOG_INFO,


    // enable / disable watching file and executing tests whenever any file changes
    autoWatch: true,


    // start these browsers
    // available browser launchers: https://npmjs.org/browse/keyword/karma-launcher
    browsers: ['PhantomJS'],


    // Continuous Integration mode
    // if true, Karma captures browsers, runs the tests and exits
    singleRun: false,
    
    coverageReporter: {
      type : 'text'
    }    
    
    
  })
}
