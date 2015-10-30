var commonFactories = angular.module("common", []);

commonFactories.factory("ChartData", function(){
  function ChartData(obj, options){
    options = options || {};
    var valueProperty = options.valueProperty || "value";

    return {
      toString: function(){
        return obj[valueProperty];
      },
      data: obj
    };
  };

  return ChartData;
});
