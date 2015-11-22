describe("Unit: DashboardCtrl", function(){
  beforeEach(module("dashboardControllers"));
  beforeEach(module("common"))
  var ctrl,
    scope,
    mockDashboardService,
    $rootScope;

  beforeEach(inject(function($injector){
    scope = {
      $on: function(id, fn){
        fn();
      }
    };
    var ChartData = $injector.get("ChartData");
    $rootScope = $injector.get("$rootScope");
    var $q = $injector.get("$q");

    mockDashboardService = {
      getDashboardData: function(){
        var deferred = $q.defer();
        deferred.resolve({ data: {
          assignmentGrades: [
            { grade: "A", recordCount: 10 },
            { grade: "B", recordCount: 15 }
          ],
          referralCounts: [
            { gradeLevel: "9", numberOfReferrals: 5 },
            { gradeLevel: "10", numberOfReferrals: 10 }
          ],
          attendance: [
            { day: "9/1/2015", count: 400 },
            { day: "9/2/2015", count: 450 }
          ]
        }});
        return deferred.promise;
      }
    };

    ctrl = $injector.get("$controller")("DashboardCtrl", {
      $scope: scope,
      DashboardSvc: mockDashboardService,
      ChartData: ChartData
    });
  }));


  it("should have a message property", function(){
    expect(scope.message).not.toBeNull();
    expect(scope.message).not.toBeUndefined();
    expect(scope.message).toEqual("Hello Dashboard");
  });

  it("should parse the service data into different properties", function(){
    expect(scope.dailyAssignmentGradeLabels.length).toBe(0);
    expect(scope.dailyAssignmentGradeData.length).toBe(0);
    expect(scope.referralCountLabels.length).toBe(0);
    expect(scope.referralCountData.length).toBe(0);
    expect(scope.attendanceLabels.length).toBe(0);
    expect(scope.attendanceData.length).toBe(0);

    $rootScope.$apply();
    expect(scope.dailyAssignmentGradeLabels.length).toBe(2);
    expect(scope.dailyAssignmentGradeLabels[0]).toBe("A");
    expect(scope.dailyAssignmentGradeLabels[1]).toBe("B");
    expect(scope.dailyAssignmentGradeData.length).toBe(2);
    expect(scope.dailyAssignmentGradeData[0].toString()).toBe(10);
    expect(scope.dailyAssignmentGradeData[1].toString()).toBe(15);
    expect(scope.referralCountLabels.length).toBe(2);
    expect(scope.referralCountData.length).toBe(1);
    expect(scope.referralCountData[0].length).toBe(2);
    expect(scope.attendanceLabels.length).toBe(2);
    expect(scope.attendanceData.length).toBe(1);
    expect(scope.attendanceData[0].length).toBe(2);
  });

});
