using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;
using AdventCalendar.Day07;

namespace AdventCalendar.Tests.Day07
{
    public class Day7Tests
    {
        IList<string> sampleData = new List<string> { "Step C must be finished before step A can begin.", "Step C must be finished before step F can begin.", "Step A must be finished before step B can begin.", "Step A must be finished before step D can begin.", "Step B must be finished before step E can begin.", "Step D must be finished before step E can begin.", "Step F must be finished before step E can begin." };
        IList<string> actualData = new List<string> { "Step A must be finished before step N can begin.", "Step P must be finished before step R can begin.", "Step O must be finished before step T can begin.", "Step J must be finished before step U can begin.", "Step M must be finished before step X can begin.", "Step E must be finished before step X can begin.", "Step N must be finished before step T can begin.", "Step W must be finished before step G can begin.", "Step Z must be finished before step D can begin.", "Step F must be finished before step Q can begin.", "Step U must be finished before step L can begin.", "Step I must be finished before step X can begin.", "Step X must be finished before step Y can begin.", "Step D must be finished before step Y can begin.", "Step S must be finished before step K can begin.", "Step C must be finished before step G can begin.", "Step K must be finished before step V can begin.", "Step B must be finished before step R can begin.", "Step Q must be finished before step L can begin.", "Step T must be finished before step H can begin.", "Step H must be finished before step G can begin.", "Step V must be finished before step L can begin.", "Step L must be finished before step R can begin.", "Step G must be finished before step Y can begin.", "Step R must be finished before step Y can begin.", "Step G must be finished before step R can begin.", "Step X must be finished before step V can begin.", "Step V must be finished before step Y can begin.", "Step Z must be finished before step U can begin.", "Step U must be finished before step R can begin.", "Step J must be finished before step Y can begin.", "Step Z must be finished before step C can begin.", "Step O must be finished before step L can begin.", "Step C must be finished before step H can begin.", "Step V must be finished before step G can begin.", "Step F must be finished before step K can begin.", "Step Q must be finished before step G can begin.", "Step S must be finished before step Q can begin.", "Step M must be finished before step G can begin.", "Step T must be finished before step L can begin.", "Step C must be finished before step Q can begin.", "Step T must be finished before step V can begin.", "Step W must be finished before step Z can begin.", "Step C must be finished before step K can begin.", "Step I must be finished before step C can begin.", "Step X must be finished before step Q can begin.", "Step F must be finished before step X can begin.", "Step J must be finished before step S can begin.", "Step I must be finished before step K can begin.", "Step U must be finished before step Q can begin.", "Step I must be finished before step Q can begin.", "Step N must be finished before step H can begin.", "Step A must be finished before step T can begin.", "Step T must be finished before step G can begin.", "Step D must be finished before step T can begin.", "Step A must be finished before step X can begin.", "Step D must be finished before step G can begin.", "Step C must be finished before step T can begin.", "Step W must be finished before step Q can begin.", "Step W must be finished before step K can begin.", "Step V must be finished before step R can begin.", "Step H must be finished before step R can begin.", "Step F must be finished before step H can begin.", "Step F must be finished before step V can begin.", "Step U must be finished before step T can begin.", "Step K must be finished before step H can begin.", "Step B must be finished before step T can begin.", "Step H must be finished before step Y can begin.", "Step J must be finished before step Z can begin.", "Step B must be finished before step Y can begin.", "Step I must be finished before step V can begin.", "Step W must be finished before step V can begin.", "Step Q must be finished before step R can begin.", "Step I must be finished before step S can begin.", "Step E must be finished before step H can begin.", "Step J must be finished before step B can begin.", "Step S must be finished before step G can begin.", "Step E must be finished before step S can begin.", "Step N must be finished before step I can begin.", "Step Z must be finished before step F can begin.", "Step E must be finished before step I can begin.", "Step S must be finished before step B can begin.", "Step D must be finished before step L can begin.", "Step Q must be finished before step T can begin.", "Step Q must be finished before step H can begin.", "Step K must be finished before step Y can begin.", "Step M must be finished before step U can begin.", "Step U must be finished before step K can begin.", "Step W must be finished before step I can begin.", "Step J must be finished before step W can begin.", "Step K must be finished before step T can begin.", "Step P must be finished before step Y can begin.", "Step L must be finished before step G can begin.", "Step K must be finished before step B can begin.", "Step I must be finished before step Y can begin.", "Step U must be finished before step B can begin.", "Step P must be finished before step O can begin.", "Step O must be finished before step W can begin.", "Step O must be finished before step J can begin.", "Step A must be finished before step J can begin.", "Step F must be finished before step G can begin." };


        [Fact]
        public void ActualDataPart1()
        {
            var steps = StepTreeParser.Parse(actualData);

            Assert.Equal("AEMNPOJWISZCDFUKBXQTHVLGRY", steps.GetOrder());
        }

        [Fact]
        public void ActualDataPart2()
        {
            var steps = StepTreeParser.Parse(actualData);

            var buildOrder = steps.Construct(5, 60);

            Assert.Equal("AEMPNOJWIZSCDFUXKBQTHVLGRY", buildOrder.Order);
            Assert.Equal(1081, buildOrder.Elapsed);
        }


        [Fact]
        public void TestSampleData_Part2()
        {
            var steps = StepTreeParser.Parse(sampleData);

            var buildOrder = steps.Construct(2, 0);

            Assert.Equal("CABFDE", buildOrder.Order);
            Assert.Equal(15, buildOrder.Elapsed);
        }

        [Fact]
        public void TestSampleData()
        {
            var steps = StepTreeParser.Parse(sampleData);

            Assert.Equal("CABDFE", steps.GetOrder());
        }
        
        [Fact]
        public void TestAB()
        {
            var steps = StepTreeParser.Parse(new List<string> {
                "Step A must be finished before step B can begin."
            });

            Assert.Equal("AB", steps.GetOrder());

            steps.Reset();
        }

        [Fact]
        public void TestACB()
        {
            var steps = StepTreeParser.Parse(new List<string> {
                "Step A must be finished before step C can begin.",
                "Step C must be finished before step B can begin."
            });

            Assert.Equal("ACB", steps.GetOrder());

            steps.Reset();
        }

        [Fact]
        public void TestACDB()
        {
            var steps = StepTreeParser.Parse(new List<string> {
                "Step A must be finished before step C can begin.",
                "Step A must be finished before step D can begin.",
                "Step C must be finished before step B can begin.",
                "Step D must be finished before step B can begin."
            });

            Assert.Equal("ACDB", steps.GetOrder());

            steps.Reset();

            var build = steps.Construct(2, 0);
            Assert.Equal(7, build.Elapsed);
            Assert.Equal("ACDB", build.Order);
        }
    }
}
